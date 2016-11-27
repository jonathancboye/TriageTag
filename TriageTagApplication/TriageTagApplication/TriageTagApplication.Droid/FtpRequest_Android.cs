using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TriageTagApplication.Droid;
using Xamarin.Forms;

[assembly: Dependency( typeof(FtpRequest_Android) )]
namespace TriageTagApplication.Droid
{
    class FtpRequest_Android : IFtpRequest
    {
        public FtpRequest_Android() { }

        public void FtpRequest( string ftpUri, string username, string password ) {
            
            // Start Ftp client
            FtpWebRequest webrequest = WebRequest.Create( ftpUri ) as FtpWebRequest;

            webrequest.UsePassive = true;

            // Set credientials
            webrequest.Credentials = new NetworkCredential( username, password);

            // Set the ftp protocol method
            webrequest.Method = WebRequestMethods.Ftp.DownloadFile;

            // Make request and get response
            FtpWebResponse response = webrequest.GetResponse() as FtpWebResponse;
            System.Diagnostics.Debug.WriteLine( "HERE");

            // Write response stream to file
            Stream stream = response.GetResponseStream();
            string sqliteFilename = "database.db3";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = Path.Combine( folderPath, sqliteFilename );

            using ( var fileStream = new FileStream( filePath, FileMode.Create, FileAccess.Write ) ) {
                stream.CopyTo( fileStream );
            }

            response.Close();
        }
    }
}