using System;

namespace SDG.Unturned
{
	// Token: 0x0200074C RID: 1868
	public class WorkshopTool
	{
		// Token: 0x0600348F RID: 13455 RVA: 0x001597C4 File Offset: 0x00157BC4
		public static bool checkMapMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Map.meta", false, usePath);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x001597D8 File Offset: 0x00157BD8
		public static bool checkMapValid(string path, bool usePath)
		{
			return ReadWrite.getFolders(path, usePath).Length == 1;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x001597E6 File Offset: 0x00157BE6
		public static bool checkLocalizationMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Localization.meta", false, usePath);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x001597FA File Offset: 0x00157BFA
		public static bool checkLocalizationValid(string path, bool usePath)
		{
			return ReadWrite.getFolders(path, usePath).Length == 4;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x00159808 File Offset: 0x00157C08
		public static bool checkObjectMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Object.meta", false, usePath);
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0015981C File Offset: 0x00157C1C
		public static bool checkItemMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Item.meta", false, usePath);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00159830 File Offset: 0x00157C30
		public static bool checkVehicleMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Vehicle.meta", false, usePath);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00159844 File Offset: 0x00157C44
		public static bool checkSkinMeta(string path, bool usePath)
		{
			return ReadWrite.fileExists(path + "/Skin.meta", false, usePath);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x00159858 File Offset: 0x00157C58
		public static bool checkBundleValid(string path, bool usePath)
		{
			return ReadWrite.getFolders(path, usePath).Length > 0;
		}
	}
}
