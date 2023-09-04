using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public class ListPool<T> : ExtendablePool<List<T>>
	{
		public ListPool([NotNull] Pool<List<T>> pool) : base(pool) { }

		public T[] GetValueAndRelease(List<T> value)
		{
			T[] result = value.ToArray();
			Release(value);

			return result;
		}

		public override void Release(List<T> value)
		{
			value.Clear();
			base.Release(value);
		}
	}
}
