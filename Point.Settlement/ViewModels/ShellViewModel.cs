using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using Point.Settlement.Model;

namespace Point.Settlement.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Screen,IHandle<InfoEventArgs>
    {
        private IEventAggregator _eventAggregator;
        private IClearDateValidator _clearDateValidator;
        private IClearStepRunService _clearStepRunService;
        private DateTime _clearDate=DateTime.Now;

        public DateTime ClearDate
        {
            get { return this._clearDate; }
            set
            {
                if (this._clearDate == value)
                    return;

                this.SetClearDate(value);
            }
        }

        private void SetClearDate(DateTime date)
        {
            var validDate = this._clearDateValidator.Validate(date > DateTime.Now.Date.AddDays(-1) ? DateTime.Now.Date.AddDays(-1) : date);
            this._clearDate = validDate;
            this.NotifyOfPropertyChange(() => this.ClearDate);
            this._eventAggregator.PublishOnUIThread(new ClearDateChangeEventArgs(validDate));
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
            this.InitizeClearDate(clearDateValidator);
            this.InitizeInfo();
        }

        private void InitizeInfo()
        {
            var step = this._clearStepRunService.GetRuningStep(this.ClearDate);
            if (step == null)
                return;
            switch (step.ClearState)
            {
                case EnumClearState.AllComplete:
                    this.Info = "清算已结束，完成于:" + step.Finish.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case EnumClearState.Clearing:
                    this.Info = "清算中，当前步骤:" + step.ClearStepName;
                    break;
                case EnumClearState.Finished:
                    this.Info = "请继续清算下一步";
                    break;
                case EnumClearState.NotBegin:
                    this.Info = "清算未开始。";
                    break;
                case EnumClearState.Error:
                    this.Info = "当前步骤[" + step.ClearStepName + "]清算有一场，需要重新清算。";
                    break;
                default:break;
            };
                  
        }

        private void InitizeClearDate(IClearDateValidator clearDateValidator)
        {
            this._clearDateValidator = clearDateValidator;
            this._clearDate = clearDateValidator.GetDate();
            this.NotifyOfPropertyChange(() => this.ClearDate);
            this._eventAggregator.PublishOnUIThread(new ClearDateChangeEventArgs(this._clearDate));
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
             
        private void Initize(IEnumerable<RunStepViewModelBase> runSteps)
        {
            this.RunSteps = new ObservableCollection<RunStepViewModelBase>(runSteps.OrderBy(x=>x.Order));
          
            //todo -- read configInfo
        }

        void IHandle<InfoEventArgs>.Handle(InfoEventArgs arg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.Info = arg.Content;
            });
        }

        public ObservableCollection<RunStepViewModelBase> RunSteps { get; private set; }

    }
}
