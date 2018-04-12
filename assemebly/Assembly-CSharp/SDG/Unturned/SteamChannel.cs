using System;
using System.Collections.Generic;
using System.Reflection;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000671 RID: 1649
	public class SteamChannel : MonoBehaviour
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x0013BBB7 File Offset: 0x00139FB7
		public SteamChannelMethod[] calls
		{
			get
			{
				return this._calls;
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0013BBBF File Offset: 0x00139FBF
		public bool checkServer(CSteamID steamID)
		{
			return steamID == Provider.server;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x0013BBCC File Offset: 0x00139FCC
		public bool checkOwner(CSteamID steamID)
		{
			return this.owner != null && steamID == this.owner.playerID.steamID;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x0013BBF4 File Offset: 0x00139FF4
		public void receive(CSteamID steamID, byte[] packet, int offset, int size)
		{
			if (SteamChannel.onTriggerReceive != null)
			{
				SteamChannel.onTriggerReceive(this, steamID, packet, offset, size);
			}
			if (size < 2)
			{
				return;
			}
			int num = (int)packet[offset + 1];
			if (num < 0 || num >= this.calls.Length)
			{
				return;
			}
			ESteamPacket esteamPacket = (ESteamPacket)packet[offset];
			if (esteamPacket == ESteamPacket.UPDATE_VOICE && size < 5)
			{
				return;
			}
			if (esteamPacket == ESteamPacket.UPDATE_UNRELIABLE_CHUNK_BUFFER || esteamPacket == ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER)
			{
				SteamPacker.openRead(offset + 2, packet);
				this.calls[num].method.Invoke(this.calls[num].component, new object[]
				{
					steamID
				});
				SteamPacker.closeRead();
			}
			else if (this.calls[num].types.Length > 0)
			{
				if (esteamPacket == ESteamPacket.UPDATE_VOICE)
				{
					SteamChannel.voice[0] = steamID;
					SteamChannel.voice[1] = packet;
					SteamChannel.voice[2] = (int)BitConverter.ToUInt16(packet, offset + 2);
					this.calls[num].method.Invoke(this.calls[num].component, SteamChannel.voice);
				}
				else
				{
					object[] objects = SteamPacker.getObjects(steamID, offset, 2, packet, this.calls[num].types);
					if (objects != null)
					{
						this.calls[num].method.Invoke(this.calls[num].component, objects);
					}
				}
			}
			else
			{
				this.calls[num].method.Invoke(this.calls[num].component, null);
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x0013BD81 File Offset: 0x0013A181
		public object read(Type type)
		{
			return SteamPacker.read(type);
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x0013BD89 File Offset: 0x0013A189
		public object[] read(Type type_0, Type type_1)
		{
			return SteamPacker.read(type_0, type_1);
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0013BD92 File Offset: 0x0013A192
		public object[] read(Type type_0, Type type_1, Type type_2)
		{
			return SteamPacker.read(type_0, type_1, type_2);
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x0013BD9C File Offset: 0x0013A19C
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3)
		{
			return SteamPacker.read(type_0, type_1, type_2, type_3);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x0013BDA8 File Offset: 0x0013A1A8
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4)
		{
			return SteamPacker.read(type_0, type_1, type_2, type_3, type_4);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x0013BDB6 File Offset: 0x0013A1B6
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5)
		{
			return SteamPacker.read(type_0, type_1, type_2, type_3, type_4, type_5);
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x0013BDC6 File Offset: 0x0013A1C6
		public object[] read(Type type_0, Type type_1, Type type_2, Type type_3, Type type_4, Type type_5, Type type_6)
		{
			return SteamPacker.read(type_0, type_1, type_2, type_3, type_4, type_5, type_6);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0013BDD8 File Offset: 0x0013A1D8
		public object[] read(params Type[] types)
		{
			return SteamPacker.read(types);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0013BDE0 File Offset: 0x0013A1E0
		public void write(object objects)
		{
			SteamPacker.write(objects);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0013BDE8 File Offset: 0x0013A1E8
		public void write(object object_0, object object_1)
		{
			SteamPacker.write(object_0, object_1);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0013BDF1 File Offset: 0x0013A1F1
		public void write(object object_0, object object_1, object object_2)
		{
			SteamPacker.write(object_0, object_1, object_2);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x0013BDFB File Offset: 0x0013A1FB
		public void write(object object_0, object object_1, object object_2, object object_3)
		{
			SteamPacker.write(object_0, object_1, object_2, object_3);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0013BE07 File Offset: 0x0013A207
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4)
		{
			SteamPacker.write(object_0, object_1, object_2, object_3, object_4);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0013BE15 File Offset: 0x0013A215
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5)
		{
			SteamPacker.write(object_0, object_1, object_2, object_3, object_4, object_5);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0013BE25 File Offset: 0x0013A225
		public void write(object object_0, object object_1, object object_2, object object_3, object object_4, object object_5, object object_6)
		{
			SteamPacker.write(object_0, object_1, object_2, object_3, object_4, object_5, object_6);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0013BE37 File Offset: 0x0013A237
		public void write(params object[] objects)
		{
			SteamPacker.write(objects);
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x0013BE3F File Offset: 0x0013A23F
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x0013BE46 File Offset: 0x0013A246
		public int step
		{
			get
			{
				return SteamPacker.step;
			}
			set
			{
				SteamPacker.step = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x0013BE4E File Offset: 0x0013A24E
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x0013BE55 File Offset: 0x0013A255
		public bool useCompression
		{
			get
			{
				return SteamPacker.useCompression;
			}
			set
			{
				SteamPacker.useCompression = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x0013BE5D File Offset: 0x0013A25D
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x0013BE64 File Offset: 0x0013A264
		public bool longBinaryData
		{
			get
			{
				return SteamPacker.longBinaryData;
			}
			set
			{
				SteamPacker.longBinaryData = value;
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0013BE6C File Offset: 0x0013A26C
		public void openWrite()
		{
			SteamPacker.openWrite(2);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x0013BE74 File Offset: 0x0013A274
		public void closeWrite(string name, CSteamID steamID, ESteamPacket type)
		{
			if (!Provider.isChunk(type))
			{
				Debug.LogError("Failed to stream non chunk.");
				return;
			}
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet);
			if (this.isOwner && steamID == Provider.client)
			{
				this.receive(Provider.client, packet, 0, size);
			}
			else if (Provider.isServer && steamID == Provider.server)
			{
				this.receive(Provider.server, packet, 0, size);
			}
			else
			{
				Provider.send(steamID, type, packet, size, this.id);
			}
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0013BF20 File Offset: 0x0013A320
		public void closeWrite(string name, ESteamCall mode, byte bound, ESteamPacket type)
		{
			if (!Provider.isChunk(type))
			{
				Debug.LogError("Failed to stream non chunk.");
				return;
			}
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet);
			this.send(mode, bound, type, size, packet);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0013BF70 File Offset: 0x0013A370
		public void closeWrite(string name, ESteamCall mode, byte x, byte y, byte area, ESteamPacket type)
		{
			if (!Provider.isChunk(type))
			{
				Debug.LogError("Failed to stream non chunk.");
				return;
			}
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet);
			this.send(mode, x, y, area, type, size, packet);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0013BFC4 File Offset: 0x0013A3C4
		public void closeWrite(string name, ESteamCall mode, ESteamPacket type)
		{
			if (!Provider.isChunk(type))
			{
				Debug.LogError("Failed to stream non chunk.");
				return;
			}
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet);
			this.send(mode, type, size, packet);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0013C010 File Offset: 0x0013A410
		public void send(string name, CSteamID steamID, ESteamPacket type, params object[] arguments)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			if (this.isOwner && steamID == Provider.client)
			{
				this.receive(Provider.client, packet, 0, size);
			}
			else if (Provider.isServer && steamID == Provider.server)
			{
				this.receive(Provider.server, packet, 0, size);
			}
			else
			{
				Provider.send(steamID, type, packet, size, this.id);
			}
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0013C0A8 File Offset: 0x0013A4A8
		public void sendAside(string name, CSteamID steamID, ESteamPacket type, params object[] arguments)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (Provider.clients[i].playerID.steamID != steamID)
				{
					Provider.send(Provider.clients[i].playerID.steamID, type, packet, size, this.id);
				}
			}
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x0013C134 File Offset: 0x0013A534
		public void send(ESteamCall mode, byte bound, ESteamPacket type, int size, byte[] packet)
		{
			if (mode == ESteamCall.SERVER)
			{
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.ALL)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].playerID.steamID != Provider.client && Provider.clients[i].player != null && Provider.clients[i].player.movement.bound == bound)
					{
						Provider.send(Provider.clients[i].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.OTHERS)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int j = 0; j < Provider.clients.Count; j++)
				{
					if (Provider.clients[j].playerID.steamID != Provider.client && Provider.clients[j].player != null && Provider.clients[j].player.movement.bound == bound)
					{
						Provider.send(Provider.clients[j].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.OWNER)
			{
				if (this.isOwner)
				{
					this.receive(this.owner.playerID.steamID, packet, 0, size);
				}
				else
				{
					Provider.send(this.owner.playerID.steamID, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.NOT_OWNER)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int k = 0; k < Provider.clients.Count; k++)
				{
					if (Provider.clients[k].playerID.steamID != this.owner.playerID.steamID && Provider.clients[k].player != null && Provider.clients[k].player.movement.bound == bound)
					{
						Provider.send(Provider.clients[k].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.CLIENTS)
			{
				for (int l = 0; l < Provider.clients.Count; l++)
				{
					if (Provider.clients[l].playerID.steamID != Provider.client && Provider.clients[l].player != null && Provider.clients[l].player.movement.bound == bound)
					{
						Provider.send(Provider.clients[l].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isClient)
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.PEERS)
			{
				for (int m = 0; m < Provider.clients.Count; m++)
				{
					if (Provider.clients[m].playerID.steamID != Provider.client && Provider.clients[m].player != null && Provider.clients[m].player.movement.bound == bound)
					{
						Provider.send(Provider.clients[m].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x0013C5E4 File Offset: 0x0013A9E4
		public void send(string name, ESteamCall mode, byte bound, ESteamPacket type, byte[] bytes, int length)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, bytes, length);
			this.send(mode, bound, type, size, packet);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x0013C620 File Offset: 0x0013AA20
		public void send(string name, ESteamCall mode, byte bound, ESteamPacket type, params object[] arguments)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			this.send(mode, bound, type, size, packet);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0013C658 File Offset: 0x0013AA58
		public void send(ESteamCall mode, byte x, byte y, byte area, ESteamPacket type, int size, byte[] packet)
		{
			if (mode == ESteamCall.SERVER)
			{
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.ALL)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].playerID.steamID != Provider.client && Provider.clients[i].player != null && Regions.checkArea(x, y, Provider.clients[i].player.movement.region_x, Provider.clients[i].player.movement.region_y, area))
					{
						Provider.send(Provider.clients[i].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.OTHERS)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int j = 0; j < Provider.clients.Count; j++)
				{
					if (Provider.clients[j].playerID.steamID != Provider.client && Provider.clients[j].player != null && Regions.checkArea(x, y, Provider.clients[j].player.movement.region_x, Provider.clients[j].player.movement.region_y, area))
					{
						Provider.send(Provider.clients[j].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.OWNER)
			{
				if (this.isOwner)
				{
					this.receive(this.owner.playerID.steamID, packet, 0, size);
				}
				else
				{
					Provider.send(this.owner.playerID.steamID, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.NOT_OWNER)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int k = 0; k < Provider.clients.Count; k++)
				{
					if (Provider.clients[k].playerID.steamID != this.owner.playerID.steamID && Provider.clients[k].player != null && Regions.checkArea(x, y, Provider.clients[k].player.movement.region_x, Provider.clients[k].player.movement.region_y, area))
					{
						Provider.send(Provider.clients[k].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.CLIENTS)
			{
				for (int l = 0; l < Provider.clients.Count; l++)
				{
					if (Provider.clients[l].playerID.steamID != Provider.client && Provider.clients[l].player != null && Regions.checkArea(x, y, Provider.clients[l].player.movement.region_x, Provider.clients[l].player.movement.region_y, area))
					{
						Provider.send(Provider.clients[l].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isClient)
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.PEERS)
			{
				for (int m = 0; m < Provider.clients.Count; m++)
				{
					if (Provider.clients[m].playerID.steamID != Provider.client && Provider.clients[m].player != null && Regions.checkArea(x, y, Provider.clients[m].player.movement.region_x, Provider.clients[m].player.movement.region_y, area))
					{
						Provider.send(Provider.clients[m].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0013CBBC File Offset: 0x0013AFBC
		public void send(string name, ESteamCall mode, byte x, byte y, byte area, ESteamPacket type, byte[] bytes, int length)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, bytes, length);
			this.send(mode, x, y, area, type, size, packet);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x0013CBFC File Offset: 0x0013AFFC
		public void send(string name, ESteamCall mode, byte x, byte y, byte area, ESteamPacket type, params object[] arguments)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			this.send(mode, x, y, area, type, size, packet);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x0013CC38 File Offset: 0x0013B038
		public void send(ESteamCall mode, ESteamPacket type, int size, byte[] packet)
		{
			if (mode == ESteamCall.SERVER)
			{
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.ALL)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].playerID.steamID != Provider.client)
					{
						Provider.send(Provider.clients[i].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.OTHERS)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int j = 0; j < Provider.clients.Count; j++)
				{
					if (Provider.clients[j].playerID.steamID != Provider.client)
					{
						Provider.send(Provider.clients[j].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.OWNER)
			{
				if (this.isOwner)
				{
					this.receive(this.owner.playerID.steamID, packet, 0, size);
				}
				else
				{
					Provider.send(this.owner.playerID.steamID, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.NOT_OWNER)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int k = 0; k < Provider.clients.Count; k++)
				{
					if (Provider.clients[k].playerID.steamID != this.owner.playerID.steamID)
					{
						Provider.send(Provider.clients[k].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.CLIENTS)
			{
				for (int l = 0; l < Provider.clients.Count; l++)
				{
					if (Provider.clients[l].playerID.steamID != Provider.client)
					{
						Provider.send(Provider.clients[l].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isClient)
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.PEERS)
			{
				for (int m = 0; m < Provider.clients.Count; m++)
				{
					if (Provider.clients[m].playerID.steamID != Provider.client)
					{
						Provider.send(Provider.clients[m].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x0013CFB0 File Offset: 0x0013B3B0
		public void send(string name, ESteamCall mode, ESteamPacket type, params object[] arguments)
		{
			if (SteamChannel.onTriggerSend != null)
			{
				SteamChannel.onTriggerSend(this.owner, name, mode, type, arguments);
			}
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			this.send(mode, type, size, packet);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x0013D004 File Offset: 0x0013B404
		public void send(string name, ESteamCall mode, ESteamPacket type, byte[] bytes, int length)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, bytes, length);
			this.send(mode, type, size, packet);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x0013D03C File Offset: 0x0013B43C
		public void send(ESteamCall mode, Vector3 point, float radius, ESteamPacket type, int size, byte[] packet)
		{
			radius *= radius;
			if (mode == ESteamCall.SERVER)
			{
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.ALL)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					if (Provider.clients[i].playerID.steamID != Provider.client && Provider.clients[i].player != null && (Provider.clients[i].player.transform.position - point).sqrMagnitude < radius)
					{
						Provider.send(Provider.clients[i].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isServer)
				{
					this.receive(Provider.server, packet, 0, size);
				}
				else
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.OTHERS)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int j = 0; j < Provider.clients.Count; j++)
				{
					if (Provider.clients[j].playerID.steamID != Provider.client && Provider.clients[j].player != null && (Provider.clients[j].player.transform.position - point).sqrMagnitude < radius)
					{
						Provider.send(Provider.clients[j].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.OWNER)
			{
				if (this.isOwner)
				{
					this.receive(this.owner.playerID.steamID, packet, 0, size);
				}
				else
				{
					Provider.send(this.owner.playerID.steamID, type, packet, size, this.id);
				}
			}
			else if (mode == ESteamCall.NOT_OWNER)
			{
				if (!Provider.isServer)
				{
					Provider.send(Provider.server, type, packet, size, this.id);
				}
				for (int k = 0; k < Provider.clients.Count; k++)
				{
					if (Provider.clients[k].playerID.steamID != this.owner.playerID.steamID && Provider.clients[k].player != null && (Provider.clients[k].player.transform.position - point).sqrMagnitude < radius)
					{
						Provider.send(Provider.clients[k].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
			else if (mode == ESteamCall.CLIENTS)
			{
				for (int l = 0; l < Provider.clients.Count; l++)
				{
					if (Provider.clients[l].playerID.steamID != Provider.client && Provider.clients[l].player != null && (Provider.clients[l].player.transform.position - point).sqrMagnitude < radius)
					{
						Provider.send(Provider.clients[l].playerID.steamID, type, packet, size, this.id);
					}
				}
				if (Provider.isClient)
				{
					this.receive(Provider.client, packet, 0, size);
				}
			}
			else if (mode == ESteamCall.PEERS)
			{
				for (int m = 0; m < Provider.clients.Count; m++)
				{
					if (Provider.clients[m].playerID.steamID != Provider.client && Provider.clients[m].player != null && (Provider.clients[m].player.transform.position - point).sqrMagnitude < radius)
					{
						Provider.send(Provider.clients[m].playerID.steamID, type, packet, size, this.id);
					}
				}
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x0013D554 File Offset: 0x0013B954
		public void sendVoice(string name, ESteamCall mode, Vector3 point, float radius, ESteamPacket type, byte[] bytes, int length)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacketVoice(type, call, out size, out packet, bytes, length);
			this.send(mode, point, radius, type, size, packet);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x0013D590 File Offset: 0x0013B990
		public void send(string name, ESteamCall mode, Vector3 point, float radius, ESteamPacket type, params object[] arguments)
		{
			int call = this.getCall(name);
			if (call == -1)
			{
				return;
			}
			int size;
			byte[] packet;
			this.getPacket(type, call, out size, out packet, arguments);
			this.send(mode, point, radius, type, size, packet);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x0013D5CC File Offset: 0x0013B9CC
		public void build()
		{
			List<SteamChannelMethod> list = new List<SteamChannelMethod>();
			Component[] components = base.GetComponents(typeof(Component));
			for (int i = 0; i < components.Length; i++)
			{
				MemberInfo[] members = components[i].GetType().GetMembers();
				for (int j = 0; j < members.Length; j++)
				{
					if (members[j].MemberType == MemberTypes.Method)
					{
						MethodInfo methodInfo = (MethodInfo)members[j];
						if (methodInfo.GetCustomAttributes(typeof(SteamCall), true).Length > 0)
						{
							ParameterInfo[] parameters = methodInfo.GetParameters();
							Type[] array = new Type[parameters.Length];
							for (int k = 0; k < parameters.Length; k++)
							{
								array[k] = parameters[k].ParameterType;
							}
							list.Add(new SteamChannelMethod(components[i], methodInfo, array));
						}
					}
				}
			}
			this._calls = list.ToArray();
			if (this.calls.Length > 235)
			{
				CommandWindow.LogError(base.name + " approaching 255 methods!");
			}
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x0013D6DF File Offset: 0x0013BADF
		public void setup()
		{
			Provider.openChannel(this);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x0013D6E7 File Offset: 0x0013BAE7
		public void getPacket(ESteamPacket type, int index, out int size, out byte[] packet)
		{
			packet = SteamPacker.closeWrite(out size);
			packet[0] = (byte)type;
			packet[1] = (byte)index;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x0013D700 File Offset: 0x0013BB00
		public void getPacket(ESteamPacket type, int index, out int size, out byte[] packet, byte[] bytes, int length)
		{
			size = 4 + length;
			packet = bytes;
			packet[0] = (byte)type;
			packet[1] = (byte)index;
			byte[] bytes2 = BitConverter.GetBytes((ushort)length);
			packet[2] = bytes2[0];
			packet[3] = bytes2[1];
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x0013D740 File Offset: 0x0013BB40
		public void getPacketVoice(ESteamPacket type, int index, out int size, out byte[] packet, byte[] bytes, int length)
		{
			size = 5 + length;
			packet = bytes;
			packet[0] = (byte)type;
			packet[1] = (byte)index;
			byte[] bytes2 = BitConverter.GetBytes((ushort)length);
			packet[2] = bytes2[0];
			packet[3] = bytes2[1];
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x0013D77F File Offset: 0x0013BB7F
		public void getPacket(ESteamPacket type, int index, out int size, out byte[] packet, params object[] arguments)
		{
			packet = SteamPacker.getBytes(2, out size, arguments);
			packet[0] = (byte)type;
			packet[1] = (byte)index;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0013D79C File Offset: 0x0013BB9C
		public int getCall(string name)
		{
			for (int i = 0; i < this.calls.Length; i++)
			{
				if (this.calls[i].method.Name == name)
				{
					return i;
				}
			}
			CommandWindow.LogError("Failed to find a method named: " + name);
			return -1;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0013D7F2 File Offset: 0x0013BBF2
		private void Awake()
		{
			this.build();
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0013D7FA File Offset: 0x0013BBFA
		private void OnDestroy()
		{
			if (this.id != 0)
			{
				Provider.closeChannel(this);
			}
		}

		// Token: 0x04001F97 RID: 8087
		private static object[] voice = new object[3];

		// Token: 0x04001F98 RID: 8088
		private SteamChannelMethod[] _calls;

		// Token: 0x04001F99 RID: 8089
		public int id;

		// Token: 0x04001F9A RID: 8090
		public SteamPlayer owner;

		// Token: 0x04001F9B RID: 8091
		public bool isOwner;

		// Token: 0x04001F9C RID: 8092
		public static TriggerReceive onTriggerReceive;

		// Token: 0x04001F9D RID: 8093
		public static TriggerSend onTriggerSend;
	}
}
