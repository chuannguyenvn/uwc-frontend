using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private ChooseMcpsStep _chooseMcpsStep;
        private ChooseWorkerStep _chooseWorkerStep;
        private ChooseDatetimeStep _chooseDatetimeStep;

        public AssignTaskFlow() : base(nameof(AssignTaskFlow))
        {
        }

        protected override void CreateSteps()
        {
            _chooseMcpsStep = new ChooseMcpsStep(this, 1);
            AddStep(_chooseMcpsStep);

            _chooseWorkerStep = new ChooseWorkerStep(this, 2);
            AddStep(_chooseWorkerStep);

            _chooseDatetimeStep = new ChooseDatetimeStep(this, 3);
            AddStep(_chooseDatetimeStep);
        }

        protected override void SubmitResult(ClickEvent evt)
        {
        }

        public new class UxmlFactory : UxmlFactory<AssignTaskFlow, UxmlTraits>
        {
        }
    }
}