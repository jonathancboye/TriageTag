using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace TriageTagApplication
{
    public class TestDatabase
    {
        public TestDatabase( SQLiteConnection connection ) {
            createUserTable( connection );
            createEmployeeTable( connection );
            createMedicalHistoryTable( connection );
        }

        public void createUserTable( SQLiteConnection connection ) {
            connection.CreateTable<EncryptedUser>();

            /*User level is added
             normal user:= 1
             admin user:= 2
             admin should have the ability to add new users and other features.*/
            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( "1", "Jonathan", "Carpenter", "2" ) ) );

            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( "2", "Troy", "Caplinger", "1" ) ) );

            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( "3", "Anthony", "Inman", "2" ) ) );

            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( "4", "Vincent", "Haenni", "1" ) ) );
        }

        public void createEmployeeTable( SQLiteConnection connection ) {
            connection.CreateTable<EncryptedEmployee>();
            connection.Insert(
                Database.encryptEmployee(
                    Database.createDecryptedEmployee(
                    "1",
                    "Jonathan",
                    "Carpenter",
                    "880 West Alkaline Springs rd",
                    "937-371-3348",
                    "Mr. Robot Chicken" ) ) );
        }


        public void createMedicalHistoryTable( SQLiteConnection connection ) {
            connection.CreateTable<EncryptedMedicalHistory>();
            connection.Insert(
                Database.encryptMedicalHistory(
                    Database.createDecryptedMedicalHistory(
                        "1", 
                        "cats, dogs, horses, dust", 
                        "O", 
                        "Atheist", 
                        "no",
                        "inhaler", 
                        "Dr. Randel" ) ) );
        }
    }
}
