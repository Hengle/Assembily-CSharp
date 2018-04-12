using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Collections.Generic
{
	// Token: 0x02000191 RID: 401
	public static class ListExtension
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0005B580 File Offset: 0x00059980
		public static void RemoveAtFast<T>(this List<T> list, int index)
		{
			list[index] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0005B5A8 File Offset: 0x000599A8
		public static void RemoveFast<T>(this List<T> list, T item)
		{
			int num = list.IndexOf(item);
			if (num < 0)
			{
				return;
			}
			list.RemoveAtFast(num);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0005B5CC File Offset: 0x000599CC
		public static T[] GetInternalArray<T>(this List<T> list)
		{
			return ListExtension.ListInternalArrayAccessor<T>.Getter(list);
		}

		// Token: 0x02000192 RID: 402
		private static class ListInternalArrayAccessor<T>
		{
			// Token: 0x06000BF3 RID: 3059 RVA: 0x0005B5DC File Offset: 0x000599DC
			static ListInternalArrayAccessor()
			{
				DynamicMethod dynamicMethod = new DynamicMethod("get", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, typeof(T[]), new Type[]
				{
					typeof(List<T>)
				}, typeof(ListExtension.ListInternalArrayAccessor<T>), true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, typeof(List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic));
				ilgenerator.Emit(OpCodes.Ret);
				ListExtension.ListInternalArrayAccessor<T>.Getter = (Func<List<T>, T[]>)dynamicMethod.CreateDelegate(typeof(Func<List<T>, T[]>));
			}

			// Token: 0x0400086E RID: 2158
			public static Func<List<T>, T[]> Getter;
		}
	}
}
