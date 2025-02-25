using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TEngine.Editor
{
    public static class LubanTools
    {
        public static string foldPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../Configs/GameConfig"));

        [MenuItem("TEngine/Tools/Luban 转表")]
        public static void BuildLubanExcel()
        {

            // 根据操作系统设置启动参数
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                string scriptPath = Path.Combine(foldPath, "gen_code_bin_to_project_lazyload.bat");
                Application.OpenURL(scriptPath);
            }
            else // macOS/Linux
            {
                string scriptPath = Path.Combine(foldPath, "gen_code_bin_to_project_lazyload.sh");
                ShellExecutor.ExecuteShellScript(scriptPath);
            }
        }

        [MenuItem("TEngine/Tools/打开表格目录")]
        public static void OpenConfigFolder()
        {
            OpenFolderHelper.Execute(foldPath);
        }
    }

    public class ShellExecutor
    {
        public static void ExecuteShellScript(string scriptPath)
        {
            // 检查脚本是否存在
            if (!File.Exists(scriptPath))
            {
                throw new FileNotFoundException($"脚本文件不存在: {scriptPath}");
            }

            // 配置进程参数
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash", // macOS 默认使用 bash
                Arguments = $"\"{scriptPath}\"", // 传递脚本路径
                UseShellExecute = false, // 不使用系统 Shell
                RedirectStandardOutput = true, // 捕获输出
                RedirectStandardError = true, // 捕获错误
                CreateNoWindow = true, // 不创建窗口
                StandardOutputEncoding = System.Text.Encoding.UTF8, // 统一使用 UTF-8
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            // 启动进程
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                // 异步读取输出和错误流（避免死锁）
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit(); // 等待执行完成

                // 输出结果
                if (!string.IsNullOrEmpty(output))
                    Log.Info($"shell脚本输出:\n{output}");

                if (!string.IsNullOrEmpty(error))
                    Log.Error($"错误:\n{error}");
            }
        }
    }
}
