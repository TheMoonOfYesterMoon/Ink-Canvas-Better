using Ink_Canvas_Better.Resources;
using Newtonsoft.Json;
using System;
using System.IO;
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

        public static void SaveStrokes(String filePath, String format = "icbetterstk")
        {
            switch (format.ToLower())
            {
                case "icbetterstk":
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    RuntimeData.mainWindow.inkCanvas.Strokes.Save(fileStream);

                    byte[] delimiter = Encoding.UTF8.GetBytes("<!--INK-CANVAS-BETTER-METADATA-->");
                    fileStream.Write(delimiter, 0, delimiter.Length);

                    var metadata = JsonConvert.SerializeObject(RuntimeData.currentMetadata, Formatting.Indented);
                    byte[] metadataBytes = Encoding.UTF8.GetBytes(metadata);
                    fileStream.Write(metadataBytes, 0, metadataBytes.Length);
                    fileStream.Close();
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
