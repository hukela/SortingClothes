using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using SQLdata;

public partial class AddOne : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设定文件处理的当前路径
            Directory.SetCurrentDirectory(ConfigurationManager.ConnectionStrings["path"].ConnectionString);
            //建立页面
            BuildView();
        }
    }
    //建立页面
    private void BuildView()
    {
        Clothes clothes = new Clothes();
        NewID.Text = clothes.GetNewID().ToString();
        DataTable data = null;
        TypeList type = new TypeList();
        data = type.ReadAll();
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
    }
    //自动上传图片
    protected void ImgUpload_Click(object sender, EventArgs e)
    {
        if (ImgSelect.HasFile)
        {
            int id = int.Parse(NewID.Text);
            //String PictureName = ImgFileUpload.PostedFile.FileName;   //客户端文件路径
            //FileInfo PictureFile = new FileInfo(PictureName);         //创建fileInfo类
            string fileType = ImgSelect.PostedFile.ContentType;         //请选择jpg文件格式
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
                ImgSelect.SaveAs(WebFilePath + ".linshi.jpg");
                ImgSit("clothes_img/" + id + ".linshi.jpg", WebFilePath + ".linshi.jpg");
                //打开图片，旋转，关闭上传
                ImgShow.Visible = true;
                divImgSelect.Visible = false;
                TurnLift.Visible = true;
                TurnRight.Visible = true;
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
        ImgShow.ImageUrl = url + "?" + DateTime.Now;
        if (Imgerect(WebFilePath))
        {
            ImgShow.Width = 300;
            ImgShow.Height = 400;
            ImgMessage.Visible = true;
            ImgMessage.Text = "竖直";
        }
        else
        {
            ImgShow.Width = 300;
            ImgShow.Height = 225;
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
        Response.Redirect("/");
    }
    //将图片向左旋转
    protected void TurnLift_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int id = int.Parse(NewID.Text);
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
        ImgShow.ImageUrl = url + "?" + DateTime.Now;
        ImgSit(url, WebFilePath);
    }
    //将图片向右旋转
    protected void TurnRight_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int id = int.Parse(NewID.Text);
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
        ImgShow.ImageUrl = url + "?" + DateTime.Now;
        ImgSit(url, WebFilePath);
    }
    //保存按键
    protected void Save_Click(object sender, EventArgs e)
    {
        int typeID = int.Parse(TypeRadio.SelectedValue);
        int placeID = int.Parse(PlaceRadio.SelectedValue);
        string seat = SeatText.Text;
        string describe = DescribeText.Value;
        byte grade = byte.Parse(GradeRadio.SelectedValue);
        char condition = ConditionRadio.Text[0];
        Clothes clothes = new Clothes();
        if (clothes.AddOne(typeID, placeID, seat, describe, grade, condition))
        {
            //将临时图片转正
            string id = NewID.Text;
            File.Move("clothes_img/" + id + ".linshi.jpg", "clothes_img/" + id + ".jpg");
            //Response.Write("<script>alert(window.location.host)</script>");
            //是否要继续添加？
            Response.Write("<script>" +
                "var domain = 'http://' + window.location.host;" +
                "if (confirm('添加成功，是否要继续添加？'))" +
                    "{window.location.href = domain + '/addone.aspx';}" +
                "else" +
                    "{window.location.href = domain;}" +
                "</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "", "window.alert('添加失败');", true);
        }
    }
}