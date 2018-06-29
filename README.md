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