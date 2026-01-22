using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiBan
{
    class Log
    {
        public static bool write_log(string file_path, string message)
        {
            try
            {
                File.AppendAllText(file_path, message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
