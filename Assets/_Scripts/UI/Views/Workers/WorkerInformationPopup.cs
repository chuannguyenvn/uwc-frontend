using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerInformationPopup : DataBasedFullscreenPopup<UserProfile>
    {
        private VisualElement _basicInformationContainer;
        private VisualElement _avatar;
        private TextElement _nameText;
        private TextElement _userRoleText;

        private VisualElement _additionalInformationContainer;
        private PopupInformationEntry _genderEntry;
        private PopupInformationEntry _dateOfBirthEntry;
        private PopupInformationEntry _addressEntry;
        private PopupInformationEntry _createdTimestampEntry;

        public WorkerInformationPopup()
        {
            ConfigureUss(nameof(WorkerInformationPopup));

            CreateBasicInformation();
            CreateAdditionalInformation();
        }

        private void CreateBasicInformation()
        {
            _basicInformationContainer = new VisualElement { name = "BasicInformationContainer" };
            AddContent(_basicInformationContainer);

            _avatar = new VisualElement { name = "Avatar" };
            _basicInformationContainer.Add(_avatar);

            _nameText = new TextElement { name = "NameText" };
            _basicInformationContainer.Add(_nameText);

            _userRoleText = new TextElement { name = "UserRoleText" };
            _basicInformationContainer.Add(_userRoleText);
        }

        private void CreateAdditionalInformation()
        {
            _additionalInformationContainer = new VisualElement { name = "AdditionalInformationContainer" };
            AddContent(_additionalInformationContainer);

            _genderEntry = new PopupInformationEntry("Gender");
            _additionalInformationContainer.Add(_genderEntry);

            _dateOfBirthEntry = new PopupInformationEntry("Date of birth");
            _additionalInformationContainer.Add(_dateOfBirthEntry);

            _addressEntry = new PopupInformationEntry("Address");
            _additionalInformationContainer.Add(_addressEntry);
            
            _createdTimestampEntry = new PopupInformationEntry("Created timestamp");
            _additionalInformationContainer.Add(_createdTimestampEntry);
        }

        public override void SetContent(UserProfile data)
        {
            _nameText.text = data.FirstName + " " + data.LastName;
            _userRoleText.text = data.UserRole.ToString();

            _genderEntry.SetValue(data.Gender.ToString());
            _dateOfBirthEntry.SetValue(data.DateOfBirth.ToString("dd/MM/yyyy"));
            _addressEntry.SetValue(data.Address);
            _createdTimestampEntry.SetValue(data.CreatedTimestamp.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}