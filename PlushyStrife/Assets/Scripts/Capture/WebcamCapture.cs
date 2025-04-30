using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Capture
{
    public class WebCam : MonoBehaviour
    {
        [SerializeField]
        private Image webcamImage;

        [SerializeField]
        private Image capturedImage;

        [SerializeField]
        private Vector2Int res;

        [SerializeField]
        private UnityEvent onCapture;

        public static WebCam Instance { get; }

        public Texture2D LastCapture => lastCapture;

        private Texture2D lastCapture;

        private WebCamTexture webcamTexture;

        private void Start()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(AskForPermissionIfRequired(
                    UserAuthorization.WebCam,
                    () => { InitializeCamera(); }));
                return;
            }

            InitializeCamera();
        }

        private void OnDestroy()
        {
            if (webcamTexture != null)
            {
                webcamTexture.Stop();
                webcamTexture = null;
            }
        }

        private bool CheckPermissionAndRaiseCallbackIfGranted(UserAuthorization authenticationType,
            Action authenticationGrantedAction)
        {
            if (Application.HasUserAuthorization(authenticationType))
            {
                if (authenticationGrantedAction != null)
                {
                    authenticationGrantedAction();
                }

                return true;
            }

            return false;
        }

        private IEnumerator AskForPermissionIfRequired(UserAuthorization authenticationType,
            Action authenticationGrantedAction)
        {
            if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
            {
                yield return Application.RequestUserAuthorization(authenticationType);
                if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType, authenticationGrantedAction))
                {
                    Debug.LogWarning($"Permission {authenticationType} Denied");
                }
            }
        }

        private void InitializeCamera()
        {
            webcamTexture = new WebCamTexture(res.x, res.y, 12);
            webcamImage.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }

        public void Capture()
        {
            Debug.Log("CAPTURE");
            // Save as texture2d
            Color[] pixels = webcamTexture.GetPixels();
            var resizedPixels = new Color[res.x * res.y];

            // resize
            for (var y = 0; y < res.y; y++)
            {
                for (var x = 0; x < res.x; x++)
                {
                    int originalX = Mathf.FloorToInt((float)x / res.x * webcamTexture.width);
                    int originalY = Mathf.FloorToInt((float)y / res.y * webcamTexture.height);
                    resizedPixels[y * res.x + x] = pixels[originalY * webcamTexture.width + originalX];
                }
            }

            lastCapture = new Texture2D(res.x, res.y);
            lastCapture.SetPixels(resizedPixels);

            lastCapture.Apply();

            capturedImage.material.mainTexture = lastCapture;

            onCapture?.Invoke();
        }
    }
}