using System.Linq;
using Authentication;
using Commons.Communications.UserProfiles;
using LocalizationNS;
using Requests;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class StatusView : View
    {
        // Panels
        private PanelList _panelList;
        private PersonalInformationPanel _personalInformationPanel;
        private Panel _addressContainer;
        private TextElement _addressTitle;
        private TextElement _addressText;

        private Panel _dobAndGenderContainer;
        
        private VisualElement _dobContainer;
        private TextElement _dobTitle;
        private TextElement _dobText;
        
        private VisualElement _genderContainer;
        private TextElement _genderTitle;
        private TextElement _genderText;

        public StatusView() : base(nameof(StatusView))
        {
            ConfigureUss(nameof(StatusView));

            AddToClassList("full-view");

            CreatePanels();

            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated += DataUpdatedHandler;
        }

        private void CreatePanels()
        {
            _panelList = new PanelList();
            Add(_panelList);

            _personalInformationPanel = new PersonalInformationPanel();
            _panelList.AddPanel(_personalInformationPanel);

            _addressContainer = new Panel { name = "AddressContainer" };
            _addressContainer.AddToClassList("rounded-32px");
            _panelList.Add(_addressContainer);

            _addressTitle = new TextElement { name = "AddressTitle" };
            _addressTitle.AddToClassList("sub-text");
            _addressTitle.AddToClassList("grey-text");
            _addressTitle.text = "Address";
            _addressContainer.Add(_addressTitle);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("black-text");
            _addressText.text = "Placeholder Address";
            _addressContainer.Add(_addressText);

            _dobAndGenderContainer = new Panel { name = "DobAndGenderContainer" };
            _dobAndGenderContainer.AddToClassList("rounded-32px");
            _panelList.Add(_dobAndGenderContainer);

            _dobContainer = new VisualElement { name = "DobContainer" };
            _dobAndGenderContainer.Add(_dobContainer);
            
            _dobTitle = new TextElement { name = "DobTitle" };
            _dobTitle.AddToClassList("sub-text");
            _dobTitle.AddToClassList("grey-text");
            _dobTitle.text = "Date of Birth";
            _dobContainer.Add(_dobTitle);

            _dobText = new TextElement { name = "DobText" };
            _dobText.AddToClassList("normal-text");
            _dobText.AddToClassList("black-text");
            _dobText.text = "Placeholder Date of Birth";
            _dobContainer.Add(_dobText);

            _genderContainer = new VisualElement { name = "GenderContainer" };
            _dobAndGenderContainer.Add(_genderContainer);
            
            _genderTitle = new TextElement { name = "GenderTitle" };
            _genderTitle.AddToClassList("sub-text");
            _genderTitle.AddToClassList("grey-text");
            _genderTitle.text = "Date of Birth";
            _genderContainer.Add(_genderTitle);

            _genderText = new TextElement { name = "GenderText" };
            _genderText.AddToClassList("normal-text");
            _genderText.AddToClassList("black-text");
            _genderText.text = "Gender";
            _genderContainer.Add(_genderText);
        }

        public override void FocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Focus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Unfocus();
        }

        private void DataUpdatedHandler(GetAllWorkerProfilesResponse response)
        {
            try
            {
                var profile = response.WorkerProfiles.First(profile => profile.Id == AuthenticationManager.Instance.UserAccountId);

                _personalInformationPanel.SetNameText(profile.FirstName + " " + profile.LastName);
                _personalInformationPanel.SetRoleText(profile.UserRole);
                _addressText.text = profile.Address;
                _dobText.text = profile.DateOfBirth.ToString("dd/MM/yyyy");
                _genderText.text = Localization.GetGender(profile.Gender);
            }
            catch
            {
                return;
            }
        }
    }
}