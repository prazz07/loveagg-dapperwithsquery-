using Dapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo1.Models
{
    public class ProductRepository
    {
        private string connectionstring;
        public ProductRepository()
        {
            connectionstring = "Server=(localdb)\\MSSQLLocalDB;Database=Pranjal3; Trusted_Connection = True; MultipleActiveResultSets = true";
        }
        public  IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionstring);
            }
        }
        public void Add(Product prod)
        {
            using(IDbConnection dbconnection=Connection)
            {
                string sQuery = @"INSERT INTO Products(Name,Quantity,Price) VALUES(@Name,@Quantity,@Price)";
                dbconnection.Open();
                dbconnection.Execute(sQuery, prod);
            }
        }

        

        public IEnumerable<Product>GetAll()
        {
            using (IDbConnection dbconnection = Connection)
            {
                string sQuery = @"Select * FROM Products";
                dbconnection.Open();
                return dbconnection.Query<Product>(sQuery);
            }
        }

        public Product GetById(int id)
        {
            using (IDbConnection dbconnection = Connection)
            {
                string sQuery = @"Select * FROM Products Where ProductId=@Id";
                dbconnection.Open();
                return dbconnection.Query<Product>(sQuery,new { Id = id }).FirstOrDefault();
            }
        }

        public void Delete (int id)
        {
            using (IDbConnection dbconnection = Connection)
            {
                string sQuery = @"DELETE FROM Products Where ProductId=@Id";
                dbconnection.Open();
                dbconnection.Execute(sQuery, new { Id = id });
            }
        }

        public void Update(Product prod)
        {
            using (IDbConnection dbconnection = Connection)
            {
                string sQuery = @"UPDATE Products SET Name=@Name,Quantity=@Quantity,Price=@Price Where ProductId=@ProductId";
                dbconnection.Open();
                dbconnection.Query(sQuery, prod);
            }
        }
    }
}
