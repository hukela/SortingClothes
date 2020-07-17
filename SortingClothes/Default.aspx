<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--viewport 用来自适应屏幕-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=no" />
    <title>主页</title>
    <style type="text/css">
        html{font-size:20px;}
        .title{
            border:2px solid black;
            text-align:center;
            font-size:30px;
        }
        #Find_But, #AddOne_But{
            height:50px;
            width:310px;
            margin:15px 15px 0 15px;
            font-size:30px;
        }
        #Type_But, #Place_But{
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
        #SeatText,#DescribeText{font-size:20px;}
        #SeatText{width:240px;}
        #DescribeText{width:280px;}
    </style>
</head>
<body>
<form id="form1" runat="server">

    <div class="title">衣物数据库管理器</div>
    
    <!--查找条件-->
    <br />分类：<hr />
    <asp:CheckBoxList ID="Type_CBL" runat="server" RepeatColumns="3" Width="100%">
    </asp:CheckBoxList>
    <br />状态：<hr />
    <asp:CheckBoxList ID="Condition_CBL" runat="server" RepeatColumns="4" Width="100%">
        <asp:ListItem Value="a">干净的</asp:ListItem>
        <asp:ListItem Value="b">待洗的</asp:ListItem>
        <asp:ListItem Value="c">使用中</asp:ListItem>
        <asp:ListItem Value="d">其它</asp:ListItem>
    </asp:CheckBoxList>
    <br />常用等级：<hr />
    <asp:CheckBoxList ID="Grade_CBL" runat="server" RepeatColumns="3" Width="100%">
        <asp:ListItem Value="3">经常穿</asp:ListItem>
        <asp:ListItem Value="2">一般</asp:ListItem>
        <asp:ListItem Value="1">很少穿</asp:ListItem>
    </asp:CheckBoxList>
    <br />地点：<hr />
    <asp:CheckBoxList ID="Site_CBL" runat="server" RepeatColumns="2" Width="100%">
    </asp:CheckBoxList>
    <br />详细位置：<asp:TextBox ID="SeatText" runat="server" /><hr />
    <br />备注：<asp:TextBox ID="DescribeText" runat="server" /><hr />

    <!--添加按键-->
    <div class="But_div">
        <asp:Button ID="Find_But" runat="server" Text="查找衣服" OnClick="Find_But_Click" />
        <asp:Button ID="AddOne_But" runat="server" Text="添加衣服" OnClick="AddOne_But_Click" />
        <asp:Button ID="Type_But" runat="server" Text="分类" OnClick="Type_But_Click" />
        <asp:Button ID="Place_But" runat="server" Text="地点" OnClick="Place_But_Click" />
    </div>
</form>
</body>
</html>
