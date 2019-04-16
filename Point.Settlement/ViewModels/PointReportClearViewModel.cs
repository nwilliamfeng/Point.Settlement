using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using System.Windows.Input;
using Point.Settlement.Model;
using Point.Settlement.Mock;

namespace Point.Settlement.ViewModels
{
    /// <summary>
    /// 报表生成
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointReportClearViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointReportClearViewModel(IEventAggregator eventAggregator, IClearStepRunService runService)
            : base(eventAggregator, runService) { }
      
        public override int Order => 3;
      
        public override string DisplayName => "生成财务报表";

        public override string Name => "报表生成";

        protected override bool CanExecute => (this.State == EnumClearState.NotBegin || this.State == EnumClearState.Finished)
            && GlobalStep != null
            && (GlobalStep.ClearStep == ClearStepNames.STEP_2 && GlobalStep.ClearState == EnumClearState.Finished)
            || (GlobalStep.ClearStep == ClearStepNames.STEP_3 && GlobalStep.ClearState == EnumClearState.Error);

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                ClearStepInfo step = GlobalStep.ClearStep == ClearStepNames.STEP_3 ? GlobalStep : GlobalStep.NextStep;
                new PointReportClearMock().RunStep(this.ClearDate, step, NotifyLogOutputMessage);
            });
        }
    }
}
