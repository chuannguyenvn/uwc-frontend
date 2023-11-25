using System;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
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
        private WebCamTexture _webCamTexture;

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

        public void TakePhoto()
        {
            StartCoroutine(TakePhoto_CO());
        }

        private IEnumerator TakePhoto_CO()
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
            _webCamTexture = new WebCamTexture(frontCamName);
            _webCamTexture.Play();

            yield return new WaitForEndOfFrame();

            Texture2D photo = new Texture2D(_webCamTexture.height, _webCamTexture.width);
            var array = _webCamTexture.GetPixels();

            for (int x = 0; x < photo.width; x++)
            {
                for (int y = 0; y < photo.height; y++)
                {
                    photo.SetPixel(x, y, array[x * photo.height + y]);
                }
            }

            photo.Apply();

            //Encode to a PNG
            byte[] bytes = photo.EncodeToPNG();
            //Write out the PNG. Of course you have to substitute your_path for something sensible
            File.WriteAllBytes(Application.persistentDataPath + "/" + DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + ".png", bytes);
        }
    }
}