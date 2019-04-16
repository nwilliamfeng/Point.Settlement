using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Point.Settlement.Model;
using Point.Settlement.Mock;

namespace Point.Settlement.ViewModels
{
    /// <summary>
    /// 活期宝充值回写
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointReturnClearViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointReturnClearViewModel(IEventAggregator eventAggregator, IClearStepRunService runService)
            : base(eventAggregator, runService) { }
      
        public override int Order => 5;
      
        public override string DisplayName => "活期宝充值记录回写";

        public override string Name => "活期宝充值回写";

        protected override bool CanExecute => (this.State == EnumClearState.NotBegin || this.State == EnumClearState.Finished)
            && GlobalStep != null
            && (GlobalStep.ClearStep == ClearStepNames.STEP_4 && GlobalStep.ClearState == EnumClearState.Finished)
            || (GlobalStep.ClearStep == ClearStepNames.STEP_5 && GlobalStep.ClearState == EnumClearState.Error);

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                ClearStepInfo step = GlobalStep.ClearStep == ClearStepNames.STEP_5 ? GlobalStep : GlobalStep.NextStep;
                new PointReturnClearMock().RunStep(this.ClearDate, step, NotifyLogOutputMessage);
            });
        }
    }
}
