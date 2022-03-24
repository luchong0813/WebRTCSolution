using Microsoft.Win32;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebRTCStart
{
    /// <summary>
    /// 服务控制帮助类
    /// </summary>
    public class ServiceHelper
    {
        private static string serviceName = "WebRTCService";
        /// <summary>
        /// 服务是否存在
        /// </summary>
        /// <returns></returns>
        public static bool ExistedService()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName == serviceName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public static bool StartService()
        {
            try
            {
                if (ExistedService())
                {
                    ServiceController service = new ServiceController(serviceName);
                    if (service.Status == ServiceControllerStatus.Running ||
                        service.Status == ServiceControllerStatus.StartPending)
                    {
                        return true;
                    }
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Info($"启动服务异常：{ex}");
            }
            return false;
        }

        /// <summary>
        /// 安装或删除服务
        /// </summary>
        /// <param name="install"></param>
        public static bool ConfigService(bool install)
        {
            try
            {
                TransactedInstaller ti = new TransactedInstaller();
                ti.Installers.Add(new ServiceProcessInstaller
                {
                    Account = ServiceAccount.LocalSystem
                });
                ti.Installers.Add(new ServiceInstaller
                {
                    DisplayName = serviceName,
                    ServiceName = serviceName,
                    Description = "用于控制和管理WebRTC视频解码服务",
                    StartType = ServiceStartMode.Automatic//运行方式
                });
                ti.Context = new InstallContext();
                string currPath = Assembly.GetEntryAssembly().Location;
                string servicePath = Path.Combine(Path.GetDirectoryName(currPath), serviceName);
                ti.Context.Parameters["assemblypath"] = servicePath;
                if (install)
                {
                    ti.Install(new Hashtable());
                }
                else
                {
                    ti.Uninstall(null);
                }
                return true;
            }
            catch (Exception ex)
            {
                var formart = install ? "安装" : "删除";
                Log.Info($"{formart}服务异常：{ex}");
            }
            return false;
        }
    }
}
