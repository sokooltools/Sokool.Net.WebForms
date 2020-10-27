using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Web.UI;
using Sokool.Net.Code;

public partial class album : Page
{

	// Not null when the response is the image
	private byte[] _imageResponse;

	// Used in page UI
	protected ImageTag ParentLink;
	protected ImageTag PreviousLink;
	protected ImageTag PictureLink;
	protected ImageTag NextLink;

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// 
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	protected string MainTitle
	{
		get => (Session["MainTitle"] ?? "").ToString();
		private set
		{
			if (value != null)
				Session["MainTitle"] = value;
		}
	}

	// Page event handlers
	protected void Page_Load(object sender, EventArgs e)
	{
		if (IsCallback)
			return;

		// Parse the mode from the query string
		var mode = AlbumHandlerMode.Default;
		string modeString = Request.QueryString["mode"];

		if (!String.IsNullOrEmpty(modeString))
			mode = (AlbumHandlerMode)Enum.Parse(typeof(AlbumHandlerMode), modeString, true);

		// Parse the path from the query string (by default it is where the page is)
		string path = Request.QueryString["path"];

		MainTitle = HttpUtility.UrlDecode(Request.QueryString["title"]);

		VirtualPath virtualPath;

		if (path == null)
			virtualPath = new VirtualPath().Root;
		else
		{
			virtualPath = new VirtualPath(path);

			//Response.Write(virtualPath.PhysicalPath + "<br/>"); // RAS

			if (!virtualPath.IsUnderRoot)
			{
				//Response.Write(virtualPath.PhysicalPath + "<br/>"); // RAS
				//throw new HttpException("Invalid path - not in the handler scope");
			}
		}

		string pPath = virtualPath.PhysicalPath;

		bool isFolder = Directory.Exists(pPath);

		if (!isFolder && !File.Exists(pPath))
			throw new HttpException(404, "Invalid path - not found");

		// Perform the action depending on the mode
		if (mode != AlbumHandlerMode.Default)
		{
			// Response is an image
			AlbumMultiView.ActiveViewIndex = -1;

			switch (mode)
			{
				case AlbumHandlerMode.PreviewImage:
					_imageResponse = Picture.GetPicture(pPath).DrawResizedImage(Album.PreviewSize);
					break;
				case AlbumHandlerMode.ThumbnailImage:
					_imageResponse = isFolder ? Folder.GetFolder(pPath).Thumbnail : Picture.GetPicture(pPath).Thumbnail;
					break;
				case AlbumHandlerMode.ParentThumbnailImage:
					_imageResponse = Folder.GetFolder(pPath).ParentThumbnail;
					break;
				case AlbumHandlerMode.Default:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		else
		{
			Header.Title = MainTitle; //vpath.Value;
			VirtualPath pvPath = virtualPath.GetParent();

			// response is HTML
			if (isFolder)
			{
				// folder view
				AlbumMultiView.ActiveViewIndex = 0;

				if (pvPath != null && pvPath.IsUnderRoot)
				{
					ParentLink = new ImageTag(AlbumHandlerMode.ParentThumbnailImage, pvPath);
					//FolderViewParentLinkSpan.Visible = true;
				}

				var listOfThumbnails = Folder.GetFolder(pPath).GetFolderItems().Select(item => new ImageTag(AlbumHandlerMode.ThumbnailImage, virtualPath.GetChild(item.Name))).ToList();

				ThumbnailList.DataSource = listOfThumbnails;
				ThumbnailList.DataBind();
			}
			else
			{
				// single picture details view
				AlbumMultiView.ActiveViewIndex = 1;

				ParentLink = new ImageTag(AlbumHandlerMode.ParentThumbnailImage, pvPath);

				VirtualPath prevvpath = null;
				VirtualPath nextvpath = null;
				bool found = false;

				foreach (ItemBase itemBase in Folder.GetFolder(pvPath.PhysicalPath).GetFolderPictures())
				{
					var pict = (Picture) itemBase;
					VirtualPath vp = pvPath.GetChild(pict.Name);

					if (vp.Value == virtualPath.Value)
						found = true;
					else if (found)
					{
						nextvpath = vp;
						break;
					}
					else
						prevvpath = vp;
				}

				if (!found)
					prevvpath = null;

				if (prevvpath != null)
				{
					PreviousLink = new ImageTag(AlbumHandlerMode.ThumbnailImage, prevvpath);
					PreviousLinkSpan.Visible = true;
				}
				else
					NoPreviousLinkSpan.Visible = true;

				if (found)
				{
					PictureLink = new ImageTag(AlbumHandlerMode.PreviewImage, virtualPath);
					PictureLinkSpan.Visible = true;
					PictureCommentSpan.Visible = true;
				}

				if (nextvpath != null)
				{
					NextLink = new ImageTag(AlbumHandlerMode.ThumbnailImage, nextvpath);
					NextLinkSpan.Visible = true;
				}
				else
					NoNextLinkSpan.Visible = true;
			}
		}
	}

	protected void Page_Unload()
	{
		if (_imageResponse == null)
			return;

		HttpResponse response = HttpContext.Current.Response;
		response.Clear();
		response.ContentType = "image/jpeg";
		response.OutputStream.Write(_imageResponse, 0, _imageResponse.Length);
	}
}
