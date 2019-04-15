using Point.Settlement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Point.Settlement
{
    public interface IClearStepRunService
    {
        Task Execute(DateTime clearDate,ClearStepInfo step,Action<string> msgHandle);

        ClearStepInfo GetRuningStep(DateTime clearDate);
    }
}
