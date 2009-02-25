﻿// <copyright file="ICommentable.cs" company="BlogSharp">
// Apache Licence 2.0 
// </copyright>
// <author>Gonzalo Brusella</author>
// <email>gonzalo@brusella.com.ar</email>
// <date>2009-02-22</date>

namespace BlogSharp.Model.Interfaces
{
	using System.Collections.Generic;

	/// <summary>
	/// Base Interface for all the commentable Items.
	/// </summary>
	public interface ICommentable
	{
		/// <summary>
		/// Gets or sets Blog.
		/// </summary>
		Blog Blog { get; set; }

		/// <summary>
		/// Gets or sets Comments.
		/// </summary>
		IList<PostComment> Comments { get; set; }

		/// <summary>
		/// Adds a Comment to a Post.
		/// </summary>
		/// <param name="comment">The Comment to add.</param>
		/// <param name="post">The Post to comment.</param>
		void AddComment(PostComment comment, ICommentable post);
	}
}