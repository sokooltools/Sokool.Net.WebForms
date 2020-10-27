using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class Display : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Page.IsPostBack) 
			return;

		//prevPage = Request.UrlReferrer.ToString();

		string path = Request.QueryString["path"];
		string video = Request.QueryString["vid"];

		Page.ClientScript.RegisterStartupScript(typeof(string), "addScript", "SetTitle(\"" + path + "\",\"" + video + "\")", true);

		//string file = Server.MapPath("/" + path + "\\" + video) + ".txt";
		string file = HttpContext.Current.Request.PhysicalApplicationPath + path.Replace('/', '\\') + "\\" + video + ".txt";
		//Response.Write(file);
		if (!System.IO.File.Exists(file)) 
			return;
		
		Control ctl1 = Page.FindControl("LyricsRow");
		if (ctl1 == null) 
			return;
		
		Control ctl2 = Page.FindControl("Lyrics");
		if (ctl2 == null) 
			return;
		
		string text = System.IO.File.ReadAllText(file);
		text = HttpUtility.HtmlEncode(text);
		((HtmlGenericControl)ctl2).InnerHtml = text.Replace("\r\n", "<br/>").Replace("'", "&#39;");
		ctl1.Visible = true;
	}
}
