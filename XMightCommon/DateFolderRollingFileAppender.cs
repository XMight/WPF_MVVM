using log4net.Appender;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMightCommon
{
    public class DateFolderRollingFileAppender : RollingFileAppender
    {
        protected override void OpenFile(string pFileName, bool pAppend)
        {
            String baseDirectory = Path.GetDirectoryName(pFileName);
            String fileNameOnly = Path.GetFileName(pFileName);
            String newDirectory = Path.Combine(baseDirectory, DateTime.Now.ToString("dd-MM-yyyy"));
            String folderInjectedFileName = Path.Combine(newDirectory, fileNameOnly);

            base.OpenFile(folderInjectedFileName, pAppend);
        }
    }
}
