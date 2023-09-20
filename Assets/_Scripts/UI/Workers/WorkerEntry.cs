using Commons.Categories;
using UI.Commons;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkerEntry : ListEntry
    {
        public WorkerEntry()
        {
            name = "WorkerEntry";

            Icon.name = "WorkerAvatar";
            PrimaryText.name = "WorkerName";
            SecondaryText.name = "WorkerDetails";

            SetData("Placeholder Name", UserRole.Driver, "Placeholder Address");
        }

        public WorkerEntry(string workerName, UserRole workerRole, string workerCurrentAddress) : this()
        {
            SetData(workerName, workerRole, workerCurrentAddress);
        }

        public void SetData(string workerName, UserRole workerRole, string workerCurrentAddress)
        {
            PrimaryText.text = workerName;
            SecondaryText.text = workerRole + " - " + workerCurrentAddress;
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkerEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}