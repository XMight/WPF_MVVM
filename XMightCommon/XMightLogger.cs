using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMightCommon
{
    public class XMightLogger
    {
        private ILog _logger;

        public XMightLogger(ILog pLogger)
        {
            _logger = pLogger;
        }
    }
}
