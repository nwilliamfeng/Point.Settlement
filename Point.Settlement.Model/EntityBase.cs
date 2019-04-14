using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Model
{
    public class EntityBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
      //  [DataField("CreateTime", typeof(DateTime))]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
      //  [DataField("UpdateTime", typeof(DateTime))]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否是新创建
        /// </summary>
        private bool isCreate = false;
        public bool IsCreate
        {
            get { return isCreate; }
            set { isCreate = value; }
        }

        /// <summary>
        /// 是否有效标志位
        /// </summary>
      //  [DataField("IsEnabled", typeof(int))]
        public int IsEnabled { get; set; }

        /// <summary>
        /// 逻辑删除标志位
        /// </summary>
      //  [DataField("IsDel", typeof(int))]
        public int IsDel { get; set; }
    }
}
