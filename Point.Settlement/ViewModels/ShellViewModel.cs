using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Point.Settlement.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private DateTime _clearDate=DateTime.Now;

        public DateTime ClearDate
        {
            get { return this._clearDate; }
            set
            {
                if (this._clearDate == value)
                    return;

                this._clearDate = value;              
                this.NotifyOfPropertyChange(() => this.ClearDate);
                this._eventAggregator.PublishOnUIThread(new ClearDateChangeEventArgs(value));
            }
        }
 


        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator,[ImportMany]RunStepViewModelBase[] runSteps)
        {
            this.DisplayName = "验证系统";
            this._eventAggregator = eventAggregator;
            this.RunSteps = new ObservableCollection<RunStepViewModelBase>();
            this.Initize();
        }


        private void Initize()
        {
            //todo -- read configInfo
        }


        public ObservableCollection<RunStepViewModelBase> RunSteps { get; private set; }

    }
}
