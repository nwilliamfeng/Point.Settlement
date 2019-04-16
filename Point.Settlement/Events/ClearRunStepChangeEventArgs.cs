using System;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public class ClearRunStepChangeEventArgs : EventArgs
    {

        public ClearRunStepChangeEventArgs(ClearStepInfo step,bool initialized)
        {
            this.Step = step;
            this.IsInitialized = initialized;
        }

        public bool IsInitialized { get;private set; }

        public ClearStepInfo Step { get; private set; }
        
    }
}
