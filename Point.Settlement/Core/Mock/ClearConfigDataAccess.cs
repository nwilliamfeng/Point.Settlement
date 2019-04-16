using Point.Settlement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Point.Settlement.Mock
{
    public class ClearConfigDataAccessMock
    {
        private static ClearConfigDataAccessMock instance = new ClearConfigDataAccessMock();

        private ClearConfigDataAccessMock()
        {

        }


        public static ClearConfigDataAccessMock Current
        {
            get { return instance; }
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<ClearConfigInfo> GetConfigInfo()
        {
            if (!File.Exists(this.GetFile()))
                return new List<ClearConfigInfo>();
            var txt = File.ReadAllText(this.GetFile());
            return JsonConvert.DeserializeObject<List<ClearConfigInfo>>(txt);
        }

        private string GetFile()
        {
            return Directory.GetCurrentDirectory() + "\\ConfigInfo.txt";

        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public ClearConfigInfo GetConfigInfo(DateTime datetime)
        {
            return this.GetConfigInfo().FirstOrDefault(x=>x.ClearDate==datetime) ;
        }

        /// <summary>
        /// 获取最近的清算日期
        /// </summary>
        /// <returns></returns>
        public ClearConfigInfo GetLatestConfigInfo()
        {
            return this.GetConfigInfo().OrderBy(x => x.ClearDate).LastOrDefault();
        }

        /// <summary>
        /// 新增或更新任务
        /// </summary>
        /// <param name="configinfo"></param>
        /// <returns></returns>
        public bool InsertOrUpdateConfigInfo(ClearConfigInfo configinfo)
        {
            var infos = this.GetConfigInfo();
            var info = infos.FirstOrDefault(x => x.Equals(configinfo));
            if (info == null)
                infos.Add(configinfo);
            else
                infos[infos.IndexOf(info)] = configinfo;
            File.WriteAllText(this.GetFile(), JsonConvert.SerializeObject(infos));
            return true;
        }

       

       
    }
}
