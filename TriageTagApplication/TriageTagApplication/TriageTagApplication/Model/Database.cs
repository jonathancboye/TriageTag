using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLite.Net;

namespace TriageTagApplication
{
    class EncryptedUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public byte[] employeeId { get; set; }
        public byte[] username { get; set; }
        public byte[] password { get; set; }
        public byte[] userLvl { get; set; }
    }

    class DecryptedUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string employeeId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string userLvl { get; set; }
    }

    class EncryptedEmployee
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

    class DecryptedEmployee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string employeeId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
        public string emergencyContact { get; set; }
    }

    class EncryptedMedicalHistory
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

    class DecryptedMedicalHistory
    {
        public string employeeId { get; set; }
        public string allergies { get; set; }
        public string bloodType { get; set; }
        public string religion { get; set; }
        public string highBloodPressure { get; set; }
        public string medications { get; set; }
        public string primaryDoctor { get; set; }
    }

    class Database
    {
        static public DecryptedMedicalHistory getMedicalHistory( string emId ) {
            byte[] encrypted_emId = Crypto.EncryptAes(emId, App.pkey, App.salt);
            List<EncryptedMedicalHistory> mhistorys = App.dbConnection.Query<EncryptedMedicalHistory>( "SELECT * FROM EncryptedMedicalHistory WHERE employeeId=?", encrypted_emId );
            if ( mhistorys.Count != 1 ) {
                return null;
            } else {
                EncryptedMedicalHistory mhistory = mhistorys[0];
                return decryptMedicalHistory( mhistory );
            }
        }

        static public DecryptedUser getUser( string username, string password ) {
            byte[] uName = Crypto.EncryptAes(username, App.pkey,App.salt);
            byte[] pValue = Crypto.EncryptAes(password, App.pkey, App.salt);
            List<EncryptedUser> users = App.dbConnection.Query<EncryptedUser>("SELECT * FROM EncryptedUser WHERE username=? AND password=?", uName, pValue);
            if ( users.Count != 1 ) {
                return null;
            } else {
                EncryptedUser user = users[0];
                return decryptUser( user );
            }
        }

        static public bool isUsernameTaken( string username ) {
            List<EncryptedUser> users = App.dbConnection.Query<EncryptedUser>("SELECT username FROM EncryptedUser");

            foreach ( EncryptedUser name in users ) {
                string decryptedUsername = Crypto.DecryptAes( name.username, App.pkey, App.salt );
                if ( decryptedUsername == username ) {
                    return false;
                }
            }
            return true;
        }

        static public void updateMedicalHistory( DecryptedMedicalHistory mhistory ) {
            EncryptedMedicalHistory eh = encryptMedicalHistory( mhistory );
            App.dbConnection.Query<EncryptedMedicalHistory>( "UPDATE EncryptedMedicalHistory "+
                                                            "SET " + 
                                                            "allergies=?," +
                                                            "bloodType=?," +
                                                            "religion=?," +
                                                            "highBloodPressure=?," +
                                                            "medications=?," +
                                                            "primaryDoctor=? " +
                                                            "WHERE employeeId=?",
                                                            eh.allergies,
                                                            eh.bloodType,
                                                            eh.religion,
                                                            eh.highBloodPressure,
                                                            eh.medications,
                                                            eh.primaryDoctor,
                                                            eh.employeeId );
        }

        static public DecryptedMedicalHistory createDecryptedMedicalHistory(
            string employeeId,
            string allergies,
            string bloodType,
            string religion,
            string highBloodPressure,
            string medications,
            string primaryDoctor ) {

            return new DecryptedMedicalHistory {
                employeeId = employeeId,
                allergies = allergies,
                bloodType = bloodType,
                religion = religion,
                highBloodPressure = highBloodPressure,
                medications = medications,
                primaryDoctor = primaryDoctor
            };
        }

        static public DecryptedEmployee createDecryptedEmployee(
            string employeeId,
            string firstname,
            string lastname,
            string address,
            string phonenumber,
            string emergencyContact ) {

            return new DecryptedEmployee {
                employeeId = employeeId,
                firstname = firstname,
                lastname = lastname,
                address = address,
                phonenumber = phonenumber,
                emergencyContact = emergencyContact,
            };
        }

        static public DecryptedUser createDecryptedUser(
           string employeeId,
           string username,
           string password,
           string userLvl ) {

            return new DecryptedUser {
                employeeId = employeeId,
                username = username,
                password = password,
                userLvl = userLvl,
            };
        }

        static public DecryptedMedicalHistory decryptMedicalHistory( EncryptedMedicalHistory mhistory ) {
            return new DecryptedMedicalHistory {
                employeeId = Crypto.DecryptAes( mhistory.employeeId, App.pkey, App.salt ),
                allergies = Crypto.DecryptAes( mhistory.allergies, App.pkey, App.salt ),
                bloodType = Crypto.DecryptAes( mhistory.bloodType, App.pkey, App.salt ),
                religion = Crypto.DecryptAes( mhistory.religion, App.pkey, App.salt ),
                highBloodPressure = Crypto.DecryptAes( mhistory.highBloodPressure, App.pkey, App.salt ),
                medications = Crypto.DecryptAes( mhistory.medications, App.pkey, App.salt ),
                primaryDoctor = Crypto.DecryptAes( mhistory.primaryDoctor, App.pkey, App.salt )
            };
        }

        static public EncryptedMedicalHistory encryptMedicalHistory( DecryptedMedicalHistory mhistory ) {
            return new EncryptedMedicalHistory {
                employeeId = Crypto.EncryptAes( mhistory.employeeId, App.pkey, App.salt ),
                allergies = Crypto.EncryptAes( mhistory.allergies, App.pkey, App.salt ),
                bloodType = Crypto.EncryptAes( mhistory.bloodType, App.pkey, App.salt ),
                religion = Crypto.EncryptAes( mhistory.religion, App.pkey, App.salt ),
                highBloodPressure = Crypto.EncryptAes( mhistory.highBloodPressure, App.pkey, App.salt ),
                medications = Crypto.EncryptAes( mhistory.medications, App.pkey, App.salt ),
                primaryDoctor = Crypto.EncryptAes( mhistory.primaryDoctor, App.pkey, App.salt )
            };
        }
        static public EncryptedUser encryptUser( DecryptedUser user ) {
            return new EncryptedUser {
                employeeId = Crypto.EncryptAes( user.employeeId, App.pkey, App.salt ),
                username = Crypto.EncryptAes( user.username, App.pkey, App.salt ),
                password = Crypto.EncryptAes( user.password, App.pkey, App.salt ),
                userLvl = Crypto.EncryptAes( user.userLvl, App.pkey, App.salt ),
            };
        }

        static public DecryptedUser decryptUser( EncryptedUser user ) {
            return new DecryptedUser {
                employeeId = Crypto.DecryptAes( user.employeeId, App.pkey, App.salt ),
                username = Crypto.DecryptAes( user.username, App.pkey, App.salt ),
                password = Crypto.DecryptAes( user.password, App.pkey, App.salt ),
                userLvl = Crypto.DecryptAes( user.userLvl, App.pkey, App.salt ),
            };
        }

        static public EncryptedEmployee encryptEmployee( DecryptedEmployee employee ) {
            return new EncryptedEmployee {
                employeeId = Crypto.EncryptAes( employee.employeeId, App.pkey, App.salt ),
                firstname = Crypto.EncryptAes( employee.firstname, App.pkey, App.salt ),
                lastname = Crypto.EncryptAes( employee.lastname, App.pkey, App.salt ),
                address = Crypto.EncryptAes( employee.address, App.pkey, App.salt ),
                phonenumber = Crypto.EncryptAes( employee.phonenumber, App.pkey, App.salt ),
                emergencyContact = Crypto.EncryptAes( employee.emergencyContact, App.pkey, App.salt )
            };
        }

        static public DecryptedEmployee decryptEmployee( EncryptedEmployee employee ) {
            return new DecryptedEmployee {
                employeeId = Crypto.DecryptAes( employee.employeeId, App.pkey, App.salt ),
                firstname = Crypto.DecryptAes( employee.firstname, App.pkey, App.salt ),
                lastname = Crypto.DecryptAes( employee.lastname, App.pkey, App.salt ),
                address = Crypto.DecryptAes( employee.address, App.pkey, App.salt ),
                phonenumber = Crypto.DecryptAes( employee.phonenumber, App.pkey, App.salt ),
                emergencyContact = Crypto.DecryptAes( employee.emergencyContact, App.pkey, App.salt )
            };
        }
    }
}
