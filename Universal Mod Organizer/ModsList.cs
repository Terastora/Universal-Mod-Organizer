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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal_Mod_Organizer
{
    public sealed class ModsList : IEquatable<ModsList>
    {
        public ModsList(
            string enabled = "",
            string order = "",
            string name = "N/A",
            string version = "0.0",
            string uid = "none",
            string conflicts = "none",
            string workshop = "",
            string filename = "none",
            string archive = "",
            string size = "0",
            string achivemenets = "???")
        {
            Enabled = enabled;
            Order = order;
            Name = name;
            Version = version;
            UID = uid;
            Conflicts = conflicts;
            Workshop = workshop;
            Filename = filename;
            Archive = archive;
            Size = size;
            Achivements = achivemenets;
        }

        public string Enabled { get; set; }

        public string Order { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string UID { get; set; }

        public string Conflicts { get; set; }

        public string Workshop { get; set; }

        public string Archive { get; set; }

        public string Filename { get; set; }

        public string Size { get; set; }

        public string Achivements { get; set; }

        public bool Equals(ModsList other)
        {
            if (Filename == other.Filename)
            {
                return true;
            }

            return false;
        }

        internal static List<ModsList> GetLootLists()
        {
            return Helper.ModListForListView;
        }
    }
}
