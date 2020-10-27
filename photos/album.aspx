<%@ Page Language="C#" AutoEventWireup="true" Inherits="album" EnableViewState="false" CodeBehind="album.aspx.cs" %>

<%@ Import Namespace="Sokool.Net.Code" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Album</title>
    <meta http-equiv="x-ua-compatible" content="IE=9" />
    <link rel="shortcut icon" href="../embed/images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../embed/images/favicon.ico" type="image/x-icon" />
    <link href="../embed/album.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <div>
        <asp:MultiView ID="AlbumMultiView" runat="server">
            <asp:View ID="FolderView" runat="server">
                <table id="MainTitle2" style="padding-top: 20px; text-align: center">
                    <tr>
                        <td style="padding-left: 5px; padding-top: 5px; vertical-align: middle;">
                            <span><a href="../index.aspx">
                                <img alt="Back to Sokool.Net Home" src="../embed/images/home-button.gif" />
                            </a>
                            </span>
                        </td>
                        <td style="text-align: center; width: 93%">
                            <%=MainTitle%>
                        </td>
                        <td class="close">
                            <a href="photos.aspx">Back</a>
                        </td>
                    </tr>
                </table>
                <span id="FolderViewParentLinkSpan" runat="server" visible="false"><a href="<%=ParentLink.Link%>">
                    <img alt="<%=ParentLink.Alt%>" src="<%=ParentLink.Src%>" width="<%=ParentLink.Width%>"
                        height="<%=ParentLink.Height%>" style="border: 0" /></a> </span>
                <div>
                    <asp:Repeater ID="ThumbnailList" runat="server">
                        <ItemTemplate>
                            <a href='<%#Eval("Link")%>'>
                                <img alt='<%#Eval("Alt")%>' src='<%#Eval("Src")%>' width='<%#Eval("Width")%>' height='<%#Eval("Height")%>' /></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:View>
            <asp:View ID="PageView" runat="server">
                <div style="height: 33px; width: 33px; float: left; margin-left: 4px; margin-top: 20px">
                    <span style="padding: 5px 12px 2px 2px;">
                        <a href="photos.aspx">
                            <img alt="Back to Sokool.Net Home" src="../embed/images/home-button.gif" /></a>
                    </span>
                </div>
                <%--width="<%=Album.ThumbnailSize + 8%>px"--%>
                <table style="margin-left: auto; margin-right: auto;">
                    <tr>
                        <td style="width: 50%;" />
                        <td>
                            <table style="margin-left: auto; margin-right: auto; text-align: center;">
                                <tr>
                                    <td>
                                        <span id="PreviousLinkSpan" runat="server" visible="false">
                                            <a href="<%=PreviousLink.Link%>">Previous Picture<br />
                                                <img style="margin-top: 3px;" alt="<%=PreviousLink.Alt%>" src="<%=PreviousLink.Src%>" width="<%=PreviousLink.Width%>" height="<%=PreviousLink.Height%>" />
                                            </a>
                                        </span>
                                        <span id="NoPreviousLinkSpan" runat="server" visible="false">No Previous Picture</span>
                                    </td>
                                    <td>
                                        <span>
                                            <a href="<%=ParentLink.Link%>">Up to Folder View<br />
                                                <img style="margin: 4px 4px 0 4px;" alt="<%=ParentLink.Alt%>" src="<%=ParentLink.Src%>" width="<%=ParentLink.Width%>" height="<%=ParentLink.Height%>" />
                                            </a>
                                        </span>
                                    </td>
                                    <td>
                                        <span id="NextLinkSpan" runat="server" visible="false">
                                            <a href="<%=NextLink.Link%>">Next Picture<br />
                                                <img style="margin-top: 3px;" alt="<%=NextLink.Alt%>" src="<%=NextLink.Src%>" width="<%=NextLink.Width%>" height="<%=NextLink.Height%>" />
                                            </a>
                                        </span>
                                        <span id="NoNextLinkSpan" runat="server" visible="false">No Next Picture</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%;" />
                    </tr>
                    <tr>
                        <td style="width: 50%;" />
                        <td style="background-color: white; padding: 6px; border-style: solid; border-color: blue; border-width: 1px;">
                            <span id="PictureLinkSpan" runat="server" visible="true">
                                <a href="<%=PictureLink.Link%>">
                                    <img alt="<%=PictureLink.Alt%>" src="<%=PictureLink.Src%>" height="350" />
                                </a>
                            </span>
                        </td>
                        <td style="width: 50%;" />
                    </tr>
                    <tr>
                        <td style="width: 50%;" />
                        <td style="text-align: center;">
                            <span id="PictureCommentSpan" runat="server" visible="false">
                                <%=PictureLink.Comments%>
                            </span>
                        </td>
                        <td style="width: 50%;" />

                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
