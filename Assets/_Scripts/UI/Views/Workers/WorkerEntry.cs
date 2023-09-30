using Commons.Categories;
using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Workers
{
    public class WorkerEntry : DataListEntry
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