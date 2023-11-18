using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerInformationPopup : DataBasedFullscreenPopup<UserProfile>
    {
        private PopupInformationEntry _roleEntry;
        private PopupInformationEntry _genderEntry;
        private PopupInformationEntry _dateOfBirthEntry;
        private PopupInformationEntry _addressEntry;
        private PopupInformationEntry _createdTimestampEntry;

        public WorkerInformationPopup()
        {
            ConfigureUss(nameof(WorkerInformationPopup));

            CreateDetailsInformation();
        }

        private void CreateDetailsInformation()
        {
            _roleEntry = new PopupInformationEntry("Role");
            DetailContainer.Add(_roleEntry);

            _genderEntry = new PopupInformationEntry("Gender");
            DetailContainer.Add(_genderEntry);

            _dateOfBirthEntry = new PopupInformationEntry("Date of birth");
            DetailContainer.Add(_dateOfBirthEntry);

            _addressEntry = new PopupInformationEntry("Address");
            DetailContainer.Add(_addressEntry);

            _createdTimestampEntry = new PopupInformationEntry("Account created");
            DetailContainer.Add(_createdTimestampEntry);
        }

        public override void SetContent(UserProfile data)
        {
            Title.text = data.FirstName + " " + data.LastName;

            _roleEntry.SetValue(data.UserRole.ToString());
            _genderEntry.SetValue(data.Gender.ToString());
            _dateOfBirthEntry.SetValue(data.DateOfBirth.ToString("dd/MM/yyyy"));
            _addressEntry.SetValue(data.Address);
            _createdTimestampEntry.SetValue(data.CreatedTimestamp.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}