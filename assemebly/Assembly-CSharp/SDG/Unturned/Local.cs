using System;

namespace SDG.Unturned
{
	// Token: 0x020004A9 RID: 1193
	public class Local
	{
		// Token: 0x06001FDC RID: 8156 RVA: 0x000AFC29 File Offset: 0x000AE029
		public Local(Data newData)
		{
			this.data = newData;
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000AFC38 File Offset: 0x000AE038
		public Local()
		{
			this.data = null;
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000AFC48 File Offset: 0x000AE048
		public string read(string key)
		{
			if (this.data != null)
			{
				return this.data.readString(key);
			}
			return "#" + key.ToUpper();
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000AFC80 File Offset: 0x000AE080
		public string format(string key)
		{
			if (this.data == null)
			{
				return "#" + key.ToUpper();
			}
			string text = this.data.readString(key);
			if (text != null)
			{
				return text;
			}
			return "#" + key.ToUpper();
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000AFCD0 File Offset: 0x000AE0D0
		public string format(string key, params object[] values)
		{
			if (this.data == null)
			{
				return "#" + key.ToUpper();
			}
			if (values == null)
			{
				return "#" + key.ToUpper();
			}
			string text = this.data.readString(key);
			if (text != null)
			{
				return string.Format(text, values);
			}
			return "#" + key.ToUpper();
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000AFD3B File Offset: 0x000AE13B
		public bool has(string key)
		{
			return this.data != null && this.data.has(key);
		}

		// Token: 0x04001315 RID: 4885
		private Data data;
	}
}
