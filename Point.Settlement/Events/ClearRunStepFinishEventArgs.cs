using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public class ClearRunStepFinishEventArgs : EventArgs
    {

        public ClearRunStepFinishEventArgs(string step)
        {
            this.Step = step;
        }

        public string Step { get; private set; }
        
    }
}
