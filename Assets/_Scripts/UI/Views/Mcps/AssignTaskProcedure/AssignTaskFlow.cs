using UI.Reusables.Procedure;
using UnityEngine.UIElements;

namespace UI.Views.Mcps.AssignTaskProcedure
{
    public sealed class AssignTaskFlow : Flow
    {
        private SetAssigningOptions _setAssigningOptions;
        private ChooseMcpsStep _chooseMcpsStep;
        private ChooseWorkerStep _chooseWorkerStep;
        private ChooseDateTimeStep _chooseDateTimeStep;

        protected override void CreateSteps()
        {
            _setAssigningOptions = new SetAssigningOptions(this, 1);
            AddStep(_setAssigningOptions);

            _chooseMcpsStep = new ChooseMcpsStep(this, 2);
            AddStep(_chooseMcpsStep);

            _chooseWorkerStep = new ChooseWorkerStep(this, 3);
            AddStep(_chooseWorkerStep);

            _chooseDateTimeStep = new ChooseDateTimeStep(this, 4);
            AddStep(_chooseDateTimeStep);
        }

        protected override void SubmitResult(ClickEvent evt)
        {
        }
    }
}