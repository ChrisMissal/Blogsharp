﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogSharp.Core.Event
{
	public interface ICancellableEvent<T>:IEvent<T>
	{
		bool Cancel { get; set; }
	}
}