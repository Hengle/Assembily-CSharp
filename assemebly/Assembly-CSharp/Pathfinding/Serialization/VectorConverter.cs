using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000049 RID: 73
	public class VectorConverter : JsonConverter
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00018E56 File Offset: 0x00017256
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(Vector2)) || object.Equals(type, typeof(Vector3)) || object.Equals(type, typeof(Vector4));
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00018E98 File Offset: 0x00017298
		public override object ReadJson(Type type, Dictionary<string, object> values)
		{
			if (object.Equals(type, typeof(Vector2)))
			{
				return new Vector2(base.CastFloat(values["x"]), base.CastFloat(values["y"]));
			}
			if (object.Equals(type, typeof(Vector3)))
			{
				return new Vector3(base.CastFloat(values["x"]), base.CastFloat(values["y"]), base.CastFloat(values["z"]));
			}
			if (object.Equals(type, typeof(Vector4)))
			{
				return new Vector4(base.CastFloat(values["x"]), base.CastFloat(values["y"]), base.CastFloat(values["z"]), base.CastFloat(values["w"]));
			}
			throw new NotImplementedException("Can only read Vector2,3,4. Not objects of type " + type);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00018FB0 File Offset: 0x000173B0
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			if (object.Equals(type, typeof(Vector2)))
			{
				Vector2 vector = (Vector2)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector.x
					},
					{
						"y",
						vector.y
					}
				};
			}
			if (object.Equals(type, typeof(Vector3)))
			{
				Vector3 vector2 = (Vector3)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector2.x
					},
					{
						"y",
						vector2.y
					},
					{
						"z",
						vector2.z
					}
				};
			}
			if (object.Equals(type, typeof(Vector4)))
			{
				Vector4 vector3 = (Vector4)value;
				return new Dictionary<string, object>
				{
					{
						"x",
						vector3.x
					},
					{
						"y",
						vector3.y
					},
					{
						"z",
						vector3.z
					},
					{
						"w",
						vector3.w
					}
				};
			}
			throw new NotImplementedException("Can only write Vector2,3,4. Not objects of type " + type);
		}
	}
}
