using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.Util;

namespace CommonSDK.Service
{
    internal class WindowsTerminalService : ITerminalService
    {

        public WindowsTerminalService()
        {
            if (!SystemUtil.IsWindowPlatform())
            {
                throw new PlatformNotSupportedException("The service only support windows platform!");
            }
        }

        public async Task<bool> ExecuteCommandAsync(string command)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(command);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(command);

            try
            {
                using Process p = new();
                
                // 设置要启动的应用程序
                p.StartInfo.FileName = "cmd.exe";
                
                // 是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                
                // 输出信息
                p.StartInfo.RedirectStandardOutput = true;
                
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                
                // 不显示程序窗口
                p.StartInfo.CreateNoWindow = true;

                // 启动程序
                p.Start();

                // 向cmd窗口发送输入信息
                p.StandardInput.WriteLine(command);

                // 自动推送
                p.StandardInput.AutoFlush = true;

                //获取输出信息
                //string strOuput = await p.StandardOutput.ReadToEndAsync();
                
                //等待程序执行完退出进程
                //p.WaitForExit();
                
                //Debug.WriteLine(strOuput);
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}
