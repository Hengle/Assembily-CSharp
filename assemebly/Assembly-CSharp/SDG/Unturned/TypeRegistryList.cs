using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x02000427 RID: 1063
	public class TypeRegistryList
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x0009C3E7 File Offset: 0x0009A7E7
		public TypeRegistryList(Type newBaseType)
		{
			this.baseType = newBaseType;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0009C401 File Offset: 0x0009A801
		public List<Type> getTypes()
		{
			return this.typesList;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0009C409 File Offset: 0x0009A809
		public void addType(Type type)
		{
			if (this.baseType.IsAssignableFrom(type))
			{
				this.typesList.Add(type);
				return;
			}
			throw new ArgumentException(this.baseType + " is not assignable from " + type, "type");
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0009C449 File Offset: 0x0009A849
		public void removeType(Type type)
		{
			this.typesList.RemoveFast(type);
		}

		// Token: 0x04001106 RID: 4358
		private Type baseType;

		// Token: 0x04001107 RID: 4359
		private List<Type> typesList = new List<Type>();
	}
}
