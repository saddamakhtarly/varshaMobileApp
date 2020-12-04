using MobileAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace MobileAppAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public UserController()
        {

        }
        public SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["getConnection"].ToString();
            return new SqlConnection(conString);
        }

        [HttpPost]
        [Route("RegisterUser")]
        public RegisterResponse RegisterUser([FromBody] User user)
        {
            RegisterResponse response = new RegisterResponse();
            try
            {
                if(string.IsNullOrEmpty(user.Email))
                {
                    response.Message = "Email is mandatory";
                }
                else if (string.IsNullOrEmpty(user.UserName))
                {
                    response.Message = "Username is mandatory";
                }
                else if (string.IsNullOrEmpty(user.Mobile))
                {
                    response.Message = "Mobile is mandatory";
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    response.Message = "Password is mandatory";
                }
                else
                {
                    // save
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_RegisterUser", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Id", user.Id);
                        com.Parameters.AddWithValue("@Username", user.UserName);
                        com.Parameters.AddWithValue("@Email", user.Email);
                        com.Parameters.AddWithValue("@Mobile", user.Mobile);
                        com.Parameters.AddWithValue("@Password", user.Password);
                        com.Parameters.Add("@ReturnParamater", SqlDbType.Int, 4);
                        com.Parameters["@ReturnParamater"].Direction = ParameterDirection.Output;

                        conn.Open();
                        int resp = com.ExecuteNonQuery();
                        if (resp > 0)
                        {
                            response.Message = "User Registration Sucessfully";
                            response.IsValid = true;
                        }
                        else
                        {
                            response.Message = "User Registration Failed";
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
        [HttpPost]
        [Route("Login")]
        public LoginResponse Login([FromBody] LoginRequest user)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    response.Message = "Username is mandatory";
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    response.Message = "Password is mandatory";
                }
                else
                {
                    using (var conn = GetConnection())
                    {
                        SqlCommand com = new SqlCommand("Sp_LoginUser", conn);
                        com.CommandType = CommandType.StoredProcedure;

                        com.Parameters.AddWithValue("@Username", user.Username);
                        com.Parameters.AddWithValue("@Password", user.Password);

                        conn.Open();
                        var reader = com.ExecuteReader();
                        List<User> users = new List<User>();
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Email = Convert.ToString(reader["Email"]),
                                Mobile = Convert.ToString(reader["Mobile"]),
                                UserName = Convert.ToString(reader["UserName"]),
                            });
                        }
                        if (users.Count > 0)
                        {
                            response.Message = "Login Success";
                            response.IsValid = true;
                        }
                        else
                        {
                            response.Message = "Invalid Login";
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return response;
        }

        [HttpPost]
        [Route("GetLessonsData")]
        public LessonsResponse GetLessonsData()
        {
            LessonsResponse response = new LessonsResponse();
            try
            {
                //using (var context = new TestDBEntities())
                //{
                //    var lessons = context.Lessons.ToList();
                //}

                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_GetLessons", conn);
                    com.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.Lessons.Add(new Lessons
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            LessonName = Convert.ToString(reader["LessonName"]),
                            Name = Convert.ToString(reader["Name"]),
                            Description = Convert.ToString(reader["Description"]),
                        });
                    }
                    if (response.Lessons.Count > 0)
                    {
                        response.Message = $"{response.Lessons.Count} lesson found";
                        response.IsValid = true;
                    }
                    else
                    {
                        response.Message = $"{response.Lessons.Count} lesson found";
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {

            }
            return response;
        }

        [HttpGet]
        [Route("GetMusicsData")]
        public MusicResponse GetMusicListData(int LessonId)
        {
            MusicResponse response = new MusicResponse();
            try
            {
                using (var conn = GetConnection())
                {
                    SqlCommand com = new SqlCommand("Sp_Music", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@LessonId", LessonId);
                    conn.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        response.Musics.Add(new Music
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            MusicName = Convert.ToString(reader["MusicName"]),
                            Duration = Convert.ToString(reader["Duration"]),
                             Url = Convert.ToString(reader["URL"])
                        });
                    }
                    if (response.Musics.Count > 0)
                    {
                        response.Message = $"{response.Musics.Count} lesson found";
                        response.IsValid = true;
                    }
                    else
                    {
                        response.Message = $"{response.Musics.Count} lesson found";
                    }
                    conn.Close();
                }

            }
            catch (Exception e)
            {

            }
            return response;
        }

        [HttpPost]
        [Route("SaveImage")]
        public SaveImageResponse SaveImage()
        {
            SaveImageResponse resp = new SaveImageResponse();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string path = HttpContext.Current.Server.MapPath($"~/Media/{postedFile.FileName}");
                        postedFile.SaveAs(path);
                        resp.imageurl.Add(path);
                        resp.Message += "Save Image Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
            }
            return resp;
        }
    }
}
