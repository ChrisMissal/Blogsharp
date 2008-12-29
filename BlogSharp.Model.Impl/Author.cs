using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogSharp.Model.Impl
{
	public class Author : IUser
	{
		public Author()
		{
			this.Posts=new List<IPost>();
			this.Blogs=new List<IBlog>();
		}


		#region IUser Members

		public IList<IPost> Posts
		{
			get;
			set;
		}

		public IList<IBlog> Blogs
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		#endregion

		#region IIdentifiable<int> Members

		public Guid Id
		{
			get;
			set;
		}

		#endregion
	}
}
