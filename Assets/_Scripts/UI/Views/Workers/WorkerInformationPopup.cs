using Commons.Models;
using LocalizationNS;
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
            _roleEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.WorkersView.ROLE));
            DetailContainer.Add(_roleEntry);

            _genderEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.WorkersView.GENDER));
            DetailContainer.Add(_genderEntry);

            _dateOfBirthEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.WorkersView.DATE_OF_BIRTH));
            DetailContainer.Add(_dateOfBirthEntry);

            _addressEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.WorkersView.ADDRESS));
            DetailContainer.Add(_addressEntry);

            _createdTimestampEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.WorkersView.ACCOUNT_CREATED));
            DetailContainer.Add(_createdTimestampEntry);
        }

        public override void SetContent(UserProfile data)
        {
            Title.text = data.FirstName + " " + data.LastName;

            _roleEntry.SetValue(Localization.GetUserRole(data.UserRole));
            _genderEntry.SetValue(Localization.GetGender(data.Gender));
            _dateOfBirthEntry.SetValue(data.DateOfBirth.ToString("dd/MM/yyyy"));
            _addressEntry.SetValue(data.Address);
            _createdTimestampEntry.SetValue(data.CreatedTimestamp.ToString("dd/MM/yyyy HH:mm"));
        }
    }
}