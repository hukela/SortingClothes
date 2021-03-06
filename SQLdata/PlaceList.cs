﻿using System.Data;
using System.Data.SqlClient;

namespace SQLdata
{
    public class PlaceList : Config
    {
        /// <summary>
        /// 读取全部
        /// </summary>
        /// <returns>数据库数据</returns>
        public DataTable ReadAll()
        {
            SqlCommand cmd = new SqlCommand("select * from Place;", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 读取读id,name,和每一个分类中所含衣服个数
        /// </summary>
        /// <returns>数据库数据</returns>
        public DataTable ReadCount()
        {
            string sql = "select id,name,SUM(Column1)'count' " +
                "from " +
                "(" +
                "select Place.id,name,COUNT(Clothes.id)'Column1' " +
                "from Place left join Clothes " +
                "on Place.id=Clothes.Place " +
                "group by Clothes.id,name,Place.id " +
                ") one " +
                "group by id,name,Column1 order by id;";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 添加新的一行
        /// </summary>
        /// <param name="name">地点名称</param>
        /// <returns>执行是否成功</returns>
        public bool AddOne(string name)
        {
            SqlCommand cmd = new SqlCommand("insert into Place(name) values(@name)", con);
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
            int rows = 0;//被影响的行数
            con.Open();
            rows = cmd.ExecuteNonQuery();
            con.Close();
            if (rows == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 修改某一行的数据
        /// </summary>
        /// <param name="id">要修改的地点id</param>
        /// <param name="name">地点的新名称</param>
        /// <returns>执行是否成功</returns>
        public bool UpdateOne(int id, string name)
        {
            SqlCommand cmd = new SqlCommand("update Place set name=@name where id = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = name;
            int rows = 0;//被影响的行数
            con.Open();
            rows = cmd.ExecuteNonQuery();
            con.Close();
            if (rows == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除某一行的数据
        /// </summary>
        /// <param name="id">要删除的数据id</param>
        /// <returns>被连带影响的行数</returns>
        public int DeleteOne(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from Place where id = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Clothes clothes = new Clothes();
            con.Open();
            //被连带影响的行数
            int rows = clothes.DeleteByPlace(id);
            cmd.ExecuteNonQuery();
            con.Close();
            return rows;
        }
    }
}
