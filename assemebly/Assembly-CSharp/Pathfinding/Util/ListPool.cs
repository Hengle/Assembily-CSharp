using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x02000024 RID: 36
	public static class ListPool<T>
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00010FA8 File Offset: 0x0000F3A8
		public static List<T> Claim()
		{
			object obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
					ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					result = list;
				}
				else
				{
					result = new List<T>();
				}
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00011028 File Offset: 0x0000F428
		public static List<T> Claim(int capacity)
		{
			object obj = ListPool<T>.pool;
			List<T> result;
			lock (obj)
			{
				if (ListPool<T>.pool.Count > 0)
				{
					List<T> list = null;
					int num = 0;
					while (num < ListPool<T>.pool.Count && num < 8)
					{
						list = ListPool<T>.pool[ListPool<T>.pool.Count - 1 - num];
						if (list.Capacity >= capacity)
						{
							ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1 - num);
							return list;
						}
						num++;
					}
					if (list == null)
					{
						list = new List<T>(capacity);
					}
					else
					{
						list.Capacity = capacity;
						ListPool<T>.pool[ListPool<T>.pool.Count - num] = ListPool<T>.pool[ListPool<T>.pool.Count - 1];
						ListPool<T>.pool.RemoveAt(ListPool<T>.pool.Count - 1);
					}
					result = list;
				}
				else
				{
					result = new List<T>(capacity);
				}
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00011140 File Offset: 0x0000F540
		public static void Warmup(int count, int size)
		{
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				List<T>[] array = new List<T>[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = ListPool<T>.Claim(size);
				}
				for (int j = 0; j < count; j++)
				{
					ListPool<T>.Release(array[j]);
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000111B4 File Offset: 0x0000F5B4
		public static void Release(List<T> list)
		{
			list.Clear();
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				for (int i = 0; i < ListPool<T>.pool.Count; i++)
				{
					if (ListPool<T>.pool[i] == list)
					{
						throw new InvalidOperationException("The List is released even though it is in the pool");
					}
				}
				ListPool<T>.pool.Add(list);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00011234 File Offset: 0x0000F634
		public static void Clear()
		{
			object obj = ListPool<T>.pool;
			lock (obj)
			{
				ListPool<T>.pool.Clear();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00011274 File Offset: 0x0000F674
		public static int GetSize()
		{
			return ListPool<T>.pool.Count;
		}

		// Token: 0x04000151 RID: 337
		private static List<List<T>> pool = new List<List<T>>();

		// Token: 0x04000152 RID: 338
		private const int MaxCapacitySearchLength = 8;
	}
}
