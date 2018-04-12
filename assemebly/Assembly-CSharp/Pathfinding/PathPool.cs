using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200002B RID: 43
	public static class PathPool<T> where T : Path, new()
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00012CF8 File Offset: 0x000110F8
		public static void Recycle(T path)
		{
			object obj = PathPool<T>.pool;
			lock (obj)
			{
				path.recycled = true;
				path.OnEnterPool();
				PathPool<T>.pool.Push(path);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00012D54 File Offset: 0x00011154
		public static void Warmup(int count, int length)
		{
			ListPool<GraphNode>.Warmup(count, length);
			ListPool<Vector3>.Warmup(count, length);
			Path[] array = new Path[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = PathPool<T>.GetPath();
				array[i].Claim(array);
			}
			for (int j = 0; j < count; j++)
			{
				array[j].Release(array);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00012DB9 File Offset: 0x000111B9
		public static int GetTotalCreated()
		{
			return PathPool<T>.totalCreated;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00012DC0 File Offset: 0x000111C0
		public static int GetSize()
		{
			return PathPool<T>.pool.Count;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00012DCC File Offset: 0x000111CC
		public static T GetPath()
		{
			object obj = PathPool<T>.pool;
			T result;
			lock (obj)
			{
				T t;
				if (PathPool<T>.pool.Count > 0)
				{
					t = PathPool<T>.pool.Pop();
				}
				else
				{
					t = Activator.CreateInstance<T>();
					PathPool<T>.totalCreated++;
				}
				t.recycled = false;
				t.Reset();
				result = t;
			}
			return result;
		}

		// Token: 0x04000175 RID: 373
		private static Stack<T> pool = new Stack<T>();

		// Token: 0x04000176 RID: 374
		private static int totalCreated;
	}
}
