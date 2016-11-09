using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace TriageTagApplication
{
    class TestDatabase
    {
        public TestDatabase(SQLiteConnection connection ) {
            createUserTable( connection );
            createEmployeeTable( connection );
            createMedicalHistoryTable( connection );
        }

        public void createUserTable( SQLiteConnection connection ) {
            connection.CreateTable<Users>();

            connection.Insert( new Users {
                employeeId = 1,
                username = "a",
                password = "a"
            } );

            connection.Insert( new Users {
                employeeId = 2,
                username = "Troy",
                password = "Caplinger"
            } );

            connection.Insert( new Users {
                employeeId = 3,
                username = "Anthony",
                password = "Inman"
            } );

            connection.Insert( new Users {
                employeeId = 4,
                username = "Vincent",
                password = "Haenni"
            } );
        }

        public void createEmployeeTable( SQLiteConnection connection ) {
            connection.CreateTable<Employee>();

            connection.Insert( new Employee {
                employeeId = 1,
                firstname = "Jonathan",
                lastname = "Carpenter",
                address = "880 West Alkaline Springs rd",
                phonenumber = "937-371-3348",
                emergencyContact = "Mr. Robot Chicken"
            } );

        }

        public void createMedicalHistoryTable(SQLiteConnection connection ) {
            connection.CreateTable<MedicalHistory>();

            connection.Insert( new MedicalHistory {
                employeeId = 1,
                allergies = "cats, dogs, horses, dust",
                bloodType = "O",
                religion = "Atheist",
                highBloodPressure = false,
                medications = "inhaler",
                primaryDoctor = "Dr. Randel"
            } );
        }
    }
}
