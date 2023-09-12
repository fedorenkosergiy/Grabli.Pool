using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public abstract class ExtendablePool<T> : Pool<T> where T : class, new()
	{
		private readonly Pool<T> pool;

		public virtual int Capacity => pool.Capacity;

		public ExtendablePool([NotNull] Pool<T> pool) => this.pool = pool;

		public virtual void Get(out T result) => pool.Get(out result);

		public virtual void Release([NotNull] T value) => pool.Release(value);

		public virtual void Clear() => pool.Clear();

		public virtual void Deinit(ISet<DeinitableTree> alreadyHandled) => pool.Deinit(alreadyHandled);

		public virtual void Resize(int capacity) => pool.Resize(capacity);

		public virtual void Init(int warmUpCount) => pool.Init(warmUpCount);
	}
}
