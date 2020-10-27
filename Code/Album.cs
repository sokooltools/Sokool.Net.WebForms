using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Sokool.Net.Code
{

	//........................................................................................................................

	#region Album Class

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Summary description for Album
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public static class Album
	{
		// customization
		public static readonly int ThumbnailSize;
		public static readonly int PreviewSize;

		public static readonly Color ChildFolderColor = Color.LightGray;
		public static readonly Color ParentFolderColor = HexToColor("#E6E2C1");

		static Album()
		{
			ThumbnailSize = 90;
			PreviewSize = 550;
		}

		//public Album(int iThumbnailSize, int iPreviewSize)
		//{
		//    ThumbnailSize = iThumbnailSize;
		//    PreviewSize = iPreviewSize;
		//}

		// helpers

		//--------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Converts a bitmap into a byte array.
		/// </summary>
		/// <param name="bitmap">The bitmap image to convert.</param>
		/// <returns>Returns bitmap coverted into a byte array</returns>
		/// ----------------------------------------------------------------------------------
		public static byte[] BitmapToBytes(Image bitmap)
		{
			var ms = new MemoryStream();
			bitmap.Save(ms, ImageFormat.Jpeg);
			ms.Close();
			return ms.ToArray();
		}

		//--------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Translates a html hexadecimal definition of a color into a .NET Framework Color.
		/// The input string must start with a '#' character and be followed by 6 hexadecimal digits. The digits A-F 
		/// are not case sensitive. If the conversion was not successful the color white will be returned.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------
		private static Color HexToColor(string hexString)
		{
			Color actColor;
			if ((hexString.StartsWith("#")) && (hexString.Length == 7))
			{
				int r = HexToInt(hexString.Substring(1, 2));
				int g = HexToInt(hexString.Substring(3, 2));
				int b = HexToInt(hexString.Substring(5, 2));
				actColor = Color.FromArgb(r, g, b);
			}
			else
			{
				actColor = Color.White;
			}
			return actColor;
		}

		//--------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Converts a hex string into an integer.
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------
		private static int HexToInt(string hexString)
		{
			return Int32.Parse(hexString, NumberStyles.HexNumber, null);
		}
	}

	#endregion

	//........................................................................................................................

	#region AlbumHandlerMode Enum

	//-----------------------------------------------------------------------------------------------------------------

	#endregion

	
}