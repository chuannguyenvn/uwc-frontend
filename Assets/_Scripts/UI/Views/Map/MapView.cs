using System;
using Authentication;
using Commons.Communications.Map;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Maps;
using Requests;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class MapView : View
    {
        private NavigationPanel _navigationPanel;
        private NextStopPanel _nextStopPanel;
        private MobileMapModeTogglePanel _mobileMapModeTogglePanel;

        private MapMode _currentMapMode = MapMode.NextStop;

        public MapView() : base(nameof(MapView))
        {
            ConfigureUss(nameof(MapView));

            AddToClassList("full-view");
            pickingMode = PickingMode.Ignore;

            if (!Configs.IS_DESKTOP)
            {
                CreateNavigationPanel();
                CreateNextStopPanel();
                CreateMobileMapModeTogglePanel();
                ToggleMapMode();
            }
        }

        private void CreateNavigationPanel()
        {
            _navigationPanel = new NavigationPanel();
            _navigationPanel.SetInformation(0, "...");
            Add(_navigationPanel);
        }

        private void CreateNextStopPanel()
        {
            _nextStopPanel = new NextStopPanel();
            _nextStopPanel.SetNextStopAddress("495/4/8 Tô Hiến Thành");
            Add(_nextStopPanel);
        }

        private void CreateMobileMapModeTogglePanel()
        {
            _mobileMapModeTogglePanel = new MobileMapModeTogglePanel();
            Add(_mobileMapModeTogglePanel);
        }

        public void ToggleMapMode()
        {
            if (_currentMapMode == MapMode.Navigation)
            {
                _currentMapMode = MapMode.NextStop;
                _nextStopPanel.style.display = DisplayStyle.Flex;
                _navigationPanel.style.display = DisplayStyle.None;
            }
            else
            {
                _currentMapMode = MapMode.Navigation;
                _nextStopPanel.style.display = DisplayStyle.None;
                _navigationPanel.style.display = DisplayStyle.Flex;
            }
        }

        public override void FocusView()
        {
            if (Configs.IS_DESKTOP) return;
            
            _navigationPanel.style.display = DisplayStyle.None;
            _nextStopPanel.style.display = DisplayStyle.None;
            
            DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<GetWorkerPrioritizedTaskResponse>(Endpoints.TaskData.GetWorkerPrioritizedTask,
                new GetWorkerPrioritizedTaskRequest
                {
                    WorkerId = AuthenticationManager.Instance.UserAccountId
                },
                (success, result) =>
                {
                    if (success && result.Task != null)
                    {
                        _nextStopPanel.SetNextStopAddress(result.Task.McpData.Address);
                            
                        DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<GetDirectionResponse>(Endpoints.Map.GetDirection,
                            new GetDirectionRequest
                            {
                                AccountId = AuthenticationManager.Instance.UserAccountId,
                                CurrentLocation = LocationManager.Instance.LastKnownCoordinate,
                                McpIds = new() { result.Task.McpDataId },
                            },
                            (success, result) =>
                            {
                                if (success)
                                {
                                    _navigationPanel.SetInformation((float)result.Direction.InstructionDistance[0], result.Direction.Instructions[0]);
                                    ToggleMapMode();
                                    ToggleMapMode();
                                }
                            }));
                    }
                }));
            // if (!Configs.IS_DESKTOP) MapManager.Instance.MapGameObject.SetActive(true);
        }

        public override void UnfocusView()
        {
            // if (!Configs.IS_DESKTOP) MapManager.Instance.MapGameObject.SetActive(false);
        }
    }
}