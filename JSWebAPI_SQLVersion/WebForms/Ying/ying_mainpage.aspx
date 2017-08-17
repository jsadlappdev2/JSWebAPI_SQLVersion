<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ying_mainpage.aspx.cs" Inherits="JSWebAPI_SQLVersion.WebForms.Ying.ying_mainpage" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ying App Backend System</title>
     <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="JS/menu/menu.css" rel="stylesheet" />
    <script src="JS/jquery.1.4.2-min.js"></script>
    <script src="JS/menu/jquery.menu.js"></script>
    <script src="JS/Showtime.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 78px;
            color: #003366;
            font-size: x-large;
        }
        .auto-style2 {
            width: 132px;
            height: 741px;
        }
        .auto-style3 {
            height: 741px;
            width: 1278px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="1560" height="960" border="0" align="center" cellpadding="0" cellspacing="0" class="BoxTable">
            <tr>
                <td colspan="2" class="auto-style1" bgcolor="#F7F7F7">
                    <strong>Welcome to Ying App Backend System!</strong></td>
            </tr>

            <tr>
               

                <td bgcolor="#F7F7F7" valign="top" class="auto-style2">
                     <asp:Menu ID="Menu1" runat="server" BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#990000" StaticSubMenuIndent="10px">
                         <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                         <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                         <DynamicMenuStyle BackColor="#FFFBD6" />
                         <DynamicSelectedStyle BackColor="#FFCC66" />
                         <Items>
                             <asp:MenuItem Text="User Manage" Value="1"></asp:MenuItem>
                             <asp:MenuItem Text="URLs Mange" Value="2" NavigateUrl="~/WebForms/Ying/URLs.aspx"></asp:MenuItem>
                         </Items>
                         <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                         <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                         <StaticSelectedStyle BackColor="#FFCC66" />
                     </asp:Menu>
                     </td>
                  <td  class="BoxBody" valign="top">
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" class="BoxFooter">Y&J Copyright@2017-2018  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           [Version 1.0] </asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
