using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point.Settlement.Model;

namespace Point.Settlement
{
    public static class ClearSteps
    {
        private static List<ClearStepInfo> steps = new List<ClearStepInfo>();

        static ClearSteps()
        {
            if (steps == null)
            {
                steps = new List<ClearStepInfo>();
                steps.Add( new ClearStepInfo()
                {
                    ClearStep = "Step1",
                    Index = 1,
                    ClearStepName = "有效性校验"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step2",
                    Index = 2,
                    ClearStepName = "异常处理"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step6",
                    Index = 3,
                    ClearStepName = "激活校验"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step7",
                    Index = 4,
                    ClearStepName = "激活回置"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step3",
                    Index = 5,
                    ClearStepName = "报表生成"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step4",
                    Index = 6,
                    ClearStepName = "生成对账单"
                });
                steps.Add(new ClearStepInfo()
                {
                    ClearStep = "Step5",
                    Index = 7,
                    ClearStepName = "活期宝充值回写"
                });
            }
           
        }

        public static ClearStepInfo Step1
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step1");
            }
        }

        public static ClearStepInfo Step2
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step2");
            }
        }

        public static ClearStepInfo Step3
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step3");
            }
        }

        public static ClearStepInfo Step4
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step4");
            }
        }

        public static ClearStepInfo Step5
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step5");
            }
        }
        public static ClearStepInfo Step6
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step6");
            }
        }
        public static ClearStepInfo Step7
        {
            get
            {
                return steps.FirstOrDefault(x => x.ClearStep == "Step7");
            }
        }


        public static ClearStepInfo GetStep(string step)
        {
            return steps.FirstOrDefault(x => x.ClearStep == step);
        }
    }
}
