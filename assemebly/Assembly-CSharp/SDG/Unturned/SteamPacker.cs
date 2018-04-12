using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000676 RID: 1654
	public class SteamPacker
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x0013D959 File Offset: 0x0013BD59
		// (set) Token: 0x0600300F RID: 12303 RVA: 0x0013D965 File Offset: 0x0013BD65
		public static int step
		{
			get
			{
				return SteamPacker.block.step;
			}
			set
			{
				SteamPacker.block.step = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003010 RID: 12304 RVA: 0x0013D972 File Offset: 0x0013BD72
		// (set) Token: 0x06003011 RID: 12305 RVA: 0x0013D97E File Offset: 0x0013BD7E
		public static bool useCompression
		{
			get
			{
				return SteamPacker.block.useCompression;
			}
			set
			{
				SteamPacker.block.useCompression = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x0013D98B File Offset: 0x0013BD8B
		// (set) Token: 0x06003013 RID: 12307 RVA: 0x0013D997 File Offset: 0x0013BD97
		public static bool longBinaryData
		{
			get
			{
				return SteamPacker.block.longBinaryData;
			}
			set
			{
				SteamPacker.block.longBinaryData = value;
			}
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x0013D9A4 File Offset: 0x0013BDA4
		public static object read(Type type)
		{
			return SteamPacker.block.read(type);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x0013D9B1 File Offset: 0x0013BDB1
		public static object[] read(Type type_0, Type type_1)
		{
			return SteamPacker.block.read(type_0, type_1);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0013D9BF File Offset: 0x0013BDBF
		public static object[] read(Type type_0, Type type_1, Type type_2)
		{
			return SteamPacker.block.read(type_0, type_1, type_2);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0013D9CE File Offset: 0x0013BDCE
		public static object[] read(Type type_0, Type type_1, Type type_2, Type type_3)
		{
			return SteamPacker.block.read(type_0, type_1, type_2, type_3);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0013D9DE File Offset: 0x0013BDDE
		public static object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4)
		{
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0013D9F0 File Offset: 0x0013BDF0
		public static object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5)
		{
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4, type_5);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0013DA04 File Offset: 0x0013BE04
		public static object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5, Type type_6)
		{
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4, type_5, type_6);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0013DA1A File Offset: 0x0013BE1A
		public static object[] read(params Type[] types)
		{
			return SteamPacker.block.read(types);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0013DA27 File Offset: 0x0013BE27
		public static void openRead(int prefix, byte[] bytes)
		{
			SteamPacker.block.reset(prefix, bytes);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0013DA35 File Offset: 0x0013BE35
		public static void closeRead()
		{
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0013DA37 File Offset: 0x0013BE37
		public static void write(object objects)
		{
			SteamPacker.block.write(objects);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x0013DA44 File Offset: 0x0013BE44
		public static void write(object object_0, object object_1)
		{
			SteamPacker.block.write(object_0, object_1);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x0013DA52 File Offset: 0x0013BE52
		public static void write(object object_0, object object_1, object object_2)
		{
			SteamPacker.block.write(object_0, object_1, object_2);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x0013DA61 File Offset: 0x0013BE61
		public static void write(object object_0, object object_1, object object_2, object object_3)
		{
			SteamPacker.block.write(object_0, object_1, object_2, object_3);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x0013DA71 File Offset: 0x0013BE71
		public static void write(object object_0, object object_1, object object_2, object object_3, object object_4)
		{
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x0013DA83 File Offset: 0x0013BE83
		public static void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5)
		{
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4, object_5);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x0013DA97 File Offset: 0x0013BE97
		public static void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5, object object_6)
		{
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4, object_5, object_6);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x0013DAAD File Offset: 0x0013BEAD
		public static void write(params object[] objects)
		{
			SteamPacker.block.write(objects);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0013DABA File Offset: 0x0013BEBA
		public static void openWrite(int prefix)
		{
			SteamPacker.block.reset(prefix);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x0013DAC7 File Offset: 0x0013BEC7
		public static byte[] closeWrite(out int size)
		{
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x0013DAD4 File Offset: 0x0013BED4
		public static byte[] getBytes(int prefix, out int size, object object_0)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0013DAF7 File Offset: 0x0013BEF7
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0013DB1B File Offset: 0x0013BF1B
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1, object object_2)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1, object_2);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0013DB41 File Offset: 0x0013BF41
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1, object object_2, object object_3)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1, object_2, object_3);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0013DB69 File Offset: 0x0013BF69
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1, object object_2, object object_3, object object_4)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0013DB93 File Offset: 0x0013BF93
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1, object object_2, object object_3, object object_4, object object_5)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4, object_5);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0013DBBF File Offset: 0x0013BFBF
		public static byte[] getBytes(int prefix, out int size, object object_0, object object_1, object object_2, object object_3, object object_4, object object_5, object object_6)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(object_0, object_1, object_2, object_3, object_4, object_5, object_6);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0013DBED File Offset: 0x0013BFED
		public static byte[] getBytes(int prefix, out int size, params object[] objects)
		{
			SteamPacker.block.reset(prefix);
			SteamPacker.block.write(objects);
			return SteamPacker.block.getBytes(out size);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0013DC10 File Offset: 0x0013C010
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(0, type_0);
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0013DC60 File Offset: 0x0013C060
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1);
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0013DCB0 File Offset: 0x0013C0B0
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1, Type type_2)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1, type_2);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1, type_2);
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0013DD04 File Offset: 0x0013C104
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1, Type type_2, Type type_3)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1, type_2, type_3);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1, type_2, type_3);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0013DD5C File Offset: 0x0013C15C
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1, type_2, type_3, type_4);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0013DDB8 File Offset: 0x0013C1B8
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1, type_2, type_3, type_4, type_5);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4, type_5);
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0013DE18 File Offset: 0x0013C218
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5, Type type_6)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (type_0 == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, type_0, type_1, type_2, type_3, type_4, type_5, type_6);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(type_0, type_1, type_2, type_3, type_4, type_5, type_6);
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0013DE7C File Offset: 0x0013C27C
		public static object[] getObjects(CSteamID steamID, int offset, int prefix, byte[] bytes, params Type[] types)
		{
			SteamPacker.block.reset(offset + prefix, bytes);
			if (types[0] == Types.STEAM_ID_TYPE)
			{
				object[] array = SteamPacker.block.read(1, types);
				array[0] = steamID;
				return array;
			}
			return SteamPacker.block.read(types);
		}

		// Token: 0x04001FAA RID: 8106
		public static Block block = new Block();
	}
}
