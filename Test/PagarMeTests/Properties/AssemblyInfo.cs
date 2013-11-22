using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("PagarMe")]
[assembly: AssemblyDescription("Integration services for Pagar.me [NUnit]")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Retail")]
#endif

[assembly: AssemblyCompany("Pagar.me Pagamentos S.A.")]
[assembly: AssemblyProduct("Pagar.me")]
[assembly: AssemblyCopyright("Copyright © Pagar.me 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: ComVisibleAttribute(false)]
