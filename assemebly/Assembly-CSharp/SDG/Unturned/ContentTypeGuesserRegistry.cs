using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000393 RID: 915
	public class ContentTypeGuesserRegistry
	{
		// Token: 0x060019A8 RID: 6568 RVA: 0x000909A8 File Offset: 0x0008EDA8
		static ContentTypeGuesserRegistry()
		{
			ContentTypeGuesserRegistry.add(".jpg", typeof(Texture2D));
			ContentTypeGuesserRegistry.add(".png", typeof(Texture2D));
			ContentTypeGuesserRegistry.add(".tga", typeof(Texture2D));
			ContentTypeGuesserRegistry.add(".psd", typeof(Texture2D));
			ContentTypeGuesserRegistry.add(".blend", typeof(Mesh));
			ContentTypeGuesserRegistry.add(".fbx", typeof(Mesh));
			ContentTypeGuesserRegistry.add(".mat", typeof(Material));
			ContentTypeGuesserRegistry.add(".prefab", typeof(GameObject));
			ContentTypeGuesserRegistry.add(".mp3", typeof(AudioClip));
			ContentTypeGuesserRegistry.add(".ogg", typeof(AudioClip));
			ContentTypeGuesserRegistry.add(".wav", typeof(AudioClip));
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00090AA4 File Offset: 0x0008EEA4
		public static Type guess(string extension)
		{
			Type result;
			ContentTypeGuesserRegistry.guesses.TryGetValue(extension, out result);
			return result;
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00090AC0 File Offset: 0x0008EEC0
		public static void add(string extension, Type type)
		{
			ContentTypeGuesserRegistry.guesses.Add(extension, type);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00090ACE File Offset: 0x0008EECE
		public static void remove(string extension, Type type)
		{
			ContentTypeGuesserRegistry.guesses.Remove(extension);
		}

		// Token: 0x04000DC2 RID: 3522
		private static Dictionary<string, Type> guesses = new Dictionary<string, Type>();
	}
}
