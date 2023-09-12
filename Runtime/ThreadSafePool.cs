using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Grabli.Pool
{
	[PublicAPI]
	public class ThreadSafePool<T> : Pool<T> where T : class, new()
	{
		private readonly SimplePool<T> pool;

		public int Capacity
		{
			get
			{
				lock (pool) { return pool.Capacity; }
			}
		}

		public ThreadSafePool() => pool = new SimplePool<T>();

		public ThreadSafePool(int capacity) => pool = new SimplePool<T>(capacity);

		public void Get(out T result)
		{
			lock (pool) { pool.Get(out result); }
		}

		public void Release(T value)
		{
			lock (pool) { pool.Release(value); }
		}

		public void Clear()
		{
			lock (pool) { pool.Clear(); }
		}

		public void Deinit(ISet<DeinitableTree> alreadyHandled)
		{
			lock (pool)
			{
				if (alreadyHandled.Contains(this)) return;

				alreadyHandled.Add(this);
				pool.Deinit(alreadyHandled);
			}
		}

		public void Resize(int capacity)
		{
			lock (pool) { pool.Resize(capacity); }
		}

		public void Init(int warmUpCount)
		{
			lock (pool) { pool.Init(warmUpCount); }
		}
	}
}
