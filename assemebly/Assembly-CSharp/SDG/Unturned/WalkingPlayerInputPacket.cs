using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000611 RID: 1553
	public class WalkingPlayerInputPacket : PlayerInputPacket
	{
		// Token: 0x06002BF4 RID: 11252 RVA: 0x00116A6C File Offset: 0x00114E6C
		public override void read(SteamChannel channel)
		{
			base.read(channel);
			this.analog = (byte)channel.read(Types.BYTE_TYPE);
			this.position = (Vector3)channel.read(Types.VECTOR3_TYPE);
			this.yaw = (float)channel.read(Types.SINGLE_TYPE);
			this.pitch = (float)channel.read(Types.SINGLE_TYPE);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00116AD8 File Offset: 0x00114ED8
		public override void write(SteamChannel channel)
		{
			base.write(channel);
			channel.write(this.analog);
			channel.write(this.position);
			channel.write(this.yaw);
			channel.write(this.pitch);
		}

		// Token: 0x04001C5F RID: 7263
		public byte analog;

		// Token: 0x04001C60 RID: 7264
		public Vector3 position;

		// Token: 0x04001C61 RID: 7265
		public float yaw;

		// Token: 0x04001C62 RID: 7266
		public float pitch;
	}
}
