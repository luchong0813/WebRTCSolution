using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRTCStart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!ServiceHelper.ExistedService() && ServiceHelper.ConfigService(true))
            {
                ServiceHelper.StartService();
                Log.Info("安装服务完成并自动运行成功！");
            }
        }
    }
}
