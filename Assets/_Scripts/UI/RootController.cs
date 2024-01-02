using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Authentication;
using Commons.Communications.Authentication;
using Commons.Endpoints;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Requests;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class RootController : Singleton<RootController>
    {
        public static event Action BackButtonPressed;

        public UIDocument RootDocument;
        [CanBeNull] private WebCamTexture _webCamTexture;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed?.Invoke();
            }
        }

        public void ShowCamera(VisualElement cameraView)
        {
            StartCoroutine(ShowCamera_CO(cameraView));
        }

        private IEnumerator ShowCamera_CO(VisualElement cameraView)
        {
            string frontCamName = null;
            var webCamDevices = WebCamTexture.devices;
            foreach (var camDevice in webCamDevices)
            {
                if (camDevice.isFrontFacing)
                {
                    frontCamName = camDevice.name;
                    break;
                }
            }

            Permission.RequestUserPermission(Permission.Camera);
            _webCamTexture = new WebCamTexture(frontCamName, 512, 512);
            _webCamTexture.Play();

            yield return new WaitForEndOfFrame();

            cameraView.style.display = DisplayStyle.Flex;

            Texture2D photo = new Texture2D(_webCamTexture.width, _webCamTexture.height);
            cameraView.style.backgroundImage = new StyleBackground(photo);

            while (_webCamTexture.isPlaying)
            {
                var array = _webCamTexture.GetPixels();
                cameraView.style.backgroundImage.value.texture.SetPixels(array);
                cameraView.style.backgroundImage.value.texture.Apply();

                yield return new WaitForEndOfFrame();
            }
        }

        public void HideCamera()
        {
            StopCoroutine(nameof(ShowCamera_CO));
            StopCoroutine(nameof(StartTakingPhotos_CO));
            if (_webCamTexture != null) _webCamTexture.Stop();
        }

        public void StartTakingPhotos(Action startCallback = null, Action endCallback = null, bool forRegistration = true)
        {
            startCallback?.Invoke();
            StopCoroutine(nameof(StartTakingPhotos_CO));
            StartCoroutine(StartTakingPhotos_CO(endCallback, forRegistration));
        }

        private IEnumerator StartTakingPhotos_CO(Action callback, bool forRegistration)
        {
            Debug.Log("Start taking photos");

            Texture2D photo = new Texture2D(_webCamTexture.width, _webCamTexture.height);

            var photos = new List<byte[]>();
            for (int i = 0; i < 5; i++)
            {
                var array = _webCamTexture.GetPixels();
                photo.SetPixels(array);
                photo.Apply();

                byte[] bytes = photo.EncodeToPNG();
                photos.Add(bytes);
                File.WriteAllBytes(Application.persistentDataPath + "/" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + ".png", bytes);

                yield return new WaitForSeconds(0.5f);
            }

            callback?.Invoke();

            // var request = new RegisterFaceRequest
            // {
            //     AccountId = AuthenticationManager.Instance.UserAccountId,
            //     Images = photos,
            // };
            //
            // var base64Images = request.Images.Select(imageBytes => Convert.ToBase64String(imageBytes)).ToList();
            //
            // var requestData = new { images = base64Images, };
            //
            // var jsonRequest = JsonConvert.SerializeObject(requestData);
            //
            // string filePath = Application.persistentDataPath + "\\File.txt";
            //
            // using (StreamWriter writer = new StreamWriter(filePath))
            // {
            //     writer.Write(jsonRequest);
            // }

            if (forRegistration)
            {
                DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<RegisterFaceResponse>(Endpoints.Authentication.RegisterFace,
                    new RegisterFaceRequest
                    {
                        AccountId = AuthenticationManager.Instance.UserAccountId,
                        Images = photos,
                    }, (success, result) =>
                    {
                        if (success)
                        {
                            if (result.Success)
                            {
                                Debug.Log("Face registered successfully!");
                            }
                            else
                            {
                                Debug.Log("Face registration failed!");
                            }
                        }
                        else
                        {
                            Debug.Log("Face registration failed!");
                        }
                    }));
            }
            else
            {
                DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<LoginResponse>(Endpoints.Authentication.LoginWithFace,
                    new LoginWithFaceRequest()
                    {
                        Username = "driver_driver",
                        Images = photos,
                    }, (success, result) =>
                    {
                        if (success)
                        {
                            AuthenticationManager.Instance.SuccessfulLoginHandler(result);
                        }
                        else
                        {
                            Debug.Log("Face registration failed!");
                        }
                    }));
            }
        }
    }
}