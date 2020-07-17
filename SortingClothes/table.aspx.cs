using System;
using System.Data;
using System.Web.UI.WebControls;
using SQLdata;

public partial class Table : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BuildView();
        }
    }
    //页面传参
    protected string title;
    //建立页面
    void BuildView()
    {
        //判断并获取数据源
        DataTable data = null;
        string table_name = Request.QueryString["name"];
        switch (table_name)
        {
            case "type":
                title = "衣服分类";
                TypeList type = new TypeList();
                data = type.ReadCount();
                break;
            case "place":
                title = "存放地点";
                PlaceList place = new PlaceList();
                data = place.ReadCount();
                break;
        }
        //建立表格
        GridView.DataSource = data;
        GridView.DataBind();
        //添加是否删除的确认弹窗
        Button delBtn;
        for (int i = 0; i < GridView.Rows.Count; i++)
        {
            delBtn = (Button)GridView.Rows[i].FindControl("Delete");
            delBtn.Attributes.Add("onclick", "javascript:if (confirm('确定要删除吗,将会删除该分类下所有衣服数据')) { return true; } else { return false; }");
        }
    }
    //保存重命名
    protected void Save_Click(object sender, EventArgs e)
    {
        Button but = (Button)sender;
        GridViewRow row = (GridViewRow)but.NamingContainer;
        //获取当前行id
        int id = int.Parse(row.Cells[0].Text);
        TextBox textBox = (TextBox)row.FindControl("ReName_text");
        //获取要修改的新名字
        string newName = textBox.Text.Trim();
        //判断操作对象并执行操作
        string table_name = Request.QueryString["name"];
        bool r = false;
        if (newName != "")
            switch (table_name)
            {
                case "type":
                    TypeList type = new TypeList();
                    r = type.UpdateOne(id, newName);
                    break;
                case "place":
                    PlaceList place = new PlaceList();
                    r = place.UpdateOne(id, newName);
                    break;
            }
        BuildView();
        if (r)
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('修改成功');", true);
        else
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('修改失败');", true);
    }
    //删除按键
    protected void Delete_Click(object sender, EventArgs e)
    {
        //获取当前行id
        Button but = (Button)sender;
        GridViewRow row = (GridViewRow)but.NamingContainer;
        int id = int.Parse(row.Cells[0].Text);
        //判断操作对象并执行操作
        string table_name = Request.QueryString["name"];
        //受影响行数
        int r = 0;
        switch (table_name)
        {
            case "type":
                TypeList type = new TypeList();
                r = type.DeleteOne(id);
                break;
            case "place":
                PlaceList place = new PlaceList();
                r = place.DeleteOne(id);
                break;
        }
        BuildView();
        if (r == int.Parse(row.Cells[2].Text))
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('删除成功');", true);
        else
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('删除失败');", true);
    }
    //保存按键
    protected void SaveNew_Click(object sender, EventArgs e)
    {
        //判断操作对象并执行操作
        string table_name = Request.QueryString["name"];
        bool r = false;
        string newName = NewName.Text.Trim();
        if (newName != "")
            switch (table_name)
            {
                case "type":
                    TypeList type = new TypeList();
                    r = type.AddOne(newName);
                    break;
                case "place":
                    PlaceList place = new PlaceList();
                    r = place.AddOne(newName);
                    break;
            }
        BuildView();
        if (r)
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('添加成功');", true);
        else
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('添加失败');", true);
    }
    //返回主页面
    protected void Return_Click(object sender, EventArgs e)
    { Response.Redirect("/"); }
}