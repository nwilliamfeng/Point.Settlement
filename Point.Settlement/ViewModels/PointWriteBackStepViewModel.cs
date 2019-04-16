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
    /// 异常处理及积分回滚
    /// </summary>
    [Export(typeof(RunStepViewModelBase))]
    public class PointWriteBackStepViewModel : RunStepViewModelBase
    {
        [ImportingConstructor]
        public PointWriteBackStepViewModel(IEventAggregator eventAggregator, IClearStepRunService runService)
            : base(eventAggregator, runService) { }
      
        public override int Order => 2;

        public override bool ShowErrorListEnable => true;

        private ICommand _showErrorListCommand;

        public ICommand ShowErrorListCommand
        {
            get {
                return this._showErrorListCommand ?? (this._showErrorListCommand = new RelayCommand(() =>
              {
                  System.Windows.MessageBox.Show("show error!");
              }));
            }
        }

        public override string DisplayName => "异常处理及积分回滚";

        public override string Name => "异常处理";

        protected override bool CanExecute => (this.State == EnumClearState.NotBegin || this.State == EnumClearState.Finished)
            && GlobalStep != null 
            && (GlobalStep.ClearStep == ClearStepNames.STEP_1 && GlobalStep.ClearState == EnumClearState.Finished )
            || (GlobalStep.ClearStep == ClearStepNames.STEP_2 && GlobalStep.ClearState == EnumClearState.Error);

       

        protected override Task ExecuteCore()
        {
            return Task.Run(() =>
            {
                ClearStepInfo step = GlobalStep.ClearStep == ClearStepNames.STEP_2 ? GlobalStep : GlobalStep.NextStep;
                new PointWriteBackMock().RunStep(this.ClearDate, step, NotifyLogOutputMessage);
            });
        }
    }
}
