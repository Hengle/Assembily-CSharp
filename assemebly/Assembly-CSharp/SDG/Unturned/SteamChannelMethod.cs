using System;
using System.Reflection;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000672 RID: 1650
	public class SteamChannelMethod
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x0013D81A File Offset: 0x0013BC1A
		public SteamChannelMethod(Component newComponent, MethodInfo newMethod, Type[] newTypes)
		{
			this._component = newComponent;
			this._method = newMethod;
			this._types = newTypes;
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x0013D837 File Offset: 0x0013BC37
		public Component component
		{
			get
			{
				return this._component;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x0013D83F File Offset: 0x0013BC3F
		public MethodInfo method
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x0013D847 File Offset: 0x0013BC47
		public Type[] types
		{
			get
			{
				return this._types;
			}
		}

		// Token: 0x04001F9E RID: 8094
		private Component _component;

		// Token: 0x04001F9F RID: 8095
		private MethodInfo _method;

		// Token: 0x04001FA0 RID: 8096
		private Type[] _types;
	}
}
