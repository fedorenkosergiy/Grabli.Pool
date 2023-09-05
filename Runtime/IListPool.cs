using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public class IListPool<T> : ExtendableCreatorBasedPool<IList<T>>
	{
		public IListPool([NotNull] CreatorBasedPool<IList<T>> pool) : base(pool) { }

		public override void Release(IList<T> value)
		{
			value.Clear();
			base.Release(value);
		}
	}
}
