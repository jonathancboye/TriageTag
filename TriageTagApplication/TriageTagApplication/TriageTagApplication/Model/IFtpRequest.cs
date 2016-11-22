using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriageTagApplication
{
    public interface IFtpRequest
    {
        void FtpRequest( string ftpUri, string username, string password);
    }
}
