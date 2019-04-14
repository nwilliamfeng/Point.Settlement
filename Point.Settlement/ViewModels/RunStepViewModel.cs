using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;

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

        private string _stateInfo;

        public string StateInfo
        {
            get { return this._stateInfo; }

            protected set
            {
                this._stateInfo = value;
                this.NotifyOfPropertyChange(() => this.StateInfo);
            }
        }

        protected abstract bool CanExecute { get; }

        protected abstract void Execute();

        private ICommand _runCommand;

        public ICommand RunCommand
        {
            get
            {
                return this._runCommand ?? (this._runCommand = new RelayCommand(() =>
                    {
                        Console.WriteLine();
                    }, () => this.CanExecute));
            }
        }
    }
}
