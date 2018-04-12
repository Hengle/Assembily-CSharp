using System;
using System.Text;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A6 RID: 1190
	public class Block
	{
		// Token: 0x06001F58 RID: 8024 RVA: 0x000ADB87 File Offset: 0x000ABF87
		public Block(int prefix, byte[] contents)
		{
			this.reset(prefix, contents);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000ADB97 File Offset: 0x000ABF97
		public Block(byte[] contents)
		{
			this.reset(contents);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000ADBA6 File Offset: 0x000ABFA6
		public Block(int prefix)
		{
			this.reset(prefix);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000ADBB5 File Offset: 0x000ABFB5
		public Block()
		{
			this.reset();
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000ADBC4 File Offset: 0x000ABFC4
		private static object[] getObjects(int index)
		{
			object[] array = Block.objects[index];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = null;
			}
			return array;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000ADBF4 File Offset: 0x000ABFF4
		public string readString()
		{
			if (this.block != null && this.step < this.block.Length)
			{
				byte b = this.block[this.step];
				string result;
				if (this.step + (int)b <= this.block.Length)
				{
					result = Encoding.UTF8.GetString(this.block, this.step + 1, (int)b);
				}
				else
				{
					result = string.Empty;
				}
				this.step = this.step + 1 + (int)b;
				return result;
			}
			return string.Empty;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x000ADC80 File Offset: 0x000AC080
		public string[] readStringArray()
		{
			if (this.block != null && this.step < this.block.Length)
			{
				string[] array = new string[(int)this.readByte()];
				byte b = 0;
				while ((int)b < array.Length)
				{
					array[(int)b] = this.readString();
					b += 1;
				}
				return array;
			}
			return new string[0];
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x000ADCE0 File Offset: 0x000AC0E0
		public bool readBoolean()
		{
			if (this.block != null && this.step <= this.block.Length - 1)
			{
				bool result = BitConverter.ToBoolean(this.block, this.step);
				this.step++;
				return result;
			}
			return false;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000ADD30 File Offset: 0x000AC130
		public bool[] readBooleanArray()
		{
			if (this.block != null && this.step < this.block.Length)
			{
				bool[] array = new bool[(int)this.readUInt16()];
				ushort num = (ushort)Mathf.CeilToInt((float)array.Length / 8f);
				for (ushort num2 = 0; num2 < num; num2 += 1)
				{
					for (byte b = 0; b < 8; b += 1)
					{
						if ((int)(num2 * 8 + (ushort)b) >= array.Length)
						{
							break;
						}
						array[(int)(num2 * 8 + (ushort)b)] = ((this.block[this.step + (int)num2] & Types.SHIFTS[(int)b]) == Types.SHIFTS[(int)b]);
					}
				}
				this.step += (int)num;
				return array;
			}
			return new bool[0];
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x000ADDF0 File Offset: 0x000AC1F0
		public byte readByte()
		{
			if (this.block != null && this.step <= this.block.Length - 1)
			{
				byte result = this.block[this.step];
				this.step++;
				return result;
			}
			return 0;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000ADE3C File Offset: 0x000AC23C
		public byte[] readByteArray()
		{
			if (this.block != null && this.step < this.block.Length)
			{
				byte[] array;
				if (this.longBinaryData)
				{
					int num = this.readInt32();
					if (num >= 30000)
					{
						return new byte[0];
					}
					array = new byte[num];
				}
				else
				{
					array = new byte[(int)this.block[this.step]];
					this.step++;
				}
				try
				{
					Buffer.BlockCopy(this.block, this.step, array, 0, array.Length);
				}
				catch
				{
				}
				this.step += array.Length;
				return array;
			}
			return new byte[0];
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000ADF08 File Offset: 0x000AC308
		public short readInt16()
		{
			if (this.block != null && this.step <= this.block.Length - 2)
			{
				short result = BitConverter.ToInt16(this.block, this.step);
				this.step += 2;
				return result;
			}
			return 0;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000ADF58 File Offset: 0x000AC358
		public ushort readUInt16()
		{
			if (this.block != null && this.step <= this.block.Length - 2)
			{
				ushort result = BitConverter.ToUInt16(this.block, this.step);
				this.step += 2;
				return result;
			}
			return 0;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000ADFA8 File Offset: 0x000AC3A8
		public int readInt32()
		{
			if (this.block != null && this.step <= this.block.Length - 4)
			{
				int result = BitConverter.ToInt32(this.block, this.step);
				this.step += 4;
				return result;
			}
			return 0;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000ADFF8 File Offset: 0x000AC3F8
		public int[] readInt32Array()
		{
			ushort num = this.readUInt16();
			int[] array = new int[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				int num3 = this.readInt32();
				array[(int)num2] = num3;
			}
			return array;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000AE034 File Offset: 0x000AC434
		public uint readUInt32()
		{
			if (this.block != null && this.step <= this.block.Length - 4)
			{
				uint result = BitConverter.ToUInt32(this.block, this.step);
				this.step += 4;
				return result;
			}
			return 0u;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000AE084 File Offset: 0x000AC484
		public float readSingle()
		{
			if (this.block != null && this.step <= this.block.Length - 4)
			{
				float result = BitConverter.ToSingle(this.block, this.step);
				this.step += 4;
				return result;
			}
			return 0f;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x000AE0D8 File Offset: 0x000AC4D8
		public long readInt64()
		{
			if (this.block != null && this.step <= this.block.Length - 8)
			{
				long result = BitConverter.ToInt64(this.block, this.step);
				this.step += 8;
				return result;
			}
			return 0L;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000AE12C File Offset: 0x000AC52C
		public ulong readUInt64()
		{
			if (this.block != null && this.step <= this.block.Length - 8)
			{
				ulong result = BitConverter.ToUInt64(this.block, this.step);
				this.step += 8;
				return result;
			}
			return 0UL;
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000AE180 File Offset: 0x000AC580
		public ulong[] readUInt64Array()
		{
			ushort num = this.readUInt16();
			ulong[] array = new ulong[(int)num];
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				ulong num3 = this.readUInt64();
				array[(int)num2] = num3;
			}
			return array;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000AE1BA File Offset: 0x000AC5BA
		public CSteamID readSteamID()
		{
			return new CSteamID(this.readUInt64());
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000AE1C8 File Offset: 0x000AC5C8
		public Guid readGUID()
		{
			GuidBuffer guidBuffer = default(GuidBuffer);
			guidBuffer.Read(this.readByteArray(), 0);
			return guidBuffer.GUID;
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000AE1F4 File Offset: 0x000AC5F4
		public Vector3 readUInt16RVector3()
		{
			byte b = this.readByte();
			double num = (double)this.readUInt16() / 65535.0;
			double num2 = (double)this.readUInt16() / 65535.0;
			byte b2 = this.readByte();
			double num3 = (double)this.readUInt16() / 65535.0;
			num = (double)(b * Regions.REGION_SIZE) + num * (double)Regions.REGION_SIZE - 4096.0;
			num2 = num2 * 2048.0 - 1024.0;
			num3 = (double)(b2 * Regions.REGION_SIZE) + num3 * (double)Regions.REGION_SIZE - 4096.0;
			return new Vector3((float)num, (float)num2, (float)num3);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000AE2A2 File Offset: 0x000AC6A2
		public Vector3 readSingleVector3()
		{
			return new Vector3(this.readSingle(), this.readSingle(), this.readSingle());
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000AE2BB File Offset: 0x000AC6BB
		public Quaternion readSingleQuaternion()
		{
			return Quaternion.Euler(this.readSingle(), this.readSingle(), this.readSingle());
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000AE2D4 File Offset: 0x000AC6D4
		public Color readColor()
		{
			return new Color((float)this.readByte() / 255f, (float)this.readByte() / 255f, (float)this.readByte() / 255f);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000AE304 File Offset: 0x000AC704
		public object read(Type type)
		{
			if (type == Types.STRING_TYPE)
			{
				return this.readString();
			}
			if (type == Types.STRING_ARRAY_TYPE)
			{
				return this.readStringArray();
			}
			if (type == Types.BOOLEAN_TYPE)
			{
				return this.readBoolean();
			}
			if (type == Types.BOOLEAN_ARRAY_TYPE)
			{
				return this.readBooleanArray();
			}
			if (type == Types.BYTE_TYPE)
			{
				return this.readByte();
			}
			if (type == Types.BYTE_ARRAY_TYPE)
			{
				return this.readByteArray();
			}
			if (type == Types.INT16_TYPE)
			{
				return this.readInt16();
			}
			if (type == Types.UINT16_TYPE)
			{
				return this.readUInt16();
			}
			if (type == Types.INT32_TYPE)
			{
				return this.readInt32();
			}
			if (type == Types.INT32_ARRAY_TYPE)
			{
				return this.readInt32Array();
			}
			if (type == Types.UINT32_TYPE)
			{
				return this.readUInt32();
			}
			if (type == Types.SINGLE_TYPE)
			{
				return this.readSingle();
			}
			if (type == Types.INT64_TYPE)
			{
				return this.readInt64();
			}
			if (type == Types.UINT64_TYPE)
			{
				return this.readUInt64();
			}
			if (type == Types.UINT64_ARRAY_TYPE)
			{
				return this.readUInt64Array();
			}
			if (type == Types.STEAM_ID_TYPE)
			{
				return this.readSteamID();
			}
			if (type == Types.GUID_TYPE)
			{
				return this.readGUID();
			}
			if (type == Types.VECTOR3_TYPE)
			{
				if (this.useCompression)
				{
					return this.readUInt16RVector3();
				}
				return this.readSingleVector3();
			}
			else
			{
				if (type == Types.COLOR_TYPE)
				{
					return this.readColor();
				}
				Debug.LogError("Failed to read type: " + type);
				return null;
			}
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000AE4D0 File Offset: 0x000AC8D0
		public object[] read(int offset, Type type_0)
		{
			object[] array = Block.getObjects(0);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			return array;
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000AE4F8 File Offset: 0x000AC8F8
		public object[] read(int offset, Type type_0, Type type_1)
		{
			object[] array = Block.getObjects(1);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			return array;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000AE52F File Offset: 0x000AC92F
		public object[] read(Type type_0, Type type_1)
		{
			return this.read(0, type_0, type_1);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000AE53C File Offset: 0x000AC93C
		public object[] read(int offset, Type type_0, Type type_1, Type type_2)
		{
			object[] array = Block.getObjects(2);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			if (offset < 3)
			{
				array[2] = this.read(type_2);
			}
			return array;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000AE585 File Offset: 0x000AC985
		public object[] read(Type type_0, Type type_1, Type type_2)
		{
			return this.read(0, type_0, type_1, type_2);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000AE594 File Offset: 0x000AC994
		public object[] read(int offset, Type type_0, Type type_1, Type type_2, Type type_3)
		{
			object[] array = Block.getObjects(3);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			if (offset < 3)
			{
				array[2] = this.read(type_2);
			}
			if (offset < 4)
			{
				array[3] = this.read(type_3);
			}
			return array;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000AE5EF File Offset: 0x000AC9EF
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3)
		{
			return this.read(0, type_0, type_1, type_2, type_3);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000AE600 File Offset: 0x000ACA00
		public object[] read(int offset, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4)
		{
			object[] array = Block.getObjects(4);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			if (offset < 3)
			{
				array[2] = this.read(type_2);
			}
			if (offset < 4)
			{
				array[3] = this.read(type_3);
			}
			if (offset < 5)
			{
				array[4] = this.read(type_4);
			}
			return array;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000AE66D File Offset: 0x000ACA6D
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4)
		{
			return this.read(0, type_0, type_1, type_2, type_3, type_4);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x000AE680 File Offset: 0x000ACA80
		public object[] read(int offset, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5)
		{
			object[] array = Block.getObjects(5);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			if (offset < 3)
			{
				array[2] = this.read(type_2);
			}
			if (offset < 4)
			{
				array[3] = this.read(type_3);
			}
			if (offset < 5)
			{
				array[4] = this.read(type_4);
			}
			if (offset < 6)
			{
				array[5] = this.read(type_5);
			}
			return array;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x000AE6FF File Offset: 0x000ACAFF
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5)
		{
			return this.read(0, type_0, type_1, type_2, type_3, type_4, type_5);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000AE714 File Offset: 0x000ACB14
		public object[] read(int offset, Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5, Type type_6)
		{
			object[] array = Block.getObjects(6);
			if (offset < 1)
			{
				array[0] = this.read(type_0);
			}
			if (offset < 2)
			{
				array[1] = this.read(type_1);
			}
			if (offset < 3)
			{
				array[2] = this.read(type_2);
			}
			if (offset < 4)
			{
				array[3] = this.read(type_3);
			}
			if (offset < 5)
			{
				array[4] = this.read(type_4);
			}
			if (offset < 6)
			{
				array[5] = this.read(type_5);
			}
			if (offset < 7)
			{
				array[6] = this.read(type_6);
			}
			return array;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000AE7A8 File Offset: 0x000ACBA8
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5, Type type_6)
		{
			return this.read(0, type_0, type_1, type_2, type_3, type_4, type_5, type_6);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000AE7C8 File Offset: 0x000ACBC8
		public object[] read(int offset, params Type[] types)
		{
			object[] array = new object[types.Length];
			for (int i = offset; i < types.Length; i++)
			{
				array[i] = this.read(types[i]);
			}
			return array;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000AE7FF File Offset: 0x000ACBFF
		public object[] read(params Type[] types)
		{
			return this.read(0, types);
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x000AE80C File Offset: 0x000ACC0C
		public void writeString(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			Block.buffer[this.step] = (byte)bytes.Length;
			this.step++;
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += bytes.Length;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000AE868 File Offset: 0x000ACC68
		public void writeStringArray(string[] values)
		{
			this.writeByte((byte)values.Length);
			byte b = 0;
			while ((int)b < values.Length)
			{
				this.writeString(values[(int)b]);
				b += 1;
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x000AE8A0 File Offset: 0x000ACCA0
		public void writeBoolean(bool value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Block.buffer[this.step] = bytes[0];
			this.step++;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000AE8D4 File Offset: 0x000ACCD4
		public void writeBooleanArray(bool[] values)
		{
			this.writeUInt16((ushort)values.Length);
			ushort num = (ushort)Mathf.CeilToInt((float)values.Length / 8f);
			for (ushort num2 = 0; num2 < num; num2 += 1)
			{
				Block.buffer[this.step + (int)num2] = 0;
				for (byte b = 0; b < 8; b += 1)
				{
					if ((int)(num2 * 8 + (ushort)b) >= values.Length)
					{
						break;
					}
					if (values[(int)(num2 * 8 + (ushort)b)])
					{
						byte[] array = Block.buffer;
						int num3 = this.step + (int)num2;
						array[num3] |= Types.SHIFTS[(int)b];
					}
				}
			}
			this.step += (int)num;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000AE97B File Offset: 0x000ACD7B
		public void writeByte(byte value)
		{
			Block.buffer[this.step] = value;
			this.step++;
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000AE998 File Offset: 0x000ACD98
		public void writeByteArray(byte[] values)
		{
			if (this.longBinaryData)
			{
				this.writeInt32(values.Length);
			}
			else
			{
				Block.buffer[this.step] = (byte)values.Length;
				this.step++;
			}
			if (values.Length < 30000)
			{
				Buffer.BlockCopy(values, 0, Block.buffer, this.step, values.Length);
				this.step += values.Length;
				return;
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000AEA14 File Offset: 0x000ACE14
		public void writeInt16(short value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 2;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000AEA4C File Offset: 0x000ACE4C
		public void writeUInt16(ushort value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 2;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000AEA84 File Offset: 0x000ACE84
		public void writeInt32(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 4;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000AEABC File Offset: 0x000ACEBC
		public void writeInt32Array(int[] values)
		{
			this.writeUInt16((ushort)values.Length);
			ushort num = 0;
			while ((int)num < values.Length)
			{
				this.writeInt32(values[(int)num]);
				num += 1;
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000AEAF4 File Offset: 0x000ACEF4
		public void writeUInt32(uint value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 4;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000AEB2C File Offset: 0x000ACF2C
		public void writeSingle(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 4;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000AEB64 File Offset: 0x000ACF64
		public void writeInt64(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 8;
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000AEB9C File Offset: 0x000ACF9C
		public void writeUInt64(ulong value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			Buffer.BlockCopy(bytes, 0, Block.buffer, this.step, bytes.Length);
			this.step += 8;
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000AEBD4 File Offset: 0x000ACFD4
		public void writeUInt64Array(ulong[] values)
		{
			this.writeUInt16((ushort)values.Length);
			ushort num = 0;
			while ((int)num < values.Length)
			{
				this.writeUInt64(values[(int)num]);
				num += 1;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000AEC09 File Offset: 0x000AD009
		public void writeSteamID(CSteamID steamID)
		{
			this.writeUInt64(steamID.m_SteamID);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000AEC18 File Offset: 0x000AD018
		public void writeGUID(Guid GUID)
		{
			GuidBuffer guidBuffer = new GuidBuffer(GUID);
			guidBuffer.Write(GuidBuffer.GUID_BUFFER, 0);
			this.writeByteArray(GuidBuffer.GUID_BUFFER);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000AEC48 File Offset: 0x000AD048
		public void writeUInt16RVector3(Vector3 value)
		{
			double num = (double)value.x + 4096.0;
			double num2 = (double)value.y + 1024.0;
			double num3 = (double)value.z + 4096.0;
			byte value2 = (byte)(num / (double)Regions.REGION_SIZE);
			byte value3 = (byte)(num3 / (double)Regions.REGION_SIZE);
			num %= (double)Regions.REGION_SIZE;
			num2 %= 2048.0;
			num3 %= (double)Regions.REGION_SIZE;
			num /= (double)Regions.REGION_SIZE;
			num2 /= 2048.0;
			num3 /= (double)Regions.REGION_SIZE;
			this.writeByte(value2);
			this.writeUInt16((ushort)(num * 65535.0));
			this.writeUInt16((ushort)(num2 * 65535.0));
			this.writeByte(value3);
			this.writeUInt16((ushort)(num3 * 65535.0));
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x000AED24 File Offset: 0x000AD124
		public void writeSingleVector3(Vector3 value)
		{
			this.writeSingle(value.x);
			this.writeSingle(value.y);
			this.writeSingle(value.z);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x000AED50 File Offset: 0x000AD150
		public void writeSingleQuaternion(Quaternion value)
		{
			Vector3 eulerAngles = value.eulerAngles;
			this.writeSingle(eulerAngles.x);
			this.writeSingle(eulerAngles.y);
			this.writeSingle(eulerAngles.z);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000AED8C File Offset: 0x000AD18C
		public void writeColor(Color value)
		{
			this.writeByte((byte)(value.r * 255f));
			this.writeByte((byte)(value.g * 255f));
			this.writeByte((byte)(value.b * 255f));
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000AEDCC File Offset: 0x000AD1CC
		public void write(object objects)
		{
			Type type = objects.GetType();
			if (type == Types.STRING_TYPE)
			{
				this.writeString((string)objects);
			}
			else if (type == Types.STRING_ARRAY_TYPE)
			{
				this.writeStringArray((string[])objects);
			}
			else if (type == Types.BOOLEAN_TYPE)
			{
				this.writeBoolean((bool)objects);
			}
			else if (type == Types.BOOLEAN_ARRAY_TYPE)
			{
				this.writeBooleanArray((bool[])objects);
			}
			else if (type == Types.BYTE_TYPE)
			{
				this.writeByte((byte)objects);
			}
			else if (type == Types.BYTE_ARRAY_TYPE)
			{
				this.writeByteArray((byte[])objects);
			}
			else if (type == Types.INT16_TYPE)
			{
				this.writeInt16((short)objects);
			}
			else if (type == Types.UINT16_TYPE)
			{
				this.writeUInt16((ushort)objects);
			}
			else if (type == Types.INT32_TYPE)
			{
				this.writeInt32((int)objects);
			}
			else if (type == Types.INT32_ARRAY_TYPE)
			{
				this.writeInt32Array((int[])objects);
			}
			else if (type == Types.UINT32_TYPE)
			{
				this.writeUInt32((uint)objects);
			}
			else if (type == Types.SINGLE_TYPE)
			{
				this.writeSingle((float)objects);
			}
			else if (type == Types.INT64_TYPE)
			{
				this.writeInt64((long)objects);
			}
			else if (type == Types.UINT64_TYPE)
			{
				this.writeUInt64((ulong)objects);
			}
			else if (type == Types.UINT64_ARRAY_TYPE)
			{
				this.writeUInt64Array((ulong[])objects);
			}
			else if (type == Types.STEAM_ID_TYPE)
			{
				this.writeSteamID((CSteamID)objects);
			}
			else if (type == Types.GUID_TYPE)
			{
				this.writeGUID((Guid)objects);
			}
			else if (type == Types.VECTOR3_TYPE)
			{
				if (this.useCompression)
				{
					this.writeUInt16RVector3((Vector3)objects);
				}
				else
				{
					this.writeSingleVector3((Vector3)objects);
				}
			}
			else if (type == Types.COLOR_TYPE)
			{
				this.writeColor((Color)objects);
			}
			else
			{
				Debug.LogError("Failed to write type: " + type);
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000AF020 File Offset: 0x000AD420
		public void write(object object_0, object object_1)
		{
			this.write(object_0);
			this.write(object_1);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000AF030 File Offset: 0x000AD430
		public void write(object object_0, object object_1, object object_2)
		{
			this.write(object_0, object_1);
			this.write(object_2);
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000AF041 File Offset: 0x000AD441
		public void write(object object_0, object object_1, object object_2, object object_3)
		{
			this.write(object_0, object_1, object_2);
			this.write(object_3);
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x000AF054 File Offset: 0x000AD454
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4)
		{
			this.write(object_0, object_1, object_2, object_3);
			this.write(object_4);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000AF069 File Offset: 0x000AD469
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5)
		{
			this.write(object_0, object_1, object_2, object_3, object_4);
			this.write(object_5);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000AF080 File Offset: 0x000AD480
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5, object object_6)
		{
			this.write(object_0, object_1, object_2, object_3, object_4, object_5);
			this.write(object_6);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000AF09C File Offset: 0x000AD49C
		public void write(params object[] objects)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				this.write(objects[i]);
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x000AF0C6 File Offset: 0x000AD4C6
		public byte[] getBytes(out int size)
		{
			if (this.block == null)
			{
				size = this.step;
				return Block.buffer;
			}
			size = this.block.Length;
			return this.block;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x000AF0F1 File Offset: 0x000AD4F1
		public byte[] getHash()
		{
			if (this.block == null)
			{
				return Hash.SHA1(Block.buffer);
			}
			return Hash.SHA1(this.block);
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000AF114 File Offset: 0x000AD514
		public void reset(int prefix, byte[] contents)
		{
			this.step = prefix;
			this.block = contents;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x000AF124 File Offset: 0x000AD524
		public void reset(byte[] contents)
		{
			this.step = 0;
			this.block = contents;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000AF134 File Offset: 0x000AD534
		public void reset(int prefix)
		{
			this.step = prefix;
			this.block = null;
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000AF144 File Offset: 0x000AD544
		public void reset()
		{
			this.step = 0;
			this.block = null;
		}

		// Token: 0x0400130A RID: 4874
		public static readonly int BUFFER_SIZE = 65535;

		// Token: 0x0400130B RID: 4875
		public static byte[] buffer = new byte[Block.BUFFER_SIZE];

		// Token: 0x0400130C RID: 4876
		private static object[][] objects = new object[][]
		{
			new object[1],
			new object[2],
			new object[3],
			new object[4],
			new object[5],
			new object[6],
			new object[7]
		};

		// Token: 0x0400130D RID: 4877
		public bool useCompression;

		// Token: 0x0400130E RID: 4878
		public bool longBinaryData;

		// Token: 0x0400130F RID: 4879
		public int step;

		// Token: 0x04001310 RID: 4880
		public byte[] block;
	}
}
