using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Drawing;

namespace WebApplication4.Models
{
    public class UserInfoDBAccessLayer
    {
        string connString = Convert.ToString(ConfigurationManager.AppSettings["ProductDBConnectionString"]);
        public string AddUserRecord(UserInfo userInfo)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AddUserInfo", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", userInfo.UserName);
                    cmd.Parameters.AddWithValue("@Email", userInfo.Email);
                    cmd.Parameters.AddWithValue("@Password", userInfo.Password);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    return ("Data save Successfully");
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return (ex.Message.ToString());
                }
            }


        }
        public List<UserInfo> GetUserRecord()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM UserInfo", connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserInfo> userInfoList = new List<UserInfo>();
                        while (reader.Read())
                        {
                            UserInfo userInfo = new UserInfo();
                            userInfo.UserName = reader.GetString(1);
                            userInfo.Email = reader.GetString(2);
                            userInfoList.Add(userInfo);
                        }
                        connection.Close();
                        return userInfoList;
                    }
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }
    }
}