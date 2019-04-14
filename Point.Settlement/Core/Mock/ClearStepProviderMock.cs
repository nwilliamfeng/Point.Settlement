using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Point.Settlement.Model;

namespace Point.Settlement.Mock
{
    [Export(typeof(IClearStepInfoProvider))]
    public class ClearStepProviderMock : IClearStepInfoProvider
    {
        private List<ClearSetpInfo> lst = new List<ClearSetpInfo>();
        public ClearSetpInfo GetRuningStep(DateTime clearDate)
        {
            return ClearSteps.Step1;
        }
    }


      
}
