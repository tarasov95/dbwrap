DbWrap
======

It's a library that can generate a C# wrapper class for any stored procedure in a MS SQL Server database. Internally the library uses T4 templates to generate the wrapper code. Generation of the wrappers in the client project is also controlled by T4 templates. 

Main goal of the library is use C# compiler to track changes in the database layer & detect problems related to compatibility of the C# code with the database public interface. Ideally, if you have a stored procedure and have used it for some time  and then, suddenly, number/types/names of the procedure parameters changes C# compiler should generate an error and point you to the problem. As a bonus Visual Studio intellisense is immediately aware of the stored procedure names, parameters and their data types.

Entity framework can be used to the same effect, but it comes with some overhead and support for the code-first approach, which is not required in applications working with large matured enterprise databases. Comparing to the Entity Framework, the library is very siimplistic: it leaves everything to the database layer and only takes care of invoking stored procedure through the generated wrappers. Another objective solved by the library is ability to integrate in msbuild (or other build systems) and perfom mandatory compile time checking of the database layer at compile time.

How to use
==========
- add DbWrap project to your solution
- reference the project in your application
- add a new .tt file to the project; in that file you'll invoke dbwrap.dll to generate c# wrapper classes for any stored proc you need in the project; template for the file is shown below:
<#@ template debug="false" hostspecific="false" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ assembly name="$(ProjectDir)$(OutDir)DbWrap.dll" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="DbWrap.Generate" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ output extension=".cs" #>

namespace MyDatabase {
}

- to generate a wrapper for a new stored procedure just add a new line in the following format <#=StoredProc.Generate("connection-string"), "schema.prodedure-name")#> to the file, for example:

<#@ template debug="false" hostspecific="false" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ assembly name="$(ProjectDir)$(OutDir)DbWrap.dll" #>
<#@ assembly name="$(ProjectDir)$(OutDir)FSLib.dll" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="DbWrap.Generate" #>
<#@ import namespace="FocalScope" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ output extension=".cs" #>

namespace MyDatabase {
   <#=StoredProc.Generate("my-connection-string"), "dbo.MyStoredProcedure")#>
}

- after you save the file Visual Studio will execute the template and generate wrapper classes for all stored procedures listed in it; project tree will look similar to the screenshot below
-if you need to regenerate the wrappers manually use [BUILD/Transform All T4 Templates] command in Visual Studio main menu