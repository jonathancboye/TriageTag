using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite.Net;

namespace TriageTagApplication
{
    public interface ISQLite
    {
        // Returns: SQLiteConnection from a database file
        Task<SQLiteConnection> getConnection(string filename);

        // Copy file to Ftp Server
        Task copyFileToFtpServer(string filename);
    }
}
