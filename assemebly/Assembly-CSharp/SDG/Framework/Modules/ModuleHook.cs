using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SDG.Framework.IO;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Modules
{
	// Token: 0x020001F4 RID: 500
	public class ModuleHook : MonoBehaviour
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00065BB5 File Offset: 0x00063FB5
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00065BBC File Offset: 0x00063FBC
		public static List<Module> modules { get; protected set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00065BC4 File Offset: 0x00063FC4
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00065BCB File Offset: 0x00063FCB
		public static Assembly coreAssembly { get; protected set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00065BD3 File Offset: 0x00063FD3
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x00065BDA File Offset: 0x00063FDA
		public static Type[] coreTypes { get; protected set; }

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000EEB RID: 3819 RVA: 0x00065BE4 File Offset: 0x00063FE4
		// (remove) Token: 0x06000EEC RID: 3820 RVA: 0x00065C18 File Offset: 0x00064018
		public static event ModulesInitializedHandler onModulesInitialized;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000EED RID: 3821 RVA: 0x00065C4C File Offset: 0x0006404C
		// (remove) Token: 0x06000EEE RID: 3822 RVA: 0x00065C80 File Offset: 0x00064080
		public static event ModulesShutdownHandler onModulesShutdown;

		// Token: 0x06000EEF RID: 3823 RVA: 0x00065CB4 File Offset: 0x000640B4
		public static void getRequiredModules(List<Module> result)
		{
			if (ModuleHook.modules == null || result == null)
			{
				return;
			}
			for (int i = 0; i < ModuleHook.modules.Count; i++)
			{
				Module module = ModuleHook.modules[i];
				if (module != null)
				{
					ModuleConfig config = module.config;
					if (config != null)
					{
						for (int j = 0; j < config.Assemblies.Count; j++)
						{
							ModuleAssembly moduleAssembly = config.Assemblies[j];
							if (moduleAssembly != null)
							{
								if (moduleAssembly.Role == EModuleRole.Both_Required)
								{
									result.Add(module);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00065D68 File Offset: 0x00064168
		public static Module getModuleByName(string name)
		{
			if (ModuleHook.modules == null)
			{
				return null;
			}
			for (int i = 0; i < ModuleHook.modules.Count; i++)
			{
				Module module = ModuleHook.modules[i];
				if (module != null && module.config != null)
				{
					if (module.config.Name == name)
					{
						return module;
					}
				}
			}
			return null;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00065DD8 File Offset: 0x000641D8
		public static void toggleModuleEnabled(int index)
		{
			if (index < 0 || index >= ModuleHook.modules.Count)
			{
				return;
			}
			Module module = ModuleHook.modules[index];
			ModuleConfig config = module.config;
			config.IsEnabled = !config.IsEnabled;
			IOUtility.jsonSerializer.serialize<ModuleConfig>(module.config, config.FilePath, true);
			ModuleHook.updateModuleEnabled(index, config.IsEnabled);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00065E44 File Offset: 0x00064244
		public static void registerAssemblyPath(string path)
		{
			AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);
			if (!ModuleHook.nameToPath.ContainsKey(assemblyName.FullName))
			{
				ModuleHook.nameToPath.Add(assemblyName.FullName, path);
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00065E80 File Offset: 0x00064280
		public static Assembly resolveAssemblyName(string name)
		{
			Assembly assembly;
			if (ModuleHook.nameToAssembly.TryGetValue(name, out assembly))
			{
				return assembly;
			}
			string path;
			if (ModuleHook.nameToPath.TryGetValue(name, out path))
			{
				assembly = Assembly.LoadFile(path);
				ModuleHook.nameToAssembly.Add(name, assembly);
				return assembly;
			}
			return null;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00065ECC File Offset: 0x000642CC
		public static Assembly resolveAssemblyPath(string path)
		{
			AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);
			return ModuleHook.resolveAssemblyName(assemblyName.FullName);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00065EEC File Offset: 0x000642EC
		protected Assembly handleAssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly assembly = ModuleHook.resolveAssemblyName(args.Name);
			if (assembly == null)
			{
				Debug.LogError("Unable to resolve dependency \"" + args.Name + "\"! Include it in one of your module assembly lists.");
			}
			return assembly;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00065F28 File Offset: 0x00064328
		private static bool areModuleDependenciesEnabled(int moduleIndex)
		{
			Module module = ModuleHook.modules[moduleIndex];
			ModuleConfig config = module.config;
			for (int i = 0; i < config.Dependencies.Count; i++)
			{
				ModuleDependency moduleDependency = config.Dependencies[i];
				int index = moduleIndex - 1;
				while (moduleIndex >= 0)
				{
					if (ModuleHook.modules[index].config.Name == moduleDependency.Name && !ModuleHook.modules[index].isEnabled)
					{
						return false;
					}
					moduleIndex--;
				}
			}
			return true;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00065FC8 File Offset: 0x000643C8
		private static void updateModuleEnabled(int index, bool enable)
		{
			if (enable)
			{
				if (ModuleHook.modules[index].config.IsEnabled && ModuleHook.areModuleDependenciesEnabled(index) && !ModuleHook.isModuleDisabledByCommandLine(ModuleHook.modules[index].config.Name))
				{
					ModuleHook.modules[index].isEnabled = true;
					for (int i = index + 1; i < ModuleHook.modules.Count; i++)
					{
						for (int j = 0; j < ModuleHook.modules[i].config.Dependencies.Count; j++)
						{
							ModuleDependency moduleDependency = ModuleHook.modules[i].config.Dependencies[j];
							if (moduleDependency.Name == ModuleHook.modules[index].config.Name)
							{
								ModuleHook.updateModuleEnabled(i, true);
								break;
							}
						}
					}
				}
			}
			else
			{
				for (int k = ModuleHook.modules.Count - 1; k > index; k--)
				{
					for (int l = 0; l < ModuleHook.modules[k].config.Dependencies.Count; l++)
					{
						ModuleDependency moduleDependency2 = ModuleHook.modules[k].config.Dependencies[l];
						if (moduleDependency2.Name == ModuleHook.modules[index].config.Name)
						{
							ModuleHook.updateModuleEnabled(k, false);
							break;
						}
					}
				}
				ModuleHook.modules[index].isEnabled = false;
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00066178 File Offset: 0x00064578
		private string getModulesRootPath()
		{
			string text = new DirectoryInfo(Application.dataPath).Parent.ToString();
			text += "/Modules";
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000661BC File Offset: 0x000645BC
		private List<ModuleConfig> findModules()
		{
			List<ModuleConfig> list = new List<ModuleConfig>();
			string modulesRootPath = this.getModulesRootPath();
			this.findModules(modulesRootPath, list);
			return list;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000661E0 File Offset: 0x000645E0
		private void findModules(string path, List<ModuleConfig> configs)
		{
			foreach (string text in Directory.GetFiles(path, "*.module"))
			{
				ModuleConfig moduleConfig = IOUtility.jsonDeserializer.deserialize<ModuleConfig>(text);
				if (moduleConfig != null)
				{
					moduleConfig.DirectoryPath = path;
					moduleConfig.FilePath = text;
					moduleConfig.Version_Internal = Parser.getUInt32FromIP(moduleConfig.Version);
					for (int j = 0; j < moduleConfig.Dependencies.Count; j++)
					{
						ModuleDependency moduleDependency = moduleConfig.Dependencies[j];
						moduleDependency.Version_Internal = Parser.getUInt32FromIP(moduleDependency.Version);
					}
					configs.Add(moduleConfig);
				}
			}
			foreach (string path2 in Directory.GetDirectories(path))
			{
				this.findModules(path2, configs);
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000662C4 File Offset: 0x000646C4
		private void sortModules(List<ModuleConfig> configs)
		{
			ModuleComparer comparer = new ModuleComparer();
			configs.Sort(comparer);
			for (int i = 0; i < configs.Count; i++)
			{
				ModuleConfig moduleConfig = configs[i];
				bool flag = true;
				for (int j = moduleConfig.Assemblies.Count - 1; j >= 0; j--)
				{
					ModuleAssembly moduleAssembly = moduleConfig.Assemblies[j];
					if (moduleAssembly.Role == EModuleRole.Client && Dedicator.isDedicated)
					{
						moduleConfig.Assemblies.RemoveAt(j);
					}
					else if (moduleAssembly.Role == EModuleRole.Server && !Dedicator.isDedicated)
					{
						moduleConfig.Assemblies.RemoveAt(j);
					}
					else
					{
						bool flag2 = false;
						for (int k = 1; k < moduleAssembly.Path.Length; k++)
						{
							if (moduleAssembly.Path[k] == '.' && moduleAssembly.Path[k - 1] == '.')
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							flag = false;
							break;
						}
						string path = moduleConfig.DirectoryPath + moduleAssembly.Path;
						if (!File.Exists(path))
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag || moduleConfig.Assemblies.Count == 0)
				{
					configs.RemoveAt(i);
					i--;
				}
				else
				{
					for (int l = 0; l < moduleConfig.Dependencies.Count; l++)
					{
						ModuleDependency moduleDependency = moduleConfig.Dependencies[l];
						bool flag3 = false;
						for (int m = i - 1; m >= 0; m--)
						{
							if (configs[m].Name == moduleDependency.Name)
							{
								if (configs[m].Version_Internal >= moduleDependency.Version_Internal)
								{
									flag3 = true;
								}
								break;
							}
						}
						if (!flag3)
						{
							configs.RemoveAtFast(i);
							i--;
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x000664D8 File Offset: 0x000648D8
		private void loadModules()
		{
			ModuleHook.modules = new List<Module>();
			ModuleHook.nameToPath = new Dictionary<string, string>();
			ModuleHook.nameToAssembly = new Dictionary<string, Assembly>();
			List<ModuleConfig> list = this.findModules();
			this.sortModules(list);
			for (int i = 0; i < list.Count; i++)
			{
				ModuleConfig moduleConfig = list[i];
				if (moduleConfig != null)
				{
					Module item = new Module(moduleConfig);
					ModuleHook.modules.Add(item);
				}
			}
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00066550 File Offset: 0x00064950
		private static bool isModuleDisabledByCommandLine(string moduleName)
		{
			string commandLine = Environment.CommandLine;
			int num = commandLine.IndexOf(moduleName, StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				return false;
			}
			string text = "-disableModule/";
			int num2 = num - text.Length;
			return num2 >= 0 && commandLine.Substring(num2, text.Length) == text;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x000665A8 File Offset: 0x000649A8
		private void initializeModules()
		{
			if (ModuleHook.modules == null)
			{
				return;
			}
			for (int i = 0; i < ModuleHook.modules.Count; i++)
			{
				Module module = ModuleHook.modules[i];
				ModuleConfig config = module.config;
				module.isEnabled = (config.IsEnabled && ModuleHook.areModuleDependenciesEnabled(i) && !ModuleHook.isModuleDisabledByCommandLine(config.Name));
			}
			if (ModuleHook.onModulesInitialized != null)
			{
				ModuleHook.onModulesInitialized();
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00066630 File Offset: 0x00064A30
		private void shutdownModules()
		{
			if (ModuleHook.modules == null)
			{
				return;
			}
			for (int i = ModuleHook.modules.Count - 1; i >= 0; i--)
			{
				Module module = ModuleHook.modules[i];
				if (module != null)
				{
					module.isEnabled = false;
				}
			}
			if (ModuleHook.onModulesShutdown != null)
			{
				ModuleHook.onModulesShutdown();
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00066698 File Offset: 0x00064A98
		public void awake()
		{
			AppDomain.CurrentDomain.AssemblyResolve += this.handleAssemblyResolve;
			ModuleHook.coreAssembly = Assembly.GetExecutingAssembly();
			try
			{
				ModuleHook.coreTypes = ModuleHook.coreAssembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				ModuleHook.coreTypes = ex.Types;
			}
			this.loadModules();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00066700 File Offset: 0x00064B00
		public void start()
		{
			ModuleHook.coreNexii = new List<IModuleNexus>();
			ModuleHook.coreNexii.Clear();
			Type typeFromHandle = typeof(IModuleNexus);
			for (int i = 0; i < ModuleHook.coreTypes.Length; i++)
			{
				Type type = ModuleHook.coreTypes[i];
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
					ModuleHook.coreNexii.Add(moduleNexus);
				}
			}
			this.initializeModules();
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x000667B4 File Offset: 0x00064BB4
		private void OnDestroy()
		{
			this.shutdownModules();
			for (int i = 0; i < ModuleHook.coreNexii.Count; i++)
			{
				try
				{
					ModuleHook.coreNexii[i].shutdown();
				}
				catch (Exception exception)
				{
					Debug.LogError("Failed to shutdown nexus!");
					Debug.LogException(exception);
				}
			}
			ModuleHook.coreNexii.Clear();
			AppDomain.CurrentDomain.AssemblyResolve -= this.handleAssemblyResolve;
		}

		// Token: 0x04000963 RID: 2403
		private static List<IModuleNexus> coreNexii;

		// Token: 0x04000966 RID: 2406
		protected static Dictionary<string, string> nameToPath;

		// Token: 0x04000967 RID: 2407
		protected static Dictionary<string, Assembly> nameToAssembly;
	}
}
