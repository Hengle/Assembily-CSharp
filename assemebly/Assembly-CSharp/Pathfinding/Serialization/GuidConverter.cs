using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;

namespace Pathfinding.Serialization
{
	// Token: 0x02000045 RID: 69
	public class GuidConverter : JsonConverter
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00018AD3 File Offset: 0x00016ED3
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(Pathfinding.Util.Guid));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00018AE8 File Offset: 0x00016EE8
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			string str = (string)values["value"];
			return new Pathfinding.Util.Guid(str);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018B14 File Offset: 0x00016F14
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Pathfinding.Util.Guid guid = (Pathfinding.Util.Guid)value;
			return new Dictionary<string, object>
			{
				{
					"value",
					guid.ToString()
				}
			};
		}
	}
}
