using System;
using UnityEngine;

namespace SDG.Provider
{
	// Token: 0x02000348 RID: 840
	public struct DynamicEconDetails
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x00085801 File Offset: 0x00083C01
		public DynamicEconDetails(string tags, string dynamic_props)
		{
			this.tags = ((!string.IsNullOrEmpty(tags)) ? tags : string.Empty);
			this.dynamic_props = ((!string.IsNullOrEmpty(dynamic_props)) ? dynamic_props : string.Empty);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0008583B File Offset: 0x00083C3B
		public bool getStatTrackerType(out EStatTrackerType type)
		{
			type = EStatTrackerType.NONE;
			if (this.tags.Contains("stat_tracker:total_kills"))
			{
				type = EStatTrackerType.TOTAL;
				return true;
			}
			if (this.tags.Contains("stat_tracker:player_kills"))
			{
				type = EStatTrackerType.PLAYER;
				return true;
			}
			return false;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00085878 File Offset: 0x00083C78
		public bool getStatTrackerValue(out EStatTrackerType type, out int kills)
		{
			kills = -1;
			if (!this.getStatTrackerType(out type))
			{
				return false;
			}
			if (type == EStatTrackerType.TOTAL)
			{
				if (string.IsNullOrEmpty(this.dynamic_props))
				{
					kills = 0;
				}
				else
				{
					kills = JsonUtility.FromJson<StatTrackerTotalKillsJson>(this.dynamic_props).total_kills;
				}
				return true;
			}
			if (type != EStatTrackerType.PLAYER)
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.dynamic_props))
			{
				kills = 0;
			}
			else
			{
				kills = JsonUtility.FromJson<StatTrackerPlayerKillsJson>(this.dynamic_props).player_kills;
			}
			return true;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0008590C File Offset: 0x00083D0C
		public string getPredictedDynamicPropsJsonForStatTracker(EStatTrackerType type, int kills)
		{
			if (type == EStatTrackerType.TOTAL)
			{
				return JsonUtility.ToJson(new StatTrackerTotalKillsJson
				{
					total_kills = kills
				});
			}
			if (type != EStatTrackerType.PLAYER)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new StatTrackerPlayerKillsJson
			{
				player_kills = kills
			});
		}

		// Token: 0x04000C6B RID: 3179
		public string tags;

		// Token: 0x04000C6C RID: 3180
		public string dynamic_props;
	}
}
