using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private ChooseMcpsStep _chooseMcpsStep;

        public AssignTaskFlow() : base(nameof(AssignTaskFlow))
        {
        }

        protected override void CreateSteps()
        {
            _chooseMcpsStep = new ChooseMcpsStep(this);
            Add(_chooseMcpsStep);
        }

        public override void SubmitResult()
        {
        }

        public new class UxmlFactory : UxmlFactory<AssignTaskFlow, UxmlTraits>
        {
        }
    }
}