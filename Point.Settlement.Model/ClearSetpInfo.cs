using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Model
{
    /// <summary>
    /// 清算步骤
    /// </summary>
    public class ClearSetpInfo
    {
        public ClearSetpInfo PrevStep;
        public ClearSetpInfo NextStep;
        /// <summary>
        /// 当前清算步骤
        /// </summary>
        public string ClearStep { get; set; }

        /// <summary>
        /// 当前清算步骤名称
        /// </summary>
        public string ClearStepName { get; set; }

        /// <summary>
        /// 当前清算步骤清算状态
        /// </summary>
        public EnumClearState ClearState { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 开始于
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// 结束于
        /// </summary>
        public DateTime Finish { get; set; }

        public override int GetHashCode()
        {
            return this.ClearStep==null?base.GetHashCode():this.ClearStep .GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ClearSetpInfo))
                return false;
            return this.ClearStep == (obj as ClearSetpInfo).ClearStep;
        }
    }
}
