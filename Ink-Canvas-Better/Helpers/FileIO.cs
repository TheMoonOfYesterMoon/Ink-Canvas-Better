using Ink_Canvas_Better.Resources;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ink_Canvas_Better.Helpers
{
    internal class FileIO
    {
        public static void DeleteOlderFiles(string directoryPath, int daysThreshold)
        {
            string[] extensionsToDel = { ".icstk", ".icart", ".png" };
            var thresholdDate = DateTime.Now.AddDays(-daysThreshold);
            if (Directory.Exists(directoryPath))
            {
                string[] subDirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.AllDirectories);
                foreach (string subDirectory in subDirectories)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(subDirectory);
                        foreach (string filePath in files)
                        {
                            DateTime creationDate = File.GetCreationTime(filePath);
                            string fileExtension = Path.GetExtension(filePath);
                            if (creationDate < thresholdDate)
                            {
                                bool b1 = Array.Exists(extensionsToDel, ext => ext.Equals(fileExtension, StringComparison.OrdinalIgnoreCase));
                                bool b2 = Path.GetFileNameWithoutExtension(filePath).Equals("PPTPosition", StringComparison.OrdinalIgnoreCase);
                                if (b1 || b2)
                                {
                                    File.Delete(filePath);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.WriteLogToFile(e.ToString(), Log.LogType.Error);
                    }
                }

                try
                {
                    DeleteEmptyFolders(directoryPath);
                }
                catch (Exception e)
                {
                    Log.WriteLogToFile(e.ToString(), Log.LogType.Error);
                }
            }
        }

        private static void DeleteEmptyFolders(string directoryPath)
        {
            foreach (string dir in Directory.GetDirectories(directoryPath))
            {
                DeleteEmptyFolders(dir);
                if (Directory.GetFiles(dir).Length == 0 && Directory.GetDirectories(dir).Length == 0)
                {
                    Directory.Delete(dir, false);
                }
            }
        }

        public static void SaveStrokes(String directoryPath, String format = "icbetterstk")
        {
            switch (format.ToLower())
            {
                case "icbpkg":
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    String currentTime = DateTime.Now.ToString("yyyy/MM/dd HH-mm-ss-fff");
                    String fileName = $"{currentTime}.icbpkg";
                    String tempStkFile = "tempStrokes.icstk";
                    String tempMetaFile = "tempMetadata.json";

                    // Create a temporary strokes file
                    FileStream stkFileStream = new FileStream(Path.Combine(directoryPath, tempStkFile), FileMode.Create);
                    RuntimeData.mainWindow.inkCanvas.Strokes.Save(stkFileStream);
                    stkFileStream.Close();

                    // Create a temporary metadata file
                    StreamWriter metaStreamWriter = new StreamWriter(Path.Combine(directoryPath, tempMetaFile), false);
                    String json = JsonConvert.SerializeObject(RuntimeData.currentMetadata, Formatting.Indented);
                    metaStreamWriter.Write(json);
                    metaStreamWriter.Close();

                    // Create a zip package
                    using (FileStream zipToOpen = new FileStream(Path.Combine(directoryPath, fileName), FileMode.Create))
                    {
                        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                        {
                            archive.CreateEntryFromFile(Path.Combine(directoryPath, tempStkFile), tempStkFile);
                            archive.CreateEntryFromFile(Path.Combine(directoryPath, tempMetaFile), tempMetaFile);
                        }
                    }

                    // Clean up temporary files
                    File.Delete(Path.Combine(directoryPath, tempStkFile));
                    File.Delete(Path.Combine(directoryPath, tempMetaFile));
                    break;
                default:
                    throw new NotSupportedException($"Format '{format}' is not supported.");
            }
        }

        public static void LoadStrokes(String filePath)
        {

        }
    }
}
