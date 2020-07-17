using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SQLdata;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //查找数据库
            BuildCheckList();
        }
    }
    void BuildCheckList()
    {
        //分类表
        TypeList type = new TypeList();
        DataTable Data = type.ReadAll();
        Type_CBL.DataSource = Data;
        Type_CBL.DataTextField = "name";
        Type_CBL.DataValueField = "id";
        Type_CBL.DataBind();
        //地点表
        PlaceList site = new PlaceList();
        Data = site.ReadAll();
        Site_CBL.DataSource = Data;
        Site_CBL.DataTextField = "name";
        Site_CBL.DataValueField = "id";
        Site_CBL.DataBind();
        //获取cookie
        HttpCookie cookie = HttpContext.Current.Request.Cookies["find"];
        //将cookie转化为字典
        Dictionary<string, string> dict = null;
        if (cookie != null) if (cookie.Value != "")
            {
                dict = new Dictionary<string, string>();
                foreach (string key in cookie.Values)
                {
                    dict.Add(key, cookie[key]);
                }
            }
        else
            return;
        //置默认选中
        string[] str;
        foreach (var item in dict)
        {
            switch (item.Key)
            {
                case "type":
                    str = item.Value.Split(',');
                    foreach (string value in str)
                        foreach (ListItem listItem in Type_CBL.Items)
                            if (value == listItem.Value)
                                listItem.Selected = true;
                            else
                                continue;
                    break;
                case "condition":
                    str = item.Value.Split(',');
                    foreach (string value in str)
                        foreach (ListItem listItem in Condition_CBL.Items)
                            if (value == listItem.Value)
                                listItem.Selected = true;
                            else
                                continue;
                    break;
                case "grade":
                    str = item.Value.Split(',');
                    foreach (string value in str)
                        foreach (ListItem listItem in Grade_CBL.Items)
                            if (value == listItem.Value)
                                listItem.Selected = true;
                            else
                                continue;
                    break;
                case "place":
                    str = item.Value.Split(',');
                    foreach (string value in str)
                        foreach (ListItem listItem in Site_CBL.Items)
                            if (value == listItem.Value)
                                listItem.Selected = true;
                            else
                                continue;
                    break;
                case "seat":
                    SeatText.Text = HttpUtility.UrlDecode(item.Value);
                    break;
                case "describe":
                    DescribeText.Text = HttpUtility.UrlDecode(item.Value);
                    break;
            }
        }
    }
    //查找按键
    protected void Find_But_Click(object sender, EventArgs e)
    {
        //将选中数据放入字典中
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string value = "";
        if (Type_CBL.SelectedIndex != -1)
        {
            foreach (ListItem item in Type_CBL.Items)
                if (item.Selected)
                    value += item.Value + ",";
            value = value.TrimEnd(',');
            dict.Add("type", value);
        }
        value = "";
        if (Condition_CBL.SelectedIndex != -1)
        {
            foreach (ListItem item in Condition_CBL.Items)
                if (item.Selected)
                    value += item.Value + ",";
            value = value.TrimEnd(',');
            dict.Add("condition", value);
        }
        value = "";
        if (Grade_CBL.SelectedIndex != -1)
        {
            foreach (ListItem item in Grade_CBL.Items)
                if (item.Selected)
                    value += item.Value + ",";
            value = value.TrimEnd(',');
            dict.Add("grade", value);
        }
        value = "";
        if (Site_CBL.SelectedIndex != -1)
        {
            foreach (ListItem item in Site_CBL.Items)
                if (item.Selected)
                    value += item.Value + ",";
            value = value.TrimEnd(',');
            dict.Add("place", value);
        }
        //通过HttpUtility.UrlEncode()转码，防止再cookie中乱码
        if (SeatText.Text != "")
            dict.Add("seat", HttpUtility.UrlEncode(SeatText.Text));
        if (DescribeText.Text != "")
            dict.Add("describe", HttpUtility.UrlEncode(DescribeText.Text));
        //将字典插入cookie
        HttpCookie cookie = HttpContext.Current.Response.Cookies["find"];
        if (cookie == null)
        {
            cookie = new HttpCookie("find");
        }
        foreach (var item in dict)
        {
            cookie.Values.Add(item.Key, item.Value);
        }
        //过期时间7天
        cookie.Expires = DateTime.Now.AddDays(7);
        HttpContext.Current.Response.Cookies.Add(cookie);
        Response.Redirect("/Find.aspx");
    }
    //分类表
    protected void Type_But_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Table.aspx?name=type");
    }
    //地点表
    protected void Place_But_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Table.aspx?name=place");
    }

    protected void AddOne_But_Click(object sender, EventArgs e)
    {
        Response.Redirect("/AddOne.aspx");
    }
}