using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;

namespace SQLdata
{
    public class Clothes : Config
    {
        public DataTable ReadOne(int id)
        {
            SqlCommand cmd = new SqlCommand("select " +
                "Type.name'type',Place.name'place',Type.id'type_id',Place.id'place_id',seat,describe,grade,condition,changeTime " +
                "from Clothes,Type,Place " +
                "where Clothes.type=Type.id and Clothes.place=Place.id and Clothes.id=@id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //判断图片横竖
            dt.Columns.Add("heng", Type.GetType("System.String"));
            if (Imgerect(id.ToString()))
                dt.Rows[0]["heng"] = "shu";
            else
                dt.Rows[0]["heng"] = "heng";
            return dt;
        }
        /// <summary>
        /// 读取全部
        /// </summary>
        /// <param name="filtrate">筛选条件</param>
        /// <returns>列heng代表横竖</returns>
        public DataTable ReadAll(Dictionary<string, string> dict)
        {
            string where = "";
            string[] str = null;
            int i;
            //设计SQL语句
            foreach (var item in dict)
            {
                where += "and ";
                if (item.Key == "seat" || item.Key == "describe")
                {
                    //对于两个模糊搜索,like就不用专门加@了吧,这里需要解码
                    where += (item.Key + " like '%" + HttpUtility.UrlDecode(item.Value) + "%' ");
                }
                else
                {
                    str = item.Value.Split(',');
                    where += "(";
                    for (i = 0; i < str.Length - 1; i++)
                    {
                        where += (item.Key + "=@" + item.Key + i + " or ");
                    }
                    //最后一次不加"or"
                    where += (item.Key + "=@" + item.Key + i + ") ");
                }
            }
            SqlCommand cmd = new SqlCommand("select " +
                "Clothes.id,Type.name'type',place.name'place',describe,condition,grade " +
                "from Clothes,Type,Place " +
                "where Clothes.type=Type.id and Clothes.place=Place.id " +
                where +
                "order by grade DESC;", con);
            //添加变量
            foreach (var item in dict)
                switch(item.Key)
                {
                    case "place":
                    case "type":
                        str = item.Value.Split(',');
                        for (i = 0; i < str.Length; i++)
                        {
                            cmd.Parameters.Add("@" + item.Key + i, SqlDbType.Int).Value = int.Parse(str[i]);
                        }
                        break;
                    case "grade":
                        str = item.Value.Split(',');
                        for (i = 0; i < str.Length; i++)
                        {
                            cmd.Parameters.Add("@" + item.Key + i, SqlDbType.TinyInt).Value = byte.Parse(str[i]);
                        }
                        break;
                    case "condition":
                        str = item.Value.Split(',');
                        for (i = 0; i < str.Length; i++)
                        {
                            cmd.Parameters.Add("@" + item.Key + i, SqlDbType.Char, 1).Value = str[i][0];
                        }
                        break;
                }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //判断图片横竖
            dt.Columns.Add("heng", Type.GetType("System.String"));
            for(i=0; i<dt.Rows.Count; i++)
            {
                if (Imgerect(dt.Rows[i]["id"].ToString()))
                    dt.Rows[i]["heng"] = "shu";
                else
                    dt.Rows[i]["heng"] = "heng";
            }
            return dt;
        }
        /// <summary>
        /// 获取新的将要被使用的id
        /// </summary>
        /// <returns>返回id</returns>
        public int GetNewID()
        {
            SqlCommand cmd = new SqlCommand("select ident_current('Clothes') + ident_incr('Clothes');", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        /// <summary>
        /// 向衣服表插入一条数据
        /// </summary>
        /// <param name="typeID">分类id</param>
        /// <param name="placeID">位置id</param>
        /// <param name="seat">详细位置 20byte</param>
        /// <param name="describe">备注 200byte</param>
        /// <param name="grade">等级</param>
        /// <param name="condition">状态</param>
        /// <returns>执行是否成功</returns>
        public bool AddOne(int typeID, int placeID, string seat, string describe, byte grade, char condition)
        {
            SqlCommand cmd = new SqlCommand("insert into Clothes(type,place,seat,describe,grade,condition,changeTime) " +
                "values(@type,@place,@seat,@describe,@grade,@condition,@changeTime)", con);
            cmd.Parameters.Add("@type", SqlDbType.Int).Value = typeID;
            cmd.Parameters.Add("@place", SqlDbType.Int).Value = placeID;
            cmd.Parameters.Add("@seat", SqlDbType.VarChar, 20).Value = seat;
            cmd.Parameters.Add("@describe", SqlDbType.VarChar, 200).Value = describe;
            cmd.Parameters.Add("@grade", SqlDbType.TinyInt, 200).Value = grade;
            cmd.Parameters.Add("@condition", SqlDbType.Char, 1).Value = condition;
            cmd.Parameters.Add("@changeTime", SqlDbType.Date).Value = DateTime.Now;
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
        /// 修改一条数据
        /// </summary>
        /// <param name="id">要修改的衣服id</param>
        /// <param name="typeID">分类id</param>
        /// <param name="placeID">位置id</param>
        /// <param name="seat">详细位置 20byte</param>
        /// <param name="describe">备注 200byte</param>
        /// <param name="grade">等级</param>
        /// <param name="condition">状态</param>
        /// <returns>执行是否成功</returns>
        public bool UpdateOne(int id, int typeID, int placeID, string seat, string describe, byte grade, char condition)
        {
            SqlCommand cmd = new SqlCommand("update Clothes " +
                "set type=@type,place=@place,seat=@seat,describe=@describe,grade=@grade,condition=@condition,changeTime=@changeTime " +
                "where id=@id", con);
            cmd.Parameters.Add("@type", SqlDbType.Int).Value = typeID;
            cmd.Parameters.Add("@place", SqlDbType.Int).Value = placeID;
            cmd.Parameters.Add("@seat", SqlDbType.VarChar, 20).Value = seat;
            cmd.Parameters.Add("@describe", SqlDbType.VarChar, 200).Value = describe;
            cmd.Parameters.Add("@grade", SqlDbType.TinyInt, 200).Value = grade;
            cmd.Parameters.Add("@condition", SqlDbType.Char, 1).Value = condition;
            cmd.Parameters.Add("@changeTime", SqlDbType.Date).Value = DateTime.Now;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
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
        /// 更新衣服状态，在干净，使用，和需清洗之间互相切换
        /// </summary>
        /// <param name="id">要修改的衣服id</param>
        /// <returns>执行是否成功</returns>
        public bool UpdateOne(int id)
        {
            SqlCommand cmd = new SqlCommand("select condition from Clothes where id=@id;", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count != 1)
                return false;
            string condition = dt.Rows[0]["condition"].ToString();
            //改变原有状态
            switch(condition)
            {
                case "a": condition = "c"; break;
                case "b": condition = "a"; break;
                case "c": condition = "b"; break;
            }
            cmd = new SqlCommand("update Clothes " +
                "set condition=@condition, changeTime=@changeTime " +
                "where id=@id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@condition", SqlDbType.Char, 1).Value = condition;
            cmd.Parameters.Add("@changeTime", SqlDbType.Date).Value = DateTime.Now;
            //计算被影响的行数
            int rows = 0;
            con.Open();
            rows = cmd.ExecuteNonQuery();
            con.Close();
            if (rows == 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">要删除的衣服id</param>
        /// <returns>执行是否成功</returns>
        public bool DeleteOne(int id)
        {
            SqlCommand cmd = new SqlCommand("delete from Clothes where id=@id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
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
        /// 删除该分类下所以衣服(不会自动打开和关闭连接)
        /// </summary>
        /// <param name="type_id">分类id</param>
        /// <returns>删除数量</returns>
        public int DeleteByType(int type_id)
        {
            SqlCommand cmd = new SqlCommand("select id from Clothes where type=@type", con);
            cmd.Parameters.Add("@type", SqlDbType.Int).Value = type_id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DeleteImg(dt);
            cmd = new SqlCommand("delete from Clothes where type=@type", con);
            cmd.Parameters.Add("@type", SqlDbType.Int).Value = type_id;
            int rows = 0;//被影响的行数
            rows = cmd.ExecuteNonQuery();
            return rows;
        }
        /// <summary>
        /// 删除该地点下所以衣服(不会自动打开和关闭连接)
        /// </summary>
        /// <param name="place_id">地点id</param>
        /// <returns>删除数量</returns>
        public int DeleteByPlace(int place_id)
        {
            SqlCommand cmd = new SqlCommand("select id from Clothes where place=@place", con);
            cmd.Parameters.Add("@place", SqlDbType.Int).Value = place_id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DeleteImg(dt);
            cmd = new SqlCommand("delete from Clothes where place=@place", con);
            cmd.Parameters.Add("@place", SqlDbType.Int).Value = place_id;
            int rows = 0;//被影响的行数
            rows = cmd.ExecuteNonQuery();
            return rows;
        }
        //批量删除图片
        private int DeleteImg(DataTable data)
        {
            Directory.SetCurrentDirectory(path);
            int i;
            for (i=0; i<data.Rows.Count; i++)
            {
                if (File.Exists("clothes_img/" + data.Rows[i]["id"] + ".jpg"))
                    File.Delete("clothes_img/" + data.Rows[i]["id"] + ".jpg");
            }
            return i;
        }
        //判断图片是否直立
        private bool Imgerect(string id)
        {
            string WebFilePath = path + "/clothes_img/" + id + ".jpg";
            Bitmap pic;
            try
            {
                pic = new Bitmap(WebFilePath);
                //try用于测试，个别没有图片
            }
            catch
            {
                return true;
            }
            int width = pic.Size.Width;   // 图片的宽度
            int height = pic.Size.Height;   // 图片的高度
            pic.Dispose();  //释放资源
            if (height > width)
                return true;
            else
                return false;
        }
    }
}