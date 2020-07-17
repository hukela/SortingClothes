using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using SQLdata;

public partial class One : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //设定文件处理的当前路径
            Directory.SetCurrentDirectory(ConfigurationManager.ConnectionStrings["path"].ConnectionString);
            //建立页面
            BuileView();
        }
    }
    //建立页面
    private void BuileView()
    {
        string id = Request.QueryString["id"];
        ClothesId.Text = id;
        Clothes clothes = new Clothes();
        DataTable data = clothes.ReadOne(int.Parse(id));
        MainImg.ImageUrl = "/clothes_img/" + id + ".jpg?" + DateTime.Now;
        if (data.Rows[0]["heng"].ToString() == "shu")
            MainImg.CssClass = "MainImg_shu";
        else
            MainImg.CssClass = "MainImg_heng";
        switch (data.Rows[0]["condition"].ToString())
        {
            case "a":
                ConditionLabel.Text = "干净的";
                break;
            case "b":
                ConditionLabel.Text = "需要清洗";
                break;
            case "c":
                ConditionLabel.Text = "使用中";
                break;
            case "d":
                ConditionLabel.Text = "其它";
                break;
        }
        switch (data.Rows[0]["grade"].ToString())
        {
            case "1":
                GradeLabel.Text = "很少穿";
                break;
            case "2":
                GradeLabel.Text = "一般";
                break;
            case "3":
                GradeLabel.Text = "经常穿";
                break;
        }
        PlaceLabe.Text = data.Rows[0]["place"].ToString();
        TypeLabel.Text = data.Rows[0]["type"].ToString();
        SeatLabel.Text = data.Rows[0]["seat"].ToString();
        DescribeLabel.Text = data.Rows[0]["describe"].ToString();
        //指示RadioButtonList的已选选项
        ConditionLabel.ToolTip = data.Rows[0]["condition"].ToString();
        GradeLabel.ToolTip = data.Rows[0]["grade"].ToString();
        PlaceLabe.ToolTip = data.Rows[0]["place_id"].ToString();
        TypeLabel.ToolTip = data.Rows[0]["type_id"].ToString();
    }
    //自动上传图片
    protected void ImgUpload_Click(object sender, EventArgs e)
    {
        if (ReUpload.HasFile)
        {
            int id = int.Parse(Request.QueryString["id"]);
            //String PictureName = ImgFileUpload.PostedFile.FileName;   //客户端文件路径
            //FileInfo PictureFile = new FileInfo(PictureName);         //创建fileInfo类
            string fileType = ReUpload.PostedFile.ContentType;         //请选择jpg文件格式
            if (fileType != "image/jpeg")
            {
                ImgMessage.Visible = true;
                ImgMessage.Text = "请选择jpg文件格式";
                return;
            }
            //将相对路径转化为绝对路径
            string WebFilePath = Server.MapPath("clothes_img/" + id);
            //删除临时储存
            if (File.Exists(WebFilePath + ".linshi.jpg"))
            {
                File.Delete(WebFilePath + ".linshi.jpg");
            }
            //上传保存
            try
            {
                //.linshi临时储存，保存时再转为正式储存
                ReUpload.SaveAs(WebFilePath + ".linshi.jpg");
                ImgSit("clothes_img/" + id + ".linshi.jpg", WebFilePath + ".linshi.jpg");
                //打开图片，旋转，关闭上传
                TurnLift.Visible = true;
                TurnRight.Visible = true;
                //设置已上传图片
                ReUpload.ToolTip = "true";
            }
            catch
            {
                ImgMessage.Visible = true;
                ImgMessage.Text = "上传失败";
            }
        }
        else
        {
            ImgMessage.Visible = true;
            ImgMessage.Text = "没有找到文件";
        }
    }
    //自动设置图片展示的长宽，并设置imgurl
    private void ImgSit(string url, string WebFilePath)
    {
        //加上时间版本号，防止手机浏览器调用缓存的老图片
        MainImg.ImageUrl = url + "?" + DateTime.Now;
        if (Imgerect(WebFilePath))
        {
            MainImg.CssClass = "MainImg_shu";
            ImgMessage.Visible = true;
            ImgMessage.Text = "竖直";
        }
        else
        {
            MainImg.CssClass = "MainImg_heng";
            ImgMessage.Visible = true;
            ImgMessage.Text = "横放";
        }
    }
    //判断图片是否直立
    private bool Imgerect(string WebFilePath)
    {
        Bitmap pic = new Bitmap(WebFilePath);
        int width = pic.Size.Width;   // 图片的宽度
        int height = pic.Size.Height;   // 图片的高度
        pic.Dispose();  //释放资源
        if (height > width)
            return true;
        else
            return false;
    }
    //返回主页面
    protected void Return_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Find.aspx");
    }
    //将图片向左旋转
    protected void TurnLift_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int id = int.Parse(Request.QueryString["id"]);
        string url = "clothes_img/" + id + ".linshi.jpg";
        //将相对路径转化为绝对路径
        string WebFilePath = Server.MapPath(url);
        //打开文件
        Bitmap pic = new Bitmap(WebFilePath);
        //旋转图片
        pic.RotateFlip(RotateFlipType.Rotate270FlipNone);
        pic.Save(WebFilePath);
        pic.Dispose();  //释放资源
        //加上时间版本号，防止手机浏览器调用缓存的老图片
        MainImg.ImageUrl = url + "?" + DateTime.Now;
        ImgSit(url, WebFilePath);
    }
    //将图片向右旋转
    protected void TurnRight_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int id = int.Parse(Request.QueryString["id"]);
        string url = "clothes_img/" + id + ".linshi.jpg";
        //将相对路径转化为绝对路径
        string WebFilePath = Server.MapPath(url);
        //打开文件
        Bitmap pic = new Bitmap(WebFilePath);
        //旋转图片
        pic.RotateFlip(RotateFlipType.Rotate90FlipNone);
        pic.Save(WebFilePath);
        pic.Dispose();  //释放资源
        //加上时间版本号，防止手机浏览器调用缓存的老图片
        MainImg.ImageUrl = url + "?" + DateTime.Now;
        ImgSit(url, WebFilePath);
    }

    protected void Change_Click(object sender, EventArgs e)
    {
        ConditionRadio.Visible = true;
        GradeRadio.Visible = true;
        PlaceRadio.Visible = true;
        TypeRadio.Visible = true;
        SeatText.Visible = true;
        DescribeText.Visible = true;
        Restore.Visible = true;
        Save.Visible = true;
        labelFileUpload.Visible = true;
        Delete.Visible = false;
        Return.Visible = false;
        Change.Visible = false;
        SeatLabel.Visible = false;
        DescribeLabel.Visible = false;
        TypeList type = new TypeList();
        DataTable data = type.ReadAll();
        TypeRadio.DataSource = data;
        TypeRadio.DataTextField = "name";
        TypeRadio.DataValueField = "id";
        TypeRadio.DataBind();
        PlaceList place = new PlaceList();
        data = place.ReadAll();
        PlaceRadio.DataSource = data;
        PlaceRadio.DataTextField = "name";
        PlaceRadio.DataValueField = "id";
        PlaceRadio.DataBind();
        //设置默认选择项
        ConditionRadio.SelectedValue = ConditionLabel.ToolTip;
        GradeRadio.SelectedValue = GradeLabel.ToolTip;
        PlaceRadio.SelectedValue = PlaceLabe.ToolTip;
        TypeRadio.SelectedValue = TypeLabel.ToolTip;
        SeatText.Text = SeatLabel.Text;
        DescribeText.Value = DescribeLabel.Text;
    }

    protected void Restore_Click(object sender, EventArgs e)
    {
        ConditionRadio.Visible = false;
        GradeRadio.Visible = false;
        PlaceRadio.Visible = false;
        TypeRadio.Visible = false;
        SeatText.Visible = false;
        DescribeText.Visible = false;
        Restore.Visible = false;
        Save.Visible = false;
        labelFileUpload.Visible = false;
        Delete.Visible = true;
        Return.Visible = true;
        Change.Visible = true;
        SeatLabel.Visible = true;
        DescribeLabel.Visible = true;
        BuileView();
        if (ReUpload.ToolTip == "true")
        {
            int id = int.Parse(Request.QueryString["id"]);
            //删除临时图片
            if (File.Exists("clothes_img/" + id + ".linshi.jpg"))
                File.Delete("clothes_img/" + id + ".linshi.jpg");
            ImgMessage.Visible = false;
            TurnLift.Visible = false;
            TurnRight.Visible = false;
        }
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["id"]);
        char condition = ConditionRadio.SelectedValue[0];
        byte grade = byte.Parse(GradeRadio.SelectedValue);
        int placeID = int.Parse(PlaceRadio.SelectedValue);
        int typeID = int.Parse(TypeRadio.SelectedValue);
        string seat = SeatText.Text;
        string describe = DescribeText.Value;
        Clothes clothes = new Clothes();
        bool r = clothes.UpdateOne(id, typeID, placeID, seat, describe, grade, condition);
        try
        {
            if (ReUpload.ToolTip == "true")
            {
                //删除原有图片
                if (File.Exists("clothes_img/" + id + ".jpg"))
                    File.Delete("clothes_img/" + id + ".jpg");
                File.Move("clothes_img/" + id + ".linshi.jpg", "clothes_img/" + id + ".jpg");
            }
        }
        catch
        {
            if (r)
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('数据修改成功但图片修改失败');", true);
            else
                ClientScript.RegisterStartupScript(GetType(), "", "window.alert('修改失败');", true);
        }
        if (r)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('修改成功');", true);
            //返回正常页面
            Restore_Click(null, null);
        }
        else
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('数据修改失败但图片修改成功');", true);
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.QueryString["id"]);
        //删除原有图片
        if (File.Exists("clothes_img/" + id + ".jpg"))
            File.Delete("clothes_img/" + id + ".jpg");
        Clothes clothes = new Clothes();
        if (clothes.DeleteOne(id))
        {
            Response.Write("<script> alert('删除成功')" +
                "var domain = 'http://' + window.location.host;" +
                "window.location.href = domain + '/addone.aspx';");
        }
    }
}