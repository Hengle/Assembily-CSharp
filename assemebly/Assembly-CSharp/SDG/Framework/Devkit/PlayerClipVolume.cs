using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000157 RID: 343
	public class PlayerClipVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x000511CB File Offset: 0x0004F5CB
		public PlayerClipVolume()
		{
			this.blockPlayer = true;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000511DA File Offset: 0x0004F5DA
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x000511E2 File Offset: 0x0004F5E2
		[Inspectable("#SDG::Devkit.Volumes.Player_Clip.Block_Player", null)]
		public bool blockPlayer
		{
			get
			{
				return this._blockPlayer;
			}
			set
			{
				this._blockPlayer = value;
				this.updateBoxEnabled();
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000511F1 File Offset: 0x0004F5F1
		public override VolumeVisibilityGroup visibilityGroupOverride
		{
			get
			{
				return PlayerClipVolumeSystem.playerClipVisibilityGroup;
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000511F8 File Offset: 0x0004F5F8
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000511FA File Offset: 0x0004F5FA
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			if (reader.containsKey("Block_Player"))
			{
				this.blockPlayer = reader.readValue<bool>("Block_Player");
			}
			else
			{
				this.blockPlayer = true;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00051230 File Offset: 0x0004F630
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<bool>("Block_Player", this.blockPlayer);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0005124C File Offset: 0x0004F64C
		protected virtual void updateBoxEnabled()
		{
			if (base.box == null)
			{
				return;
			}
			if (Level.isEditor)
			{
				base.box.enabled = PlayerClipVolumeSystem.playerClipVisibilityGroup.isVisible;
			}
			else
			{
				base.box.enabled = this.blockPlayer;
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000512A0 File Offset: 0x0004F6A0
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000512A8 File Offset: 0x0004F6A8
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			PlayerClipVolumeSystem.addVolume(this);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000512B6 File Offset: 0x0004F6B6
		protected void OnDisable()
		{
			PlayerClipVolumeSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000512C4 File Offset: 0x0004F6C4
		protected void Awake()
		{
			base.name = "Player_Clip_Volume";
			base.gameObject.layer = LayerMasks.CLIP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			this.updateBoxEnabled();
			PlayerClipVolumeSystem.playerClipVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0005131A File Offset: 0x0004F71A
		protected void OnDestroy()
		{
			PlayerClipVolumeSystem.playerClipVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x0400074F RID: 1871
		[SerializeField]
		protected bool _blockPlayer;
	}
}
