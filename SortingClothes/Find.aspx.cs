using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using SQLdata;

public partial class Find : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["find"];
            //将cookie转化为字典
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (cookie.Value != "")
                foreach (string key in cookie.Values)
                {
                    dict.Add(key, cookie[key]);
                }
            Clothes clothes = new Clothes();
            DataTable data = clothes.ReadAll(dict);
            BuildGridView(data);
        }
    }
    //页面传参
    protected string GridView;
    protected string Count;
    //建立表格
    void BuildGridView(DataTable data)
    {
        //处理数据
        GridView = "";
        string font_color = "";
        string grade = "";
        string heng = "";
        int i;
        for (i = 0; i < data.Rows.Count; i++)
        {
            switch(data.Rows[i]["condition"].ToString())
            {
                case "a":
                    data.Rows[i]["condition"] = "干净的";
                    font_color = "green";
                    break;
                case "b":
                    data.Rows[i]["condition"] = "需要清洗";
                    font_color = "red";
                    break;
                case "c":
                    data.Rows[i]["condition"] = "使用中";
                    font_color = "yellow";
                    break;
                case "d":
                    data.Rows[i]["condition"] = "其它";
                    font_color = "black";
                    break;
            }
            switch (data.Rows[i]["grade"].ToString())
            {
                case "1":
                    grade = "很少穿";
                    break;
                case "2":
                    grade = "一般";
                    break;
                case "3":
                    grade = "经常穿";
                    break;
            }
            if (data.Rows[i]["heng"].ToString() == "heng")
                heng = "class=\"img_heng\"";
            else
                heng = "class=\"img_shu\"";
            //填放数据
            GridView += "<a href=\"one.aspx?id=" +
                data.Rows[i]["id"] + //id,跳转用
                "\"><table><tr><td class=\"auto - style3\">id:" +
                data.Rows[i]["id"] + //id
                "</td><td class=\"auto - style2\">" +
                data.Rows[i]["type"] + //分类
                "</td><td rowspan=\"4\" class=\"td_img\"><img " +
                heng + //图片是否横放
                " src=\"clothes_img/" +
                data.Rows[i]["id"] + //图片
                ".jpg\" /></td></tr><tr><td>" +
                grade + //常用等级
                "</td><td class=\"td_" +
                font_color + //颜色
                "\">" + data.Rows[i]["condition"] + //状态
                "</td></tr><tr><td colspan=\"2\">" +
                data.Rows[i]["place"] + //地点
                "</td></tr><tr><td colspan=\"2\">" +
                data.Rows[i]["describe"] + //备注
                "</td></tr></table></a>";
            /*修改备份
            GridView += "<a href=\"one.aspx?id=" +
                data.Rows[i]["id"] + //id,跳转用
                "\"><table><tr><td class=\"auto - style3\">id:" +
                data.Rows[i]["id"] + //id
                "</td><td class=\"auto - style2\">" +
                data.Rows[i]["type"] + //分类
                "</td><td rowspan=\"4\" class=\"td_img\"><img " +
                heng + //图片是否横放
                " src=\"clothes_img/" +
                data.Rows[i]["id"] + //图片
                ".jpg\" /></td></tr><tr><td>" +
                grade + //常用等级
                "</td><td class=\"td_" +
                font_color + //颜色
                "\">" + data.Rows[i]["condition"] + //状态
                "</td></tr><tr><td colspan=\"2\">" +
                data.Rows[i]["place"] + //地点
                "</td></tr><tr><td colspan=\"2\">" +
                data.Rows[i]["describe"] + //备注
                "</td></tr></table></a>";*/
        }
        Count = i.ToString();
        //string html_table = "<table><tr><td class=\"auto - style3\">id:01</td><td class=\"auto - style2\">保暖内衣</td><td rowspan=\"4\" class=\"td_img\"><img src=\"img / 1.jpg\" /></td></tr><tr><td>经常穿</td><td>待清洗</td></tr><tr><td colspan=\"2\">家中衣柜</td></tr><tr><td colspan=\"2\">下面第三摞</td></tr></table>";
    }
    protected void Return_Click(object sender, EventArgs e)
    {Response.Redirect("/");}//返回主页
}