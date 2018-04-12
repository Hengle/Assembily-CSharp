using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A7 RID: 1191
	public class Data
	{
		// Token: 0x06001FA6 RID: 8102 RVA: 0x000AF1C4 File Offset: 0x000AD5C4
		public Data(string content)
		{
			this.data = new Dictionary<string, string>();
			StringReader stringReader = null;
			try
			{
				stringReader = new StringReader(content);
				string text = string.Empty;
				while ((text = stringReader.ReadLine()) != null)
				{
					if (!(text == string.Empty) && (text.Length <= 1 || !(text.Substring(0, 2) == "//")))
					{
						int num = text.IndexOf(' ');
						string text2;
						string value;
						if (num != -1)
						{
							text2 = text.Substring(0, num);
							value = text.Substring(num + 1, text.Length - num - 1);
						}
						else
						{
							text2 = text;
							value = string.Empty;
						}
						if (this.data.ContainsKey(text2))
						{
							Assets.errors.Add("Data already contains " + text2 + "!");
							this.hasError = true;
						}
						else
						{
							this.data.Add(text2, value);
						}
					}
				}
				this._hash = Hash.SHA1(content);
			}
			catch (Exception ex)
			{
				Assets.errors.Add("Failed to setup data: " + ex.Message);
				Debug.LogException(ex);
				this.data.Clear();
				this._hash = null;
			}
			finally
			{
				if (stringReader != null)
				{
					stringReader.Close();
				}
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x000AF34C File Offset: 0x000AD74C
		public Data()
		{
			this.data = new Dictionary<string, string>();
			this._hash = null;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x000AF366 File Offset: 0x000AD766
		public byte[] hash
		{
			get
			{
				return this._hash;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x000AF36E File Offset: 0x000AD76E
		public bool isEmpty
		{
			get
			{
				return this.data.Count == 0;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x000AF37E File Offset: 0x000AD77E
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x000AF386 File Offset: 0x000AD786
		public bool hasError { get; protected set; }

		// Token: 0x06001FAC RID: 8108 RVA: 0x000AF390 File Offset: 0x000AD790
		public string readString(string key)
		{
			string result;
			this.data.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x000AF3B0 File Offset: 0x000AD7B0
		public bool readBoolean(string key)
		{
			string a = this.readString(key);
			return a == "y" || a == "1" || a == "True";
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000AF3F4 File Offset: 0x000AD7F4
		public byte readByte(string key)
		{
			byte result;
			byte.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000AF414 File Offset: 0x000AD814
		public byte[] readByteArray(string key)
		{
			string s = this.readString(key);
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x000AF434 File Offset: 0x000AD834
		public short readInt16(string key)
		{
			short result;
			short.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x000AF454 File Offset: 0x000AD854
		public ushort readUInt16(string key)
		{
			ushort result;
			ushort.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x000AF474 File Offset: 0x000AD874
		public int readInt32(string key)
		{
			int result;
			int.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000AF494 File Offset: 0x000AD894
		public uint readUInt32(string key)
		{
			uint result;
			uint.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000AF4B4 File Offset: 0x000AD8B4
		public long readInt64(string key)
		{
			long result;
			long.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x000AF4D4 File Offset: 0x000AD8D4
		public ulong readUInt64(string key)
		{
			ulong result;
			ulong.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x000AF4F4 File Offset: 0x000AD8F4
		public float readSingle(string key)
		{
			float result;
			float.TryParse(this.readString(key), out result);
			return result;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000AF511 File Offset: 0x000AD911
		public Vector3 readVector3(string key)
		{
			return new Vector3(this.readSingle(key + "_X"), this.readSingle(key + "_Y"), this.readSingle(key + "_Z"));
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000AF54B File Offset: 0x000AD94B
		public Quaternion readQuaternion(string key)
		{
			return Quaternion.Euler((float)(this.readByte(key + "_X") * 2), (float)this.readByte(key + "_Y"), (float)this.readByte(key + "_Z"));
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x000AF58A File Offset: 0x000AD98A
		public Color readColor(string key)
		{
			return new Color(this.readSingle(key + "_R"), this.readSingle(key + "_G"), this.readSingle(key + "_B"));
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x000AF5C4 File Offset: 0x000AD9C4
		public CSteamID readSteamID(string key)
		{
			return new CSteamID(this.readUInt64(key));
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000AF5D2 File Offset: 0x000AD9D2
		public Guid readGUID(string key)
		{
			return new Guid(this.readString(key));
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000AF5E0 File Offset: 0x000AD9E0
		public void writeString(string key, string value)
		{
			this.data.Add(key, value);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000AF5EF File Offset: 0x000AD9EF
		public void writeBoolean(string key, bool value)
		{
			this.data.Add(key, (!value) ? "n" : "y");
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000AF612 File Offset: 0x000ADA12
		public void writeByte(string key, byte value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000AF62D File Offset: 0x000ADA2D
		public void writeByteArray(string key, byte[] value)
		{
			this.data.Add(key, Encoding.UTF8.GetString(value));
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000AF646 File Offset: 0x000ADA46
		public void writeInt16(string key, short value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000AF661 File Offset: 0x000ADA61
		public void writeUInt16(string key, ushort value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000AF67C File Offset: 0x000ADA7C
		public void writeInt32(string key, int value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000AF697 File Offset: 0x000ADA97
		public void writeUInt32(string key, uint value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000AF6B2 File Offset: 0x000ADAB2
		public void writeInt64(string key, long value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000AF6CD File Offset: 0x000ADACD
		public void writeUInt64(string key, ulong value)
		{
			this.data.Add(key, value.ToString());
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000AF6E8 File Offset: 0x000ADAE8
		public void writeSingle(string key, float value)
		{
			this.data.Add(key, (Mathf.Floor(value * 100f) / 100f).ToString());
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000AF724 File Offset: 0x000ADB24
		public void writeVector3(string key, Vector3 value)
		{
			this.writeSingle(key + "_X", value.x);
			this.writeSingle(key + "_Y", value.y);
			this.writeSingle(key + "_Z", value.z);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000AF77C File Offset: 0x000ADB7C
		public void writeQuaternion(string key, Quaternion value)
		{
			Vector3 eulerAngles = value.eulerAngles;
			this.writeByte(key + "_X", MeasurementTool.angleToByte(eulerAngles.x));
			this.writeByte(key + "_Y", MeasurementTool.angleToByte(eulerAngles.y));
			this.writeByte(key + "_Z", MeasurementTool.angleToByte(eulerAngles.z));
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000AF7E8 File Offset: 0x000ADBE8
		public void writeColor(string key, Color value)
		{
			this.writeSingle(key + "_R", value.r);
			this.writeSingle(key + "_G", value.g);
			this.writeSingle(key + "_B", value.b);
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000AF83D File Offset: 0x000ADC3D
		public void writeSteamID(string key, CSteamID value)
		{
			this.writeUInt64(key, value.m_SteamID);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000AF84D File Offset: 0x000ADC4D
		public void writeGUID(string key, Guid value)
		{
			this.writeString(key, value.ToString("N"));
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000AF864 File Offset: 0x000ADC64
		public string getFile()
		{
			string text = string.Empty;
			char c = (!this.isCSV) ? ' ' : ',';
			foreach (KeyValuePair<string, string> keyValuePair in this.data)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					keyValuePair.Key,
					c,
					keyValuePair.Value,
					"\n"
				});
			}
			return text;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x000AF910 File Offset: 0x000ADD10
		public string[] getLines()
		{
			string[] array = new string[this.data.Count];
			char c = (!this.isCSV) ? ' ' : ',';
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.data)
			{
				array[num] = keyValuePair.Key + c + keyValuePair.Value;
				num++;
			}
			return array;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000AF9B0 File Offset: 0x000ADDB0
		public KeyValuePair<string, string>[] getContents()
		{
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[this.data.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.data)
			{
				array[num] = keyValuePair;
				num++;
			}
			return array;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000AFA2C File Offset: 0x000ADE2C
		public string[] getValuesWithKey(string key)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in this.data)
			{
				if (keyValuePair.Key == key)
				{
					list.Add(keyValuePair.Value);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000AFAAC File Offset: 0x000ADEAC
		public string[] getKeysWithValue(string value)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in this.data)
			{
				if (keyValuePair.Value == value)
				{
					list.Add(keyValuePair.Key);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000AFB2C File Offset: 0x000ADF2C
		public bool has(string key)
		{
			return this.data.ContainsKey(key);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000AFB3A File Offset: 0x000ADF3A
		public void reset()
		{
			this.data.Clear();
		}

		// Token: 0x04001311 RID: 4881
		private Dictionary<string, string> data;

		// Token: 0x04001312 RID: 4882
		private byte[] _hash;

		// Token: 0x04001313 RID: 4883
		public bool isCSV;
	}
}
