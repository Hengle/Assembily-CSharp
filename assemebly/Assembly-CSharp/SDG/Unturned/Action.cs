using System;

namespace SDG.Unturned
{
	// Token: 0x02000501 RID: 1281
	public class Action
	{
		// Token: 0x06002301 RID: 8961 RVA: 0x000C3A83 File Offset: 0x000C1E83
		public Action(ushort newSource, EActionType newType, ActionBlueprint[] newBlueprints, string newText, string newTooltip, string newKey)
		{
			this._source = newSource;
			this._type = newType;
			this._blueprints = newBlueprints;
			this._text = newText;
			this._tooltip = newTooltip;
			this._key = newKey;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x000C3AB8 File Offset: 0x000C1EB8
		public ushort source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x000C3AC0 File Offset: 0x000C1EC0
		public EActionType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000C3AC8 File Offset: 0x000C1EC8
		public ActionBlueprint[] blueprints
		{
			get
			{
				return this._blueprints;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x000C3AD0 File Offset: 0x000C1ED0
		public string text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x000C3AD8 File Offset: 0x000C1ED8
		public string tooltip
		{
			get
			{
				return this._tooltip;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000C3AE0 File Offset: 0x000C1EE0
		public string key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x0400150A RID: 5386
		private ushort _source;

		// Token: 0x0400150B RID: 5387
		private EActionType _type;

		// Token: 0x0400150C RID: 5388
		private ActionBlueprint[] _blueprints;

		// Token: 0x0400150D RID: 5389
		private string _text;

		// Token: 0x0400150E RID: 5390
		private string _tooltip;

		// Token: 0x0400150F RID: 5391
		private string _key;
	}
}
