<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="Display" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>title here</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%--    <meta http-equiv="x-ua-compatible" content="IE=9" />--%>
    <link rel="shortcut icon" href="../embed/images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../embed/images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="../embed/default.css" type="text/css" />
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" type="text/css" />
    <script src="../embed/default.js" type="text/javascript"></script>
    <script src="../embed/jwplayer.js" type="text/javascript"></script>
    <script type="text/javascript">jwplayer.key = "EHjW+hdQYtvvEWMPhpfaHx/mKqR/sQ97d8n/rg==";</script>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.js" type="text/javascript"></script>
</head>
<body>
    <noscript>Please enable javascript to see this content.</noscript>
    <form id="form1" runat="server">
    <div class="outerborder2">
        <div class="outerborder1">
            <table class="table1">
                <tr>
                    <td>
                        <a href="../index.aspx">
                            <img alt="Back to Sokool.Net Home" src="../embed/images/home-button.gif" class="home" /></a>
                    </td>
                    <td>
                        <div id="title" class="title">
                            Title
                        </div>
                    </td>
                    <td style="text-align: right">
                        <a href ="javascript:BackToIndex()" style="font-size: small">Back to Index</a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;"></td>
                    <td>
                        <div class="playerborder">
                            <div id="player">
                                player
                            </div>
                        </div>
                    </td>
                    <td style="width: 40%;"></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr runat="server" id="LyricsRow" visible="false">
                    <td></td>
                    <td>
                        <input id="ShowHide" onclick="OpenDialog()" type="button" value="Show Lyrics" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div runat="server" id="Lyrics" style="background-color: Bisque" class="lyrics">
                        </div>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Lyrics").dialog({ autoOpen: false, zIndex: 999999, maxWidth: 400, show: "slide", hide: "slide" });
            $("#Lyrics").dialog("option", "closeOnEscape", true);
            $("#Lyrics").bind("dialogopen", function () { $("#ShowHide").attr('value', 'Hide Lyrics'); });
            $("#Lyrics").bind("dialogclose", function () { $("#ShowHide").attr('value', 'Show Lyrics'); });
            InitPol();
        });
    </script>
    </form>
</body>
</html>
