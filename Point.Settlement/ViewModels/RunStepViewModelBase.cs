using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using Point.Settlement.Model;

namespace Point.Settlement.ViewModels
{
    public abstract class RunStepViewModelBase:PropertyChangedBase
        ,IHandle<ClearDateChangeEventArgs>
        ,IHandle<ClearRunStepChangeEventArgs>
    {
        protected IEventAggregator EventAggregator { get;private set; }

        protected IClearStepRunService ClearStepRunService { get; private set; }

        public string Step => $"Step{this.Order}";
       
        protected RunStepViewModelBase(IEventAggregator eventAggregator,IClearStepRunService runService)
        {
            this.EventAggregator = eventAggregator;
            this.ClearStepRunService = runService;
            this.EventAggregator.Subscribe(this);
        }

        private bool _isChecked;

        public bool IsChecked
        {
            get { return this._isChecked; }
            protected set
            {
                this._isChecked = value;
                this.NotifyOfPropertyChange(() => this.IsChecked);
            }
        }

        protected void NotifyLogOutputMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => this.EventAggregator.PublishOnUIThread(new LogOutputEventArgs(msg)));
        }

        private EnumClearState _state= EnumClearState.NotBegin;

        public EnumClearState State
        {
            get { return this._state; }
            protected set
            {
                this._state = value;
                this.NotifyOfPropertyChange(() => this.State);
            }
        }

        protected DateTime ClearDate { get; private set; }

        public virtual bool ShowErrorListEnable => false;    

        public abstract int Order { get; }

        public abstract string Name { get; }

        public string NameWithOrder => $"{Order}.{Name}";

        public string DisplayNameWithOrder => $"{Order}.{DisplayName}";

        public abstract string DisplayName { get; }

        protected abstract bool CanExecute { get; }

        protected abstract Task ExecuteCore();
    
        void IHandle<ClearDateChangeEventArgs>.Handle(ClearDateChangeEventArgs arg)
        {
            this.OnClearDateChange(arg.Date);
        }

        protected virtual void OnClearDateChange(DateTime date)
        {
            this.ClearDate = date;
            this.State = EnumClearState.NotBegin;
            CommandManager.InvalidateRequerySuggested();
        }

        void IHandle<ClearRunStepChangeEventArgs>.Handle(ClearRunStepChangeEventArgs arg)
        {
            this.OnClearRunStepChange(arg);
        }

        protected ClearStepInfo GlobalStep { get; private set; }

        protected virtual void OnClearRunStepChange(ClearRunStepChangeEventArgs arg)
        {
            GlobalStep = arg.Step;                      
            switch (GlobalStep.ClearState)
            {
                case EnumClearState.AllComplete:
                    this.IsChecked = true;
                    break;

                case EnumClearState.Error:
                    if (GlobalStep.ClearStep == this.Step)
                        this.State = EnumClearState.Error;
                    break;
                case EnumClearState.Finished:
                    if (GlobalStep.ClearStep == this.Step || this.Order < GlobalStep.GetOrder())
                        this.State = EnumClearState.Finished; 
                    break;
                default:
                    break;
            }
        }

        private ICommand _runCommand;

        public ICommand RunCommand
        {
            get
            {
                return this._runCommand ?? (this._runCommand = new RelayCommand(async() =>
                    {                   
                        this.State = EnumClearState.Clearing;
                        this.EventAggregator.PublishOnUIThread(new InfoEventArgs("当前正在清算中！"));
                        CommandManager.InvalidateRequerySuggested();
                        await this.ExecuteCore();
                        var step = this.ClearStepRunService.GetRuningStep(this.ClearDate);
                        this.State = step == null ? EnumClearState.Error : step.ClearState;
                        this.IsChecked = true;
                        this.EventAggregator.PublishOnUIThread(new ClearRunStepChangeEventArgs(step,false));
                        CommandManager.InvalidateRequerySuggested();
                    }, () => this.CanExecute));
            }
        }
        
    }
}
