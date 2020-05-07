using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("bitrun-比特胜")]
[assembly: AssemblyDescription("说明 \n 帮助用户快速搜集数据和发布数据 \n http://www.ebotop.com")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("深圳易播天下信息技术有限公司")]
[assembly: AssemblyProduct("bitrun-比特胜")]
[assembly: AssemblyCopyright("深圳易播天下信息技术有限公司版权所有")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c32274f6-22b5-4395-8f32-4e2cf69f91d1")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.9.1")]
[assembly: AssemblyFileVersion("1.9.*")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
[assembly: Obfuscation(Feature = "encrypt symbol names with password 2013$cnboker.com#@!", Exclude = false)]
[assembly: Obfuscation(Feature = "encrypt resources", Exclude = false)]
[assembly: Obfuscation(Feature = "code control flow obfuscation", Exclude = false)]
