using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Lib_MeasurementUtilities
{
    /// <summary>
    /// A utility class to log unhandled exceptions
    /// </summary>
    public class UnhandledExceptionLogger
    {
        /// <summary>
        /// Log the unhandled exception into a file
        /// </summary>
        /// <param name="logFile"> Output file to be logged. A new one will be created if not presented</param>
        /// <param name="exception">Exception to be logged</param>
        /// <param name="promptUser">Whether to notify the user the exception is logged</param>
        /// <param name="lineNumber">The line number where this method is called</param>
        /// <param name="filePath">The file where this method is called</param>
        public static void Log(string logFile, Exception exception, bool promptUser, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null)
        {

            using (StreamWriter sw = File.Exists(logFile) ? File.AppendText(logFile) : File.CreateText(logFile))
            {
                sw.WriteLine(DateTime.Now.ToString("MM-dd hh:mm:ss"));
                sw.WriteLine("Catch point: at " + filePath + ", line: " + lineNumber);
                sw.WriteLine("Exception name:\n " + exception.GetType().FullName);
                sw.WriteLine("Exception message:\n " + exception.Message);
                sw.WriteLine("Stack trace:\n" + exception.StackTrace + "\n\n");
            }

            if (promptUser)
            {
                MessageBox.Show("已保存异常信息。请重启软件并联系软件维护人员。", "未处理的异常", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}