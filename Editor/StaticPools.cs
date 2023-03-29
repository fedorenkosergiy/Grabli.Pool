using System;
using System.Text;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Grabli.Pool
{
	[PublicAPI]
	public static class StaticPools
	{
		private static StringBuilderPool stringBuilder;
		private static IDictionary<Type, object> stackPools;

		public static StringBuilderPool StringBuilder
		{
			get => stringBuilder ??= new StringBuilderPool(new ThreadSafePool<StringBuilder>());
		}

		public static StackPool<Stack<T>> GetStackPool<T>()
		{
			Type type = typeof(T);

			if (stackPools == null)
			{
				stackPools = new Dictionary<Type, object>();

				return CreatStackPool<T>();
			}

			if (stackPools.TryGetValue(type, out object result)) return (StackPool<Stack<T>>)result;

			return CreatStackPool<T>();
		}

		private static StackPool<Stack<T>> CreatStackPool<T>()
		{
			StackPool<Stack<T>> stack = new StackPool<Stack<T>>(new ThreadSafePool<Stack<Stack<T>>>());
			stackPools.Add(typeof(T), stack);

			return stack;
		}
	}
}
