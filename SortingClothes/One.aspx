<%@ Page Language="C#" AutoEventWireup="true" CodeFile="One.aspx.cs" Inherits="One" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--viewport 用来自适应屏幕-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=no" />
    <title>查看衣服</title>
    <style type="text/css">
        html{font-size:20px;}
        .MainImg_shu{
            height:400px;
            width:300px;
            display:block;
            margin:auto;
        }
        .MainImg_heng{
            height:230px;
            width:310px;
            display:block;
            margin:auto;
        }
        .title{
            text-align:center;
            width:100px;
            height:30px;
            line-height:30px;
            border:1px solid black;
        }
        #labelFileUpload{
            text-align:center;
            float:right;
            font-size:20px;
            width:110px;
            height:30px;
            line-height:30px;
            border:1px solid black;
        }
        #textarea,#TextBox1{
            font-size:20px;
        }
        #ReUpload{display:none}
        #ImgMessage{
            display:block;
            margin:10px auto;
            border:1px solid red;
            text-align:center;
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
        #TurnRight{float:right;}
        #SeatText{
            width:100%;
            font-size:20px;
            display:block;
        }
        #SeatLabel{display:block}
        #DescribeText{ font-size:20px;}
        .But_div{
             border:1px solid black;
             padding-bottom:15px;
             margin-top:10px;
        }
        #Return{
            height:50px;
            width:310px;
            margin:15px 15px 0 15px;
            font-size:30px;
        }
        #Restore, #Change, #Save, #Delete{
            height:50px;
            width:145px;
            margin:15px 0 0 15px;
            font-size:30px;
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
            return true;
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <label id="labelFileUpload" runat="server" for="ReUpload" visible="false">重新上传</label>
    <!--Upload被隐藏,通过label调用,通过ToolTip来指示是否上传了图片-->
    <asp:FileUpload ID="ReUpload" ToolTip="false" runat="server" capture="camera" accept="image/*" onchange="javascript:__doPostBack('ImgUpload','')" />
    <!--为了自动上传而建立的辅助控件-->
    <asp:LinkButton ID="ImgUpload" runat="server" OnClick="ImgUpload_Click"></asp:LinkButton>
    <div class="title">id:<asp:Label ID="ClothesId" runat="server" Text="Label" /></div>
    <hr />
    <asp:Image ID="MainImg" runat="server" />
    <!--图片信息和上传是否成功-->
    <asp:Label ID="ImgMessage" runat="server" Text="Label" Visible="false" />
    <!--两个旋转图片的按键-->
    <asp:ImageButton ID="TurnLift" runat="server" ImageUrl="~/img/arrows.jpg" Visible="false" OnClick="TurnLift_Click" />
    <asp:ImageButton ID="TurnRight" runat="server" ImageUrl="~/img/arrows.jpg" Visible="false" OnClick="TurnRight_Click" />
    <hr /><br />
    <!--这里用Label的ToolTip属性来存放value来指示RadioButtonList的已选选项-->
    <div>状态:&nbsp;<asp:Label ID="ConditionLabel" runat="server" Text="Label" /></div><hr />
    <asp:RadioButtonList ID="ConditionRadio" runat="server" RepeatColumns="4" Width="100%" Visible="false">
        <asp:ListItem Value="a">干净的</asp:ListItem>
        <asp:ListItem Value="b">待洗的</asp:ListItem>
        <asp:ListItem Value="c">使用中</asp:ListItem>
        <asp:ListItem Value="d">其它</asp:ListItem>
    </asp:RadioButtonList>
    <br /><div>常用等级:&nbsp;<asp:Label ID="GradeLabel" runat="server" Text="Label" /></div><hr />
    <asp:RadioButtonList ID="GradeRadio" runat="server" RepeatColumns="3" Width="100%" Visible="false">
        <asp:ListItem Value="3" Text="经常穿" />
        <asp:ListItem Value="2" Text="一般" />
        <asp:ListItem Value="1" Text="很少穿" />
    </asp:RadioButtonList>
    <br /><div>地点:&nbsp;<asp:Label ID="PlaceLabe" runat="server" Text="Label" /></div><hr />
    <asp:RadioButtonList ID="PlaceRadio" runat="server" RepeatColumns="2" Width="100%" Visible="false">
    </asp:RadioButtonList>
    <br /><div>分类:&nbsp;<asp:Label ID="TypeLabel" runat="server" Text="Label" /></div><hr />
    <asp:RadioButtonList ID="TypeRadio" runat="server" RepeatColumns="3" Width="100%" Visible="false">
    </asp:RadioButtonList>
    <br /><div>详细位置:</div><hr />
    <asp:Label ID="SeatLabel" runat="server" Text="Label" />
    <asp:TextBox ID="SeatText" runat="server" Visible="false" />
    <br /><div>备注:</div><hr />
    <asp:Label ID="DescribeLabel" runat="server" Text="Label" />
    <textarea id="DescribeText" runat="server" cols="28" rows="3" Visible="false"></textarea>
    <div class="But_div">
        <asp:Button ID="Return" runat="server" Text="返回" OnClick="Return_Click" />
        <asp:Button ID="Change" runat="server" Text="修改" OnClick="Change_Click" />
        <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />
        <asp:Button ID="Restore" runat="server" Text="还原" Visible="false" OnClick="Restore_Click" />
        <asp:Button ID="Save" runat="server" Text="保存" Visible="false" OnClick="Save_Click" OnClientClick="return CheckIsSelected()" />
    </div>
</form>
</body>
</html>