using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200002C RID: 44
	public static class StackPool<T>
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00012E5C File Offset: 0x0001125C
		public static Stack<T> Claim()
		{
			if (StackPool<T>.pool.Count > 0)
			{
				Stack<T> result = StackPool<T>.pool[StackPool<T>.pool.Count - 1];
				StackPool<T>.pool.RemoveAt(StackPool<T>.pool.Count - 1);
				return result;
			}
			return new Stack<T>();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00012EB0 File Offset: 0x000112B0
		public static void Warmup(int count)
		{
			Stack<T>[] array = new Stack<T>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = StackPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				StackPool<T>.Release(array[j]);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00012EF8 File Offset: 0x000112F8
		public static void Release(Stack<T> stack)
		{
			for (int i = 0; i < StackPool<T>.pool.Count; i++)
			{
				if (StackPool<T>.pool[i] == stack)
				{
					Debug.LogError("The Stack is released even though it is inside the pool");
				}
			}
			stack.Clear();
			StackPool<T>.pool.Add(stack);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00012F4C File Offset: 0x0001134C
		public static void Clear()
		{
			StackPool<T>.pool.Clear();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00012F58 File Offset: 0x00011358
		public static int GetSize()
		{
			return StackPool<T>.pool.Count;
		}

		// Token: 0x04000177 RID: 375
		private static List<Stack<T>> pool = new List<Stack<T>>();
	}
}
