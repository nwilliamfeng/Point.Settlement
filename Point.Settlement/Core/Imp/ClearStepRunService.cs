using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Mock;
using System.ComponentModel.Composition;
using Point.Settlement.Model;

namespace Point.Settlement 
{
    [Export(typeof(IClearStepRunService))]
    public class ClearStepRunService : IClearStepRunService
    {
        public Task Execute(DateTime clearDate, ClearStepInfo step, Action<string> msgHandle)
        {
            return null;
        }

        public ClearStepInfo GetRuningStep(DateTime clearDate)
        {
            return ClearConfigManagerMock.GetRuningStep(clearDate);
        }
    }
}
