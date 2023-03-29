using Grabli.Abstraction;
using JetBrains.Annotations;
using System.Text;

namespace Grabli.Pool
{
	[PublicAPI]
	public class StringBuilderPool : ExtendablePool<StringBuilder>
	{
		public StringBuilderPool([NotNull] Pool<StringBuilder> pool) : base(pool) { }

		public string GetValueAndRelease(StringBuilder value)
		{
			string result = value.ToString();
			Release(value);

			return result;
		}

		public override void Release(StringBuilder value)
		{
			value.Clear();
			base.Release(value);
		}
	}
}
