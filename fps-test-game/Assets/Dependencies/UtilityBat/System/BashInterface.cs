
using System;
using System.Diagnostics;

public static class BashInterface {

    public static void RunCommand (string command) {

        Process bashProcess = new Process();
        bashProcess.StartInfo.RedirectStandardOutput = true;
        bashProcess.StartInfo.UseShellExecute = false;
        bashProcess.StartInfo.CreateNoWindow = true;
        bashProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        bashProcess.StartInfo.FileName = "/bin/bash";
        bashProcess.StartInfo.Arguments = "-c \"" + command + "\"";
        bashProcess.Start();
        bashProcess.WaitForExit();
    }
}
