﻿using System;
using System.Collections;
using System.Collections.Generic;
using BlogSharp.Model;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace BlogSharp.Core.Impl
{
	public class EntityFactory<T>:IEntityFactory<T>
	{
		public EntityFactory(ProxyGenerator generator)
		{
			this.generator = generator;
		}

		private readonly ProxyGenerator generator;
		#region IEntityFactory<T> Members

		public T Create()
		{
			T obj = (T)generator.CreateInterfaceProxyWithoutTarget<T>(new EntityInterceptor());
			return obj;
		}

		#endregion
		internal class EntityInterceptor:IInterceptor
		{
			private readonly IDictionary dictionary;

			public EntityInterceptor()
			{
				dictionary = new System.Collections.Specialized.HybridDictionary();
			}
			#region IInterceptor Members

			public void Intercept(IInvocation invocation)
			{
				string methodName = invocation.Method.Name;
				
				var v = invocation.Method.MemberType;
				if(methodName.StartsWith("get_"))
				{
					string propName = invocation.Method.Name.Substring(4);
					if (dictionary.Contains(propName))
						invocation.ReturnValue = dictionary[propName];
					else if(invocation.Method.ReturnType.GetGenericTypeDefinition()==typeof(IList<>))
					{
						Type elementType = invocation.Method.ReturnType.GetGenericArguments()[0];
						invocation.ReturnValue = GenerateAndStoreListForName(propName, elementType);
					}
				}
				else if (methodName.StartsWith("set_"))
				{
					string propName = invocation.Method.Name.Substring(4);
					dictionary[propName] = invocation.Arguments[0];
				}
			}
			private object GenerateAndStoreListForName(string propName, Type t)
			{
				object o = Activator.CreateInstance(typeof (List<>).MakeGenericType(t));
				dictionary[propName] = o;
				return o;
			}
			#endregion
		}
	}
}
