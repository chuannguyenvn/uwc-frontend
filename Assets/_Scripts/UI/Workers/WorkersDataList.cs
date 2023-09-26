using UI.Common;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Workers
{
    public class WorkersDataList : DataList
    {
        public WorkersDataList()
        {
            name = "WorkersList";

            if (Configs.IS_DEBUGGING)
                for (var i = 0; i < 20; i++)
                    Add(new WorkerEntry());
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<WorkersDataList, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : ScrollView.UxmlTraits
        {
        }

        #endregion
    }
}