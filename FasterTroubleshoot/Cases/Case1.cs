using FASTER.core;
using System;

namespace FasterTroubleshoot
{
	class Case1
	{
		private FasterKV<CacheKey, CacheValue> _fasterStore;

		public Case1()
		{
			_fasterStore = new FasterKV<CacheKey, CacheValue>(
				1 << 20,
				Settings.LogSettings,
				null,
				Settings.SerializerSettings);
		}

		public void Execute()
		{
			var session = _fasterStore.NewSession(new StoreFunctions());

			var left = DemoDataFactory.LeftList;
			foreach (var item in left)
			{
				var context = new StoreContext();
				session.Upsert(item.Key, item.Value, context, 1);

				//session.CompletePending(true);
				//session.Dispose();			
			}

			//session.CompletePending(true, true);
			//session.Refresh();

			var right = DemoDataFactory.RightList;
			var matches = 0;
			foreach (var item in right)
			{
				var context = new StoreContext();
				CacheKey key = item.Key;
				CacheValue value = null;
				var status = session.Read(ref key, ref value, context, 1);
				switch (status)
				{
					case Status.OK:
						break;
					case Status.PENDING:
						session.CompletePending(true);
						context.FinalizeRead(ref status, ref value);
						break;
					default:
						throw new Exception();
				}
			}

			Console.WriteLine("KeySerializer.EmptyReads: " + KeySerializer.EmptyReads);
			Console.WriteLine("ValueSerializer.EmptyReads: " + ValueSerializer.EmptyReads);
		}

		

	}
}
