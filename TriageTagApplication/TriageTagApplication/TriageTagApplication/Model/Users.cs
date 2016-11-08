using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TriageTagApplication
{
    class Users {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int employeeId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
