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
    public abstract class RunStepViewModelBase:PropertyChangedBase,IHandle<ClearDateChangeEventArgs>,IHandle<ClearRunStepFinishEventArgs>
    {
        protected IEventAggregator EventAggregator { get;private set; }

        protected IClearStepRunService ClearStepRunService { get; private set; }

        protected RunStepViewModelBase(IEventAggregator eventAggregator,IClearStepRunService runService)
        {
            this.EventAggregator = eventAggregator;
            this.ClearStepRunService = runService;
            this.EventAggregator.Subscribe(this);
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

        void IHandle<ClearRunStepFinishEventArgs>.Handle(ClearRunStepFinishEventArgs arg)
        {
            this.OnClearRunStepChange(arg.Step);
        }

        protected ClearStepInfo CurrentRunStep { get; private set; }

        protected abstract void OnClearRunStepChange(string step);
       

        private ICommand _runCommand;

        public ICommand RunCommand
        {
            get
            {
                return this._runCommand ?? (this._runCommand = new RelayCommand(async() =>
                    {                   
                        this.State = EnumClearState.Clearing;
                        await this.ExecuteCore();
                        var step = this.ClearStepRunService.GetRuningStep(this.ClearDate);
                        this.State=step==null?EnumClearState.Error:step.ClearState;
                    }, () => this.CanExecute));
            }
        }
    }
}
