using System;

namespace SDG.Framework.Debug
{
	// Token: 0x0200010B RID: 267
	[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
	public class InspectableEnumAttribute : Attribute
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0004CB45 File Offset: 0x0004AF45
		public InspectableEnumAttribute(EEnumInspectionMode inspectionMode)
		{
			this.inspectionMode = inspectionMode;
		}

		// Token: 0x040006C3 RID: 1731
		public EEnumInspectionMode inspectionMode;
	}
}
