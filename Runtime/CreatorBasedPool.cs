using Grabli.Abstraction;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

namespace Grabli.Pool
{
	[PublicAPI]
	public class CreatorBasedPool<T> : Pool<T> where T : class
	{
		public const int MinCapacity = 4;
		public const int DefaultCapacity = 128;
		public const int MaxReasonableInitCapacity = 256;

		private Creator<T> creator;
		private int initialCapacity;
		private readonly Stack<T> pool;

		public int Capacity { get; private set; }

		public CreatorBasedPool(Creator<T> customCreator) : this(customCreator, DefaultCapacity) { }

		public CreatorBasedPool(Creator<T> customCreator, int capacity)
		{
			if (capacity < MinCapacity) throw new ArgumentOutOfRangeException(nameof(capacity));

			creator = customCreator;
			initialCapacity = capacity;
			Capacity = capacity;
			pool = new Stack<T>(Math.Min(capacity, MaxReasonableInitCapacity));
		}

		public void Get(out T result) => result = pool.Count == 0 ? creator.Create() : pool.Pop();

		public void Release([NotNull] T value)
		{
			if (pool.Count >= Capacity) return;

			pool.Push(value);
		}

		public void Clear() => pool.Clear();

		public void Deinit(ISet<Deinitable> alreadyHandled)
		{
			if (alreadyHandled.Contains(this)) return;

			alreadyHandled.Add(this);
			pool.Clear();
			Capacity = initialCapacity;
		}

		public void Resize(int capacity)
		{
			if (capacity <= MinCapacity) throw new ArgumentOutOfRangeException(nameof(capacity));

			Capacity = capacity;

			while (pool.Count > Capacity) { pool.Pop(); }
		}

		public void Init(int warmUpCount)
		{
			while (pool.Count < Capacity && pool.Count < warmUpCount) { pool.Push(creator.Create()); }
		}
	}
}
