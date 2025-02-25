using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace TEngine.Editor
{
    public static class LubanTools
    {
        [MenuItem("TEngine/Tools/Luban 转表")]
        public static void BuildLubanExcel()
        {
            string scriptPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../Configs/GameConfig/gen_code_bin_to_project_lazyload.sh"));
        
        Process process = new Process();
        
        // 根据操作系统设置启动参数
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/C \"{scriptPath}\"";
        }
        else // macOS/Linux
        {
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"\"{scriptPath}\"";
        }

        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        
        process.Start();
        
        // 捕获输出（可选）
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        
        process.WaitForExit();

        Log.Info(output);
        
        if (!string.IsNullOrEmpty(error))
            Log.Error(error);

            // Application.OpenURL(Application.dataPath + @"/../../Configs/GameConfig/gen_code_bin_to_project_lazyload.sh");
        }
        
        [MenuItem("TEngine/Tools/打开表格目录")]
        public static void OpenConfigFolder()
        {
            OpenFolderHelper.Execute(Application.dataPath + @"/../../Configs/GameConfig");
        }
    }
}