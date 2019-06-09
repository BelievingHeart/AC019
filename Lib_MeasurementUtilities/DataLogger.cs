using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Cognex.VisionPro.ToolBlock;

namespace Lib_MeasurementUtilities
{
    public class DataLogger
    {
        private List<string> argNames;
        private string title;
        private string _logDir;

        public DataLogger(List<string> argNames, string logDir)
        {
            this.argNames = argNames;
            this._logDir = logDir;
            title = string.Join(",", this.argNames);

        }

        public string WriteLine(string line)
        {
            if(!Directory.Exists(_logDir)) Directory.CreateDirectory(_logDir);
            string logFile = _logDir + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (!File.Exists(logFile))
            {
                using (var ss = new StreamWriter(logFile, true))
                {
                    ss.WriteLine(title);
                    ss.WriteLine(line);
                }
            }
            else
            {
                using (var ss = new StreamWriter(logFile, true))
                {
                    ss.WriteLine(line);
                }
            }

            return logFile;
        }

        public string WriteLine(CogToolBlock block, string result, params string[] outputNames)
        {
            var values = outputNames.Select(a => ((double) block.Outputs[a].Value).ToString("f3")).ToList();
            values.Insert(0, DateTime.Now.ToString("HH:mm:ss"));
            values.Add(result);

            var line = string.Join(",", values);

            return WriteLine(line);
        }

        public void CleanOutdatedFiles()
        {
            string[] files = Directory.GetFiles(_logDir);
            foreach (var f in files)
            {
                DateTime dt = File.GetCreationTime(f);
                TimeSpan ts = DateTime.Now - dt;
                if (ts.Days > 30)
                {
                    File.Delete(f);
                }
            }
        }
    }
}
