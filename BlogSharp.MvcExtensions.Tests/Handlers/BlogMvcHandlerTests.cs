﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlogSharp.MvcExtensions.Handlers;
using Rhino.Mocks;
using Xunit;

namespace BlogSharp.MvcExtensions.Tests.Handlers
{
	public class DummyController:ControllerBase
	{
		protected override void Execute(RequestContext requestContext)
		{
			WasCalled = true;
			base.Execute(requestContext);
		}

		public bool WasCalled { get; set; }

		protected override void ExecuteCore()
		{
			
		}
	}
	public class DummyController2 : ControllerBase
	{
		protected override void Execute(RequestContext requestContext)
		{
			throw new Exception();
			base.Execute(requestContext);
		}


		protected override void ExecuteCore()
		{

		}
	}
	public class BlogMvcHandlerTests
	{
		public BlogMvcHandlerTests()
		{
			context = TestsHelper.PrepareRequestContext();
			handler = new BlogMvcHandler(context);
			context.RouteData.Values["controller"] = typeof (Controller);
			methodInfo = handler.GetType()
				.GetMethod("ProcessRequest", BindingFlags.NonPublic | BindingFlags.Instance, null,
				           new[] {typeof (HttpContextBase)}, null);
			dummyFactory = MockRepository.GenerateStub<IExtendedControllerFactory>();
			ControllerBuilder.Current.SetControllerFactory(dummyFactory);

		}

		private MethodInfo methodInfo;
		private MvcHandler handler;
		private IExtendedControllerFactory dummyFactory;
		private RequestContext context;
		[Fact]
		public void Factory_should_resolve_given_controller_with_its_type()
		{
			var dummyController = new DummyController();
			dummyFactory.Expect(x => x.CreateController(Arg<RequestContext>.Is.Anything,
			                                            Arg<Type>.Is.Equal(typeof (Controller))))
				.Return(dummyController)
				.Repeat.Any();

			methodInfo.Invoke(handler, new object[]{context.HttpContext});
			dummyFactory.AssertWasCalled(x => x.CreateController(Arg<RequestContext>.Is.Anything, Arg<Type>.Is.Equal(typeof(Controller))));
			Assert.True(dummyController.WasCalled);
		}
		[Fact]
		public void Can_release_when_exception_occurs()
		{
			var dummyController2 = new DummyController2();
			dummyFactory.Expect(x => x.CreateController(Arg<RequestContext>.Is.Anything,
			                                            Arg<Type>.Is.Equal(typeof (Controller))))
				.Return(dummyController2)
				.Repeat.Any();
			ControllerBuilder.Current.SetControllerFactory(dummyFactory);
			try
			{
				methodInfo.Invoke(handler, new object[] {context.HttpContext});
			}
			catch
			{
				
			}
			dummyFactory.AssertWasCalled(x => x.CreateController(Arg<RequestContext>.Is.Anything, Arg<Type>.Is.Equal(typeof(Controller))));
			dummyFactory.AssertWasCalled(x => x.ReleaseController(dummyController2));
		}
	}
}
