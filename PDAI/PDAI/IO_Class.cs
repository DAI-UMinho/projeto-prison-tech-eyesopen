using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace PDAI
{
    static class IO_Class
    {

        public static bool CreateFolder(string folderName)
        {
            string path = @"" + folderName;

            try
            {
                if (!(Directory.Exists(path)))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }


        public static void DeleteFolder(string pathName)
        {
          string path = @"" + pathName;
          try
            {
                if (Directory.Exists(path))
                {
                     Directory.Delete(path,true);
                }
            }
            catch (Exception) { };

        }



        public static void CopyFile(string sourcePath, string targetPath)
        {
            try
            {
                if (System.IO.File.Exists(sourcePath))
                {
                    string fileName = System.IO.Path.GetFileName(sourcePath);
                    string sourceFile = sourcePath;
                    string destFile = System.IO.Path.Combine(targetPath, fileName);

                    System.IO.File.Copy(@""+sourceFile, @"" + destFile, true);
                }
            }
            catch (Exception) { /*System.Windows.Forms.MessageBox.Show(">> "+ error);*/ }
        }



        public static string FindFile(string sourcePath)
        {
            string path = "";
            try
            {
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(sourcePath);
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.jpg");
                if(filesInDir.Count()==0) filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.jpeg");
                if (filesInDir.Count() == 0) filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.gif");
                if (filesInDir.Count() == 0) filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.bmp");
                if (filesInDir.Count() == 0) filesInDir = hdDirectoryInWhichToSearch.GetFiles("*.png");

                foreach (FileInfo foundFile in filesInDir)
                {
                    path = foundFile.FullName;
                }
            }
            catch (Exception) { /*System.Windows.Forms.MessageBox.Show(">> "+ error);*/ }
            return path;
        }


    }
}
