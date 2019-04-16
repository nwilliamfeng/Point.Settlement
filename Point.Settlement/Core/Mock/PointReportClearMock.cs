using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public class PointReportClearMock:VaildClearMockBase
    {
        public override void Run(DateTime cleardate)
        {
            Output("报表生成正在运行...");
            System.Threading.Thread.Sleep(5000);
            Output("报表生成完成");
        }
    }
}
