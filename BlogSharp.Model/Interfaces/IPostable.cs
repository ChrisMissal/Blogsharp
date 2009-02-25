﻿// <copyright file="IPostable.cs" company="BlogSharp">
// Apache Licence 2.0 
// </copyright>
// <author>Gonzalo Brusella</author>
// <email>gonzalo@brusella.com.ar</email>
// <date>2009-02-22</date>

namespace BlogSharp.Model.Interfaces
{
	using System;

	/// <summary>
	/// Define the common base for all the postable items.
	/// </summary>
	public interface IPostable
	{
		/// <summary>
		/// Gets or sets Blog.
		/// </summary>
		Blog Blog { get; set; }

		/// <summary>
		/// Gets or sets User.
		/// </summary>
		User User { get; set; }

		/// <summary>
		/// Gets or sets DateCreated.
		/// </summary>
		DateTime DateCreated { get; set; }

		/// <summary>
		/// Gets or sets DatePublished.
		/// </summary>
		DateTime DatePublished { get; set; }

		/// <summary>
		/// Gets or sets Content.
		/// </summary>
		string Content { get; set; }
	}
}