#region License

// ====================================================
// Universal Mod Organizer by ARZUMATA.
// 
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// 
// ====================================================

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Universal_Mod_Organizer
{
    internal struct ModStruct
    {
        public string Enabled;
        public string Order;
        public string Name;
        public string Version;
        public string UID;
        public int Conflicts;
        public string Workshop;
        public string Filename;
        public string Archive;
        public long Size;
        public string Achivements;
        public int FileCount;
        public List<string> ModFiles;
        public Dictionary<string, List<string>> ModConflicts;
        public bool Highlight;
    }

    public sealed class Mod : IEquatable<Mod>
    {
        // Various Regex
        private readonly Regex regexGetOnlyLineName = new Regex("^#|=\".+$");
        private readonly Regex regexGetOnlyLineData = new Regex("^(#|[a-z]+|_+)+=\"|\"$");
        private readonly Regex regexChecksum;

        // List with files and pathes that lead to checksum change
        private readonly List<string> checksumChangingFoldersAndFiles = new List<string>
        {
            "^common/.+$",
            "^events/.+$",
            "^map/.+$"
        };

        private ModStruct modStruct;

        public Mod(string filePath)
        {
            FilePath = filePath;

            modStruct = new ModStruct
            {
                Enabled = string.Empty,
                Order = string.Empty,
                Name = "N/A",
                Version = "0.0",
                UID = "none",
                Conflicts = 0,
                Workshop = string.Empty,
                Filename = "none",
                Archive = string.Empty,
                Size = 0,
                Achivements = "???",
                ModFiles = new List<string>(),
                ModConflicts = new Dictionary<string, List<string>>()
            };

            // This is used for mods that change checksum.
            regexChecksum = new Regex(string.Join("|", checksumChangingFoldersAndFiles.Select(item => item)));
        }

        public string Filename { get => modStruct.Filename; set => modStruct.Filename = value; }

        public string Enabled { get => modStruct.Enabled; set => modStruct.Enabled = value; }

        public string Order { get => modStruct.Order; set => modStruct.Order = value; }

        public string Achivements { get => modStruct.Achivements; set => modStruct.Achivements = value; }

        public string Archive { get => modStruct.Archive; set => modStruct.Archive = value; }

        public string Name { get => modStruct.Name; set => modStruct.Name = value; }

        public string FilePath { get; }

        public string Version { get => modStruct.Version; set => modStruct.Version = value; }

        public string UID { get => modStruct.UID; set => modStruct.UID = value; }

        public int Conflicts { get => modStruct.Conflicts; set => modStruct.Conflicts = value; }

        public long Size { get => modStruct.Size; set => modStruct.Size = value; }

        public string Workshop { get => modStruct.Workshop; set => modStruct.Workshop = value; }

        public int FileCount { get => modStruct.FileCount; set => modStruct.FileCount = value; }

        public bool Highlight { get => modStruct.Highlight; set => modStruct.Highlight = value; }

        public Dictionary<string, List<string>> ModConflicts { get => modStruct.ModConflicts; set => modStruct.ModConflicts = value; }

        public List<string> ModFiles { get => modStruct.ModFiles; }

        public bool Equals(Mod other)
        {
            if (FilePath == other.FilePath)
            {
                return true;
            }

            return false;
        }

        public void GetModInfo()
        {
            // Defaults.
            modStruct.Filename = @"mod\" + Path.GetFileName(FilePath);
            modStruct.UID = modStruct.Filename;

            foreach (var line in File.ReadLines(FilePath))
            {
                var stringSwitch = regexGetOnlyLineName.Replace(line, string.Empty);

                switch (stringSwitch)
                {
                    case "original_name":
                        modStruct.Name = regexGetOnlyLineData.Replace(line, string.Empty);
                        break;

                    case "name":
                        modStruct.Name = regexGetOnlyLineData.Replace(line, string.Empty);
                        break;

                    case "supported_version":
                        modStruct.Version = regexGetOnlyLineData.Replace(line, string.Empty);
                        break;

                    case "remote_file_id":
                        modStruct.UID = regexGetOnlyLineData.Replace(line, string.Empty);
                        modStruct.Workshop = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + modStruct.UID;
                        break;

                    case "achievement_compatible":
                        modStruct.Achivements = regexGetOnlyLineData.Replace(line, string.Empty);
                        break;

                    case "archive": // Regex (\\{2}|\/)
                        modStruct.Archive = regexGetOnlyLineData.Replace(line, string.Empty).Replace(@"\\", @"\").Replace(@"/", @"\");
                        break;

                    default:
                        break;
                }
            }

            if (!modStruct.Archive.Equals(string.Empty))
            {
                if (File.Exists(modStruct.Archive))
                {
                    modStruct.Size = new FileInfo(modStruct.Archive).Length;
                }
                else
                {
                    modStruct.Archive = string.Empty;
                }
            }
        }

        public void GetAchievements()
        {
            if (modStruct.Archive.Equals(string.Empty))
            {
                modStruct.Achivements = "???";
            }

            modStruct.FileCount = 0;

            string zipPath = modStruct.Archive;

            // Assume that all mods are compatible by default.
            modStruct.Achivements = "yes";

            // In case it is very short or empty.
            if (zipPath.Length < 5)
            {
                return;
            }

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (var entry in archive.Entries)
                {
                    modStruct.FileCount++;

                    // Populate files list for that mod.
                    if (!entry.ToString().Equals("descriptor.mod"))
                    {
                        modStruct.ModFiles.Add(entry.ToString());
                    }

                    // Found entry that probably changes checksum.
                    Match match = regexChecksum.Match(entry.ToString());

                    if (match.Success)
                    {
                        modStruct.Achivements = "no";
                    }
                }
            }
        }

        public int GetFilesCount()
        {
            return modStruct.ModFiles.Count;
        }

        public void WriteDataToModFile(string modFolder)
        {
            var fileName = modFolder + modStruct.Filename;

            string[] allFileLines = File.ReadAllLines(fileName);

            // Deleting the file
            File.Delete(fileName);

            using (StreamWriter sw = File.AppendText(fileName))
            {
                var regexMatchLineName = new Regex("^name=.*$");
                var regexMatchLineNameIrrelevantData = new Regex("^#(original_name|achievement_compatible)=.*$");

                foreach (string line in allFileLines)
                {
                    Match matchName = regexMatchLineName.Match(line);
                    Match matchOriginalName = regexMatchLineNameIrrelevantData.Match(line);

                    if (matchName.Success)
                    {
                        // Write Name with Order and Original Name Commented
                        if (modStruct.Enabled.Equals(Helper.SymbolYes))
                        {
                            sw.WriteLine("name=\"~" + modStruct.Order + "~ " + modStruct.Name + "\"");
                        }
                        else
                        {
                            sw.WriteLine("name=\"" + modStruct.Name + "\"");
                        }

                        sw.WriteLine("#original_name=\"" + modStruct.Name + "\"");
                        sw.WriteLine("#achievement_compatible=\"" + modStruct.Achivements + "\"");
                    }
                    else if (matchOriginalName.Success)
                    {
                        // We have found another one original name, skip.
                    }
                    else
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
