using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public class StackPool<T> : ExtendablePool<Stack<T>>
	{
		public StackPool([NotNull] Pool<Stack<T>> pool) : base(pool) { }

		public T[] GetValueAndRelease(Stack<T> value)
		{
			T[] result = value.ToArray();
			Release(value);

			return result;
		}

		public override void Release(Stack<T> value)
		{
			value.Clear();
			base.Release(value);
		}
	}
}
