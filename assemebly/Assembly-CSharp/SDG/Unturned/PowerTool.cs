using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000743 RID: 1859
	public class PowerTool
	{
		// Token: 0x06003467 RID: 13415 RVA: 0x00157194 File Offset: 0x00155594
		public static void checkInteractables<T>(Vector3 point, float radius, ushort plant, List<T> interactablesInRadius) where T : Interactable
		{
			float sqrRadius = radius * radius;
			if (plant == 65535)
			{
				PowerTool.regionsInRadius.Clear();
				Regions.getRegionsInRadius(point, radius, PowerTool.regionsInRadius);
				PowerTool.barricadesInRadius.Clear();
				BarricadeManager.getBarricadesInRadius(point, sqrRadius, PowerTool.regionsInRadius, PowerTool.barricadesInRadius);
				ObjectManager.getObjectsInRadius(point, sqrRadius, PowerTool.regionsInRadius, PowerTool.barricadesInRadius);
			}
			else
			{
				PowerTool.barricadesInRadius.Clear();
				BarricadeManager.getBarricadesInRadius(point, sqrRadius, plant, PowerTool.barricadesInRadius);
			}
			for (int i = 0; i < PowerTool.barricadesInRadius.Count; i++)
			{
				T component = PowerTool.barricadesInRadius[i].GetComponent<T>();
				if (!(component == null))
				{
					interactablesInRadius.Add(component);
				}
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00157258 File Offset: 0x00155658
		public static void checkInteractables<T>(Vector3 point, float radius, List<T> interactablesInRadius) where T : Interactable
		{
			float sqrRadius = radius * radius;
			PowerTool.regionsInRadius.Clear();
			Regions.getRegionsInRadius(point, radius, PowerTool.regionsInRadius);
			PowerTool.barricadesInRadius.Clear();
			BarricadeManager.getBarricadesInRadius(point, sqrRadius, PowerTool.regionsInRadius, PowerTool.barricadesInRadius);
			BarricadeManager.getBarricadesInRadius(point, sqrRadius, PowerTool.barricadesInRadius);
			for (int i = 0; i < PowerTool.barricadesInRadius.Count; i++)
			{
				T component = PowerTool.barricadesInRadius[i].GetComponent<T>();
				if (!(component == null))
				{
					interactablesInRadius.Add(component);
				}
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x001572F0 File Offset: 0x001556F0
		public static bool checkFires(Vector3 point, float radius)
		{
			PowerTool.firesInRadius.Clear();
			PowerTool.checkInteractables<InteractableFire>(point, radius, ushort.MaxValue, PowerTool.firesInRadius);
			for (int i = 0; i < PowerTool.firesInRadius.Count; i++)
			{
				if (PowerTool.firesInRadius[i].isLit)
				{
					return true;
				}
			}
			PowerTool.ovensInRadius.Clear();
			PowerTool.checkInteractables<InteractableOven>(point, radius, ushort.MaxValue, PowerTool.ovensInRadius);
			for (int j = 0; j < PowerTool.ovensInRadius.Count; j++)
			{
				if (PowerTool.ovensInRadius[j].isWired && PowerTool.ovensInRadius[j].isLit)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x001573AD File Offset: 0x001557AD
		public static List<InteractableGenerator> checkGenerators(Vector3 point, float radius, ushort plant)
		{
			PowerTool.generatorsInRadius.Clear();
			PowerTool.checkInteractables<InteractableGenerator>(point, radius, plant, PowerTool.generatorsInRadius);
			return PowerTool.generatorsInRadius;
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x001573CB File Offset: 0x001557CB
		public static List<InteractablePower> checkPower(Vector3 point, float radius, ushort plant)
		{
			PowerTool.powerInRadius.Clear();
			PowerTool.checkInteractables<InteractablePower>(point, radius, plant, PowerTool.powerInRadius);
			return PowerTool.powerInRadius;
		}

		// Token: 0x040023A2 RID: 9122
		private static List<RegionCoordinate> regionsInRadius = new List<RegionCoordinate>(4);

		// Token: 0x040023A3 RID: 9123
		private static List<Transform> barricadesInRadius = new List<Transform>();

		// Token: 0x040023A4 RID: 9124
		private static List<InteractableFire> firesInRadius = new List<InteractableFire>();

		// Token: 0x040023A5 RID: 9125
		private static List<InteractableOven> ovensInRadius = new List<InteractableOven>();

		// Token: 0x040023A6 RID: 9126
		private static List<InteractablePower> powerInRadius = new List<InteractablePower>();

		// Token: 0x040023A7 RID: 9127
		private static List<InteractableGenerator> generatorsInRadius = new List<InteractableGenerator>();
	}
}
