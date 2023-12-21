using System;
using System.Collections;
using Authentication;
using Commons.Communications.Map;
using Commons.Endpoints;
using Commons.Types;
using Requests;
using Settings;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Maps
{
    public class LocationManager : PersistentSingleton<LocationManager>
    {
        public event Action<Coordinate> LocationUpdated;
        public Coordinate LastKnownCoordinate { get; private set; }

        public float AccuracyInMeters => Input.location.status == LocationServiceStatus.Running
            ? Input.location.lastData.horizontalAccuracy
            : Random.Range(5f, 100f);

        private void Start()
        {
            if (!Configs.IS_DESKTOP)
            {
                AuthenticationManager.Instance.Initialized += (data) =>
                {
                    Input.location.Start(5, 5);
                    StartCoroutine(UpdateLocation_CO());
                };
            }
        }

        private IEnumerator UpdateLocation_CO()
        {
            while (true)
            {
                if (Input.location.status == LocationServiceStatus.Running)
                {
                    LastKnownCoordinate = new Coordinate(Input.location.lastData.latitude, Input.location.lastData.longitude);
                    LocationUpdated?.Invoke(LastKnownCoordinate);
                    SendLocationToServer(LastKnownCoordinate);
                    Debug.Log("Location updated: " + LastKnownCoordinate);
                }
                else
                {
                    Debug.LogError("Location service is not running. Using default location.");
                    LastKnownCoordinate = new Coordinate(10.7670552457392, 106.656326672901);
                    LocationUpdated?.Invoke(LastKnownCoordinate);
                    SendLocationToServer(LastKnownCoordinate);
                }

                yield return new WaitForSeconds(5f);
            }
        }

        private void SendLocationToServer(Coordinate coordinate)
        {
            StartCoroutine(RequestHelper.SendPostRequest(
                Endpoints.Map.UpdateLocation,
                new LocationUpdateRequest
                {
                    AccountId = AuthenticationManager.Instance.UserAccountId,
                    NewLocation = coordinate
                },
                success =>
                {
                    if (success)
                    {
                        Debug.Log("Location sent to server: " + coordinate);
                    }
                    else
                    {
                        Debug.LogError("Failed to send location to server.");
                    }
                }));
        }
    }
}