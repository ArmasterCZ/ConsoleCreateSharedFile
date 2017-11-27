using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;

// V0:00:07
// program for create folders for offline files with share rights

namespace ConsoleCreateSharedFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //načtení informací z config
            //string test1 = ReadSetting("Setting1");

            //spuštění zvuku
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav");
            //player.Play();
            //player.Stop();

            //sestavení stringu
            //string test = String.Format("{0} necco neco {1}", "-1-", "-2-");

            string PcName = string.Empty;
            string userName = string.Empty;
            string department = string.Empty;
            string serverName = string.Empty;

            PcName = "D15141";
            userName = "jhofman";
            department = "14000";
            serverName = "files";

            string fileShareName = department + "_" + PcName;
            //outside server
            string fileSharePath = "\\\\" + serverName + "\\" + fileShareName;
            string fileSharePathFull = "\\\\" + serverName + "\\D$\\"+department+"\\backup_ntb\\" + fileShareName;
            //in server
            string fileServerPathFull = "D:\\" + department + "\\backup_ntb\\" + fileShareName;

            Console.WriteLine(fileSharePathFull);
            Console.WriteLine(fileServerPathFull);

            string path = "C:\\složka";
            fileNew(path);
            FileSystemRights right = new FileSystemRights();
            AccessControlType access = new AccessControlType();
            
            AddDirectorySecurity("složka", "jvaldaf", right, access);
            Console.ReadLine();
        }

        //read string from .config
        static string readSetting(string key)
        {
            //string test1 = ConfigurationManager.AppSettings["Setting1"]; //one line simple load

            string result = null;
            try
            {
                result = ConfigurationManager.AppSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return result ?? "";
        }

        // Create File
        public static void fileNew(string path)
        {
            //check if folder exist and create new
            System.IO.Directory.CreateDirectory(path);

            /*try
            {
                string DirectoryName = "TestDirectory";

                Console.WriteLine("Adding access control entry for " + DirectoryName);

                // Add the access control entry to the directory.
                AddDirectorySecurity(DirectoryName, @"MYDOMAIN\MyAccount", FileSystemRights.ReadData, AccessControlType.Allow);

                Console.WriteLine("Removing access control entry from " + DirectoryName);

                // Remove the access control entry from the directory.
                RemoveDirectorySecurity(DirectoryName, @"MYDOMAIN\MyAccount", FileSystemRights.ReadData, AccessControlType.Allow);

                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }*/
        }

        public static void removeDirectoryRight()
        {

        }

        // check // Adds an ACL entry on the specified directory for the specified account.
        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        // check // Removes an ACL entry on the specified directory for the specified account.
        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }


    }
}
