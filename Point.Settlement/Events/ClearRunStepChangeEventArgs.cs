using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public class ClearRunStepChangeEventArgs : EventArgs
    {

        public ClearRunStepChangeEventArgs(ClearStepInfo step)
        {
            this.Step = step;
        }

        public ClearStepInfo Step { get; private set; }
        
    }
}
