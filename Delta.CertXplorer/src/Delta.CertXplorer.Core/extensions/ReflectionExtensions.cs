using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Contains extension methods on types defined in the <see cref="N:System.Reflection"/>
    /// namespace such as <see cref="System.Reflection.Assembly"/>.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether the specified assembly is a satellite assembly.
        /// </summary>
        /// <remarks>
        /// We base our test on the examination of the <c>Culture</c> attribute of the assembly.
        /// According to MSDN (http://msdn.microsoft.com/en-us/library/4w8c1y2s.aspx):
        /// <para>
        /// The runtime treats any assembly that does not have the culture attribute set to null 
        /// as a satellite assembly. Such assemblies are subject to satellite assembly binding rules.
        /// </para>
        /// </remarks>
        /// <param name="assembly">The assembly to test.</param>
        /// <returns>
        /// 	<c>true</c> if the specified assembly is a satellite; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSatellite(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return GetCulture(assembly) != CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Determines whether the specified assembly is dynamic.
        /// </summary>
        /// <param name="assembly">The assembly to test.</param>
        /// <returns>
        /// 	<c>true</c> if the specified assembly is dynamic; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDynamic(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            return assembly is System.Reflection.Emit.AssemblyBuilder;
        }

        /// <summary>
        /// Gets the culture of the specified assembly.
        /// </summary>
        /// <remarks>
        /// <para>
        /// To retrieve the culture of the specified assembly, we first try to
        /// read a <see cref="System.Reflection.AssemblyCultureAttribute"/> if present. 
        /// </para>
        /// <para>
        /// If not, we rely on the parsing of the specified assembly display name 
        /// (<see cref="ReflectionExtensions.GetProperties"/>).
        /// </para>
        /// <para>
        /// We assume that a call to <see cref="System.Reflection.Assembly.ToString"/> should give us
        /// back a result similar to: <c>Delta.CertXplorerV3.resources, Version=3.0.0.0, Culture=fr, PublicKeyToken=null</c>.
        /// </para>
        /// </remarks>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static CultureInfo GetCulture(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            // Search for the CultureAttribute
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCultureAttribute), false);
            var cultureString = string.Empty;
            if (attributes.Length > 0)
            {
                AssemblyCultureAttribute cultureAttribute = (AssemblyCultureAttribute)attributes[0];
                cultureString = cultureAttribute.Culture;
            }
            else // This doesn't mean the assembly has no culture
            {
                // At this step, we determine the culture by parsing the assembly name.                
                var elements = GetProperties(assembly);
                if (elements.ContainsKey("Culture")) cultureString = elements["Culture"];
            }

            if (!string.IsNullOrEmpty(cultureString))
            {
                if (cultureString == "neutral") return CultureInfo.InvariantCulture;

                try
                {
                    var culture = new CultureInfo(cultureString);
                    return culture;
                }
                catch (Exception ex) // not a culture info
                {
                    var debugEx = ex;
                    return CultureInfo.InvariantCulture;
                }
            }
            else return CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Gets the properties of the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A dictionary.</returns>
        /// <remarks>
        /// 	<para>
        /// These properties are obtained by parsing the display name of the specified assembly.
        /// </para>
        /// 	<para>
        /// We assume that a call to <see cref="System.Reflection.Assembly.ToString"/> gives us
        /// back a result similar to: <c>Delta.CertXplorerV3.resources, Version=3.0.0.0, Culture=fr, PublicKeyToken=null</c>.
        /// </para>
        /// 	<para>
        /// The first part of this string representation is associated with the key <c>"Name"</c>.
        /// This means the preceding example would produce a dictionary containing the following
        /// key/value pairs:
        /// <list type="table">
        /// 			<listheader><term>Key</term><description>Value</description></listheader>
        /// 			<item><term>Name</term><description>Delta.CertXplorerV3.resources</description></item>
        /// 			<item><term>Version</term><description>3.0.0.0</description></item>
        /// 			<item><term>Culture</term><description>fr</description></item>
        /// 			<item><term>PublicKeyToken</term><description>null</description></item>
        /// 		</list>
        /// 	</para>
        /// </remarks>
        public static IDictionary<string, string> GetProperties(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            var elements = new Dictionary<string, string>();
            var name = assembly.ToString();
            if (string.IsNullOrEmpty(name)) return elements;

            var parts = name.Split(',');
            if ((parts == null) || (parts.Length == 0)) return elements;

            foreach (var part in parts)
            {
                string[] items = part.Trim().Split('=');
                if (items.Length > 0)
                {
                    var key = string.Empty;
                    var value = string.Empty;
                    if (items.Length == 1)
                    {
                        key = "Name";
                        value = items[0].Trim();
                    }
                    else
                    {
                        key = items[0].Trim();
                        value = items[1].Trim();
                    }

                    if (!elements.ContainsKey(key)) elements.Add(key, value);
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets the description of the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A string representing the description of the specified assembly.</returns>
        public static string GetDescription(this Assembly assembly)
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0) return string.Empty;
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        /// <summary>
        /// Gets the specified assembly version.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The assembly version as a string.</returns>
        public static string GetVersion(this Assembly assembly)
        {
            return assembly.GetName().Version.ToString();
        }

        /// <summary>Obtains the title of the specified assembly.</summary>
        /// <remarks>
        /// <para>
        /// We try to obtain the title by firstly examining a 
        /// <see cref="System.Reflection.AssemblyTitleAttribute"/> 
        /// if it exists; otherwise, by returning a name based on 
        /// the assembly file name.
        /// </para>
        /// <para>
        /// If <paramref name="appendAssemblyCulture"/> is set to 
        /// <c>true</c> and the assembly culture is not the invariant 
        /// culture (<see cref="GetCulture"/>) then its code is appended 
        /// at the end of the title string.
        /// </para>
        /// </remarks>
        /// <param name="assembly">The assembly.</param>
        /// <param name="appendAssemblyCulture">
        /// if set to <c>true</c> append the assembly culture (if it is not the invariant culture).
        /// </param>
        /// <returns>The assembly title as a string.</returns>
        public static string GetTitle(this Assembly assembly, bool appendAssemblyCulture)
        {
            if (assembly.IsDynamic)
            {
                return assembly.GetName().Name;
            }

            var title = string.Empty;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (!string.IsNullOrEmpty(titleAttribute.Title))
                    title = titleAttribute.Title;
            }
            else title = Path.GetFileNameWithoutExtension(assembly.CodeBase);

            var ci = assembly.GetCulture();
            if (ci != CultureInfo.InvariantCulture)
                title = string.Format("{0} ({1})", title, ci);
            return title;
        }

        /// <summary>
        /// Gets the path or UNC location of the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The assembly location.</returns>
        public static string GetLocation(this Assembly assembly)
        {
            return assembly.IsDynamic ? SR.DynamicAssembly : assembly.Location;
        }

        /// <summary>
        /// Examines the specified application domain for its loaded assemblies
        /// and adds a textual representation of these assemblies to the specified string builder.
        /// </summary>
        /// <param name="domain">The application domain to examine.</param>
        /// <param name="builder">The string builder into which to append the discovered data.</param>
        public static void AppendModuleDescriptions(this AppDomain domain, StringBuilder builder)
        {
            if (domain == null) throw new ArgumentNullException("domain");
            if (builder == null) throw new ArgumentNullException("builder");
            var nameHeader = SR.Name;
            var versionHeader = SR.Version;
            var pathHeader = SR.Path;

            var data = domain.GetAssemblies().Select(assembly => new
            {
                Name = GetTitle(assembly, true),
                Version = assembly.GetName().Version.ToString(),
                Path = assembly.GetLocation()
            }).ToList();

            var q = data.Select(item => new
            {
                NameLength = data.Max(i => Math.Max(i.Name.Length, nameHeader.Length)),
                VersionLength = data.Max(i => Math.Max(i.Version.Length, versionHeader.Length)),
                PathLength = data.Max(i => Math.Max(i.Path.Length, pathHeader.Length))
            }).First();

            builder.AppendLine(string.Format("{0} {1} {2}",
                nameHeader.PadRight(q.NameLength),
                versionHeader.PadRight(q.VersionLength),
                pathHeader.PadRight(q.PathLength)));

            builder.AppendLine(string.Format("{0} {1} {2}",
                new string('-', q.NameLength),
                new string('-', q.VersionLength),
                new string('-', q.PathLength)));

            foreach (var item in data.OrderBy(i => i.Name))
                builder.AppendLine(string.Format("{0} {1} {2}",
                    item.Name.PadRight(q.NameLength),
                    item.Version.PadRight(q.VersionLength),
                    item.Path.PadRight(q.PathLength)));
        }

        /// <summary>
        /// Writes a stack trace as a formatted output.
        /// </summary>
        /// <param name="stack">The stack trace to write.</param>
        /// <returns>The formatted representation of the stack trace.</returns>
        public static string WriteStackTrace(this StackTrace stack)
        {
            if (stack == null) return string.Empty;

            StringBuilder builder = new StringBuilder();
            const string tab = " ";
            string tabs = tab;
            StackFrame[] frames = stack.GetFrames();
            for (int i = 0; i < frames.Length; i++)
            {
                var method = frames[i].GetMethod();
                string info = string.Format("{0} [{1}]", method.Name, method.Module.Assembly.FullName);
                builder.AppendFormat("{0}:{1}{2}\r\n", i, tabs, info);
                tabs += tab;
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines whether the specified assembly is a .NET framework assembly.
        /// </summary>
        /// <remarks>
        /// This method is based on comparing the assembly's public key token with two well known values:
        /// <c>b03f5f7f11d50a3a</c> and <c>b77a5c561934e089</c>.
        /// </remarks>
        /// <param name="assembly">The assembly to test.</param>
        /// <returns>
        /// 	<c>true</c> if the specified assembly is a .NET framework assembly; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFrameworkAssembly(this Assembly assembly)
        {
            if (assembly == null) return false;

            var publicKeyToken = assembly.GetName().GetPublicKeyToken();
            if ((publicKeyToken == null) || (publicKeyToken.Length == 0)) return false;

            // Grabbed from NDepend.Platform.DotNet.DotNetFramework.DotNetFrameworkAssemblies
            string stringToken = string.Empty;
            foreach (byte b in publicKeyToken) stringToken += b.ToString("x2");

            return stringToken == "b03f5f7f11d50a3a" || stringToken == "b77a5c561934e089";
        }
    }
}
