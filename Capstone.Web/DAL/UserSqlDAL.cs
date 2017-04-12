using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserDAL
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["potholeDB"].ConnectionString;

        private const int workFactor = 100000;

        public bool RegisterUser(RegisterModel newUser)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand regUser = new SqlCommand($"INSERT INTO appUser(email, password, name, userType, salt) VALUES(@email, @password, @name, @userType, @salt);", conn);

                    KeyValuePair<string, string> saltAndHash = RegisterModel.HashPassword(newUser.Password, 8, workFactor);

                    regUser.Parameters.AddWithValue("@email", newUser.Email);
                    regUser.Parameters.AddWithValue("@password", saltAndHash.Value);
                    regUser.Parameters.AddWithValue("@name", newUser.Name);
                    regUser.Parameters.AddWithValue("@userType", "r");
                    regUser.Parameters.AddWithValue("@salt", saltAndHash.Key);

                    int result = regUser.ExecuteNonQuery();

                    if(result > 0)
                    {
                        return true;
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }




            return false;
        }

        public bool VerifyUniqueEmail(string email)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand checkEmail = new SqlCommand($"SELECT * FROM appUser WHERE email = @email;", conn);

                    checkEmail.Parameters.AddWithValue("@email", email);

                    var result = checkEmail.ExecuteScalar();

                    if(result != null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
            }
            catch(SqlException ex)
            {
                throw;
            }

            return false;
        }

        public int LogInUser(LoginModel user)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand getUser = new SqlCommand($"SELECT * FROM appUser WHERE email = @email", conn);

                    getUser.Parameters.AddWithValue("@email", user.Email);

                    SqlDataReader reader = getUser.ExecuteReader();

                    while (reader.Read())
                    {
                        string existingHashedPassword = Convert.ToString(reader["password"]);
                        string salt = Convert.ToString(reader["salt"]);

                        if(LoginModel.ComparePasswords(user.Password, existingHashedPassword, salt, workFactor))
                        {
                            return Convert.ToInt32(reader["userId"]);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }

            return -1;
        }

        public User GetUser(int userId)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand getUser = new SqlCommand($"SELECT * FROM appUser WHERE userId = @userId", conn);

                    getUser.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = getUser.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User();

                        user.Email = Convert.ToString(reader["email"]);
                        user.Name = Convert.ToString(reader["name"]);
                        user.UserType = Convert.ToString(reader["userType"]);
                        user.UserId = userId;

                        return user;
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }

            return null;
        }
    }
}