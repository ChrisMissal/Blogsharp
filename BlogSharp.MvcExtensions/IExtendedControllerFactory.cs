﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSharp.MvcExtensions
{
	public interface IExtendedControllerFactory:IControllerFactory
	{
		IController CreateController(RequestContext context, Type controllerType);
	}
}