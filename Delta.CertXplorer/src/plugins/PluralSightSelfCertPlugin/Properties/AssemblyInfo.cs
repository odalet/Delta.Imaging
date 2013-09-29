using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("PluralSightSelfCertPlugin")]
[assembly: AssemblyDescription(ThisAssembly.Description)]
[assembly: AssemblyVersion(ThisAssembly.PluginVersion)]
[assembly: AssemblyFileVersion(ThisAssembly.PluginVersion)]
[assembly: ComVisible(false)]

partial class ThisAssembly
{
    public const string PluginVersion = "1.1.4.0";
    public const string Description = "Self-signed certificate generation plugin for CertXplorer.";
}