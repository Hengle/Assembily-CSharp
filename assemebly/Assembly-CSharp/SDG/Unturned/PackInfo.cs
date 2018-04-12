using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000583 RID: 1411
	public class PackInfo
	{
		// Token: 0x060026E5 RID: 9957 RVA: 0x000E7308 File Offset: 0x000E5708
		public PackInfo()
		{
			this.spawns = new List<AnimalSpawnpoint>();
			this.animals = new List<Animal>();
			this.wanderAngle = UnityEngine.Random.Range(0f, 360f);
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000E733B File Offset: 0x000E573B
		// (set) Token: 0x060026E7 RID: 9959 RVA: 0x000E7343 File Offset: 0x000E5743
		public List<AnimalSpawnpoint> spawns { get; private set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x000E734C File Offset: 0x000E574C
		// (set) Token: 0x060026E9 RID: 9961 RVA: 0x000E7354 File Offset: 0x000E5754
		public List<Animal> animals { get; private set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060026EA RID: 9962 RVA: 0x000E735D File Offset: 0x000E575D
		// (set) Token: 0x060026EB RID: 9963 RVA: 0x000E7365 File Offset: 0x000E5765
		public float wanderAngle
		{
			get
			{
				return this._wanderAngle;
			}
			set
			{
				this._wanderAngle = value;
				this.wanderNormal = new Vector3(Mathf.Cos(0.0174532924f * this.wanderAngle), 0f, Mathf.Sin(0.0174532924f * this.wanderAngle));
			}
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000E73A0 File Offset: 0x000E57A0
		public Vector3 getWanderDirection()
		{
			return this.wanderNormal;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000E73A8 File Offset: 0x000E57A8
		public Vector3 getAverageSpawnPoint()
		{
			if (this.avgSpawnPoint == null)
			{
				this.avgSpawnPoint = new Vector3?(Vector3.zero);
				for (int i = 0; i < this.spawns.Count; i++)
				{
					AnimalSpawnpoint animalSpawnpoint = this.spawns[i];
					if (animalSpawnpoint != null)
					{
						Vector3? vector = this.avgSpawnPoint;
						this.avgSpawnPoint = ((vector == null) ? null : new Vector3?(vector.GetValueOrDefault() + animalSpawnpoint.point));
					}
				}
				Vector3? vector2 = this.avgSpawnPoint;
				this.avgSpawnPoint = ((vector2 == null) ? null : new Vector3?(vector2.GetValueOrDefault() / (float)this.spawns.Count));
			}
			return this.avgSpawnPoint.Value;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000E7494 File Offset: 0x000E5894
		public Vector3 getAverageAnimalPoint()
		{
			if (Time.frameCount > this.avgAnimalPointRecalculation)
			{
				this.avgAnimalPoint = Vector3.zero;
				for (int i = 0; i < this.animals.Count; i++)
				{
					Animal animal = this.animals[i];
					if (!(animal == null))
					{
						this.avgAnimalPoint += animal.transform.position;
					}
				}
				this.avgAnimalPoint /= (float)this.animals.Count;
				this.avgAnimalPointRecalculation = Time.frameCount;
			}
			return this.avgAnimalPoint;
		}

		// Token: 0x0400189D RID: 6301
		private Vector3 wanderNormal;

		// Token: 0x0400189E RID: 6302
		private float _wanderAngle;

		// Token: 0x0400189F RID: 6303
		private Vector3? avgSpawnPoint;

		// Token: 0x040018A0 RID: 6304
		private int avgAnimalPointRecalculation;

		// Token: 0x040018A1 RID: 6305
		private Vector3 avgAnimalPoint;
	}
}
