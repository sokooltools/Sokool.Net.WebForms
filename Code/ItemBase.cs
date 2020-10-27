using System;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Base class for Picture and Folder
	/// </summary>
	//----------------------------------------------------------------------------------------------------------------------------
	public abstract class ItemBase
	{
		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		protected string PhysicalPath { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public string Name { get; private set; }

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets a byte array of a thumbnail.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public abstract byte[] Thumbnail { get; }

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="physicalPath"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		protected static T GetCachedItem<T>(string physicalPath) where T : ItemBase, new()
		{
			string cacheKey = $"AlbumItem({physicalPath})";

			// Lookup in cache
			var item = (T)HttpRuntime.Cache.Get(cacheKey);
			if (item != null)
				return item;

			// Create new
			item = new T
			{
				PhysicalPath = physicalPath,
				Name = Path.GetFileName(physicalPath)
			};

			// Cache it
			HttpRuntime.Cache.Insert(cacheKey, item,
				new CacheDependency(HttpContext.Current.Request.PhysicalPath), DateTime.MaxValue, TimeSpan.FromMinutes(4));
			return item;
		}
	}
}