using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x0200002A RID: 42
	public static class ObjectPool<T> where T : class, IAstarPooledObject, new()
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00012BC8 File Offset: 0x00010FC8
		public static T Claim()
		{
			if (ObjectPool<T>.pool.Count > 0)
			{
				T result = ObjectPool<T>.pool[ObjectPool<T>.pool.Count - 1];
				ObjectPool<T>.pool.RemoveAt(ObjectPool<T>.pool.Count - 1);
				return result;
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00012C1C File Offset: 0x0001101C
		public static void Warmup(int count)
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ObjectPool<T>.Claim();
			}
			for (int j = 0; j < count; j++)
			{
				ObjectPool<T>.Release(array[j]);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00012C6C File Offset: 0x0001106C
		public static void Release(T obj)
		{
			for (int i = 0; i < ObjectPool<T>.pool.Count; i++)
			{
				if (ObjectPool<T>.pool[i] == obj)
				{
					throw new InvalidOperationException("The object is released even though it is in the pool. Are you releasing it twice?");
				}
			}
			obj.OnEnterPool();
			ObjectPool<T>.pool.Add(obj);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00012CD2 File Offset: 0x000110D2
		public static void Clear()
		{
			ObjectPool<T>.pool.Clear();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00012CDE File Offset: 0x000110DE
		public static int GetSize()
		{
			return ObjectPool<T>.pool.Count;
		}

		// Token: 0x04000174 RID: 372
		private static List<T> pool = new List<T>();
	}
}
