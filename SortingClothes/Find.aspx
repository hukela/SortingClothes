<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Find.aspx.cs" Inherits="Find" EnableEventValidation="False" %>
<!--EnableEventValidation取消回调参数验证，方便__doPostBack()传参-->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--viewport 用来自适应屏幕-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=no" />
    <title>查找页面</title>
    <style type="text/css">
        html{font-size:20px;}
        .title{
            border:2px solid black;
            text-align:center;
            font-size:30px;
            margin-bottom:15px;
        }
        #Return{
            height:50px;
            width:310px;
            margin:15px 15px 0 15px;
            font-size:30px;
        }
        .But_div{
             border:1px solid black;
             padding-bottom:15px;
             margin-top:10px;
        }
        table{
            border:1px solid black;
            width:100%;
        }
        td{
            border:1px solid black;
        }
        .img_heng{
            display:inline;
            height:124px;
            width:165px;
            border:0;
            margin:auto;
            padding:0;
            line-height:220px;
        }
        .img_shu{
            display:inline;
            height:220px;
            width:165px;
            border:0;
            margin:auto;
            padding:0;
        }
        a{
            color:black
        }
        .td_img{
            text-align:center;
            width:165px;
        }
        .td_red{background-color:red;}
        .td_red a input{
            border:1px solid black;
            font-size:20px;
            background-color:white;
            color:red;
        }
        .td_yellow a input{
            border:1px solid black;
            color:#DAA520;
            font-size:20px;
            background-color:white;
        }
        .td_green a input{
            border:1px solid black;
            color:green;
            font-size:20px;
            background-color:white;
        }
        .td_black{}
    </style>
</head>
<body>
<form id="form1" runat="server">

    <div class="title">查找结果:<%=Count %></div>

    <!--数据表-->
    <%=GridView %>

    <!--<a href="http://www.baidu.com"><table>
        <tr>
            <td class="auto-style3">id:01</td>
            <td class="auto-style2">保暖内衣</td>
            <td rowspan="4" class="td_img">
                <img class="img_shu" src="clothes_img/2.jpg" />
            </td>
        </tr>
        <tr>
            <td>经常穿</td>
            <td class="td_red">
                <a><input onclick="__doPostBack('ChangeCondition','15')" type="button" value="待清洗" /></a>
            </td>
        </tr>
        <tr>
            <td colspan="2">家中衣柜</td>
        </tr>
        <tr>
            <td colspan="2">下面第三摞</td>
        </tr>
    </table></a>-->

    <!--为了在页面上直接修改衣服状态功能而建立的辅助控件-->
    <asp:LinkButton ID="ChangeCondition" runat="server" OnClick="ChangeCondition_Click"></asp:LinkButton>
    <div class="But_div">
        <asp:Button ID="Return" runat="server" Text="返回主页面" OnClick="Return_Click"  />
    </div>
</form>
</body>
</html>