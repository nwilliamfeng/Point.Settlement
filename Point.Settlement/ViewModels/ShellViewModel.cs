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
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator,[ImportMany]RunStepViewModelBase[] runSteps)
        {
            this.RunSteps = new ObservableCollection<RunStepViewModelBase>();
        }



        public ObservableCollection<RunStepViewModelBase> RunSteps { get; private set; }

    }
}
