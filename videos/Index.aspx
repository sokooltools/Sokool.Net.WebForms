<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Index" %>
<%@ Register TagPrefix="cc1" Namespace="Sokool.Net.Code" Assembly="Sokool.Net" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Video List</title>
    <link rel="shortcut icon" href="~/embed/images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="~/embed/images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" href="../embed/default.css" />
    <style type="text/css">
        .rowA {
            background-color: #EFF3FB;
            font-size: 9pt;
        }

        .rowB {
            background-color: White;
            font-size: 9pt;
        }
    </style>

    <%--    <script type="text/javascript" src="../embed/jquery.js" ></script>
    <script type="text/javascript">
        $(document).ready(function()
        {
            $("tr").filter(function()
            {
                return $('td', this).length && !$('table', this).length
            }).css({ background: "ffffff" }).hover(
                function() { $(this).css({ background: "#C1DAD7" }); },
                function() { $(this).css({ background: "#ffffff" }); }
                );
        });
    </script>
    --%>
</head>
<body>
    <form id="form1" runat="server">
    <div class="outerborder2">
        <div class="outerborder1">
            <table class="table1">
                <tr>

                    <td id="home" class="home" style="width: 40%">
                        <a href="../index.aspx">
                            <img alt="Back to Sokool.Net Home" src="../embed/images/home-button.gif" class="home" />
                        </a>
                    </td>
                    <td  >
                        <div class="title" id="title" runat="server">Index</div>
                    </td>
                    <td id="close" class="close" style="width: 40%; text-align: right; font-size: small;">
                        <a href="videos.aspx">Back to Main Index</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            Width="100%" Font-Names="Verdana" CellPadding="4" ForeColor="#333333" GridLines="None"
                            DataSourceID="ObjectDataSource1">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="9pt" ForeColor="White" />
                            <RowStyle CssClass="rowA" />
                            <AlternatingRowStyle CssClass="rowB" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Path,Name" DataTextField="DisplayName" HeaderText="File Name"
                                    SortExpression="Name" DataNavigateUrlFormatString="display.aspx?path={0}&vid={1}">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="LastWriteTime" SortExpression="LastWriteTime" HeaderText="Create Date"
                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy hh:mm tt}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Length" SortExpression="Length" HeaderText="File Size"
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,### KB}">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Path" Visible="false"></asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetData" TypeName="Sokool.Net.Code.DataAdapter" SortParameterName="sortExp"
                            OnSelecting="ObjectDataSource1_Selecting">
                            <SelectParameters>
                                <asp:Parameter Name="sortExp" Type="String" DefaultValue="LastWriteTime DESC" />
                                <asp:Parameter Name="path" Type="String" DefaultValue="flvs" />
                                <asp:Parameter Name="fileExt" Type="String" DefaultValue="*.mp4" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:GridViewSortExtender ID="GridViewSortExtender2" runat="server" AscendingImageUrl="~/embed/images/Ascending.gif"
                            DescendingImageUrl="~/embed/images/Descending.gif" ExtendeeID="GridView1">
                        </cc1:GridViewSortExtender>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
