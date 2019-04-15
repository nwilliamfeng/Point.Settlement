using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public interface IClearDateValidator
    {
        DateTime GetDate();

        DateTime Validate(DateTime date);
    }

    
}
