using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------
	/// <summary>
	/// The GridViewSortExtender is used to add a sort indicator to a <see cref="GridView"/>.
	/// The sort indicator is an image that is shown in the sort column and displays the sort
	/// direction.
	/// </summary>
	//----------------------------------------------------------------------------------------------
	[Designer(typeof(GridViewSortExtenderDesigner))]
	public class GridViewSortExtender : Control
	{
		//------------------------------------------------------------------------------------------
		/// <summary>
		/// Image that is displayed when SortDirection is ascending.
		/// </summary>
		//------------------------------------------------------------------------------------------
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[Description("Image that is displayed when SortDirection is ascending.")]
		public string AscendingImageUrl
		{
			get => ViewState["AscendingImageUrl"] != null ? (string)ViewState["AscendingImageUrl"] : "";
			set => ViewState["AscendingImageUrl"] = value;
		}

		//------------------------------------------------------------------------------------------
		/// <summary>
		/// Image that is displayed when SortDirection is descending.
		/// </summary>
		//------------------------------------------------------------------------------------------
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[Description("Image that is displayed when SortDirection is descending.")]
		public string DescendingImageUrl
		{
			get => ViewState["DescendingImageUrl"] != null ? (string)ViewState["DescendingImageUrl"] : "";
			set => ViewState["DescendingImageUrl"] = value;
		}

		//------------------------------------------------------------------------------------------
		/// <summary>
		/// The GridView that is extended.
		/// </summary>
		//------------------------------------------------------------------------------------------
		[DefaultValue("")]
		[IDReferenceProperty(typeof(GridView))]
		[TypeConverter(typeof(GridViewIDConverter))]
		[Description("The GridView that is extended.")]
		public string ExtendeeID
		{
			get => ViewState["Extendee"] != null ? (string)ViewState["Extendee"] : "";
			set => ViewState["Extendee"] = value;
		}

		//------------------------------------------------------------------------------------------
		/// <summary>
		/// Adds an event handler to the DataBound event of the extended GridView.
		/// </summary>
		/// <param name="e"></param>
		//------------------------------------------------------------------------------------------
		protected override void OnPreRender(EventArgs e)
		{
			OnInit(e);

			var extendee = NamingContainer.FindControl(ExtendeeID) as GridView;

			if (extendee == null || !extendee.AllowSorting || extendee.HeaderRow == null ||
			    String.IsNullOrEmpty(extendee.SortExpression)) return;

			int field = GetSortField(extendee);

			if (field < 0) return;

			var img = new Image
			{
				ImageUrl = extendee.SortDirection == SortDirection.Ascending ? AscendingImageUrl : DescendingImageUrl,
				ImageAlign = ImageAlign.TextTop
			};

			extendee.HeaderRow.Cells[field].Controls.Add(img);
		}

		//------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns the index of the sort-column.
		/// </summary>
		/// <param name="extendee"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------
		private static int GetSortField(GridView extendee)
		{
			var i = 0;
			foreach (DataControlField field in extendee.Columns)
			{
				if (field.SortExpression == extendee.SortExpression)
					return i;
				i++;
			}
			return -1;
		}
	}

	//----------------------------------------------------------------------------------------------
	/// <summary>
	/// Designer for <see cref="GridViewSortExtender"/>.
	/// </summary>
	//----------------------------------------------------------------------------------------------
	public class GridViewSortExtenderDesigner : ControlDesigner
	{
		public override string GetDesignTimeHtml()
		{
			return "<div style=\"background-color: #C8C8C8; border: groove 2 Gray;\"><b>GridViewSortExtender</b> - " +
			       Component.Site.Name + "</div>";
		}
	}

	//----------------------------------------------------------------------------------------------
	/// <summary>
	///
	/// </summary>
	//----------------------------------------------------------------------------------------------
	public class GridViewIDConverter : ControlIDConverter
	{
		// Methods
		protected override bool FilterControl(Control control)
		{
			return control is GridView;
		}
	}
}