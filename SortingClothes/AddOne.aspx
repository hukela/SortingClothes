<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddOne.aspx.cs" Inherits="AddOne" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--viewport 用来自适应屏幕-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=no" />
    <title>添加衣服</title>
    <style type="text/css">
        html{font-size:20px;}
        .title{
            border:2px solid black;
            text-align:center;
            font-size:30px;
        }
        /*#a_upload {
            position: relative;
            background: #fafafa;
            border: 1px solid #ddd;
            *border-radius: 4px;
            overflow: hidden;
            display: block;
            margin:auto;
            height:200px;
            width:200px;
            background-image:url(/img/photograph.png)
        }*/
        #divImgSelect{
            position: relative;
            border: 1px solid black;
            overflow: hidden;
            display: block;
            margin:auto;
            height:250px;
            width:250px;
            background-image:url(/img/photograph.png);
            background-size:250px 250px;
        }
        #ImgSelect {
            position: absolute;
            font-size: 200px;
            right: 0;
            top: 0;
            opacity: 0;
            filter: alpha(opacity=0);
            z-index:2;
        }
        #ImgMessage{
            display:block;
            margin:10px auto;
            border:1px solid red;
            text-align:center;
        }
        #ImgShow{
            display:block;
            margin:auto;
        }
        #TurnLift,#TurnRight{
            width:60px;
            height:60px;
            margin:15px 15px 0 15px;
            padding:5px 30px;
            border:1px solid black;
        }
        #TurnLift{
            /*这5条只有一个作用，水平翻转图片*/
            -moz-transform: scaleX(-1);
            -webkit-transform: scaleX(-1); 
            -o-transform: scaleX(-1);
            transform: scaleX(-1);
            filter: fliph; /*IE*/
        }
        #TurnRight{
            float:right;
        }
        #Return, #Save{
            height:50px;
            width:145px;
            margin:15px 0 0 15px;
            font-size:30px;
        }
        .But_div{
             border:1px solid black;
             padding-bottom:15px;
             margin-top:10px;
        }
        #SeatText{
            width:100%;
            font-size:20px;
        }
        #DescribeText{
            font-size:20px;
        }
    </style>
    <script type="text/javascript">
        //判断必填字段
        function CheckIsSelected()
        {
            var rbl = document.getElementById("ConditionRadio");
            var radio = rbl.getElementsByTagName("input");
            var isSelect = false;
            for (var i = 0; i < radio.length; i++)
            {
                if (radio[i].checked)
                {
                    isSelect = true;
                    break;
                }
            }
            if (!isSelect)
            {
                alert("请选择状态");
                return false;
            }

            rbl = document.getElementById("GradeRadio");
            radio = rbl.getElementsByTagName("input");
            isSelect = false;
            for (var i = 0; i < radio.length; i++)
            {
                if (radio[i].checked)
                {
                    isSelect = true;
                    break;
                }
            }
            if (!isSelect)
            {
                alert("请选常用等级");
                return false;
            }

            rbl = document.getElementById("PlaceRadio");
            radio = rbl.getElementsByTagName("input");
            isSelect = false;
            for (var i = 0; i < radio.length; i++)
            {
                if (radio[i].checked)
                {
                    isSelect = true;
                    break;
                }
            }
            if (!isSelect)
            {
                alert("请选择地点");
                return false;
            }

            rbl = document.getElementById("TypeRadio");
            radio = rbl.getElementsByTagName("input");
            isSelect = false;
            for (var i = 0; i < radio.length; i++)
            {
                if (radio[i].checked)
                {
                    isSelect = true;
                    break;
                }
            }
            if (!isSelect)
            {
                alert("请选择分类");
                return false;
            }

            rbl = document.getElementById("ImgShow")
            if (rbl == undefined)
            {
                alert("还没有选择图片")
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div class="title">新id: <asp:Label ID="NewID" runat="server" Text="Label" /></div>
    <hr />
    <asp:Image ID="ImgShow" runat="server" Visible="false" />
    <!--选择图片上传-->
    <div id="divImgSelect" runat="server">
        <asp:FileUpload ID="ImgSelect" runat="server" capture="camera" accept="image/*" onchange="javascript:__doPostBack('ImgUpload','')" />
    </div>
    <!--真实活见鬼了，明明一摸一样的onchange，下面的这个就是运行不了-->
    <!--<asp:FileUpload runat="server" capture="camera" accept="image/*" οnchange="javascript:__doPostBack('ImgUpload','')" />-->
    <!--为了自动上传而建立的辅助控件-->
    <asp:LinkButton ID="ImgUpload" runat="server" OnClick="ImgUpload_Click"></asp:LinkButton>
    <!--图片信息和上传是否成功-->
    <asp:Label ID="ImgMessage" runat="server" Text="Label" Visible="false" />
    <!--两个旋转图片的按键-->
    <asp:ImageButton ID="TurnLift" runat="server" ImageUrl="~/img/arrows.jpg" Visible="false" OnClick="TurnLift_Click" />
    <asp:ImageButton ID="TurnRight" runat="server" ImageUrl="~/img/arrows.jpg" Visible="false" OnClick="TurnRight_Click" />
    <hr /><br />
    <div>状态:</div><hr />
    <asp:RadioButtonList ID="ConditionRadio" runat="server" RepeatColumns="4" Width="100%">
        <asp:ListItem Value="a">干净的</asp:ListItem>
        <asp:ListItem Value="b">待洗的</asp:ListItem>
        <asp:ListItem Value="c">使用中</asp:ListItem>
        <asp:ListItem Value="d">其它</asp:ListItem>
    </asp:RadioButtonList>
    <br /><div>常用等级:</div><hr />
    <asp:RadioButtonList ID="GradeRadio" runat="server" RepeatColumns="3" Width="100%">
        <asp:ListItem Value="3" Text="经常穿" />
        <asp:ListItem Value="2" Text="一般" />
        <asp:ListItem Value="1" Text="很少穿" />
    </asp:RadioButtonList>
    <br /><div>地点:</div><hr />
    <asp:RadioButtonList ID="PlaceRadio" runat="server" RepeatColumns="2" Width="100%">
    </asp:RadioButtonList>
    <br /><div>分类:</div><hr />
    <asp:RadioButtonList ID="TypeRadio" runat="server" RepeatColumns="3" Width="100%">
    </asp:RadioButtonList>
    <br /><div>详细位置:</div><hr />
    <asp:TextBox ID="SeatText" runat="server"></asp:TextBox>
    <br /><div>备注:</div><hr />
    <textarea id="DescribeText" runat="server" cols="28" rows="3"></textarea>
    <div class="But_div">
        <asp:Button ID="Return" runat="server" Text="返回" OnClick="Return_Click" />
        <asp:Button ID="Save" runat="server" Text="保存" OnClick="Save_Click" OnClientClick="return CheckIsSelected()" />
    </div>
</form>
</body>
</html>
