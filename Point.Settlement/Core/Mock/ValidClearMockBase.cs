using Point.Settlement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Settlement.Mock
{
    public abstract class VaildClearMockBase 
    {
        public delegate void OutputEvent(string log);
        private OutputEvent OutputHandler = null;

       

        public abstract void Run(DateTime cleardate);

        public void RunStep(DateTime cleardate, ClearStepInfo step, OutputEvent func)
        {
            RegisterOutput(func);
            try
            {
                ClearConfigManagerMock.RunStep(step);
                Run(cleardate);
                ClearConfigManagerMock.FinishStep(step);
            }
            catch (Exception ex)
            {
                ClearConfigManagerMock.ErrorStep(step);
                throw ex;
            }
        }

        public void RunStep(DateTime cleardate, ClearStepInfo step)
        {


            try
            {
                ClearConfigManagerMock.RunStep(step);
                Run(cleardate);
                ClearConfigManagerMock.FinishStep(step);
            }
            catch (Exception ex)
            {
                ClearConfigManagerMock.ErrorStep(step);
                throw ex;
            }


        }

        protected void Output(string log)
        {
            if (OutputHandler != null)
            {
                OutputHandler(log);
            }
        }

        protected void RegisterOutput(OutputEvent func)
        {
            if (OutputHandler == null)
            {
                OutputHandler += func;
            }
        }

        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="log"></param>
        protected void Log(string logformat, params object[] args)
        {
            // 日志组装
            string log = string.Format(logformat, args);

            /// 日志回调
            this.Output(log);

            /// 控制台输出
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd .ms"), log));

         
        }

    }
}
