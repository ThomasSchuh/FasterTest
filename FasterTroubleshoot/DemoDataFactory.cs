using System.Collections.Generic;
using System.Linq;

namespace FasterTroubleshoot
{
	public class DemoDataFactory
	{
		public static int _itemCount = 500000;	

		public static IEnumerable<KeyValuePair<CacheKey, CacheValue>> LeftList => Enumerable.Range(1, _itemCount)
			.Select(i => new KeyValuePair<CacheKey, CacheValue>(
				new CacheKey { Value = "key " + i },
				new CacheValue { Value = "value left " + i }));
		public static IEnumerable<KeyValuePair<CacheKey, CacheValue>> RightList => Enumerable.Range(1, _itemCount)
			.Select(i => new KeyValuePair<CacheKey, CacheValue>(
				new CacheKey { Value = "key " + i },
				new CacheValue { Value = "value right " + i }));
	}
}
