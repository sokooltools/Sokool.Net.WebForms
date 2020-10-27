using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Index : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (Page.IsPostBack) 
			return;

		// Set the initial sort.
		GridView1.Sort("DisplayName", SortDirection.Ascending);
		
		string path = Request.QueryString["path"];
		string file = HttpContext.Current.Request.PhysicalApplicationPath + path.Replace('/', '\\') + "\\_config.txt";

		//Response.Write(file);
		if (!System.IO.File.Exists(file))
			return;

		Control ctl1 = Page.FindControl("title");
		if (ctl1 == null)
			return;

		string text = System.IO.File.ReadAllText(file);
		text = HttpUtility.HtmlEncode(text);
		((HtmlGenericControl)ctl1).InnerHtml = text.Replace("\r\n", "<br/>").Replace("'", "&#39;");
		ctl1.Visible = true;
	}

	protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
	{
		if (Request.QueryString["path"] != null)
			e.InputParameters["path"] = Request.QueryString["path"];
	}

}

