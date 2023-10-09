using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Commons.Communications.Mcps;
using Commons.Models;
using Commons.Types;
using Newtonsoft.Json;
using UI.Views.Mcps;
using UnityEngine;

namespace Requests
{
    public class DataStore : Singleton<DataStore>
    {
        public static event Action PendingRequests;

        private Dictionary<DataType, bool> _dataTypeFocusStates = new();
        public static readonly Dictionary<DataType, Action<object>> DataTypeUpdateCallbacks = new();

        static DataStore()
        {
            foreach (var value in Enum.GetValues(typeof(DataType)))
            {
                DataTypeUpdateCallbacks.Add((DataType)value, null);
            }
        }

        private void Start()
        {
            PendingRequests?.Invoke();

            foreach (var value in Enum.GetValues(typeof(DataType)))
            {
                _dataTypeFocusStates.Add((DataType)value, false);
            }

            InvokeRepeating(nameof(UpdateData), 1f, 5f);
        }

        public void FocusDataType(DataType dataType)
        {
            _dataTypeFocusStates[dataType] = true;
            UpdateData();
        }

        public void UnfocusDataType(DataType dataType)
        {
            _dataTypeFocusStates[dataType] = false;
        }

        private void UpdateData()
        {
            if (_dataTypeFocusStates.Count == 0) return;

            if (_dataTypeFocusStates[DataType.McpsViewListData])
            {
                StartCoroutine(HttpsClient.SendRequest<GetMcpDataResponse>(
                    Endpoints.McpData.GET,
                    HttpsClient.RequestType.POST, (success, response) =>
                    {
                        if (success)
                        {
                            Debug.Log(JsonConvert.SerializeObject(response));
                            DataTypeUpdateCallbacks[DataType.McpsViewListData]?.Invoke(response);
                        }
                        else
                        {
                            Debug.LogError("Failed to get Mcp data");
                        }
                    },
                    "",
                    new McpDataQueryParameters
                    {
                        PageIndex = 0
                    }));
            }
        }
    }
}