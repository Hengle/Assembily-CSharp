using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000124 RID: 292
	public class AmbianceVolume : DevkitHierarchyVolume, IAmbianceNode, IDevkitHierarchySpawnable
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0004DF60 File Offset: 0x0004C360
		public AmbianceVolume()
		{
			this.id = 0;
			this.noWater = false;
			this.noLighting = false;
			this.canRain = true;
			this.canSnow = true;
			this.overrideFog = false;
			this.fogColor = Color.white;
			this.fogHeight = -1024f;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0004DFB3 File Offset: 0x0004C3B3
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0004DFBB File Offset: 0x0004C3BB
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.ID", null)]
		public ushort id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0004DFC4 File Offset: 0x0004C3C4
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0004DFCC File Offset: 0x0004C3CC
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.No_Water", null)]
		public bool noWater
		{
			get
			{
				return this._noWater;
			}
			set
			{
				this._noWater = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0004DFD5 File Offset: 0x0004C3D5
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x0004DFDD File Offset: 0x0004C3DD
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.No_Lighting", null)]
		public bool noLighting
		{
			get
			{
				return this._noLighting;
			}
			set
			{
				this._noLighting = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0004DFE6 File Offset: 0x0004C3E6
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0004DFEE File Offset: 0x0004C3EE
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.Can_Rain", null)]
		public bool canRain
		{
			get
			{
				return this._canRain;
			}
			set
			{
				this._canRain = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0004DFF7 File Offset: 0x0004C3F7
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0004DFFF File Offset: 0x0004C3FF
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.Can_Snow", null)]
		public bool canSnow
		{
			get
			{
				return this._canSnow;
			}
			set
			{
				this._canSnow = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0004E008 File Offset: 0x0004C408
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0004E010 File Offset: 0x0004C410
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.Override_Fog", null)]
		public bool overrideFog
		{
			get
			{
				return this._overrideFog;
			}
			set
			{
				this._overrideFog = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0004E019 File Offset: 0x0004C419
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0004E021 File Offset: 0x0004C421
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.Fog_Color", null)]
		public Color fogColor
		{
			get
			{
				return this._fogColor;
			}
			set
			{
				this._fogColor = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0004E02A File Offset: 0x0004C42A
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x0004E032 File Offset: 0x0004C432
		[Inspectable("#SDG::Devkit.Volumes.Ambiance.Fog_Height", null)]
		public float fogHeight
		{
			get
			{
				return this._fogHeight;
			}
			set
			{
				this._fogHeight = value;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0004E03B File Offset: 0x0004C43B
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0004E040 File Offset: 0x0004C440
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.id = reader.readValue<ushort>("Ambiance_ID");
			this.noWater = reader.readValue<bool>("No_Water");
			this.noLighting = reader.readValue<bool>("No_Lighting");
			if (reader.containsKey("Can_Rain"))
			{
				this.canRain = reader.readValue<bool>("Can_Rain");
			}
			if (reader.containsKey("Can_Snow"))
			{
				this.canSnow = reader.readValue<bool>("Can_Snow");
			}
			this.overrideFog = reader.readValue<bool>("Override_Fog");
			this.fogColor = reader.readValue<Color>("Fog_Color");
			this.fogHeight = reader.readValue<float>("Fog_Height");
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0004E0FC File Offset: 0x0004C4FC
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<ushort>("Ambiance_ID", this.id);
			writer.writeValue<bool>("No_Water", this.noWater);
			writer.writeValue<bool>("No_Lighting", this.noLighting);
			writer.writeValue<bool>("Can_Rain", this.canRain);
			writer.writeValue<bool>("Can_Snow", this.canSnow);
			writer.writeValue<bool>("Override_Fog", this.overrideFog);
			writer.writeValue<Color>("Fog_Color", this.fogColor);
			writer.writeValue<float>("Fog_Height", this.fogHeight);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0004E198 File Offset: 0x0004C598
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Level.isEditor && AmbianceSystem.ambianceVisibilityGroup.isVisible);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0004E1BC File Offset: 0x0004C5BC
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0004E1C4 File Offset: 0x0004C5C4
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			AmbianceSystem.addVolume(this);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0004E1D2 File Offset: 0x0004C5D2
		protected void OnDisable()
		{
			AmbianceSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0004E1E0 File Offset: 0x0004C5E0
		protected void Awake()
		{
			base.name = "Ambiance_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			AmbianceSystem.ambianceVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0004E242 File Offset: 0x0004C642
		protected void OnDestroy()
		{
			AmbianceSystem.ambianceVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x040006FA RID: 1786
		[SerializeField]
		protected ushort _id;

		// Token: 0x040006FB RID: 1787
		[SerializeField]
		protected bool _noWater;

		// Token: 0x040006FC RID: 1788
		[SerializeField]
		protected bool _noLighting;

		// Token: 0x040006FD RID: 1789
		[SerializeField]
		protected bool _canRain;

		// Token: 0x040006FE RID: 1790
		[SerializeField]
		protected bool _canSnow;

		// Token: 0x040006FF RID: 1791
		[SerializeField]
		protected bool _overrideFog;

		// Token: 0x04000700 RID: 1792
		[SerializeField]
		protected Color _fogColor;

		// Token: 0x04000701 RID: 1793
		[SerializeField]
		protected float _fogHeight;
	}
}
