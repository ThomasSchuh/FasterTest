using FASTER.core;
using System;

namespace FasterTroubleshoot
{
	internal class KeySerializer : BinaryObjectSerializer<CacheKey>
	{
		public static int EmptyReads = 0;

		public override void Deserialize(out CacheKey obj)
		{
			obj = new CacheKey();
			var bytesr = new byte[4];
			reader.Read(bytesr, 0, 4);
			var sizet = BitConverter.ToInt32(bytesr);
			var bytes = new byte[sizet];
			reader.Read(bytes, 0, sizet);

			if (sizet <= 0)
			{
				EmptyReads++;
				Console.WriteLine("Empty stream found");
			}

			obj.Value = System.Text.Encoding.UTF8.GetString(bytes);
		}

		public override void Serialize(ref CacheKey obj)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(obj.Value);
			var len = BitConverter.GetBytes(bytes.Length);
			writer.Write(len);
			writer.Write(bytes);
		}
	}

	internal class ValueSerializer : BinaryObjectSerializer<CacheValue>
	{
		public static int EmptyReads = 0;
		public override void Deserialize(out CacheValue obj)
		{
			obj = new CacheValue();
			var bytesr = new byte[4];
			reader.Read(bytesr, 0, 4);
			var sizet = BitConverter.ToInt32(bytesr);
			var bytes = new byte[sizet];
			reader.Read(bytes, 0, sizet);

			if (sizet <= 0) EmptyReads++;

			obj.Value = System.Text.Encoding.UTF8.GetString(bytes);
		}

		public override void Serialize(ref CacheValue obj)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(obj.Value);
			var len = BitConverter.GetBytes(bytes.Length);
			writer.Write(len);
			writer.Write(bytes);
		}
	}
}
