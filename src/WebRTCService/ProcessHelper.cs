using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRTCService
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 启动无窗体程序
        /// </summary>
        /// <param name="exePath"></param>
        public static void NoWindowStartProcess(string exePath)
        {
            try
            {
                ProcessStartInfo ps = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = exePath
                };
                Process.Start(ps);
            }
            catch (Exception ex)
            {
                Log.Info($"启动进程出错：{ex}");
            }

        }

        /// <summary>
        /// 根据进程名结束掉进程
        /// </summary>
        /// <param name="strProcName"></param>
        public static void KillProcess(string strProcName)
        {
            try
            {
                foreach (Process p in Process.GetProcessesByName(strProcName))
                {
                    if (!p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info($"停止进程出错：{ex}");
            }
        }
    }
}
