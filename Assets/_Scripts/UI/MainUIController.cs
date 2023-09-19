using System;
using UI.Authentication;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainUIController : MonoBehaviour
    {
        [SerializeField] private AuthenticationUIController _authenticationUiController;

        [SerializeField] private UIDocument _uiDocument;

        private void Start()
        {
            gameObject.SetActive(false);
        }
    }
}