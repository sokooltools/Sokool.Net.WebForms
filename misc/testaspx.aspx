<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testaspx.aspx.cs" Inherits="testaspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flash Movie Viewer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
        </div>
        <asp:Button ID="UploadButton" runat="server" OnClick="UploadButton_Click" Text="UploadButton"
            Width="136px" />
        <input type="button" onclick="PlayMovie()" value="PlayMovie" />
        <input type="button" onclick="StopMovie()" value="Stop" />
        <p>
            <object id="myFlash" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://active.macromedia.com/flash5/cabs/swflash.cab#version=5,0,0,0"
                width="80%" height="90%">
                <param name="movie" value=""/>
                <param name="play" value="false"/>
                <param name="quality" value="high"/>
            </object>
        </p>
    </form>

    <script type="text/javascript">
	var oMovie = window.document.getElementById("myFlash");
	function StopMovie()
	{
		 if (oMovie != null)
		 {
			  oMovie.StopPlay();
		 }
	}
	function PlayMovie()
	{
		 if (oMovie != null)
		 {
			  var oFilename = window.document.getElementById("FileUpload1")
			  var sCurrent = GetCurrentDirectory();
			  movieTitle = oFilename.value;
			  if (movieTitle.lastIndexOf(".swf") > -1)
			  {
					var idx = movieTitle.toLowerCase().lastIndexOf("sokool.net\\");
					movieTitle = movieTitle.substring(idx + 11, movieTitle.length);
					oMovie.StopPlay();
					oMovie.LoadMovie(0,  sCurrent  + movieTitle );
					oMovie.Play();
			  }
		 }
	}
	function GetCurrentDirectory()
	{
		var myloc = window.location.href;
		var locarray = myloc.split("/");
		delete locarray[(locarray.length-1)];
		return locarray.join("/");
	}
    </script>

</body>
</html>
