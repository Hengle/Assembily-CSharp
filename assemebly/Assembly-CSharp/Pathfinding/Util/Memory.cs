using System;

namespace Pathfinding.Util
{
	// Token: 0x020000EE RID: 238
	public static class Memory
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0004AEBC File Offset: 0x000492BC
		public static void MemSet(byte[] array, byte value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i++] = value;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i, Math.Min(num, num2 - i));
				i += num;
				num *= 2;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0004AF28 File Offset: 0x00049328
		public static void MemSet<T>(T[] array, T value, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, array.Length);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			num2 = array.Length;
			while (i < num2)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, num2 - i) * byteSize);
				i += num;
				num *= 2;
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0004AF9C File Offset: 0x0004939C
		public static void MemSet<T>(T[] array, T value, int totalSize, int byteSize) where T : struct
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = 32;
			int i = 0;
			int num2 = Math.Min(num, totalSize);
			while (i < num2)
			{
				array[i] = value;
				i++;
			}
			while (i < totalSize)
			{
				Buffer.BlockCopy(array, 0, array, i * byteSize, Math.Min(num, totalSize - i) * byteSize);
				i += num;
				num *= 2;
			}
		}
	}
}
