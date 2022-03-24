using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebRTCStart
{
    /// <summary>
    /// 日志
    /// </summary>
    public class Log
    {
        //读写锁，当资源处于写入模式时，其他线程写入需要等待本次写入结束之后才能继续写入
        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        //日志文件路径
        public static string logPath = "logs\\client.txt";

        static Log()
        {
            //创建文件夹
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
        }

        /// <summary>
        /// 写入日志.
        /// </summary>
        public static void Info(string msg)
        {
            try
            {
                LogWriteLock.EnterWriteLock();
                using (FileStream stream = new FileStream(logPath, FileMode.Append))
                {
                    StreamWriter write = new StreamWriter(stream);
                    string content = String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
                    write.WriteLine(content);
                    //关闭并销毁流写入文件
                    write.Close();
                    write.Dispose();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
    }
}
