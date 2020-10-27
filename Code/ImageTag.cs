using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// represents HTML image tag
	/// </summary>
	//----------------------------------------------------------------------------------------------------------------------------
	[DebuggerDisplay("This represents the HTML Tag Source={Src}, Link={Link}, Width={Width}, Height={Height}")]
	public class ImageTag
	{
		private readonly int _height;
		private readonly int _width;

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageTag"/> class.
		/// </summary>
		/// <param name="mode">The mode.</param>
		/// <param name="vpath">The vpath.</param>
		//------------------------------------------------------------------------------------------------------------------------
		public ImageTag(AlbumHandlerMode mode, VirtualPath vpath)
		{
			string p = vpath.Encode();
			Src = $"?mode={mode}&path={p}";
			string p2 = Path.GetFileName(p);
			switch (mode)
			{
				case AlbumHandlerMode.ThumbnailImage:
					Link = vpath.IsRoot ? "?" : $"?path={p}";
					_width = Album.ThumbnailSize;
					_height = Album.ThumbnailSize;
					Alt = $"Click to see an enlarged view of &quot;{HttpUtility.UrlDecode(p2)}&quot;";
					break;
				case AlbumHandlerMode.ParentThumbnailImage:
					Link = vpath.IsRoot ? "?" : $"?path={p}";
					_width = Album.ThumbnailSize;
					_height = Album.ThumbnailSize;
					Alt = "Click to go back to the folder view";
					break;
				case AlbumHandlerMode.PreviewImage:
					//if (!vpath.IsRoot)
					//	_link = p;
					//else
					// _link = @"sokool.net" + p;

					string temp = HostingEnvironment.MapPath(vpath.Value);
					if (temp != null && !temp.ToLower().Contains("sokool.net"))
						Link = @"/sokool.net" + p;
					else
						Link = p;

					Picture.GetPicture(vpath.PhysicalPath).CalcResizedDims(Album.PreviewSize, out _width, out _height);
					Alt = p;
					// Get the File Explorer property value for 'Comments'.
					Comments = HttpUtility.HtmlEncode(GetExplorerProperty(vpath.PhysicalPath, FileExplorerProperty.Comments));
					break;
				case AlbumHandlerMode.Default:
					break;
				default:
					throw new HttpException("invalid link mode");
			}
		}

		public string Src { get; }

		public string Link { get; }

		public int Width => _width;

		public int Height => _height;

		public string Alt { get; }

		public string Comments { get; }

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns a Window's Explorer file property such as its keywords or comments.
		/// </summary>
		/// <param name="sFilePath"></param>
		/// <param name="eFileExplorerProperty"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		private static string GetExplorerProperty(string sFilePath, FileExplorerProperty eFileExplorerProperty)
		{
			var sb = new StringBuilder();
			Image image = new Bitmap(sFilePath);
			foreach (int id in image.PropertyIdList)
			//int id = (int)eFileExplorerProperty;
			//if (image.PropertyIdList.Contains(id))
			{
				PropertyItem item = image.GetPropertyItem(id);
				if ((FileExplorerProperty)item.Id != eFileExplorerProperty)
					continue;
				for (int i = 0; i < (item.Len); i++)
				{
					char c = (char)item.Value[i];
					if (c != '\0')
						sb.Append(c);
				}
			}
			return sb.ToString();
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Defines the various File Explorer properties which can be retrieved.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		private enum FileExplorerProperty
		{
			Title = 0x9c9b,
			Subject = 0x9c9f,
			Keywords = 0x9c9e,
			Comments = 0x9c9c,
			Author = 0x9c9d
		}
	}
}