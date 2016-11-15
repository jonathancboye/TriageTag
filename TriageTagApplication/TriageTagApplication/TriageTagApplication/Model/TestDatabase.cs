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

            /*User level is added
             normal user:= 1
             admin user:= 2
             admin should have the ability to add new users and other features.*/
            connection.Insert( new Users {
                employeeId = 1,
<<<<<<< HEAD:TriageTagApplication/TriageTagApplication/TriageTagApplication/TestDatabase.cs
                username = "Jonathan",
                password = "Carpenter",
                userLvl = 2
=======
                username = "a",
                password = "a"
>>>>>>> 3d3736bee418a4b8e9104833d34b98920e77ccc4:TriageTagApplication/TriageTagApplication/TriageTagApplication/Model/TestDatabase.cs
            } );

            connection.Insert( new Users {
                employeeId = 2,
                username = "Troy",
                password = "Caplinger",
                userLvl = 1
            } );

            connection.Insert( new Users {
                employeeId = 3,
                username = "Anthony",
                password = "Inman",
                userLvl = 2
            } );

            connection.Insert( new Users {
                employeeId = 4,
                username = "Vincent",
                password = "Haenni",
                userLvl = 1
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
<<<<<<< HEAD:TriageTagApplication/TriageTagApplication/TriageTagApplication/TestDatabase.cs
                employeeId = 2,
                Allergies = "cats, dogs, horses, dust",
=======
                employeeId = 1,
                allergies = "cats, dogs, horses, dust",
>>>>>>> 3d3736bee418a4b8e9104833d34b98920e77ccc4:TriageTagApplication/TriageTagApplication/TriageTagApplication/Model/TestDatabase.cs
                bloodType = "O",
                religion = "Atheist",
                highBloodPressure = "no",
                medications = "inhaler",
                primaryDoctor = "Dr. Randel"
            } );
        }
    }
}
