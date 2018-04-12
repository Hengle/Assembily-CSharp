using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.IO.FormattedFiles;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200014F RID: 335
	public class KillVolume : DevkitHierarchyVolume, IDevkitHierarchySpawnable
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x0005062C File Offset: 0x0004EA2C
		public KillVolume()
		{
			this.killPlayers = true;
			this.killZombies = true;
			this.killAnimals = true;
			this.killVehicles = false;
			this.deathCause = EDeathCause.SUICIDE;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00050658 File Offset: 0x0004EA58
		public void devkitHierarchySpawn()
		{
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0005065C File Offset: 0x0004EA5C
		protected override void readHierarchyItem(IFormattedFileReader reader)
		{
			base.readHierarchyItem(reader);
			this.killPlayers = reader.readValue<bool>("Kill_Players");
			this.killZombies = reader.readValue<bool>("Kill_Zombies");
			this.killAnimals = reader.readValue<bool>("Kill_Animals");
			this.killVehicles = reader.readValue<bool>("Kill_Vehicles");
			this.deathCause = reader.readValue<EDeathCause>("Death_Cause");
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000506C8 File Offset: 0x0004EAC8
		protected override void writeHierarchyItem(IFormattedFileWriter writer)
		{
			base.writeHierarchyItem(writer);
			writer.writeValue<bool>("Kill_Players", this.killPlayers);
			writer.writeValue<bool>("Kill_Zombies", this.killZombies);
			writer.writeValue<bool>("Kill_Animals", this.killAnimals);
			writer.writeValue<bool>("Kill_Vehicles", this.killVehicles);
			writer.writeValue<EDeathCause>("Death_Cause", this.deathCause);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00050731 File Offset: 0x0004EB31
		protected virtual void updateBoxEnabled()
		{
			base.box.enabled = (Dedicator.isDedicated || KillVolumeSystem.killVisibilityGroup.isVisible);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00050755 File Offset: 0x0004EB55
		protected virtual void handleVisibilityGroupIsVisibleChanged(IVisibilityGroup group)
		{
			this.updateBoxEnabled();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00050760 File Offset: 0x0004EB60
		public void OnTriggerEnter(Collider other)
		{
			if (other.isTrigger)
			{
				return;
			}
			if (!Provider.isServer)
			{
				return;
			}
			if (other.CompareTag("Player"))
			{
				if (this.killPlayers)
				{
					Player player = DamageTool.getPlayer(other.transform);
					if (player != null)
					{
						EPlayerKill eplayerKill;
						DamageTool.damage(player, this.deathCause, ELimb.SPINE, CSteamID.Nil, Vector3.up, 101f, 1f, out eplayerKill);
					}
				}
			}
			else if (other.CompareTag("Agent"))
			{
				if (this.killZombies || this.killAnimals)
				{
					Zombie zombie = DamageTool.getZombie(other.transform);
					if (zombie != null)
					{
						if (this.killZombies)
						{
							EPlayerKill eplayerKill2;
							uint num;
							DamageTool.damage(zombie, Vector3.up, 65000f, 1f, out eplayerKill2, out num);
						}
					}
					else if (this.killAnimals)
					{
						Animal animal = DamageTool.getAnimal(other.transform);
						if (animal != null)
						{
							EPlayerKill eplayerKill3;
							uint num2;
							DamageTool.damage(animal, Vector3.up, 65000f, 1f, out eplayerKill3, out num2);
						}
					}
				}
			}
			else if (other.CompareTag("Vehicle") && this.killVehicles)
			{
				InteractableVehicle vehicle = DamageTool.getVehicle(other.transform);
				if (vehicle != null && !vehicle.isDead)
				{
					EPlayerKill eplayerKill4;
					DamageTool.damage(vehicle, false, Vector3.zero, false, 65000f, 1f, false, out eplayerKill4);
				}
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000508E7 File Offset: 0x0004ECE7
		protected void OnEnable()
		{
			LevelHierarchy.addItem(this);
			KillVolumeSystem.addVolume(this);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000508F5 File Offset: 0x0004ECF5
		protected void OnDisable()
		{
			KillVolumeSystem.removeVolume(this);
			LevelHierarchy.removeItem(this);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00050904 File Offset: 0x0004ED04
		protected void Awake()
		{
			base.name = "Kill_Volume";
			base.gameObject.layer = LayerMasks.TRAP;
			base.box = base.gameObject.getOrAddComponent<BoxCollider>();
			base.box.isTrigger = true;
			this.updateBoxEnabled();
			KillVolumeSystem.killVisibilityGroup.isVisibleChanged += this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00050966 File Offset: 0x0004ED66
		protected void OnDestroy()
		{
			KillVolumeSystem.killVisibilityGroup.isVisibleChanged -= this.handleVisibilityGroupIsVisibleChanged;
		}

		// Token: 0x0400073F RID: 1855
		[Inspectable("#SDG::Devkit.Volumes.Kill.Players", null)]
		public bool killPlayers;

		// Token: 0x04000740 RID: 1856
		[Inspectable("#SDG::Devkit.Volumes.Kill.Zombies", null)]
		public bool killZombies;

		// Token: 0x04000741 RID: 1857
		[Inspectable("#SDG::Devkit.Volumes.Kill.Animals", null)]
		public bool killAnimals;

		// Token: 0x04000742 RID: 1858
		[Inspectable("#SDG::Devkit.Volumes.Kill.Vehicles", null)]
		public bool killVehicles;

		// Token: 0x04000743 RID: 1859
		[Inspectable("#SDG::Devkit.Volumes.Death_Cause", null)]
		public EDeathCause deathCause;
	}
}
