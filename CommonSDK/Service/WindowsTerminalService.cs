using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonSDK.Util;

namespace CommonSDK.Service
{
    public class WindowsTerminalService : ITerminalService
    {
        private readonly Process _cureentProcess;

        public WindowsTerminalService()
        {
            if (!SystemUtil.IsWindowPlatform())
            {
                throw new PlatformNotSupportedException("The service only support windows platform!");
            }

            _cureentProcess = new();
            _cureentProcess.StartInfo.FileName = "cmd.exe";
            _cureentProcess.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            _cureentProcess.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            _cureentProcess.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            _cureentProcess.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            _cureentProcess.StartInfo.CreateNoWindow = true;//不显示程序窗口
            _cureentProcess.Start();//启动程序
        }

        public async Task<bool> ExecuteCommandAsync(string command)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(command);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(command);

            try
            {
                await _cureentProcess.StandardInput.WriteAsync(command);
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
