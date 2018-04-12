using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000190 RID: 400
	[StructLayout(LayoutKind.Explicit)]
	internal struct GuidBuffer
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0005B492 File Offset: 0x00059892
		public GuidBuffer(Guid GUID)
		{
			this = default(GuidBuffer);
			this.GUID = GUID;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0005B4A4 File Offset: 0x000598A4
		public unsafe void Read(byte[] source, int offset)
		{
			if (offset + 16 > source.Length)
			{
				throw new ArgumentException("Destination buffer is too small!");
			}
			fixed (byte* ptr = (source != null && source.Length != 0) ? ref source[0] : ref *null)
			{
				fixed (ulong* ptr2 = &this.buffer.FixedElementField)
				{
					byte* ptr3 = ptr + (IntPtr)offset;
					ulong* ptr4 = (ulong*)ptr3;
					*ptr2 = *ptr4;
					ptr2[8] = ptr4[1];
				}
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0005B50C File Offset: 0x0005990C
		public unsafe void Write(byte[] destination, int offset)
		{
			if (offset + 16 > destination.Length)
			{
				throw new ArgumentException("Destination buffer is too small!");
			}
			fixed (byte* ptr = (destination != null && destination.Length != 0) ? ref destination[0] : ref *null)
			{
				fixed (ulong* ptr2 = &this.buffer.FixedElementField)
				{
					byte* ptr3 = ptr + (IntPtr)offset;
					ulong* ptr4 = (ulong*)ptr3;
					*ptr4 = *ptr2;
					ptr4[1] = ptr2[8];
				}
			}
		}

		// Token: 0x0400086B RID: 2155
		public static readonly byte[] GUID_BUFFER = new byte[16];

		// Token: 0x0400086C RID: 2156
		[FieldOffset(0)]
		private GuidBuffer.<buffer>__FixedBuffer0 buffer;

		// Token: 0x0400086D RID: 2157
		[FieldOffset(0)]
		public Guid GUID;

		// Token: 0x02000848 RID: 2120
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <buffer>__FixedBuffer0
		{
			// Token: 0x0400305F RID: 12383
			public ulong FixedElementField;
		}
	}
}
