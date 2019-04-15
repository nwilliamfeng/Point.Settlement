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
    public abstract class RunStepViewModelBase:PropertyChangedBase,IHandle<ClearDateChangeEventArgs>,IHandle<ClearRunStepChangeEventArgs>
    {
        protected IEventAggregator EventAggregator { get; set; }

        protected RunStepViewModelBase(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            this.EventAggregator.Subscribe(this);
        }

        protected void NotifyLogOutputMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => this.EventAggregator.PublishOnUIThread(new LogOutputEventArgs(msg)));
        }

        //private EnumClearState _state= EnumClearState.NotBegin;

        //public EnumClearState State
        //{
        //    get { return this._state; }
        //    protected set
        //    {
        //        this._state = value;
        //        this.NotifyOfPropertyChange(() => this.State);
        //    }
        //}

        protected DateTime ClearDate { get; private set; }

        public virtual bool ShowErrorListEnable => false;    

        public abstract int Order { get; }

        private string _info;

        public string Info
        {
            get { return this._info; }
            protected set
            {
                this._info = value;
                this.NotifyOfPropertyChange(()=>this.Info);
            }
        }
        

        public abstract string Name { get; }

        public string NameWithOrder => $"{Order}.{Name}";

        public string DisplayNameWithOrder => $"{Order}.{DisplayName}";

        public abstract string DisplayName { get; }

        protected abstract bool CanExecute { get; }

        protected abstract Task ExecuteCore();

        void IHandle<ClearDateChangeEventArgs>.Handle(ClearDateChangeEventArgs arg)
        {
            this.ClearDate = arg.Date;
        }

        void IHandle<ClearRunStepChangeEventArgs>.Handle(ClearRunStepChangeEventArgs arg)
        {
            this.OnClearRunStepChange(arg.Step);
        }

        protected ClearStepInfo CurrentRunStep { get; private set; }

        protected virtual void OnClearRunStepChange(ClearStepInfo step)
        {
            this.CurrentRunStep = step;
            CommandManager.InvalidateRequerySuggested();          
        }

        private ICommand _runCommand;

        public ICommand RunCommand
        {
            get
            {
                return this._runCommand ?? (this._runCommand = new RelayCommand(async() =>
                    {                   
                        this.State = EnumClearState.Clearing;
                        await this.ExecuteCore();
                        this.State = EnumClearState.Finished;
                    }, () => this.CanExecute));
            }
        }
    }
}
