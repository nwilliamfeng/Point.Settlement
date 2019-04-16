using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{

    public class PointReturnClearMock : VaildClearMockBase
    {
        public override void Run(DateTime cleardate)
        {
            Output("活期宝值回写正在运行...");
            System.Threading.Thread.Sleep(5000);
            Output("活期宝充值回写完成");
        }
    }
}
