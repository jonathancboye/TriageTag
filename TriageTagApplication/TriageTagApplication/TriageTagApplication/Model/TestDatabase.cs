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

            Random random = new Random();
            for ( int i = 0; i < 30; i++ ) {

                insertRandomUsers( connection );
            }
        }

        public void createUserTable( SQLiteConnection connection ) {
            connection.CreateTable<EncryptedUser>();

            /*User level is added
             normal user:= 1
             admin user:= 2
             admin should have the ability to add new users and other features.*/
            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( "1", "j", "j", "2" ) ) );

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

        void insertRandomUsers( SQLiteConnection connection ) {
            Random random = new Random();
            string fname = createRandomName(random);
            string lname = createRandomName(random);
            string eid = fname + lname + random.Next( 1, 1000 );

            connection.Insert(
                Database.encryptUser(
                    Database.createDecryptedUser( eid,
                    fname,
                    createRandomName( random ),
                    "1" ) ) );

            connection.Insert(
                Database.encryptEmployee(
                    Database.createDecryptedEmployee( eid,
                    fname,
                    lname,
                    createRandomName( random ),
                    randomPhonenumber(),
                    createRandomName( random ) ) ) );
        }

        string createRandomName( Random random ) {
            List<char> letters = new List<char>();
            int size = random.Next(5, 10);
            for ( int i = 0; i < size; i++ ) {
                letters.Add( System.Convert.ToChar( random.Next( 65, 90 ) ) );
            }

            return string.Join( "", letters.ToArray() );
        }

        string randomPhonenumber() {
            Random random = new Random();
            List<int> numbers = new List<int>();
            for ( int i = 0; i < 10; i++ ) {
                numbers.Add( random.Next( 0, 9 ) );
            }
            return string.Join( "", numbers.ToArray() );
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

            connection.Insert(
                Database.encryptEmployee(
                    Database.createDecryptedEmployee(
                        "2",
                        "Troy",
                        "Caplinger",
                        "123 NoWhere",
                        "222-555-4444",
                        "Dogg The Bounty Hunter" ) ) );
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


            connection.Insert(
                Database.encryptMedicalHistory(
                    Database.createDecryptedMedicalHistory(
                        "2",
                        "monkey poop",
                        "B+",
                        "Pluming",
                        "yes",
                        "Everything under the sun",
                        "Dr. Tommy Pickels" ) ) );
        }
    }
}
