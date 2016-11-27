using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TriageTagApplication
{
    class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public byte[] employeeId { get; set; }
        public byte[] firstname { get; set; }
        public byte[] lastname { get; set; }
        public byte[] address { get; set; }
        public byte[] phonenumber { get; set; }
        public byte[] emergencyContact { get; set; }
    }
}
