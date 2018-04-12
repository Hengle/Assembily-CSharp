using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200041A RID: 1050
	public class ObjectNPCAsset : ObjectAsset
	{
		// Token: 0x06001C53 RID: 7251 RVA: 0x0009AAE8 File Offset: 0x00098EE8
		public ObjectNPCAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this.npcName = localization.format("Character");
			this.npcName = ItemTool.filterRarityRichText(this.npcName);
			this.shirt = data.readUInt16("Shirt");
			this.pants = data.readUInt16("Pants");
			this.hat = data.readUInt16("Hat");
			this.backpack = data.readUInt16("Backpack");
			this.vest = data.readUInt16("Vest");
			this.mask = data.readUInt16("Mask");
			this.glasses = data.readUInt16("Glasses");
			this.face = data.readByte("Face");
			this.hair = data.readByte("Hair");
			this.beard = data.readByte("Beard");
			this.skin = Palette.hex(data.readString("Color_Skin"));
			this.color = Palette.hex(data.readString("Color_Hair"));
			this.isBackward = data.has("Backward");
			this.primary = data.readUInt16("Primary");
			this.secondary = data.readUInt16("Secondary");
			this.tertiary = data.readUInt16("Tertiary");
			if (data.has("Equipped"))
			{
				this.equipped = (ESlotType)Enum.Parse(typeof(ESlotType), data.readString("Equipped"), true);
			}
			else
			{
				this.equipped = ESlotType.NONE;
			}
			this.dialogue = data.readUInt16("Dialogue");
			if (data.has("Pose"))
			{
				this.pose = (ENPCPose)Enum.Parse(typeof(ENPCPose), data.readString("Pose"), true);
			}
			else
			{
				this.pose = ENPCPose.STAND;
			}
			if (data.has("Pose_Lean"))
			{
				this.poseLean = data.readSingle("Pose_Lean");
			}
			if (data.has("Pose_Pitch"))
			{
				this.posePitch = data.readSingle("Pose_Pitch");
			}
			else
			{
				this.posePitch = 90f;
			}
			if (data.has("Pose_Head_Offset"))
			{
				this.poseHeadOffset = data.readSingle("Pose_Head_Offset");
			}
			else if (this.pose == ENPCPose.CROUCH)
			{
				this.poseHeadOffset = 0.1f;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x0009AD60 File Offset: 0x00099160
		// (set) Token: 0x06001C55 RID: 7253 RVA: 0x0009AD68 File Offset: 0x00099168
		public string npcName { get; protected set; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x0009AD71 File Offset: 0x00099171
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x0009AD79 File Offset: 0x00099179
		public ushort shirt { get; protected set; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x0009AD82 File Offset: 0x00099182
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x0009AD8A File Offset: 0x0009918A
		public ushort pants { get; protected set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0009AD93 File Offset: 0x00099193
		// (set) Token: 0x06001C5B RID: 7259 RVA: 0x0009AD9B File Offset: 0x0009919B
		public ushort hat { get; protected set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0009ADA4 File Offset: 0x000991A4
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x0009ADAC File Offset: 0x000991AC
		public ushort backpack { get; protected set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0009ADB5 File Offset: 0x000991B5
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0009ADBD File Offset: 0x000991BD
		public ushort vest { get; protected set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0009ADC6 File Offset: 0x000991C6
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x0009ADCE File Offset: 0x000991CE
		public ushort mask { get; protected set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x0009ADD7 File Offset: 0x000991D7
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0009ADDF File Offset: 0x000991DF
		public ushort glasses { get; protected set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0009ADE8 File Offset: 0x000991E8
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x0009ADF0 File Offset: 0x000991F0
		public byte face { get; protected set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0009ADF9 File Offset: 0x000991F9
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x0009AE01 File Offset: 0x00099201
		public byte hair { get; protected set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0009AE0A File Offset: 0x0009920A
		// (set) Token: 0x06001C69 RID: 7273 RVA: 0x0009AE12 File Offset: 0x00099212
		public byte beard { get; protected set; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0009AE1B File Offset: 0x0009921B
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0009AE23 File Offset: 0x00099223
		public Color skin { get; protected set; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x0009AE2C File Offset: 0x0009922C
		// (set) Token: 0x06001C6D RID: 7277 RVA: 0x0009AE34 File Offset: 0x00099234
		public Color color { get; protected set; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0009AE3D File Offset: 0x0009923D
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0009AE45 File Offset: 0x00099245
		public bool isBackward { get; protected set; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0009AE4E File Offset: 0x0009924E
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x0009AE56 File Offset: 0x00099256
		public ushort primary { get; protected set; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0009AE5F File Offset: 0x0009925F
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0009AE67 File Offset: 0x00099267
		public ushort secondary { get; protected set; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x0009AE70 File Offset: 0x00099270
		// (set) Token: 0x06001C75 RID: 7285 RVA: 0x0009AE78 File Offset: 0x00099278
		public ushort tertiary { get; protected set; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x0009AE81 File Offset: 0x00099281
		// (set) Token: 0x06001C77 RID: 7287 RVA: 0x0009AE89 File Offset: 0x00099289
		public ESlotType equipped { get; protected set; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x0009AE92 File Offset: 0x00099292
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x0009AE9A File Offset: 0x0009929A
		public ushort dialogue { get; protected set; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x0009AEA3 File Offset: 0x000992A3
		// (set) Token: 0x06001C7B RID: 7291 RVA: 0x0009AEAB File Offset: 0x000992AB
		public ENPCPose pose { get; protected set; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0009AEB4 File Offset: 0x000992B4
		// (set) Token: 0x06001C7D RID: 7293 RVA: 0x0009AEBC File Offset: 0x000992BC
		public float poseLean { get; protected set; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0009AEC5 File Offset: 0x000992C5
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0009AECD File Offset: 0x000992CD
		public float posePitch { get; protected set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0009AED6 File Offset: 0x000992D6
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0009AEDE File Offset: 0x000992DE
		public float poseHeadOffset { get; protected set; }
	}
}
