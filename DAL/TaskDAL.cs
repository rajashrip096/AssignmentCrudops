using AssignmentCrudops.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AssignmentCrudops.DAL
{
    public class TaskDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public TaskDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public List<Tasks> GetAllTask()
        {
            List<Tasks> tasklist = new List<Tasks>();
            string qry = "select * from Task";
            cmd = new SqlCommand(qry,con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Tasks task = new Tasks();
                    task.TaskId = Convert.ToInt32(dr["TaskId"]);
                    task.TaskName = dr["TaskName"].ToString();
                    task.Description = dr["Description"].ToString();

                    tasklist.Add(task);
                }
            }
            con.Close();
            return tasklist;
        }
        public Tasks GetTaskById(int TaskId)
        {
            Tasks task = new Tasks();
            string qry = "select * from Task where TaskId=@taskid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@taskid", TaskId);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    task.TaskId = Convert.ToInt32(dr["TaskId"]);
                    task.TaskName = dr["TaskName"].ToString();
                    task.Description = dr["Description"].ToString();
                }
            }
            con.Close();
            return task;
        }
        
        public int AddTask(Tasks task)
        {
            string qry = "insert into Task values(@taskname,@description)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@taskname", task.TaskName);
            cmd.Parameters.AddWithValue("@description", task.Description);
            
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int UpdateTask(Tasks task)
        {
            string qry = "update Task set TaskName=@taskname,Description=@description where TaskId=@taskid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@taskid", task.TaskId);
            cmd.Parameters.AddWithValue("@taskname", task.TaskName);
            cmd.Parameters.AddWithValue("@description", task.Description);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int DeleteTask(int TaskId)
        {
            string qry = "delete from Task where TaskId=@taskid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@taskid", TaskId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
