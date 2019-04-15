using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Point.Settlement.Model
{
    public enum EnumClearState
    {
        /// <summary>
        /// 已完成或未开始
        /// </summary>
        NotBegin = -1,

        /// <summary>
        /// 单个步骤结束
        /// </summary>
        Finished = 0,

        /// <summary>
        /// 清算中
        /// </summary>
        Clearing = 1,

        /// <summary>
        /// 全部结束
        /// </summary>
        AllComplete = 2,

        /// <summary>
        /// 清算异常
        /// </summary>
        Error = 9,
    }
}
