namespace FasterTroubleshoot
{
	public class CacheValue
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
