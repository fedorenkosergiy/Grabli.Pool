using Grabli.Abstraction;
using JetBrains.Annotations;

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

		public void Reset()
		{
			lock (pool) { pool.Reset(); }
		}

		public void Resize(int capacity)
		{
			lock (pool) { pool.Resize(capacity); }
		}

		public void WarmUp(int count)
		{
			lock (pool) { pool.WarmUp(count); }
		}
	}
}
