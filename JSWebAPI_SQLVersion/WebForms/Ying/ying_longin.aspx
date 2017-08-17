<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ying_longin.aspx.cs" Inherits="JSWebAPI_SQLVersion.WebForms.Ying.ying_longin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 232px;
        }
        .auto-style2 {
            color: #0000FF;
            font-size: xx-large;
        }
        .auto-style3 {
            width: 232px;
            text-align: right;
            height: 52px;
        }
        .auto-style4 {
            height: 37px;
        }
        .auto-style5 {
            width: 232px;
            text-align: right;
            height: 37px;
        }
        .auto-style6 {
            height: 52px;
        }
        .auto-style7 {
            margin-right: 11px;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <p>
        <br />
    </p>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">Welcome to Ying App Backend</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style5">Username:</td>
                <td class="auto-style4">
                    <asp:TextBox ID="username" runat="server" Height="32px" Width="240px"></asp:TextBox>
                </td>
                <td class="auto-style4"></td>
            </tr>
            <tr>
                <td class="auto-style6"></td>
                <td class="auto-style3">Password:</td>
                <td class="auto-style6">
                    <asp:TextBox ID="password" runat="server" Height="32px" Width="240px"></asp:TextBox>
                </td>
                <td class="auto-style6"></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Height="40px" Text="Button" Width="101px" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="auto-style7" Text="Reset" Width="114px" Height="40px" OnClick="Button2_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Label ID="msg" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
