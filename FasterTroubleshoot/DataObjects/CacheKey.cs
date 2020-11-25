using System;
using System.Collections.Generic;
using System.Text;

namespace FasterTroubleshoot
{
	public class CacheKey
	{
		public string Value { get; set; }

		public override bool Equals(object obj)
		{
			return obj is CacheKey key &&
						 Value == key.Value;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
