using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement
{
    public class InfoEventArgs:EventArgs
    {

        public InfoEventArgs(string msg)
        {
            this.Content = msg;
        }

        public string Content { get; private set; }
        
    }
}
