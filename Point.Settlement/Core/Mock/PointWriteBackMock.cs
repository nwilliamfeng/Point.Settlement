using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public class PointWriteBackMock: VaildClearMockBase
    {
        public override void Run(DateTime cleardate)
        {
            Output("异常处理正在运行...");
            System.Threading.Thread.Sleep(5000);
            Output("异常处理完成");
        }
    }
}
