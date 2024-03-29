﻿using API_DemoBlazor.Models;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Crypt = BCrypt.Net;
namespace API_DemoBlazor.Services
{
    public class UserService
    {
        private readonly SqlConnection _connection;
        public UserService(SqlConnection connection)
        {
            _connection = connection;
        }

        public void Register(User u)
        {
            u.Password = Crypt.BCrypt.HashPassword(u.Password);
            string sql = "INSERT INTO Users (Email, Password, Nickname)" +
                " VALUES (@Email, @Password, @Nickname)";

            _connection.Execute(sql, u);
        }

        public User? Login(string email, string password)
        {
            string getPassword = "SELECT password FROM Users " +
                "WHERE Email = @Email";

            string pwdToVerify = 
                _connection.QueryFirst<string>(getPassword, new { Email = email });

            if (pwdToVerify != null)
            {
                if(Crypt.BCrypt.Verify(password, pwdToVerify)) {
                    string getUser = "SELECT * FROM Users WHERE Email = @email";
                    return _connection.QueryFirst<User>(getUser, new {email});
                }
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            string sql = "SELECT * FROM Users";
            return _connection.Query<User>(sql);  
        }
    }
}
