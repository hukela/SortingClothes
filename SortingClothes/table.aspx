<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Table.aspx.cs" Inherits="Table" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--viewport 用来自适应屏幕-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=no" />
    <title>表格修改</title>
    <style type="text/css">
        html{font-size:20px;}
        .title{
            border:2px solid black;
            text-align:center;
            font-size:30px;
            margin-bottom:15px;
        }
        table{width:100%;}
        table .input_but{
            width:65px;
            font-size:20px;
            display:block;
            margin:auto;
        }
        table .input_none{
            width:65px;
            font-size:20px;
            display:none;
            margin:auto;
        }
        table .input_text{
            width:120px;
            font-size:20px;
            display:none;
        }
        #NewName{
            width:310px;
            font-size:20px;
            display:none;
            margin:auto;
            margin-top:15px;
        }
        #AddNew,#Return{
            height:50px;
            width:310px;
            margin:15px 15px 0 15px;
            font-size:30px;
        }
        #SaveNew,#RestoreNew{
            height:50px;
            width:145px;
            margin:15px 0 0 15px;
            font-size:30px;
            display:none;
            float:left;
        }
        .But_div{
             border:1px solid black;
             padding-bottom:15px;
             margin-top:10px;
        }
    </style>
    <script>
        //添加新的
        function AddNew_Click()
        {
            document.getElementById("NewName").style.display = "block";
            document.getElementById("SaveNew").style.display = "block";
            document.getElementById("RestoreNew").style.display = "block";
            document.getElementById("AddNew").style.display = "none";
        }
        //撤销上一个
        function RestoreNew_Click()
        {
            document.getElementById("NewName").style.display = "none";
            document.getElementById("SaveNew").style.display = "none";
            document.getElementById("RestoreNew").style.display = "none";
            document.getElementById("AddNew").style.display = "block";
        }
        //重命名
        function ReName_Click(row)
        {
            document.getElementById("ReName_" + row).style.display = "none";
            document.getElementById("GridView_Save_" + row).style.display = "block";
            document.getElementById("GridView_Delete_" + row).style.display = "none";
            document.getElementById("Restore_" + row).style.display = "block";
            document.getElementById("ReName_div_" + row).style.display = "none";
            document.getElementById("GridView_ReName_text_" + row).style.display = "block";
            document.getElementById("GridView_ReName_text_" + row).value = document.getElementById("ReName_div_" + row).innerText;
        }
        //撤销上一个
        function Restore_Click(row)
        {
            document.getElementById("ReName_" + row).style.display = "block";
            document.getElementById("GridView_Save_" + row).style.display = "none";
            document.getElementById("GridView_Delete_" + row).style.display = "block";
            document.getElementById("Restore_" + row).style.display = "none";
            document.getElementById("ReName_div_" + row).style.display = "block";
            document.getElementById("GridView_ReName_text_" + row).style.display = "none";
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <div class="title"><%=title %></div>
    <!--表格-->
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="id" DataField="id" />
            <asp:TemplateField>
                <HeaderTemplate>名称</HeaderTemplate>
                <ItemTemplate>
                    <div id="ReName_div_<%#Container.DisplayIndex %>"><%#Eval("name") %></div>
                    <asp:TextBox ID="ReName_text" runat="server" CssClass="input_text"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="数量" DataField="count" />
            <asp:TemplateField>
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <input id="ReName_<%#Container.DisplayIndex %>" type="button" class="input_but" value="修改" onclick="ReName_Click(<%#Container.DisplayIndex %>)" />
                    <asp:Button ID="Save" runat="server" Text="保存" CssClass="input_none" OnClick="Save_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <asp:Button ID="Delete" runat="server" Text="删除" CssClass="input_but" OnClick="Delete_Click" />
                    <input type="button" id="Restore_<%#Container.DisplayIndex %>" value="还原" class="input_none" onclick="Restore_Click(<%#Container.DisplayIndex %>)" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="But_div">
        <input type="button" id="AddNew" value="添加新的" onclick="AddNew_Click()" />
        <asp:TextBox ID="NewName" runat="server"></asp:TextBox>
        <asp:Button ID="SaveNew" runat="server" Text="保存" OnClick="SaveNew_Click" />
        <input type="button" id="RestoreNew" runat="server" value="还原" onclick="RestoreNew_Click()" />
        <asp:Button ID="Return" runat="server" Text="返回主页面" OnClick="Return_Click" />
    </div>
</form>
</body>
</html>
