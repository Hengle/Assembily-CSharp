using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200060F RID: 1551
	public class DrivingPlayerInputPacket : PlayerInputPacket
	{
		// Token: 0x06002BF1 RID: 11249 RVA: 0x00116928 File Offset: 0x00114D28
		public override void read(SteamChannel channel)
		{
			base.read(channel);
			this.position = (Vector3)channel.read(Types.VECTOR3_TYPE);
			this.angle_x = (byte)channel.read(Types.BYTE_TYPE);
			this.angle_y = (byte)channel.read(Types.BYTE_TYPE);
			this.angle_z = (byte)channel.read(Types.BYTE_TYPE);
			this.speed = (byte)channel.read(Types.BYTE_TYPE);
			this.physicsSpeed = (byte)channel.read(Types.BYTE_TYPE);
			this.turn = (byte)channel.read(Types.BYTE_TYPE);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x001169D8 File Offset: 0x00114DD8
		public override void write(SteamChannel channel)
		{
			base.write(channel);
			channel.write(this.position);
			channel.write(this.angle_x);
			channel.write(this.angle_y);
			channel.write(this.angle_z);
			channel.write(this.speed);
			channel.write(this.physicsSpeed);
			channel.write(this.turn);
		}

		// Token: 0x04001C4D RID: 7245
		public Vector3 position;

		// Token: 0x04001C4E RID: 7246
		public byte angle_x;

		// Token: 0x04001C4F RID: 7247
		public byte angle_y;

		// Token: 0x04001C50 RID: 7248
		public byte angle_z;

		// Token: 0x04001C51 RID: 7249
		public byte speed;

		// Token: 0x04001C52 RID: 7250
		public byte physicsSpeed;

		// Token: 0x04001C53 RID: 7251
		public byte turn;
	}
}
