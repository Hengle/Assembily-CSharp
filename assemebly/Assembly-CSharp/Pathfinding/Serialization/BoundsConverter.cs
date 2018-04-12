using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000047 RID: 71
	public class BoundsConverter : JsonConverter
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00018C5D File Offset: 0x0001705D
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(Bounds));
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00018C70 File Offset: 0x00017070
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			return new Bounds
			{
				center = new Vector3(base.CastFloat(values["cx"]), base.CastFloat(values["cy"]), base.CastFloat(values["cz"])),
				extents = new Vector3(base.CastFloat(values["ex"]), base.CastFloat(values["ey"]), base.CastFloat(values["ez"]))
			};
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00018D0C File Offset: 0x0001710C
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Bounds bounds = (Bounds)value;
			return new Dictionary<string, object>
			{
				{
					"cx",
					bounds.center.x
				},
				{
					"cy",
					bounds.center.y
				},
				{
					"cz",
					bounds.center.z
				},
				{
					"ex",
					bounds.extents.x
				},
				{
					"ey",
					bounds.extents.y
				},
				{
					"ez",
					bounds.extents.z
				}
			};
		}
	}
}
