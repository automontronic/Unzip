using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace automontronic.unzip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to unzip:");
            string path = Console.ReadLine();

            if (File.Exists(path))
            {
                // This path is a file
                ProcessFile(path);
            }
            else if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
            Console.WriteLine("Unzip Complete");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        // Process all files in the directory passed in, recurse on any directories  
        // that are found, and process the files they contain. 
        public static void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory. 
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory. 
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // process the file  
        public static void ProcessFile(string path)
        {
            try
            {
                if (path.EndsWith(".zip"))
                {
                    string zipPath = path.Replace(".zip", "");
                    ZipFile.ExtractToDirectory(path, zipPath);
                    Console.WriteLine("Extracted file '{0}'.", path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
