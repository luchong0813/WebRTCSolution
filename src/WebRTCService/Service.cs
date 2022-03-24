using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebRTCService
{
    public partial class Service : ServiceBase
    {
        private readonly string processName= "webrtc-streamer";
        private readonly string webRtcPath=string.Empty;
        private DaemoProc daemoProc;
        public Service()
        {
            InitializeComponent();
            webRtcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"webrtc-streamer-v0.6.5-Windows-AMD64-Release\{processName}.exe");
            daemoProc = new DaemoProc(processName, webRtcPath);

        }

        protected override void OnStart(string[] args)
        {
            ProcessHelper.NoWindowStartProcess( webRtcPath);
            Log.Info("WebRTC服务已启动！");
            daemoProc.Start();
        }

        protected override void OnStop()
        {
            ProcessHelper.KillProcess(processName);
            Log.Info("WebRTC服务已停止！");
            daemoProc.Stop();
        }
    }
}
