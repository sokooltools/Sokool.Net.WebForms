using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// class to represent one picture
	/// </summary>
	//----------------------------------------------------------------------------------------------------------------------------
	public class Picture : ItemBase
	{
		private Size? _dims;
		private byte[] _thumbnail;

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public override byte[] Thumbnail => _thumbnail ?? (_thumbnail = DrawPictureThumbnail());

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="physicalPath"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public static Picture GetPicture(string physicalPath)
		{
			return GetCachedItem<Picture>(physicalPath);
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		//------------------------------------------------------------------------------------------------------------------------
		private void GetDimensions(out int width, out int height)
		{
			if (!_dims.HasValue)
			{
				using (var originalImage = (Bitmap)Image.FromFile(PhysicalPath, false))
				{
					_dims = new Size(originalImage.Width, originalImage.Height);
				}
			}
			width = _dims.Value.Width;
			height = _dims.Value.Height;
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="newSize"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		//------------------------------------------------------------------------------------------------------------------------
		public void CalcResizedDims(int newSize, out int width, out int height)
		{
			GetDimensions(out width, out height);

			if (width >= height && width > newSize)
			{
				height = Convert.ToInt32(((double)newSize * height) / width);
				width = newSize;
			}
			else if (height >= width && height > newSize)
			{
				width = Convert.ToInt32(((double)newSize * width) / height);
				height = newSize;
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public byte[] DrawResizedImage(int size)
		{
			// load original image
			using (var originalImage = (Bitmap)Image.FromFile(PhysicalPath, false))
			{
				if (!_dims.HasValue)
				{
					_dims = new Size(originalImage.Width, originalImage.Height);
				}

				// calculate
				int width, height;
				CalcResizedDims(size, out width, out height);

				// draw new image
				using (var newImage = new Bitmap(width, height))
				{
					using (var g = Graphics.FromImage(newImage))
					{
						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						g.DrawImage(originalImage, 0, 0, width, height);
					}

					return Album.BitmapToBytes(newImage);
				}
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		private byte[] DrawPictureThumbnail()
		{
			// load original image
			using (var originalImage = (Bitmap)Image.FromFile(PhysicalPath, false))
			{
				if (!_dims.HasValue)
				{
					_dims = new Size(originalImage.Width, originalImage.Height);
				}

				// calculate
				int size = Album.ThumbnailSize;
				int width, height;
				CalcResizedDims(size, out width, out height);
				int drawWidth = width;
				int drawHeight = height;
				width = Math.Max(width, size);
				height = Math.Max(height, size);
				int drawXOffset = (width - drawWidth) / 2;
				int drawYOffset = (height - drawHeight) / 2;

				// draw new image
				using (var newImage = new Bitmap(width, height))
				{
					using (Graphics g = Graphics.FromImage(newImage))
					{
						g.InterpolationMode = InterpolationMode.HighQualityBicubic;
						g.FillRectangle(Brushes.GhostWhite, 0, 0, width, height);
						g.DrawRectangle(Pens.LightGray, 0, 0, width - 1, height - 1);
						g.DrawImage(originalImage, drawXOffset, drawYOffset, drawWidth, drawHeight);
						return Album.BitmapToBytes(newImage);
					}
				}
			}
		}
	}
}