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
   安裝mysql.data 版本8.0.11，這個要來安裝
   連接串如下
   ```
   "ConnectionStrings": {
    "GomoDatabase": "Server=192.168.2.152;Database=Gomo.Dev;user id=username;password=yourpassword;sslmode=None;CharSet=utf8;persistsecurityinfo=True;allowuservariables=True;minpoolsize=10;"
  }
   ```
   [參考](https://www.codeproject.com/Articles/889668/SQL-Server-Dapper)
   [在C#的控制枱應用中使用Dapper鏈接MySQL並執行一些增刪改查](https://hk.saowen.com/a/9a7242f0903cddce8196b776b3613329dd71826441d28abe7eae5675e3a7dac5)
   [C#的dapper使用](https://www.jianshu.com/p/c4ca2989d26a)
5. 存取資料庫
   資料表和物件的mapping
   5.1 因為資料表的欄位名稱和物件的屬性名稱可能會不一到
```
//物件
public partial class UserInfo
    {
        public int Id { get; set; }
        [ColumnMapping("User_Id")]
        public string UserId { get; set; }
        //以下省略
    }
```   
   資料庫的欄位名稱是 User_Id，要將兩個名稱mapping起來，可以加入Dapper.ColumnMapper的函式庫
   在物件的屬性上加入 **[ColumnMapping("User_Id")]**
   [參考](https://dotblogs.com.tw/supershowwei/2016/08/16/175753)
   
   5.2 範例一：查詢資料
   
   ```
    //以下可以2選一
    //SqlMapper.SetTypeMap(typeof(UserInfo), new ColumnTypeMapper(typeof(UserInfo)));
    ColumnTypeMapper.RegisterForTypes(typeof(UserInfo), typeof(UserInfo));
	using (var cn = new MySqlConnection(cnstr))
    {
        // 開啟連線
        cn.Open();
        String sql = "select * from UserInfo";
        var UserInfos = cn.Query<UserInfo>(sql);
        foreach (var userinfo in UserInfos)
        {
            Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", userinfo.Id, userinfo.UserId, userinfo.Uname);
        }
    }		
   ```
   5.3 參數式查詢
   ```
    using (var cn = new MySqlConnection(cnstr))
    {
        // 開啟連線
        cn.Open();
        String sql = "select * from UserInfo where User_Id like @User_Id or UName=@UName";
        var parameters = new
        {
            User_Id="pg%",
            UName= "pg%"
        };
        var UserInfos = cn.Query<UserInfo>(sql, parameters);
        foreach (var userinfo in UserInfos)
        {
            Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", userinfo.Id, userinfo.UserId, userinfo.Uname);
        }
    }
   ```
   [參考](https://www.huanlintalk.com/2014/03/a-micro-orm-dapper.html)