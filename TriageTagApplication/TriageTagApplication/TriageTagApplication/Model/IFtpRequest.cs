﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriageTagApplication
{
    public interface IFtpRequest
    {
        Task<bool> FtpRequest( string ftpUri, string filename, string username, string password);
    }
}
