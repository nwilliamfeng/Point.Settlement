using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public class PointVaildClearMock: VaildClearMockBase
    {
        public override void Run(DateTime cleardate)
        {
            Output("积分验证正在运行...");
            System.Threading.Thread.Sleep(5000);
            Output("积分验证完成");
        }
    }
}
