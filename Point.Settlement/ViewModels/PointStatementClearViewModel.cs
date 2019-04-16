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
    /// 生成对账单
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointStatementClearViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointStatementClearViewModel(IEventAggregator eventAggregator, IClearStepRunService runService)
            : base(eventAggregator, runService) { }
      
        public override int Order => 4;
      
        public override string DisplayName => "生成消费对账单";

        public override string Name => "生成对账单";

        protected override bool CanExecute => (this.State == EnumClearState.NotBegin || this.State == EnumClearState.Finished)
            && GlobalStep != null
            && (GlobalStep.ClearStep == ClearStepNames.STEP_3 && GlobalStep.ClearState == EnumClearState.Finished)
            || (GlobalStep.ClearStep == ClearStepNames.STEP_4 && GlobalStep.ClearState == EnumClearState.Error);

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                ClearStepInfo step = GlobalStep.ClearStep == ClearStepNames.STEP_4 ? GlobalStep : GlobalStep.NextStep;
                new  PointStatementClearMock().RunStep(this.ClearDate, step, NotifyLogOutputMessage);
            });
        }
    }
}
