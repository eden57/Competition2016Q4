<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>图形相交检索</title>
    <link href="App_Themes/Flat/shell.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Flat/Grid.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/Flat/Toolbar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function check()
        {          
            if (!confirm('分析速度比较慢，确认重新分析?')) return false;

            return true;
        }
    </script>
</head>

<body class="GridBody">
    <form id="GridForm" runat="server">
        <div class="GridPage">
            <div class="GridToolbar">
                <table class="toolbarTable" cellpadding="0" cellspacing="5">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID='btnAdd' runat='server' Text='重新分析' CssClass="styButton" OnClientClick="return check();" OnClick="btnAdd_Click" />
                            <asp:Button ID='btnView' runat='server' Text='分析结果' CssClass="styButton" style="display:none;" OnClick="btnView_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <%--表格区域--%>
            <div class="GridTable">
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" Width="600px" CssClass="GridView">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="序号">
                            <ItemStyle Width="5%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AA01_ID" HeaderText="A表唯一编码">
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AA02_ID" HeaderText="B表唯一编码">
                            <ItemStyle Width="8%" HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STArea" HeaderText="重叠面积">
                            <ItemStyle Width="8%" HorizontalAlign="left" />
                        </asp:BoundField>                       
                    </Columns>
                    <RowStyle CssClass="GridViewRow" />
                    <AlternatingRowStyle CssClass="AlternatingRow" />
                    <SelectedRowStyle CssClass="GridViewRowSelected" />
                    <HeaderStyle CssClass="GridViewHeader" />
                    <FooterStyle CssClass="GridViewRow" />
                </asp:GridView>
            </div>
            <%--分页控件--%>
            <div class="GridPager">
                <qhh:Navigator ID="Navigator" runat="server" OnPageChanged="Navigator_PageChanged">
                </qhh:Navigator>
            </div>
        </div>
    </form>
</body>

</html>
