using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;
using System.ComponentModel.Composition;
using Point.Settlement.Model;

namespace Point.Settlement.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Screen,IHandle<InfoEventArgs>,IHandle<ClearRunStepChangeEventArgs>,IHandle<LogOutputEventArgs>
    {
        private IEventAggregator _eventAggregator;
        private IClearDateValidator _clearDateValidator;
        private IClearStepRunService _clearStepRunService;
        private DateTime _clearDate=DateTime.Now.Date;

        public DateTime ClearDate
        {
            get { return this._clearDate; }
            set
            {
                if (this._clearDate == value)
                    return;
                var validDate = this._clearDateValidator.Validate(value > DateTime.Now.Date.AddDays(-1) ? DateTime.Now.Date.AddDays(-1) : value);
                this.SetClearDate(validDate);
            }
        }

        private void SetClearDate(DateTime date)
        {
            var validDate = this._clearDateValidator.Validate(date > DateTime.Now.Date.AddDays(-1) ? DateTime.Now.Date.AddDays(-1) : date);
            this._clearDate = validDate;
            this.NotifyOfPropertyChange(() => this.ClearDate);
            this._logOutputMsgs.Clear();
            this.NotifyOfPropertyChange(() => this.Output);
            this._eventAggregator.PublishOnUIThread(new ClearDateChangeEventArgs(validDate));
            this._eventAggregator.PublishOnUIThread(new ClearRunStepChangeEventArgs( this._clearStepRunService.GetRuningStep(date),true));         
        }
 
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator,
            IClearStepRunService stepRunService,
            IClearDateValidator clearDateValidator,
            [ImportMany]RunStepViewModelBase[] runSteps)
        {
            this.DisplayName = "积分清算";
            this._clearStepRunService = stepRunService;
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);
            this.InitizeRunSteps(runSteps);
            this.InitizeClearDate(clearDateValidator);
        }

        private void InitizeClearDate(IClearDateValidator clearDateValidator)
        {
            this._clearDateValidator = clearDateValidator;
            this.SetClearDate(clearDateValidator.GetDate());   
        }
 
        private string _info;

        public string Info
        {
            get { return this._info; }
            private set
            {
                this._info = value;
                this.NotifyOfPropertyChange(() => this.Info);
            }
        }

        private List<string> _logOutputMsgs = new List<string>();

        public string Output
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                this._logOutputMsgs.ForEach(x => sb.AppendLine(x));
                return sb.ToString();
            }
        }
             
        private void InitizeRunSteps(IEnumerable<RunStepViewModelBase> runSteps)
        {
            this.RunSteps = new ObservableCollection<RunStepViewModelBase>(runSteps.OrderBy(x=>x.Order));
        }

        void IHandle<InfoEventArgs>.Handle(InfoEventArgs arg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => this.Info = arg.Content);
        }

        void IHandle<ClearRunStepChangeEventArgs>.Handle(ClearRunStepChangeEventArgs arg)
        {
            var step = arg.Step;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (step.ClearState == EnumClearState.Clearing)
                    this.Info = "当前正在清算中！";
                else if (step.ClearState == EnumClearState.AllComplete)
                    this.Info = "当前日期清算完毕！";
                else if (step.ClearState == EnumClearState.Finished)
                    this.Info =step.ClearStep==ClearStepNames.STEP_5? "当前自然日已经清算完毕！" : "请继续清算下一步！";
                else if (step.ClearState == EnumClearState.NotBegin)
                    this.Info = "清算未开始。";
                else if (step.ClearState == EnumClearState.AllComplete)
                    this.Info = "当前日期清算完毕！";
                else if (step.ClearState == EnumClearState.Error)
                    this.Info = "清算异常结束！";
            });
        }

        void IHandle<LogOutputEventArgs>.Handle(LogOutputEventArgs arg)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this._logOutputMsgs.Add(arg.Content);
                this.NotifyOfPropertyChange(() => this.Output);
            });
        }

        public ObservableCollection<RunStepViewModelBase> RunSteps { get; private set; }

    }
}
