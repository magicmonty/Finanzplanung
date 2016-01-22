namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Finanzplanung")>]
[<assembly: AssemblyProductAttribute("Finanzplanung")>]
[<assembly: AssemblyDescriptionAttribute("An attempt for a personal budgeting app with F")>]
[<assembly: AssemblyVersionAttribute("0.0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.1"
