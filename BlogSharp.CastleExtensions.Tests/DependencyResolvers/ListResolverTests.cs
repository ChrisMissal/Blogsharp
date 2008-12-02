﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogSharp.CastleExtensions.DependencyResolvers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Xunit;

namespace BlogSharp.CastleExtensions.Tests.DependencyResolvers
{
	public class EnumerableDependencyResolverTests
	{
		[Fact]
		public void Can_resolve_IList_with_all_services()
		{
			IWindsorContainer x = new WindsorContainer();
			x.Kernel.Resolver.AddSubResolver(new ListResolver(x.Kernel));
			x.Register(Component.For<ISample>().ImplementedBy<SampleImpl1>());
			x.Register(Component.For<ISample>().ImplementedBy<SampleImpl2>());
			x.Register(Component.For<SimpleServiceRequiringClass>());
			var item = x.Resolve<SimpleServiceRequiringClass>();
			Assert.Equal(2, item.Samples.Count);
		}
	}

	public class SimpleServiceRequiringClass
	{
		public SimpleServiceRequiringClass(IList<ISample> samples)
		{
			Samples = samples;
		}

		public IList<ISample> Samples { get; set; }
	}

	public interface ISample
	{
	}

	public class SampleImpl1 : ISample
	{
	}

	public class SampleImpl2 : ISample
	{
	}
}
