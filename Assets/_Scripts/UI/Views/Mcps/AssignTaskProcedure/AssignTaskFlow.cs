using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private ChooseMcpsStep _chooseMcpsStep;
        private ChooseWorkerStep _chooseWorkerStep;
        private ChooseDateTimeStep _chooseDateTimeStep;

        protected override void CreateSteps()
        {
            _chooseMcpsStep = new ChooseMcpsStep(this, 1);
            AddStep(_chooseMcpsStep);

            _chooseWorkerStep = new ChooseWorkerStep(this, 2);
            AddStep(_chooseWorkerStep);

            _chooseDateTimeStep = new ChooseDateTimeStep(this, 3);
            AddStep(_chooseDateTimeStep);
        }

        protected override void SubmitResult(ClickEvent evt)
        {
        }
    }
}