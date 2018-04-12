using System;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004AD RID: 1197
	public class River
	{
		// Token: 0x0600202B RID: 8235 RVA: 0x000B1230 File Offset: 0x000AF630
		public River(string newPath)
		{
			this.path = ReadWrite.PATH + newPath;
			if (!Directory.Exists(Path.GetDirectoryName(this.path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(this.path));
			}
			this.stream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			this.water = 0;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000B1298 File Offset: 0x000AF698
		public River(string newPath, bool usePath)
		{
			this.path = newPath;
			if (usePath)
			{
				this.path = ReadWrite.PATH + this.path;
			}
			if (!Directory.Exists(Path.GetDirectoryName(this.path)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(this.path));
			}
			this.stream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
			this.water = 0;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000B1310 File Offset: 0x000AF710
		public River(string newPath, bool usePath, bool useCloud, bool isReading)
		{
			this.path = newPath;
			if (useCloud)
			{
				if (isReading)
				{
					this.block = ReadWrite.readBlock(this.path, useCloud, 0);
				}
				if (this.block == null)
				{
					this.block = new Block();
				}
			}
			else
			{
				if (usePath)
				{
					this.path = ReadWrite.PATH + this.path;
				}
				if (!Directory.Exists(Path.GetDirectoryName(this.path)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(this.path));
				}
				this.stream = new FileStream(this.path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				this.water = 0;
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000B13C4 File Offset: 0x000AF7C4
		public string readString()
		{
			if (this.block != null)
			{
				return this.block.readString();
			}
			int count = this.stream.ReadByte();
			this.stream.Read(River.buffer, 0, count);
			return Encoding.UTF8.GetString(River.buffer, 0, count);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000B141A File Offset: 0x000AF81A
		public bool readBoolean()
		{
			if (this.block != null)
			{
				return this.block.readBoolean();
			}
			return this.stream.ReadByte() != 0;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000B1444 File Offset: 0x000AF844
		public byte readByte()
		{
			if (this.block != null)
			{
				return this.block.readByte();
			}
			return (byte)this.stream.ReadByte();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000B146C File Offset: 0x000AF86C
		public byte[] readBytes()
		{
			if (this.block != null)
			{
				return this.block.readByteArray();
			}
			byte[] array = new byte[(int)this.readUInt16()];
			this.stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000B14AE File Offset: 0x000AF8AE
		public short readInt16()
		{
			if (this.block != null)
			{
				return this.block.readInt16();
			}
			this.stream.Read(River.buffer, 0, 2);
			return BitConverter.ToInt16(River.buffer, 0);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000B14E5 File Offset: 0x000AF8E5
		public ushort readUInt16()
		{
			if (this.block != null)
			{
				return this.block.readUInt16();
			}
			this.stream.Read(River.buffer, 0, 2);
			return BitConverter.ToUInt16(River.buffer, 0);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000B151C File Offset: 0x000AF91C
		public int readInt32()
		{
			if (this.block != null)
			{
				return this.block.readInt32();
			}
			this.stream.Read(River.buffer, 0, 4);
			return BitConverter.ToInt32(River.buffer, 0);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000B1553 File Offset: 0x000AF953
		public uint readUInt32()
		{
			if (this.block != null)
			{
				return this.block.readUInt32();
			}
			this.stream.Read(River.buffer, 0, 4);
			return BitConverter.ToUInt32(River.buffer, 0);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000B158A File Offset: 0x000AF98A
		public float readSingle()
		{
			if (this.block != null)
			{
				return this.block.readSingle();
			}
			this.stream.Read(River.buffer, 0, 4);
			return BitConverter.ToSingle(River.buffer, 0);
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000B15C1 File Offset: 0x000AF9C1
		public long readInt64()
		{
			if (this.block != null)
			{
				return this.block.readInt64();
			}
			this.stream.Read(River.buffer, 0, 8);
			return BitConverter.ToInt64(River.buffer, 0);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000B15F8 File Offset: 0x000AF9F8
		public ulong readUInt64()
		{
			if (this.block != null)
			{
				return this.block.readUInt64();
			}
			this.stream.Read(River.buffer, 0, 8);
			return BitConverter.ToUInt64(River.buffer, 0);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000B162F File Offset: 0x000AFA2F
		public CSteamID readSteamID()
		{
			return new CSteamID(this.readUInt64());
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000B163C File Offset: 0x000AFA3C
		public Guid readGUID()
		{
			if (this.block != null)
			{
				return this.block.readGUID();
			}
			GuidBuffer guidBuffer = default(GuidBuffer);
			guidBuffer.Read(this.readBytes(), 0);
			return guidBuffer.GUID;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000B167D File Offset: 0x000AFA7D
		public Vector3 readSingleVector3()
		{
			return new Vector3(this.readSingle(), this.readSingle(), this.readSingle());
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000B1696 File Offset: 0x000AFA96
		public Quaternion readSingleQuaternion()
		{
			return Quaternion.Euler(this.readSingle(), this.readSingle(), this.readSingle());
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000B16AF File Offset: 0x000AFAAF
		public Color readColor()
		{
			return new Color((float)this.readByte() / 255f, (float)this.readByte() / 255f, (float)this.readByte() / 255f);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000B16E0 File Offset: 0x000AFAE0
		public void writeString(string value)
		{
			if (this.block != null)
			{
				this.block.writeString(value);
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				this.stream.WriteByte((byte)bytes.Length);
				this.stream.Write(bytes, 0, bytes.Length);
				this.water += 1 + bytes.Length;
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000B1748 File Offset: 0x000AFB48
		public void writeBoolean(bool value)
		{
			if (this.block != null)
			{
				this.block.writeBoolean(value);
			}
			else
			{
				this.stream.WriteByte((!value) ? 0 : 1);
				this.water++;
			}
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000B1797 File Offset: 0x000AFB97
		public void writeByte(byte value)
		{
			if (this.block != null)
			{
				this.block.writeByte(value);
			}
			else
			{
				this.stream.WriteByte(value);
				this.water++;
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000B17D0 File Offset: 0x000AFBD0
		public void writeBytes(byte[] values)
		{
			if (this.block != null)
			{
				this.block.writeByteArray(values);
			}
			else
			{
				this.writeUInt16((ushort)values.Length);
				this.stream.Write(values, 0, values.Length);
				this.water += values.Length;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000B1824 File Offset: 0x000AFC24
		public void writeInt16(short value)
		{
			if (this.block != null)
			{
				this.block.writeInt16(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 2);
				this.water += 2;
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000B1870 File Offset: 0x000AFC70
		public void writeUInt16(ushort value)
		{
			if (this.block != null)
			{
				this.block.writeUInt16(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 2);
				this.water += 2;
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000B18BC File Offset: 0x000AFCBC
		public void writeInt32(int value)
		{
			if (this.block != null)
			{
				this.block.writeInt32(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 4);
				this.water += 4;
			}
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000B1908 File Offset: 0x000AFD08
		public void writeUInt32(uint value)
		{
			if (this.block != null)
			{
				this.block.writeUInt32(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 4);
				this.water += 4;
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000B1954 File Offset: 0x000AFD54
		public void writeSingle(float value)
		{
			if (this.block != null)
			{
				this.block.writeSingle(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 4);
				this.water += 4;
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000B19A0 File Offset: 0x000AFDA0
		public void writeInt64(long value)
		{
			if (this.block != null)
			{
				this.block.writeInt64(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 8);
				this.water += 8;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000B19EC File Offset: 0x000AFDEC
		public void writeUInt64(ulong value)
		{
			if (this.block != null)
			{
				this.block.writeUInt64(value);
			}
			else
			{
				byte[] bytes = BitConverter.GetBytes(value);
				this.stream.Write(bytes, 0, 8);
				this.water += 8;
			}
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000B1A38 File Offset: 0x000AFE38
		public void writeSteamID(CSteamID steamID)
		{
			this.writeUInt64(steamID.m_SteamID);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000B1A48 File Offset: 0x000AFE48
		public void writeGUID(Guid GUID)
		{
			GuidBuffer guidBuffer = new GuidBuffer(GUID);
			guidBuffer.Write(GuidBuffer.GUID_BUFFER, 0);
			this.writeBytes(GuidBuffer.GUID_BUFFER);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000B1A75 File Offset: 0x000AFE75
		public void writeSingleVector3(Vector3 value)
		{
			this.writeSingle(value.x);
			this.writeSingle(value.y);
			this.writeSingle(value.z);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000B1AA0 File Offset: 0x000AFEA0
		public void writeSingleQuaternion(Quaternion value)
		{
			Vector3 eulerAngles = value.eulerAngles;
			this.writeSingle(eulerAngles.x);
			this.writeSingle(eulerAngles.y);
			this.writeSingle(eulerAngles.z);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000B1ADC File Offset: 0x000AFEDC
		public void writeColor(Color value)
		{
			this.writeByte((byte)(value.r * 255f));
			this.writeByte((byte)(value.g * 255f));
			this.writeByte((byte)(value.b * 255f));
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000B1B1A File Offset: 0x000AFF1A
		public byte[] getHash()
		{
			this.stream.Position = 0L;
			return Hash.SHA1(this.stream);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000B1B34 File Offset: 0x000AFF34
		public void closeRiver()
		{
			if (this.block != null)
			{
				ReadWrite.writeBlock(this.path, true, this.block);
			}
			else
			{
				if (this.water > 0)
				{
					this.stream.SetLength((long)this.water);
				}
				this.stream.Flush();
				this.stream.Close();
				this.stream.Dispose();
			}
		}

		// Token: 0x0400131C RID: 4892
		private static byte[] buffer = new byte[Block.BUFFER_SIZE];

		// Token: 0x0400131D RID: 4893
		private int water;

		// Token: 0x0400131E RID: 4894
		private string path;

		// Token: 0x0400131F RID: 4895
		private FileStream stream;

		// Token: 0x04001320 RID: 4896
		private Block block;
	}
}
