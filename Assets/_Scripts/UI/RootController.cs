using System;
using System.Collections;
using System.IO;
using JetBrains.Annotations;
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

            while (true)
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

        public void StartTakingPhotos(Action startCallback = null, Action endCallback = null)
        {
            startCallback?.Invoke();
            StartCoroutine(StartTakingPhotos_CO(endCallback));
        }

        private IEnumerator StartTakingPhotos_CO(Action callback)
        {
            Texture2D photo = new Texture2D(_webCamTexture.width, _webCamTexture.height);

            for (int i = 0; i < 5; i++)
            {
                var array = _webCamTexture.GetPixels();
                photo.SetPixels(array);
                photo.Apply();

                byte[] bytes = photo.EncodeToPNG();
                File.WriteAllBytes(Application.persistentDataPath + "/" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + ".png", bytes);

                yield return new WaitForSeconds(0.5f);
            }

            callback?.Invoke();
        }
    }
}