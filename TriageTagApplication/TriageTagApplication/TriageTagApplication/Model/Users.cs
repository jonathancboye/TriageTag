﻿using System;
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
        public byte[] employeeId { get; set; }
        public byte[] username { get; set; }
        public byte[] password { get; set; }
        public byte[] userLvl { get; set; }
    }
}
