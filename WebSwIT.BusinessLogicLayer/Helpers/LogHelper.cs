using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace WebSwIT.BusinessLogicLayer.Helpers
{
    public class LogHelper
    {
        public static void WriteLogToFile(string comment)
        {
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logger.txt"));
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            FileInfo fInfo = new FileInfo(filePath);
            FileSecurity fSecurity = fInfo.GetAccessControl();

            fSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.ReadData, AccessControlType.Allow));
            fInfo.SetAccessControl(fSecurity);

            using (StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(comment);
                sw.Close();
            }

        }
    }
}
