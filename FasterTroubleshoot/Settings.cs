using FASTER.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FasterTroubleshoot
{
	public class Settings
	{
		private static readonly string _deviceLogName = Guid.NewGuid().ToString();

		public const int IndexCapacity = 20;
		public static IDevice LogDevice { get; private set; } = BuildLogDevice();
		public static IDevice ObjectLogDevice { get; private set; } = BuildObjectLogDevice();

		public static LogSettings LogSettings { get; private set; } = BuildLogAndSettings();

		public static SerializerSettings<CacheKey, CacheValue> SerializerSettings { get; private set; } = new SerializerSettings<CacheKey, CacheValue>
		{
			keySerializer = () => new KeySerializer(),
			valueSerializer = () => new ValueSerializer()
		};

		private static LogSettings BuildLogAndSettings()
		{
			var logSettings = new LogSettings
			{
				LogDevice = LogDevice,
				ObjectLogDevice = ObjectLogDevice,
				PageSizeBits = 12,
				MemorySizeBits = 16,
			};
			return logSettings;
		}

		private static IDevice BuildLogDevice()
		{
			return Devices.CreateLogDevice(
				Path.Combine(@".\Cache", $"{_deviceLogName}.log"),
				preallocateFile: false,
				deleteOnClose: false);
		}
		private static IDevice BuildObjectLogDevice()
		{
			return Devices.CreateLogDevice(
				Path.Combine(@".\Cache", $"{ _deviceLogName}.obj.log"),
				preallocateFile: false,
				deleteOnClose: false);
		}
	}
}
