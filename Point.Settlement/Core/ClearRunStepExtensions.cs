using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public static class ClearRunStepExtensions
    {
        public static int GetOrder(this ClearStepInfo step)
        {
            return int.Parse(step.ClearStep.Last().ToString());
        }
    }
}
