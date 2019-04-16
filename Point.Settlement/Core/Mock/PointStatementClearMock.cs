using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public class PointStatementClearMock: VaildClearMockBase
    {
        public override void Run(DateTime cleardate)
        {
            Output("生成对账单正在运行...");
            System.Threading.Thread.Sleep(5000);
            Output("生成对账单完成");
        }
    }
}
