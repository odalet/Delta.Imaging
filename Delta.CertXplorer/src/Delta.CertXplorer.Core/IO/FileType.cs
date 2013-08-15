using System;
using System.Text;
using System.Collections.Generic;

namespace Delta.CertXplorer.IO
{
    /// <summary>
    /// Helps supporting file types by associating extensions and descriptions.
    /// </summary>
    /// <remarks>
    /// The only supported patterns are of the followinf form: *.extension
    /// </remarks>
    public class FileType
    {
        private static Dictionary<string, FileType> types = new Dictionary<string, FileType>();

        public static readonly FileType UNKNOWN = new FileType("UNKNOWN", new string[] { "*.*" }, "Unknown Files");
        public static readonly FileType ALL = new FileType("ALL", new string[] { "*.*" }, "All Files");
        public static readonly FileType TXT = new FileType("TXT", new string[] { "*.txt" }, "Text Files");
        public static readonly FileType LOG = new FileType("LOG", new string[] { "*.log", "*.txt" }, "Log Files");
        public static readonly FileType RTF = new FileType("RTF", new string[] { "*.rtf" }, "Rich Text Files");

        internal static IDictionary<string, FileType> Types { get { return types; } }

        internal static string CombineFilters(params FileType[] types)
        {
            FileType[] combinedTypes = null;
            return CombineFilters(out combinedTypes, types);
        }

        internal static string CombineFilters(out FileType[] combinedTypes, params FileType[] types)
        {
            if ((types == null) || (types.Length == 0))
            {
                combinedTypes = new FileType[] { ALL };
                return ALL.Filter;
            }

            bool containsFilter = ALL.IsFilterInArray(types);

            int combinedTypesCount = types.Length;
            if (!containsFilter) combinedTypesCount++;
            combinedTypes = new FileType[combinedTypesCount];
            types.CopyTo(combinedTypes, 0);
            if (!containsFilter) combinedTypes[types.Length] = ALL;

            string filter = string.Empty;
            foreach (FileType type in combinedTypes) filter += type.Filter + "|";
            return filter.Substring(0, filter.Length - 1);
        }

        private string typeId = string.Empty;
        private string[] patterns = null;
        private string filter = null;

        internal string TypeId { get { return typeId; } }

        public string[] Patterns { get { return patterns; } }
        public string Filter { get { return filter; } }
        public string FilterWithAll 
        { 
            get 
            {
                if (typeId == ALL.TypeId) return filter;
                else return filter + "|" + ALL.Filter; 
            } 
        }

        internal bool IsFilterInArray(params FileType[] types)
        {
            foreach (FileType ft in types) { if (ft == this) return true; }
            return false;
        }

        public bool Matches(string filename)
        {
            foreach (string pattern in patterns)
            {
                if (MatchesNoCase(filename.ToUpper(), pattern.ToUpper())) return true;
            }            
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is FileType) return ((FileType)obj).typeId == typeId;
            else return obj.ToString() == typeId;
        }

        public override int GetHashCode() { return typeId.GetHashCode(); }

        private FileType(string id, string[] patts, string filterText)
        {
            if ((patts == null) || (patts.Length == 0)) throw new ArgumentNullException("patts");
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id");
            if (types.ContainsKey(id)) throw new ArgumentException("This id (" + id + ") is already registred", "id");

            if (string.IsNullOrEmpty(filterText)) filterText = patts[0] + " Files";

            for (int i=0; i<patts.Length; i++)
            {
                if (!patts[i].StartsWith("*.")) patts[i] = "*." + patts[i];
            }

            typeId = id;
            patterns = patts;
            filter = filterText;

            BuildFilter();

            types.Add(typeId, this);
        }

        private void BuildFilter()
        {
            string patternsList = string.Empty;
            foreach (string pattern in patterns) patternsList += pattern + ";";
            patternsList = patternsList.Substring(0, patternsList.Length - 1);

            StringBuilder sb = new StringBuilder();   
            sb.Append(filter);
            sb.Append(" (");
            sb.Append(patternsList);
            sb.Append(" )|");
            sb.Append(patternsList);

            filter = sb.ToString();
        }

        //TODO : revoir les algos Match...

        private bool MatchesNoCase(string filename, string pattern)
        {
            if (pattern == "*.*") return true;
            if (pattern[0] == '*')
            {
                int flength = filename.Length;
                int plength = pattern.Length;

                while (--plength > 0)
                {
                    if (pattern[plength] == '*') return MatchesNoCase(filename, pattern, 0, 0);
                    if (flength-- == 0) return false;
                    if ((pattern[plength] != filename[flength]) && (pattern[plength] != '?')) return false;
                }
                return true;
            }
            else return MatchesNoCase(filename, pattern, 0, 0);
        }

        private bool MatchesNoCase(string filename, string pattern, int findex, int pindex)
        {
            int flength = filename.Length;
            int plength = pattern.Length;
            char next;
            while(true)
            {
                if (pindex == plength) return (findex == flength);
                next = pattern[pindex++];
                if (next == '?')
                {
                    if (findex == flength) return false;
                    findex++;
                }
                else if (next == '*')
                {
                    if (pindex == plength) return true;
                    while (findex < flength)
                    {
                        if (MatchesNoCase(filename, pattern, findex, pindex)) return true;
                        findex++;
                    }
                }
                else
                {
                    if ((findex == flength) || (filename[findex] != next)) return false;
                    findex++;
                }
            }
        }
    }
}
