using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("CertToolsPlugin")]
[assembly: AssemblyDescription(ThisAssembly.Description)]
[assembly: AssemblyVersion(ThisAssembly.PluginVersion)]
[assembly: AssemblyFileVersion(ThisAssembly.PluginVersion)]
[assembly: ComVisible(false)]

partial class ThisAssembly
{
    public const string PluginVersion = "1.0.1.0";
    public const string Description = "Misc Certificates related tools.";
}