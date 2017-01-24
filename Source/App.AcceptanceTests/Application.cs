namespace App.AcceptanceTests
{
    using System;
    using System.Diagnostics;
    using System.IO;

    internal class Application
    {
        public static string Output;
        private static int _exitCode;
        private const string EXE = "App.exe";

        public static void Start(string arguments)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EXE);

            using (var process = new Process())
            {
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                Output = process.StandardOutput.ReadToEnd();

                var exited = process.WaitForExit(30000);
                if (exited)
                {
                    _exitCode = process.ExitCode;
                }
                else
                {
                    _exitCode = -1;
                }
            }
        }
    }
}
