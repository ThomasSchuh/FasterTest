using FASTER.core;
using System;
using System.Collections.Generic;
using System.Text;
using static FasterTroubleshoot.Case1;

namespace FasterTroubleshoot
{
  public class StoreFunctions : SimpleFunctions<CacheKey, CacheValue, StoreContext>
  {
    public override void ReadCompletionCallback(ref CacheKey key, ref CacheValue input, ref CacheValue output, StoreContext ctx, Status status)
       => ctx.Populate(ref status, ref output);
  }
	public class StoreContext
	{
		private Status status;
		private CacheValue output;

		internal void Populate(ref Status status, ref CacheValue output)
		{
			this.status = status;
			this.output = output;
		}

		internal void FinalizeRead(ref Status status, ref CacheValue output)
		{
			status = this.status;
			output = this.output;
		}
	}
}
