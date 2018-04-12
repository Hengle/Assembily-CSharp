using System;

namespace SDG.Unturned
{
	// Token: 0x0200073A RID: 1850
	public class MeasurementTool
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x00154A4B File Offset: 0x00152E4B
		public static float speedToKPH(float speed)
		{
			return speed * 3.6f;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x00154A54 File Offset: 0x00152E54
		public static float KPHToMPH(float kph)
		{
			return kph / 1.609344f;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x00154A5D File Offset: 0x00152E5D
		public static float KtoM(float k)
		{
			return k * 0.621371f;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00154A66 File Offset: 0x00152E66
		public static float MtoYd(float m)
		{
			return m * 1.09361f;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x00154A6F File Offset: 0x00152E6F
		public static int MtoYd(int m)
		{
			return (int)((float)m * 1.09361f);
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00154A7A File Offset: 0x00152E7A
		public static long MtoYd(long m)
		{
			return (long)((float)m * 1.09361f);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x00154A85 File Offset: 0x00152E85
		public static byte angleToByte(float angle)
		{
			if (angle < 0f)
			{
				return (byte)((360f + angle % 360f) / 2f);
			}
			return (byte)(angle % 360f / 2f);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x00154AB5 File Offset: 0x00152EB5
		public static float byteToAngle(byte angle)
		{
			return (float)angle * 2f;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x00154ABF File Offset: 0x00152EBF
		public static byte angleToByte2(float angle)
		{
			if (angle < 0f)
			{
				return (byte)((360f + angle % 360f) / 1.5f);
			}
			return (byte)(angle % 360f / 1.5f);
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x00154AEF File Offset: 0x00152EEF
		public static float byteToAngle2(byte angle)
		{
			return (float)angle * 1.5f;
		}
	}
}
