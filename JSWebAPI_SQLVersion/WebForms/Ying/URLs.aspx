<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="URLs.aspx.cs" Inherits="JSWebAPI_SQLVersion.WebForms.Ying.URLs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-left: 28px;
        }
        .auto-style3 {
            color: #003366;
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span class="auto-style3"><strong>Add new URLs</strong></span><br />
        =============================================================<br />
        Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="type" runat="server" Height="60px" Width="492px">
            <asp:ListItem>web</asp:ListItem>
            <asp:ListItem>audio</asp:ListItem>
            <asp:ListItem>video</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        URL:&nbsp;&nbsp; <asp:TextBox ID="url" runat="server" CssClass="auto-style1" Height="42px" Width="487px"></asp:TextBox>
        <br />
        <br />
        Description:
        <asp:TextBox ID="description" runat="server" Height="35px" Width="480px"></asp:TextBox>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Height="33px" Text="Add" Width="137px" OnClick="Button2_Click" />
&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Height="33px" Text="Refresh" Width="137px" OnClick="Button3_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="msge" runat="server" Text=""></asp:Label>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <div>
          <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" CssClass="GridStyle" DataKeyNames="id" ForeColor="#333333" GridLines="None" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Width="1285px" style="font-size: small">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                           <ItemTemplate>
                              <asp:CheckBox ID="chkSelected" runat="server" Checked="False" Visible="True" /></ItemTemplate>
                    </asp:TemplateField>

                     
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" />
                         <asp:CommandField ShowEditButton="True" HeaderText="Edit" />
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"><%# Eval("type") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="URL">
                             <EditItemTemplate>
                                <asp:TextBox ID="url" Width="100px" runat="server" Text='<%# Eval("url") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server"><%# Eval("url") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="description">
                             <EditItemTemplate>
                                <asp:TextBox ID="description" Width="150px" runat="server" Text='<%# Eval("description") %>'></asp:TextBox>
                            </EditItemTemplate>
                             <ItemTemplate>
                                <asp:Label ID="Label5" runat="server"><%# Eval("description") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="150px" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Valid">
                                <EditItemTemplate>
                                <asp:TextBox ID="valid_flag" Width="20px" runat="server" Text='<%# Eval("valid_flag") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server"><%# Eval("valid_flag") %></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                       
                        
                       
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#003366" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#003366" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
