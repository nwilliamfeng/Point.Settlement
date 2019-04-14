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
    {
        protected IEventAggregator EventAggregator { get; set; }

        protected RunStepViewModelBase(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
        }

        protected void NotifyMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() => this.EventAggregator.PublishOnUIThread(new MessageEventArgs(msg)));
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

        protected abstract bool CanExecute { get; }

        protected abstract Task ExecuteCore();

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
