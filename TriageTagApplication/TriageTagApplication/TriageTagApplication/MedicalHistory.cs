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
        public int employeeId { get; set; }
        public string allergies { get; set; }
        public string bloodType { get; set; }
        public string religion { get; set; }
        public bool highBloodPressure { get; set; }
        public string medications { get; set; }
        public string primaryDoctor { get; set; }
    }
}
