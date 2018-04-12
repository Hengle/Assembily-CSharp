using System;

namespace SDG.Unturned
{
	// Token: 0x02000699 RID: 1689
	public class FilterSettings
	{
		// Token: 0x06003113 RID: 12563 RVA: 0x001405EC File Offset: 0x0013E9EC
		public static void load()
		{
			if (ReadWrite.fileExists("/Filters.dat", true))
			{
				Block block = ReadWrite.readBlock("/Filters.dat", true, 0);
				if (block != null)
				{
					byte b = block.readByte();
					if (b > 2)
					{
						FilterSettings.filterMap = block.readString();
						if (b > 5)
						{
							FilterSettings.filterPassword = (EPassword)block.readByte();
							FilterSettings.filterWorkshop = (EWorkshop)block.readByte();
						}
						else
						{
							block.readBoolean();
							block.readBoolean();
							FilterSettings.filterPassword = EPassword.NO;
							FilterSettings.filterWorkshop = EWorkshop.NO;
						}
						if (b < 7)
						{
							FilterSettings.filterPlugins = EPlugins.ANY;
						}
						else
						{
							FilterSettings.filterPlugins = (EPlugins)block.readByte();
						}
						FilterSettings.filterAttendance = (EAttendance)block.readByte();
						FilterSettings.filterVACProtection = (EVACProtectionFilter)block.readByte();
						if (b > 10)
						{
							FilterSettings.filterBattlEyeProtection = (EBattlEyeProtectionFilter)block.readByte();
						}
						else
						{
							FilterSettings.filterBattlEyeProtection = EBattlEyeProtectionFilter.Secure;
						}
						FilterSettings.filterCombat = (ECombat)block.readByte();
						if (b < 8)
						{
							FilterSettings.filterCheats = ECheats.NO;
						}
						else
						{
							FilterSettings.filterCheats = (ECheats)block.readByte();
						}
						FilterSettings.filterMode = (EGameMode)block.readByte();
						if (b < 9)
						{
							FilterSettings.filterMode = EGameMode.NORMAL;
						}
						if (b > 3)
						{
							FilterSettings.filterCamera = (ECameraMode)block.readByte();
						}
						else
						{
							FilterSettings.filterCamera = ECameraMode.ANY;
						}
						return;
					}
				}
			}
			FilterSettings.filterMap = string.Empty;
			FilterSettings.filterPassword = EPassword.NO;
			FilterSettings.filterWorkshop = EWorkshop.NO;
			FilterSettings.filterPlugins = EPlugins.ANY;
			FilterSettings.filterAttendance = EAttendance.SPACE;
			FilterSettings.filterVACProtection = EVACProtectionFilter.Secure;
			FilterSettings.filterBattlEyeProtection = EBattlEyeProtectionFilter.Secure;
			FilterSettings.filterCombat = ECombat.ANY;
			FilterSettings.filterCheats = ECheats.NO;
			FilterSettings.filterMode = EGameMode.NORMAL;
			FilterSettings.filterCamera = ECameraMode.ANY;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x00140768 File Offset: 0x0013EB68
		public static void save()
		{
			Block block = new Block();
			block.writeByte(FilterSettings.SAVEDATA_VERSION);
			block.writeString(FilterSettings.filterMap);
			block.writeByte((byte)FilterSettings.filterPassword);
			block.writeByte((byte)FilterSettings.filterWorkshop);
			block.writeByte((byte)FilterSettings.filterPlugins);
			block.writeByte((byte)FilterSettings.filterAttendance);
			block.writeByte((byte)FilterSettings.filterVACProtection);
			block.writeByte((byte)FilterSettings.filterBattlEyeProtection);
			block.writeByte((byte)FilterSettings.filterCombat);
			block.writeByte((byte)FilterSettings.filterCheats);
			block.writeByte((byte)FilterSettings.filterMode);
			block.writeByte((byte)FilterSettings.filterCamera);
			ReadWrite.writeBlock("/Filters.dat", true, block);
		}

		// Token: 0x040020A4 RID: 8356
		public static readonly byte SAVEDATA_VERSION = 11;

		// Token: 0x040020A5 RID: 8357
		public static string filterMap;

		// Token: 0x040020A6 RID: 8358
		public static EPassword filterPassword;

		// Token: 0x040020A7 RID: 8359
		public static EWorkshop filterWorkshop;

		// Token: 0x040020A8 RID: 8360
		public static EPlugins filterPlugins;

		// Token: 0x040020A9 RID: 8361
		public static EAttendance filterAttendance;

		// Token: 0x040020AA RID: 8362
		public static EVACProtectionFilter filterVACProtection;

		// Token: 0x040020AB RID: 8363
		public static EBattlEyeProtectionFilter filterBattlEyeProtection;

		// Token: 0x040020AC RID: 8364
		public static ECombat filterCombat;

		// Token: 0x040020AD RID: 8365
		public static ECheats filterCheats;

		// Token: 0x040020AE RID: 8366
		public static EGameMode filterMode;

		// Token: 0x040020AF RID: 8367
		public static ECameraMode filterCamera;
	}
}
