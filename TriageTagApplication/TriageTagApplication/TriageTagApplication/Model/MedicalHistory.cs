using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace TriageTagApplication
{
    class MedicalHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public byte[] employeeId { get; set; }
        public byte[] allergies { get; set; }
        public byte[] bloodType { get; set; }
        public byte[] religion { get; set; }
        public byte[] highBloodPressure { get; set; }
        public byte[] medications { get; set; }
        public byte[] primaryDoctor { get; set; }
    }
}
