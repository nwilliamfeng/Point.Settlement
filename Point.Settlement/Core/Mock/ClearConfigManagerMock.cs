using Point.Settlement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public static class ClearConfigManagerMock
    {
        public static DateTime? ClearDate;
        private static ClearConfigInfo _clearconfig = null;

        private static ClearStepInfo step1;
        private static ClearStepInfo step2;
      //  private static ClearStepInfo step6;
      //  private static ClearStepInfo step7;
        private static ClearStepInfo step3;
        private static ClearStepInfo step4;
        private static ClearStepInfo step5;
        private static Dictionary<string, ClearStepInfo> dicstep = null;
        static ClearConfigManagerMock()
        {
            dicstep = new Dictionary<string, ClearStepInfo>();
            step1 = new ClearStepInfo()
            {
                ClearStep = "Step1",
                Index = 1,
                ClearStepName = "有效性校验"
            };
            step2 = new ClearStepInfo()
            {
                ClearStep = "Step2",
                Index = 2,
                ClearStepName = "异常处理"
            };
            //step6 = new ClearStepInfo()
            //{
            //    ClearStep = "Step6",
            //    Index = 3,
            //    ClearStepName = "激活校验"
            //};
            //step7 = new ClearStepInfo()
            //{
            //    ClearStep = "Step7",
            //    Index = 4,
            //    ClearStepName = "激活回置"
            //};
            step3 = new ClearStepInfo()
            {
                ClearStep = "Step3",
                Index = 5,
                ClearStepName = "报表生成"
            };
            step4 = new ClearStepInfo()
            {
                ClearStep = "Step4",
                Index = 6,
                ClearStepName = "生成对账单"
            };
            step5 = new ClearStepInfo()
            {
                ClearStep = "Step5",
                Index = 7,
                ClearStepName = "活期宝充值回写"
            };

            step1.PrevStep = null;
            step1.NextStep = step2;
            dicstep.Add(step1.ClearStep, step1);

            step2.PrevStep = step1;
            step2.NextStep = step3;
            dicstep.Add(step2.ClearStep, step2);

          //  step6.PrevStep = step2;
          //  step6.NextStep = step7;
         //   dicstep.Add(step6.ClearStep, step6);

          //  step7.PrevStep = step6;
         //   step7.NextStep = step3;
         //   dicstep.Add(step7.ClearStep, step7);

            step3.PrevStep = step2;
            step3.NextStep = step4;
            dicstep.Add(step3.ClearStep, step3);

            step4.PrevStep = step3;
            step4.NextStep = step5;
            dicstep.Add(step4.ClearStep, step4);

            step5.PrevStep = step4;
            step5.NextStep = null;
            dicstep.Add(step5.ClearStep, step5);

        }

        /// <summary>
        /// 清算是否进行中
        /// </summary>
        /// <returns></returns>
        public static bool IsRuningFinished()
        {
            if (ClearDate.HasValue)
            {
                _clearconfig =  ClearConfigDataAccessMock.Current.GetConfigInfo(ClearDate.Value);
                if (_clearconfig == null)
                {
                    _clearconfig = ClearConfigBuinder();
                    return false;
                }
            }

            if (_clearconfig.ClearState == EnumClearState.Finished && _clearconfig.ClearStep == "9999")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取当前清算步骤
        /// </summary>
        /// <returns></returns>
        public static ClearStepInfo GetRuningStep(DateTime cleardate)
        {
            ClearDate = cleardate;
            return GetRuningStep();
        }

        /// <summary>
        /// 获取当前清算步骤
        /// </summary>
        /// <returns></returns>
        private static ClearStepInfo GetRuningStep()
        {
            if (ClearDate.HasValue)
            {
                Console.WriteLine("GetRuningStep");
                _clearconfig = ClearConfigDataAccessMock.Current.GetConfigInfo(ClearDate.Value);
                if (_clearconfig == null)
                {
                    step1.ClearState = EnumClearState.NotBegin;
                    return step1;
                }
                else
                {
                    if (dicstep.ContainsKey(_clearconfig.ClearStep))
                    {
                        ClearStepInfo step = dicstep[_clearconfig.ClearStep];
                        step.ClearState = _clearconfig.ClearState;
                        step.Start = _clearconfig.BeginTime;
                        step.Finish = _clearconfig.FinishTime;
                        return step;
                    }
                    else
                    {
                        throw new Exception("步骤异常");
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 开始清算步骤
        /// </summary>
        public static void RunStep(ClearStepInfo step)
        {
            if (!ClearDate.HasValue)
            {
                throw new Exception("未设置当前清算时间");
            }

            _clearconfig = ClearConfigDataAccessMock.Current.GetConfigInfo(ClearDate.Value);
            if (_clearconfig == null)
            {
                _clearconfig = ClearConfigBuinder();
                _clearconfig.BeginTime = DateTime.Now;
            }
            _clearconfig.ClearState = EnumClearState.Clearing;
            if (step != null && !string.IsNullOrEmpty(step.ClearStep))
            {
                _clearconfig.ClearStep = step.ClearStep;
                _clearconfig.ClearStepName = step.ClearStepName;
                SaveToOracle();
            }
        }

        public static void FinishStep(ClearStepInfo step)
        {
            if (!ClearDate.HasValue)
            {
                throw new Exception("未设置当前清算时间");
            }
            _clearconfig = ClearConfigDataAccessMock.Current.GetConfigInfo(ClearDate.Value);
            if (_clearconfig == null)
            {
                throw new Exception("出错了");
            }
            if (step.NextStep == null)
            {
                _clearconfig.ClearState = EnumClearState.AllComplete;
            }
            else
            {
                _clearconfig.ClearState = EnumClearState.Finished;
            }
            _clearconfig.FinishTime = DateTime.Now;
            SaveToOracle();
        }


        public static void ErrorStep(ClearStepInfo step)
        {
            if (!ClearDate.HasValue)
            {
                throw new Exception("未设置当前清算时间");
            }
            _clearconfig = ClearConfigDataAccessMock.Current.GetConfigInfo(ClearDate.Value);
            if (_clearconfig == null)
            {
                throw new Exception("出错了");
            }

            _clearconfig.ClearState = EnumClearState.Error;
            _clearconfig.FinishTime = DateTime.Now;
            SaveToOracle();
        }

        private static ClearConfigInfo ClearConfigBuinder()
        {
            ClearConfigInfo info = new ClearConfigInfo();
            if (ClearDate != null)
            {
                info.NextClearDate = ClearDate.Value.AddDays(1);
                info.ClearDate = ClearDate.Value;
            }
            return info;
        }

      
        /// <summary>
        /// 保存到数据库
        /// </summary>
        private static void SaveToOracle()
        {
            bool ret = ClearConfigDataAccessMock.Current.InsertOrUpdateConfigInfo(_clearconfig);

            if (!ret)
            {
                throw new Exception("数据库访问异常，请检查日志获取详细信息！");
            }
        }

        /// <summary>
        /// 获取最近的清算日期
        /// </summary>
        /// <returns></returns>
        public static ClearConfigInfo GetLatestConfigInfo()
        {
            return ClearConfigDataAccessMock.Current.GetLatestConfigInfo();
        }

        /// <summary>
        /// 获取最近的清算日期
        /// </summary>
        /// <returns></returns>
        public static List<ClearConfigInfo> GetAllConfigInfo()
        {
            return ClearConfigDataAccessMock.Current.GetConfigInfo();
        }
    }
}
