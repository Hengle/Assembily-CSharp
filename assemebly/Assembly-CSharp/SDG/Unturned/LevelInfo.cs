using System;

namespace SDG.Unturned
{
	// Token: 0x0200054B RID: 1355
	public class LevelInfo
	{
		// Token: 0x060024D0 RID: 9424 RVA: 0x000D2EE1 File Offset: 0x000D12E1
		public LevelInfo(string newPath, string newName, ELevelSize newSize, ELevelType newType, bool newEditable, LevelInfoConfigData newConfigData)
		{
			this._path = newPath;
			this._name = newName;
			this._size = newSize;
			this._type = newType;
			this._isEditable = newEditable;
			this.configData = newConfigData;
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000D2F16 File Offset: 0x000D1316
		public string path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x000D2F1E File Offset: 0x000D131E
		public string name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x000D2F26 File Offset: 0x000D1326
		public bool canAnalyticsTrack
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000D2F2E File Offset: 0x000D132E
		public bool hasTriggers
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x000D2F38 File Offset: 0x000D1338
		public bool isSpecial
		{
			get
			{
				return this.name == "Alpha Valley" || this.name == "Monolith" || this.name == "Paintball_Arena_0" || this.name == "PEI" || this.name == "PEI Arena" || this.name == "Tutorial" || this.name == "Washington" || this.name == "Washington Arena" || this.name == "Yukon" || this.name == "Russia" || this.name == "Hawaii" || this.name == "Germany";
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000D303F File Offset: 0x000D143F
		public ELevelSize size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x000D3047 File Offset: 0x000D1447
		public ELevelType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x000D304F File Offset: 0x000D144F
		public bool isEditable
		{
			get
			{
				return this._isEditable;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x000D3057 File Offset: 0x000D1457
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x000D305F File Offset: 0x000D145F
		public LevelInfoConfigData configData { get; private set; }

		// Token: 0x040016CC RID: 5836
		private string _path;

		// Token: 0x040016CD RID: 5837
		private string _name;

		// Token: 0x040016CE RID: 5838
		public bool isFromWorkshop;

		// Token: 0x040016CF RID: 5839
		private ELevelSize _size;

		// Token: 0x040016D0 RID: 5840
		private ELevelType _type;

		// Token: 0x040016D1 RID: 5841
		private bool _isEditable;
	}
}
