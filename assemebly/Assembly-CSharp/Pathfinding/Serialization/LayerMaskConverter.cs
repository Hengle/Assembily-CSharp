using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000048 RID: 72
	public class LayerMaskConverter : JsonConverter
	{
		// Token: 0x06000320 RID: 800 RVA: 0x00018DED File Offset: 0x000171ED
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(LayerMask));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00018DFF File Offset: 0x000171FF
		public override object ReadJson(Type type, Dictionary<string, object> values)
		{
			return (int)values["value"];
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00018E1C File Offset: 0x0001721C
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			return new Dictionary<string, object>
			{
				{
					"value",
					((LayerMask)value).value
				}
			};
		}
	}
}
