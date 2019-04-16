using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Point.Settlement.Model;
using System.IO;
using Point.Settlement.Mock;
 

namespace Point.Settlement
{
    [Export(typeof(IClearDateValidator))]
    public class ClearDateValidator : IClearDateValidator
    {
        private readonly DateTime _maxDate = DateTime.Now.Date.AddDays(-1);
      
        public DateTime GetDate( )
        {
            ClearConfigInfo configinfo = ClearConfigManagerMock.GetLatestConfigInfo();

            if (configinfo != null)
            {
                if (configinfo.ClearStep == "Step5" ||
                    (configinfo.ClearStep == "Step4" && configinfo.ClearState == EnumClearState.Finished))
                {
                    if (configinfo.NextClearDate.Date > this._maxDate)
                        return this._maxDate;
                    else
                        return configinfo.NextClearDate;
                }
                else
                   return configinfo.ClearDate;
            }
            else
               return _maxDate;


        }

        public DateTime Validate(DateTime cleardate)
        {
          
            ClearConfigInfo configinfo = ClearConfigManagerMock.GetLatestConfigInfo();
            if (configinfo == null || configinfo.ClearDate > cleardate)
            {
               
            }
            else if (cleardate == configinfo.NextClearDate)
            {
                if (!(configinfo.ClearStep == "Step5" || (configinfo.ClearStep == "Step4" && configinfo.ClearState == EnumClearState.Finished)))
                    return configinfo.ClearDate;
            }
            else if (cleardate > configinfo.NextClearDate)
            {
                if (configinfo.ClearStep == "Step5" || (configinfo.ClearStep == "Step4" && configinfo.ClearState == EnumClearState.Finished))
                {
                   return configinfo.NextClearDate;
                }
                else
                {
                    return configinfo.ClearDate;
                }
            }
           
            return cleardate.Date;
        }

        
    }


      
}
