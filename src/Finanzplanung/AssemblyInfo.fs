namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Finanzplanung")>]
[<assembly: AssemblyProductAttribute("Finanzplanung")>]
[<assembly: AssemblyDescriptionAttribute("An attempt for a personal budgeting app with F")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
