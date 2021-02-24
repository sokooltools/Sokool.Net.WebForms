using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// class to represent one folder
	/// </summary>
	//----------------------------------------------------------------------------------------------------------------------------
	public class Folder : ItemBase
	{
		private byte[] _parentThumbnail;
		private byte[] _thumbnail;

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current thumbnail overriding the item base
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public override byte[] Thumbnail => _thumbnail ?? (_thumbnail = DrawFolderThumbnail(false));

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current parent thumbnail
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public byte[] ParentThumbnail => _parentThumbnail ?? (_parentThumbnail = DrawFolderThumbnail(true));

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="physicalPath"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public static Folder GetFolder(string physicalPath)
		{
			return GetCachedItem<Folder>(physicalPath);
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="picturesOnly"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		private List<ItemBase> GetFolderItems(bool picturesOnly, int count)
		{
			var list = new List<ItemBase>();
			if (!picturesOnly)
			{
				foreach (string dir in Directory.GetDirectories(PhysicalPath))
				{
					string fileName = Path.GetFileName(dir);
					if (fileName == null)
						continue;
					string d = fileName.ToLowerInvariant();

					if (d.StartsWith("_vti_") 
					    || d.StartsWith("app_") 
					    || d.StartsWith("bin") 
					    || d.StartsWith("aspnet_client"))
						continue;
					list.Add(GetFolder(dir));

					if (list.Count >= count)
					{
						break;
					}
				}
			}

			if (list.Count >= count) 
				return list;

			foreach (string file in Directory.GetFiles(PhysicalPath, "*.jpg"))
			{
				list.Add(Picture.GetPicture(file));
				if (list.Count >= count)
					break;
			}
			return list;
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public IEnumerable<ItemBase> GetFolderItems()
		{
			return GetFolderItems(false, Int32.MaxValue);
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public IEnumerable<ItemBase> GetFolderPictures()
		{
			return GetFolderItems(true, Int32.MaxValue);
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		private List<ItemBase> GetFirstFolderItems(int count)
		{
			return GetFolderItems(false, count);
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns a byte array containing a thumbnail image of a folder
		/// </summary>
		/// <param name="isParent"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		private byte[] DrawFolderThumbnail(bool isParent)
		{
			int size = Album.ThumbnailSize;

			using (var newImage = new Bitmap(size, size))
			{
				using (Graphics g = Graphics.FromImage(newImage))
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					using (var b = new LinearGradientBrush(new Point(0, 0),
						new Point(size, size), isParent ? Album.ParentFolderColor : Album.ChildFolderColor, Color.GhostWhite))
					{
						g.FillRectangle(b, 0, 0, size, size);
					}

					g.DrawRectangle(Pens.LightGray, 0, 0, size - 1, size - 1);

					// draw up to 4 subitems
					List<ItemBase> folderItems = GetFirstFolderItems(4);
					const int delta = 10;
					int side = (size - 3 * delta) / 2 - 1;

					var itemRects = new[]
					{
						new Rectangle(delta + 3, delta + 12, side, side),
						new Rectangle(size/2 + delta/2 - 3, delta + 12, side, side),
						new Rectangle(delta + 3, size/2 + delta/2 + 6, side, side),
						new Rectangle(size/2 + delta/2 - 3, size/2 + delta/2 + 6, side, side)
					};

					for (int i = 0; i < 4; i++)
					{
						Rectangle r = itemRects[i];
						if (i < folderItems.Count)
						{
							using (var subImage = (Bitmap)
								Image.FromStream(new MemoryStream(folderItems[i].Thumbnail), false))
							{
								g.DrawImage(subImage, r);
							}
						}
						g.DrawRectangle(Pens.LightGray, r);
					}

					// add folder name
					string name = isParent ? "[..]" : Name;

					using (var f = new Font("Arial", 10))
					{
						g.DrawString(name, f, Brushes.Black, new RectangleF(2, 2, size - 2, size - 2));
					}

					return Album.BitmapToBytes(newImage);
				}
			}
		}
	}
}