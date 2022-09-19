using AssignmentCrudops.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentCrudops.DAL
{
    public class UserDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public UserDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public List<User> GetAllUser()
        {
            List<User> userlist = new List<User>();
            string qry = "select * from UserProfile";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(dr["UserId"]); 
                    user.Name = dr["Name"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.DOB = dr["DOB"].ToString();
                    user.Gender = dr["Gender"].ToString();
                    user.Address = dr["Address"].ToString();
                    user.Password = dr["Password"].ToString();

                    userlist.Add(user);
                }
            }
            con.Close();
            return userlist;
        }
        public User GetUserById(int id)
        {
            User user = new User();
            string qry = "select * from UserProfile where UserId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.Name = dr["Name"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.DOB = dr["DOB"].ToString();
                    user.Gender = dr["Gender"].ToString();
                    user.Address = dr["Address"].ToString();
                    user.Password = dr["Password"].ToString();

                }
            }
            con.Close();
            return user;
        }
        public User GetUserByEmail(User u)
        {
            User user = new User();
            string qry = "select * from UserProfile where Email=@email";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@email", u.Email);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.Name = dr["Name"].ToString();
                    user.Email = dr["Email"].ToString();
                    user.DOB = dr["DOB"].ToString();
                    user.Gender = dr["Address"].ToString();
                    user.Address = dr["Address"].ToString();
                    user.Password = dr["Password"].ToString();

                }
            }
            con.Close();
            return user;
        }
        public int AddUser(User objuser)
        {
            string qry = "insert into UserProfile values(@name,@email,@dob,@gender,@Address,@pass)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", objuser.Name);
            cmd.Parameters.AddWithValue("@email", objuser.Email);
            cmd.Parameters.AddWithValue("@dob", objuser.DOB);
            cmd.Parameters.AddWithValue("@gender", objuser.Gender);
            cmd.Parameters.AddWithValue("@address", objuser.Address);
            cmd.Parameters.AddWithValue("@pass", objuser.Password);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int UpdateUser(User objuser)
        {
            string qry = "update UserProfile set Name=@name,DOB=@dob, Email=@email, Gender=@gender,Address=@address, Password=@pass where UserId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", objuser.UserId);
            cmd.Parameters.AddWithValue("@name", objuser.Name);
            cmd.Parameters.AddWithValue("@email", objuser.Email);
            cmd.Parameters.AddWithValue("@dob", objuser.DOB);
            cmd.Parameters.AddWithValue("@gender", objuser.Gender);
            cmd.Parameters.AddWithValue("@address", objuser.Address);
            cmd.Parameters.AddWithValue("@pass", objuser.Password);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int DeleteUser(int id)
        {
            string qry = "delete from UserProfile where UserId=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}


