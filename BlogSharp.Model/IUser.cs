﻿using System;
using System.Collections.Generic;

namespace BlogSharp.Model
{
	public interface IUser : IIdentifiable<Guid>, IEntity
	{
		IList<IPost> Posts { get; set; }
		IList<IBlog> Blogs { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		string Email { get; set; }
	}
}
