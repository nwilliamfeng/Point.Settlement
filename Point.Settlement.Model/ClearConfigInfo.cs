using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Model
{
    public class ClearConfigInfo : EntityBase
    {
        /// <summary>
        /// 清算日期
        /// </summary>
        public DateTime ClearDate { get; set; }

        /// <summary>
        /// 下一个清算日期
        /// </summary>
        public DateTime NextClearDate { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 当前清算步骤
        /// </summary>
        public string ClearStep { get; set; }

        /// <summary>
        /// 当前清算步骤名称
        /// </summary>
        public string ClearStepName { get; set; }

        /// <summary>
        /// 清算备注说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 清算状态
        /// </summary>
        public EnumClearState ClearState { get; set; }

        /// <summary>
        /// 增加清算备注
        /// </summary>
        /// <param name="remark"></param>
        public void AddRemark(string remark)
        {
            this.Remark += string.Format("[{0}]{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), remark);
        }
    }
}
