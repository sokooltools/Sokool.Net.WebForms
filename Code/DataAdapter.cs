using System;
using System.ComponentModel;
using System.IO;
using System.Web;

namespace Sokool.Net.Code
{
	[DataObject(true)]
	public abstract class DataAdapter
	{
		//------------------------------------------------------------------------------------------
		/// <summary>
		/// This method is called by the ObjectDataSource when the GridView is bound.
		/// Therefore we return data sorted by the sort expression.
		/// </summary>
		/// <param name="sortExp">How the data should be sorted (e.g. "Col1 DESC").</param>
		/// <param name="path">Physical file path as located on the server (e.g. "videos/tv/flvs")</param>
		/// <param name="fileExt">File extension to retrieve (e.g. *.flv")</param>
		/// <returns>Strongly typed in-memory cache of data.</returns>
		//------------------------------------------------------------------------------------------
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DataSet1 GetData(string sortExp, string path, string fileExt)
		{
			var ds1 = new DataSet1();
			var dirInfo = new DirectoryInfo(HttpContext.Current.Server.MapPath("~\\" + path));
			if (dirInfo.Exists)
			{
				var fileInfoArray = dirInfo.GetFiles(fileExt);
				foreach (FileInfo fileInfo in fileInfoArray)
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
					ds1.DataTable1.AddDataTable1Row
						(
						 fileNameWithoutExtension.Replace(' ', '+'),
							fileInfo.CreationTime,
							Convert.ToInt64(fileInfo.Length/1024),
							path,
							fileNameWithoutExtension
						);
				}
			}
			var result = new DataSet1();
			string filterExp = String.Empty;
			result.Merge(ds1.DataTable1.Select(filterExp, sortExp));
			return result;
		}
	}
}