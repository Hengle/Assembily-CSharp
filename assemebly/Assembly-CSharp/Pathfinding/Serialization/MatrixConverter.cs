using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000046 RID: 70
	public class MatrixConverter : JsonConverter
	{
		// Token: 0x06000318 RID: 792 RVA: 0x00018B5C File Offset: 0x00016F5C
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(Matrix4x4));
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00018B70 File Offset: 0x00016F70
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			Array array = (Array)values["values"];
			if (array.Length != 16)
			{
				Debug.LogError("Number of elements in matrix was not 16 (got " + array.Length + ")");
				return matrix4x;
			}
			for (int i = 0; i < 16; i++)
			{
				matrix4x[i] = Convert.ToSingle(array.GetValue(new int[]
				{
					i
				}));
			}
			return matrix4x;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00018C00 File Offset: 0x00017000
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Matrix4x4 matrix4x = (Matrix4x4)value;
			for (int i = 0; i < this.values.Length; i++)
			{
				this.values[i] = matrix4x[i];
			}
			return new Dictionary<string, object>
			{
				{
					"values",
					this.values
				}
			};
		}

		// Token: 0x04000263 RID: 611
		private float[] values = new float[16];
	}
}
