using Dapper;
using GomoCC.Model;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ded01ConsoleApp
{
    public interface IBatchService
    {
        void WriteInformation(string input);
        void TestMysqlRead();
        void TestTempQuery();

    }

    public class BatchService : IBatchService
    {
        private readonly string _baseUrl;
        private readonly string _token;
        readonly ILogger _logger;

        public BatchService(IConfigurationRoot config, ILogger logger)
        {
            var baseUrl = config["SomeConfigItem:BaseUrl"];
            var token = config["SomeConfigItem:Token"];
            _baseUrl = baseUrl;
            _token = token;
            _logger = logger;
        }
        /// <summary>
        /// 直接讀取資料
        /// </summary>
        public void TestMysqlRead()
        {
            SqlHelper.ConnectionTimeout = 20;
            var cnstr = SqlHelper.GetConnectionString();

           
            using (var cn = new MySqlConnection(cnstr))
            {
                // 開啟連線
                cn.Open();
                String sql = "select Id,User_Id,UName from UserInfo";
                var command = new MySqlCommand(sql, cn);
                // 讀取資料
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", reader["Id"], reader["User_Id"], reader["UName"]);
                }

            }
        }
        /// <summary>
        /// 使用泛型版本的 Query 方法
        /// </summary>
        public void TestTempQuery()
        {
            Dapper.SqlMapper.SetTypeMap(
                typeof(UserInfo),
                new ColumnAttributeTypeMapper<UserInfo>());

            var cnstr = SqlHelper.GetConnectionString();


            using (var cn = new MySqlConnection(cnstr))
            {
                // 開啟連線
                cn.Open();
                String sql = "select * from UserInfo";
                var UserInfos=cn.Query<UserInfo>(sql);
                foreach (var userinfo in UserInfos)
                {
                    Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", userinfo.Id, userinfo.UserId, userinfo.Uname);
                }
                //while (reader.Read())
                //{
                //    Console.WriteLine("Id: {0}, UserId: {1}, Uname:{2}", reader["Id"], reader["User_Id"], reader["UName"]);
                //}

            }
        }

        public void WriteInformation(string input)
        {
            Console.WriteLine(input);
            Console.WriteLine(_baseUrl);
            Console.WriteLine(_token);
            _logger.Information("log test");

        }
    }
}
