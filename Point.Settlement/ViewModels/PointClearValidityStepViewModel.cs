using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Point.Settlement.ViewModels
{
    [Export(typeof(RunStepViewModelBase))]
    public class PointClearValidityStepViewModel : RunStepViewModelBase
    {

        public PointClearValidityStepViewModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {

        }

        protected override bool CanExecute => throw new NotImplementedException();

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
