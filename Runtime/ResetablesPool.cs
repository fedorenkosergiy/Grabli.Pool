using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public class ResetablesPool<T> : Pool<T> where T : class, Resetable, new()
	{
		private readonly Pool<T> pool;

		public int Capacity => pool.Capacity;

		public ResetablesPool(Pool<T> pool) => this.pool = pool;

		public void Get(out T result) => pool.Get(out result);

		public void Release([NotNull] T value)
		{
			value.Reset();
			pool.Release(value);
		}

		public void Clear() => pool.Clear();

		public void Deinit(ISet<DeinitableTree> alreadyHandled)
		{
			if (alreadyHandled.Contains(this)) return;

			alreadyHandled.Add(this);
			pool.Deinit(alreadyHandled);
		}

		public void Resize(int capacity) => pool.Resize(capacity);

		public void Init(int warmUpCount) => pool.Init(warmUpCount);
	}
}
