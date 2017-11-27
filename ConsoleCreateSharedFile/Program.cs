using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.AccessControl;
//using Microsoft.HomeServer.SDK.Interop.v1;

// V0:00:10
// program for create folders for offline files with share rights

namespace ConsoleCreateSharedFile
{
    class Program
    {
        static void Main(string[] args)
        {
            //načtení informací z config
            //string test1 = ReadSetting("Setting1");

            //nová složka
            string path = "C:\\Nová_složka\\složka na test sdílení";
            fileNew(path);

            //oprávnění
            /*
            Console.WriteLine(path);
            Console.WriteLine("");
            RemoveDirectorySecurity(path, @"Authenticated Users", FileSystemRights.ReadData, AccessControlType.Allow);
            AddDirectorySecurity(path, @"sitel\Domain Admins", FileSystemRights.ReadData, AccessControlType.Allow);
            AddDirectorySecurity(path, @"sitel\ftester", FileSystemRights.ReadData, AccessControlType.Allow);
            ReadDirectoryRight(path);
            */

            //sdílení
            WHSInfoClass pInfo = new WHSInfoClass();


            Console.ReadLine();
        }


        public static void playSound()
        {
            //spuštění zvuku
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav");

            string pathWAV = Directory.GetCurrentDirectory() + "\\resources\\houk.wav";
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(pathWAV);
            player.Play();
            Console.WriteLine("houkám");
            Console.ReadLine();
            player.Stop();
            Console.WriteLine("nehoukám");
        }

        // create paths for offlineFiles
        public static void folderNames()
        {
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
            string fileSharePathFull = "\\\\" + serverName + "\\D$\\" + department + "\\backup_ntb\\" + fileShareName;
            //in server
            string fileServerPathFull = "D:\\" + department + "\\backup_ntb\\" + fileShareName;

            Console.WriteLine(fileSharePathFull);
            Console.WriteLine(fileServerPathFull);
        }

        // read string from .config
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

        #region zabezpeceni

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

        // read Directory right
        public static void ReadDirectoryRight(string FileName)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            AuthorizationRuleCollection rules = dSecurity.GetAccessRules(true, false, typeof(System.Security.Principal.NTAccount));
            foreach (FileSystemAccessRule rule in rules)
            {
                Console.WriteLine(rule.IdentityReference);
                Console.WriteLine("     " + rule.AccessControlType);
            }
        }

        #endregion zabezpeceni

        public static void shareFolder(string fileName)
        {
            //I already use AccessControl ...
            DirectoryInfo dInfo = new DirectoryInfo(fileName);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

            ////... but it allow me to set "Security permission" not "Shared permission".
            ////To activate sharing in the directory I use WMI
            //ManagementClass mc = new ManagementClass("win32_share");
            //ManagementBaseObject inParams = mc.GetMethodParameters("Create");
            //inParams("Description") = "My Shared Folder";
            //inParams("Name") = "Shared Folder Name";
            //inParams("Path") = "C:\\Folder1";
            //inParams("Type") = ShareResourceType.DiskDrive;
            //inParams("MaximumAllowed") = null;
            //inParams("Password") = null;
            //inParams("Access") = null; // Make Everyone has full control access.
            //ManagementBaseObject outParams = classObj.InvokeMethod("Create", inParams, null);

        }

    }
}


//https://www.softcom.cz/eshop/tp-link-tl-wn722n-bezdratovy-lite-n-usb-klient-802-11n-ext-antena_d85859.html

