using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Point.Settlement.Model;

namespace Point.Settlement.ViewModels
{
    /// <summary>
    /// 积分有效性校验
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointClearValidityStepViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointClearValidityStepViewModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
        }

        public override int Order => 1;

        public override string DisplayName => "积分有效性校验";

        public override string Name => "有效性校验";

        protected override void OnClearRunStepChange(ClearStepInfo step)
        {
            base.OnClearRunStepChange(step);
            if (!this.CanExecute)
            {
                this.de
            }
        }

        protected override bool CanExecute =>this.CurrentRunStep!=null && this.CurrentRunStep.PrevStep==null &&
             (CurrentRunStep.ClearState == EnumClearState.NotBegin || CurrentRunStep.ClearState == EnumClearState.Error);

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("run!!");
            });
        }
    }
}
