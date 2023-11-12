using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private ChooseMcpsStep _chooseMcpsStep;
        private ChooseWorkerStep _chooseWorkerStep;

        public AssignTaskFlow() : base(nameof(AssignTaskFlow))
        {
        }

        protected override void CreateSteps()
        {
            _chooseMcpsStep = new ChooseMcpsStep(this, 1);
            AddStep(_chooseMcpsStep);

            _chooseWorkerStep = new ChooseWorkerStep(this, 2);
            AddStep(_chooseWorkerStep);
        }

        public override void SubmitResult()
        {
        }

        public new class UxmlFactory : UxmlFactory<AssignTaskFlow, UxmlTraits>
        {
        }
    }
}