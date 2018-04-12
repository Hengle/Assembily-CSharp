using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000423 RID: 1059
	public class StereoSongAsset : Asset, IDevkitAssetSpawnable
	{
		// Token: 0x06001CB9 RID: 7353 RVA: 0x0009C187 File Offset: 0x0009A587
		public StereoSongAsset()
		{
			this.construct();
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0009C195 File Offset: 0x0009A595
		public StereoSongAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.construct();
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0009C1A6 File Offset: 0x0009A5A6
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0009C1A8 File Offset: 0x0009A5A8
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.title = reader.readValue<TranslationReference>("Title");
			this.song = reader.readValue<ContentReference<AudioClip>>("Song");
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0009C1D3 File Offset: 0x0009A5D3
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<TranslationReference>("Title", this.title);
			writer.writeValue<ContentReference<AudioClip>>("Song", this.song);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0009C1FE File Offset: 0x0009A5FE
		protected virtual void construct()
		{
			this.title = TranslationReference.invalid;
			this.song = ContentReference<AudioClip>.invalid;
		}

		// Token: 0x04001100 RID: 4352
		[Inspectable("#SDG::Asset.Stereo_Song.Title.Name", null)]
		public TranslationReference title;

		// Token: 0x04001101 RID: 4353
		[Inspectable("#SDG::Asset.Stereo_Song.Song.Name", null)]
		public ContentReference<AudioClip> song;
	}
}
