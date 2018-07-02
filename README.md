# ConsoleService
主要是為了在linux或是windows下可以執行來取存database
1. 建立基本的ConsoleApp
// Requires NuGet package
// Microsoft.Extensions.Configuration.Json

//log
    AutofacSerilogIntegration
	Serilog.AspNetCore
	Serilog.Sinks.Console
	Serilog.Sinks.RollingFileAlternate (2.0.9)
//DI
   Autofac.Extensions.DependencyInjection
2. 使用Serilog來當成log   
   [參考1](https://github.com/nblumhardt/autofac-serilog-integration)
   
3. 使用Autofac來DI一下
   要使用Serilog要安裝 AutofacSerilogIntegration
   [參考2](https://gist.github.com/greatb/1bfd9a5bd579a65e4eee1c4b074dacd0)

4. 使用Dapper來存取資料庫
   安裝dapper,目前的版本是1.50.5
   安裝mysql.data 版本8.0.11
   連接串如下
   ```
   "ConnectionStrings": {
    "GomoDatabase": "Server=192.168.2.152;Database=Gomo.Dev;user id=username;password=yourpassword;sslmode=None;CharSet=utf8;persistsecurityinfo=True;allowuservariables=True;minpoolsize=10;"
  }
   ```
   [參考](https://www.codeproject.com/Articles/889668/SQL-Server-Dapper)
   [在C#的控制枱應用中使用Dapper鏈接MySQL並執行一些增刪改查](https://hk.saowen.com/a/9a7242f0903cddce8196b776b3613329dd71826441d28abe7eae5675e3a7dac5)
   [C#的dapper使用](https://www.jianshu.com/p/c4ca2989d26a)