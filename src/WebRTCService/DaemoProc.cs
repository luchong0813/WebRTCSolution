using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WebRTCService
{
    /// <summary>
    /// 守护进程
    /// </summary>
    public class DaemoProc
    {
        private Timer timer=new Timer();
        private readonly string _ProcessName;
        private readonly string _ExePath;

        public DaemoProc(string processName,string exePath)
        {
            _ProcessName = processName;
            _ExePath = exePath;
            timer.Interval = 5000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Process[] myproc = Process.GetProcessesByName(_ProcessName);
            if (myproc.Length == 0)
            {
                Log.Info($"检测到程序已退出，开始重新激活程序，程序路径：{_ExePath}");
                ProcessHelper.NoWindowStartProcess(_ExePath);
            }
        }

        public void Start()
        {
            timer.Start();
            Log.Info("守护进程已启动！");
        }


        public void Stop()
        {
            timer.Stop();
            Log.Info("守护进程已停止！");
        }
    }
}
