using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000CC RID: 204
	public class ModifierConverter
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0004282C File Offset: 0x00040C2C
		public static bool AllBits(ModifierData a, ModifierData b)
		{
			return (a & b) == b;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00042834 File Offset: 0x00040C34
		public static bool AnyBits(ModifierData a, ModifierData b)
		{
			return (a & b) != ModifierData.None;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00042840 File Offset: 0x00040C40
		public static ModifierData Convert(Path p, ModifierData input, ModifierData output)
		{
			if (!ModifierConverter.CanConvert(input, output))
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Can't convert ",
					input,
					" to ",
					output
				}));
				return ModifierData.None;
			}
			if (ModifierConverter.AnyBits(input, output))
			{
				return input;
			}
			if (ModifierConverter.AnyBits(input, ModifierData.Nodes) && ModifierConverter.AnyBits(output, ModifierData.Vector))
			{
				p.vectorPath.Clear();
				for (int i = 0; i < p.vectorPath.Count; i++)
				{
					p.vectorPath.Add((Vector3)p.path[i].position);
				}
				return ModifierData.VectorPath | ((!ModifierConverter.AnyBits(input, ModifierData.StrictNodePath)) ? ModifierData.None : ModifierData.StrictVectorPath);
			}
			Debug.LogError(string.Concat(new object[]
			{
				"This part should not be reached - Error in ModifierConverted\nInput: ",
				input,
				" (",
				(int)input,
				")\nOutput: ",
				output,
				" (",
				(int)output,
				")"
			}));
			return ModifierData.None;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00042970 File Offset: 0x00040D70
		public static bool CanConvert(ModifierData input, ModifierData output)
		{
			ModifierData b = ModifierConverter.CanConvertTo(input);
			return ModifierConverter.AnyBits(output, b);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0004298C File Offset: 0x00040D8C
		public static ModifierData CanConvertTo(ModifierData a)
		{
			if (a == ModifierData.All)
			{
				return ModifierData.All;
			}
			ModifierData modifierData = a;
			if (ModifierConverter.AnyBits(a, ModifierData.Nodes))
			{
				modifierData |= ModifierData.VectorPath;
			}
			if (ModifierConverter.AnyBits(a, ModifierData.StrictNodePath))
			{
				modifierData |= ModifierData.StrictVectorPath;
			}
			if (ModifierConverter.AnyBits(a, ModifierData.StrictVectorPath))
			{
				modifierData |= ModifierData.VectorPath;
			}
			return modifierData;
		}
	}
}
