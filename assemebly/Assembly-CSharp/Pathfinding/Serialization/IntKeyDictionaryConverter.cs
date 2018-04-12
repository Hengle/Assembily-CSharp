using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004A RID: 74
	public class IntKeyDictionaryConverter : JsonConverter
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00019110 File Offset: 0x00017510
		public override bool CanConvert(Type type)
		{
			return object.Equals(type, typeof(Dictionary<int, int>)) || object.Equals(type, typeof(SortedDictionary<int, int>));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001913C File Offset: 0x0001753C
		public override object ReadJson(Type type, Dictionary<string, object> values)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (KeyValuePair<string, object> keyValuePair in values)
			{
				dictionary.Add(Convert.ToInt32(keyValuePair.Key), Convert.ToInt32(keyValuePair.Value));
			}
			return dictionary;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000191B4 File Offset: 0x000175B4
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<int, int> dictionary2 = (Dictionary<int, int>)value;
			foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
			{
				dictionary.Add(keyValuePair.Key.ToString(), keyValuePair.Value);
			}
			return dictionary;
		}
	}
}
