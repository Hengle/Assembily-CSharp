using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SDG.Framework.Modules
{
	// Token: 0x020001ED RID: 493
	public class Module
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00065546 File Offset: 0x00063946
		public Module(ModuleConfig newConfig)
		{
			this.config = newConfig;
			this.isEnabled = false;
			this.status = EModuleStatus.None;
			this.nexii = new List<IModuleNexus>();
			this.register();
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00065574 File Offset: 0x00063974
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x0006557C File Offset: 0x0006397C
		public bool isEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (this.isEnabled == value)
				{
					return;
				}
				this._isEnabled = value;
				if (this.isEnabled)
				{
					this.load();
					this.initialize();
				}
				else
				{
					this.shutdown();
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000655B4 File Offset: 0x000639B4
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x000655BC File Offset: 0x000639BC
		public ModuleConfig config { get; protected set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000655C5 File Offset: 0x000639C5
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x000655CD File Offset: 0x000639CD
		public Assembly[] assemblies { get; protected set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x000655D6 File Offset: 0x000639D6
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x000655DE File Offset: 0x000639DE
		public Type[] types { get; protected set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x000655E7 File Offset: 0x000639E7
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x000655EF File Offset: 0x000639EF
		public EModuleStatus status { get; protected set; }

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000ECD RID: 3789 RVA: 0x000655F8 File Offset: 0x000639F8
		// (remove) Token: 0x06000ECE RID: 3790 RVA: 0x00065630 File Offset: 0x00063A30
		public event ModuleLoaded onModuleLoaded;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000ECF RID: 3791 RVA: 0x00065668 File Offset: 0x00063A68
		// (remove) Token: 0x06000ED0 RID: 3792 RVA: 0x000656A0 File Offset: 0x00063AA0
		public event ModuleInitialized onModuleInitialized;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000ED1 RID: 3793 RVA: 0x000656D8 File Offset: 0x00063AD8
		// (remove) Token: 0x06000ED2 RID: 3794 RVA: 0x00065710 File Offset: 0x00063B10
		public event ModuleShutdown onModuleShutdown;

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00065748 File Offset: 0x00063B48
		protected void register()
		{
			if (this.config == null)
			{
				return;
			}
			for (int i = 0; i < this.config.Assemblies.Count; i++)
			{
				ModuleHook.registerAssemblyPath(this.config.DirectoryPath + this.config.Assemblies[i].Path);
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000657B0 File Offset: 0x00063BB0
		protected void load()
		{
			if (this.config == null || this.assemblies != null)
			{
				return;
			}
			if (!this.config.IsEnabled)
			{
				return;
			}
			List<Type> list = new List<Type>();
			this.assemblies = new Assembly[this.config.Assemblies.Count];
			for (int i = 0; i < this.config.Assemblies.Count; i++)
			{
				Assembly assembly = ModuleHook.resolveAssemblyPath(this.config.DirectoryPath + this.config.Assemblies[i].Path);
				this.assemblies[i] = assembly;
				Type[] types;
				try
				{
					types = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					types = ex.Types;
				}
				if (types != null)
				{
					for (int j = 0; j < types.Length; j++)
					{
						if (types[j] != null)
						{
							list.Add(types[j]);
						}
					}
				}
			}
			this.types = list.ToArray();
			if (this.onModuleLoaded != null)
			{
				this.onModuleLoaded(this);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000658E0 File Offset: 0x00063CE0
		protected void initialize()
		{
			if (this.config == null || this.assemblies == null)
			{
				return;
			}
			if (this.status != EModuleStatus.None && this.status != EModuleStatus.Shutdown)
			{
				return;
			}
			this.nexii.Clear();
			Type typeFromHandle = typeof(IModuleNexus);
			for (int i = 0; i < this.types.Length; i++)
			{
				Type type = this.types[i];
				if (!type.IsAbstract && typeFromHandle.IsAssignableFrom(type))
				{
					IModuleNexus moduleNexus = Activator.CreateInstance(type) as IModuleNexus;
					try
					{
						moduleNexus.initialize();
					}
					catch (Exception exception)
					{
						Debug.LogError("Failed to initialize nexus!");
						Debug.LogException(exception);
					}
					this.nexii.Add(moduleNexus);
				}
			}
			this.status = EModuleStatus.Initialized;
			if (this.onModuleInitialized != null)
			{
				this.onModuleInitialized(this);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000659D4 File Offset: 0x00063DD4
		protected void shutdown()
		{
			if (this.config == null || this.assemblies == null)
			{
				return;
			}
			if (this.status != EModuleStatus.Initialized)
			{
				return;
			}
			for (int i = 0; i < this.nexii.Count; i++)
			{
				try
				{
					this.nexii[i].shutdown();
				}
				catch (Exception exception)
				{
					Debug.LogError("Failed to shutdown nexus!");
					Debug.LogException(exception);
				}
			}
			this.nexii.Clear();
			this.status = EModuleStatus.Shutdown;
			if (this.onModuleShutdown != null)
			{
				this.onModuleShutdown(this);
			}
		}

		// Token: 0x0400094A RID: 2378
		protected bool _isEnabled;

		// Token: 0x0400094F RID: 2383
		private List<IModuleNexus> nexii;
	}
}
