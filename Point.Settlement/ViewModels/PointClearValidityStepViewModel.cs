using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Point.Settlement.Model;
using System.Windows.Input;
using Point.Settlement.Mock;

namespace Point.Settlement.ViewModels
{
    /// <summary>
    /// 积分有效性校验
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointClearValidityStepViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointClearValidityStepViewModel(IEventAggregator eventAggregator, IClearStepRunService runService)
            : base(eventAggregator, runService) { }       

        public override int Order => 1;


        public override string DisplayName => "积分有效性校验";

        public override string Name => "有效性校验";

        protected override bool CanExecute =>(this.State== EnumClearState.NotBegin|| this.State== EnumClearState.Finished) && GlobalStep != null && GlobalStep.PrevStep==null &&
             (GlobalStep.ClearState == EnumClearState.NotBegin || GlobalStep.ClearState == EnumClearState.Error);

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                new PointVaildClearMock().RunStep(this.ClearDate, this.GlobalStep, NotifyLogOutputMessage);
            });
        }
    }
}
