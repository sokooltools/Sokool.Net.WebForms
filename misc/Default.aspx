<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Untitled Page</title>
<style type="text/css">
body {
	margin: 10px 10px 0px 10px;
	padding: 0px;
}
#leftcontent {
	position: absolute;
	left: 10px;
	top: 50px;
	width: 200px;
	background: #fff;
	border: 1px solid #000;
}
#centercontent {
	background: #fff;
	margin-left: 199px;
	margin-right: 199px;
	border: 1px solid #000;
/*
		IE5x PC mis-implements the box model. Because of that we sometimes have
		to perform a little CSS trickery to get pixel-perfect display across browsers.
		The following bit of code was proposed by Tantek Celik, and it preys upon a CSS
		parsing bug in IE5x PC that will prematurly close a style rule when it runs
		into the string "\"}\"". After that string appears in a rule, then, we can override
		previously set attribute values and only browsers without the parse bug will
		recognize the new values. So any of the name-value pairs above this comment
		that we need to override for browsers with correct box-model implementations
		will be listed below.

		We use the voice-family property because it is likely to be used very infrequently,
		and where it is used it will be set on the body tag. So the second voice-family value
		of "inherit" will override our bogus "\"}\"" value and allow the proper value to
		cascade down from the body tag.

		The style rule immediately following this rule offers another chance for CSS2
		aware browsers to pick up the values meant for correct box-model implementations.
		It uses a CSS2 selector that will be ignored by IE5x PC.

		Read more at http://www.glish.com/css/hacks.asp
*/	voice-family: "\"}\"";
	voice-family: inherit;
	margin-left: 201px;
	margin-right: 201px;
}
html > body #centercontent {
	margin-left: 201px;
	margin-right: 201px;
}
#rightcontent {
	position: absolute;
	right: 10px;
	top: 50px;
	width: 200px;
	background: #fff;
	border: 1px solid #000;
}
#banner {
	background: #fff;
	height: 40px;
	border-top: 1px solid #000;
	border-right: 1px solid #000;
	border-left: 1px solid #000;
	voice-family: "\"}\"";
	voice-family: inherit;
	height: 39px;
}
html > body #banner {
	height: 39px;
}
p, h1, pre {
	margin: 0px 10px 10px 10px;
}
h1 {
	font-size: 14px;
	padding-top: 10px;
}
#banner h1 {
	font-size: 14px;
	padding: 10px 10px 0px 10px;
	margin: 0px;
}
#rightcontent p {
	font-size: 10px;
}
.newStyle1 {
	border: 1px solid #0000FF;
	line-height: 1.em;
	float: left;
	padding: 8px;
	margin-left: 20px;
	vertical-align: middle;
	background-color: #00FF00;
}
.newStyle2 {
	line-height: 1.em;
	padding: 8px;
	margin-left: 200px;
	margin-right: 200px;
	vertical-align: middle;
	white-space: nowrap;
	background-color: #FFFF00;
}
.newStyle3 {
	border: thin solid #FF0000;
	line-height: 1.em;
	padding: 8px;
	float: right;
	position: relative;
	vertical-align: middle;
	margin-right: 20px;
	clear: none;
}
</style>
</head>
<body>
<div>
	<asp:Label ID="Label1" runat="server" Text="Label" Width="229px"></asp:Label>
	<i>This is another test</i> <br />
	<strong>Yet another test</strong> <br />
</div>
<div id="test" style="height: 110px">
	<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Photos/album.aspx" CssClass="newStyle1">Cruise Photos 1
		</asp:HyperLink>
	<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Cruise3/pg2/album.aspx" CssClass="newStyle2">Cruise Photos 2
		</asp:HyperLink>
	<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Cruise3/pg1/album.aspx" CssClass="newStyle3">Cruise Photos 3
		</asp:HyperLink>
	</div>
	</body>
</html>
