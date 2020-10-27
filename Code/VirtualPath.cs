using System;
using System.Web;
using System.Web.Hosting;

namespace Sokool.Net.Code
{
	//----------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Virtual path helper
	/// </summary>
	//----------------------------------------------------------------------------------------------------------------------------
	public class VirtualPath
	{
		private VirtualPath _root;

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="VirtualPath"/> class.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public VirtualPath()
		{
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Sets the virtual path to the specified string.
		/// </summary>
		/// <param name="str"></param>
		//------------------------------------------------------------------------------------------------------------------------
		public VirtualPath(string str)
		{
			string s = str.ToLowerInvariant();

			if (s != "/" && s.EndsWith("/"))
				s = s.TrimEnd('/');

			if (!s.StartsWith("/") || s.IndexOf("/.", StringComparison.Ordinal) >= 0 || s.IndexOf('\\') >= 0)
				throw new Exception("invalid virtual path " + str);

			Value = s;
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the current virtual path.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public string Value { get; }

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the physical path from the hosting environment based on the current virtual path.
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public string PhysicalPath
		{
			//get { return HostingEnvironment.MapPath(_str); }
			get
			{
				string path = HostingEnvironment.ApplicationPhysicalPath.TrimEnd('\\');
				return path + (Value == null ? String.Empty : Value.Replace('/', '\\'));
			}
		}

		//public string Name
		//{
		//	get { return (_str == "/") ? string.Empty : _str.Substring(_str.LastIndexOf('/') + 1); }
		//}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets an indication as to whether the virtual path is at the root
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public bool IsRoot => Value == Root.Value;

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets an indication as to whether the virtual path is under the root
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public bool IsUnderRoot
		{
			get
			{
				string r = Root.Value;
				return r == "/" || r == Value || Value.StartsWith(r + "/");
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Vutual path of the current request
		/// </summary>
		//------------------------------------------------------------------------------------------------------------------------
		public VirtualPath Root => _root ?? (_root = new VirtualPath(HttpContext.Current.Request.FilePath).GetParent());

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns the virtual path encoded as a unicode string
		/// </summary>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public string Encode()
		{
			return HttpUtility.UrlEncodeUnicode(Value).Replace("'", "%27").Replace("%2f", "/").Replace("+", "%20");
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Gets the parent of the virtual path.
		/// </summary>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public VirtualPath GetParent()
		{
			if (Value == "/")
				return null;
			int i = Value.LastIndexOf('/');
			return new VirtualPath(i > 0 ? Value.Substring(0, i) : "/");
		}

		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns the child of the virtual path.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		//------------------------------------------------------------------------------------------------------------------------
		public VirtualPath GetChild(string name)
		{
			return Value == "/" ? new VirtualPath("/" + name) : new VirtualPath(Value + "/" + name);
		}
	}
}