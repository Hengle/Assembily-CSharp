using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x02000044 RID: 68
	public class UnityObjectConverter : JsonConverter
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0001888A File Offset: 0x00016C8A
		public override bool CanConvert(Type type)
		{
			return typeof(UnityEngine.Object).IsAssignableFrom(type);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001889C File Offset: 0x00016C9C
		public override object ReadJson(Type objectType, Dictionary<string, object> values)
		{
			if (values == null)
			{
				return null;
			}
			string text = (string)values["Name"];
			if (text == null)
			{
				return null;
			}
			string text2 = (string)values["Type"];
			Type type = Type.GetType(text2);
			if (object.Equals(type, null))
			{
				Debug.LogError("Could not find type '" + text2 + "'. Cannot deserialize Unity reference");
				return null;
			}
			if (values.ContainsKey("GUID"))
			{
				string b = (string)values["GUID"];
				UnityReferenceHelper[] array = UnityEngine.Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[];
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].GetGUID() == b)
					{
						if (object.Equals(type, typeof(GameObject)))
						{
							return array[i].gameObject;
						}
						return array[i].GetComponent(type);
					}
					else
					{
						i++;
					}
				}
			}
			UnityEngine.Object[] array2 = Resources.LoadAll(text, type);
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].name == text || array2.Length == 1)
				{
					return array2[j];
				}
			}
			return null;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000189E0 File Offset: 0x00016DE0
		public override Dictionary<string, object> WriteJson(Type type, object value)
		{
			UnityEngine.Object @object = (UnityEngine.Object)value;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (value == null)
			{
				dictionary.Add("Name", null);
				return dictionary;
			}
			dictionary.Add("Name", @object.name);
			dictionary.Add("Type", @object.GetType().AssemblyQualifiedName);
			Component component = value as Component;
			GameObject gameObject = value as GameObject;
			if (component != null || gameObject != null)
			{
				if (component != null && gameObject == null)
				{
					gameObject = component.gameObject;
				}
				UnityReferenceHelper unityReferenceHelper = gameObject.GetComponent<UnityReferenceHelper>();
				if (unityReferenceHelper == null)
				{
					Debug.Log("Adding UnityReferenceHelper to Unity Reference '" + @object.name + "'");
					unityReferenceHelper = gameObject.AddComponent<UnityReferenceHelper>();
				}
				unityReferenceHelper.Reset();
				dictionary.Add("GUID", unityReferenceHelper.GetGUID());
			}
			return dictionary;
		}
	}
}
