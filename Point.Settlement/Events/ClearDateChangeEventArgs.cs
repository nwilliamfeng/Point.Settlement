﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement
{
    public class ClearDateChangeEventArgs : EventArgs
    {

        public ClearDateChangeEventArgs(DateTime date)
        {
            this.Date = date;
        }

        public DateTime Date { get; private set; }
        
    }
}
