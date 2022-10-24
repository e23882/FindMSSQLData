using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMSSQLData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server IP");
            var Server = Console.ReadLine();
            Console.WriteLine("DB Name");
            var DB = Console.ReadLine();
            Console.WriteLine("Account");
            var Account = Console.ReadLine();
            Console.WriteLine("Password");
            var Password = Console.ReadLine();
            Console.WriteLine("Search Count");
            var Count = Console.ReadLine();
            Console.WriteLine("關鍵字");
            var keyword = Console.ReadLine();
            int searchCount = 0;
            int searchTable = 0;
            int passTableCount = 0;
            Console.WriteLine($"開始搜尋....");
            using (SqlConnection conn = new SqlConnection($"Data Source={Server};Initial Catalog={DB};Persist Security Info=True;User ID={Account};Password={Password}"))
            {
                string searchAllTableQuery = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
                
                var allTable = conn.Query<string>(searchAllTableQuery);
                foreach(var table in allTable) 
                {
                    searchTable++;
                    try 
                    {
                        string query = $"Select top {Count} * from {table}";
                        var result = conn.Query<dynamic>(query);
                        foreach (var item in result)
                        {
                            searchCount++;
                            if (Convert.ToString(item).Contains(keyword))
                            {
                                Console.WriteLine($"資料表 {table} 包含指定資料");
                                break;
                            }
                            //else 
                            //{
                            //    Console.Write(".");
                            //}
                        }
                    }
                    catch 
                    {
                        Console.WriteLine($"查詢資料表 {table} 發生例外");
                    }
                }
            }
            Console.WriteLine($"Done.....搜尋資料表 {searchTable}, 搜尋總資料筆數 {searchCount}");
            Console.Read();

        }
    }
}
