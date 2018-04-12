using System;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;

namespace SDG.Framework.Translations
{
	// Token: 0x0200020A RID: 522
	public class Translator
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0006897C File Offset: 0x00066D7C
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00068984 File Offset: 0x00066D84
		[TerminalCommandProperty("language", "name of language to load translations for", "english")]
		public static string language
		{
			get
			{
				return Translator._language;
			}
			set
			{
				if (Translator.language == value)
				{
					return;
				}
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				string language = Translator.language;
				Translator._language = value.ToLower();
				TerminalUtility.printCommandPass("Set language to: " + Translator.language);
				Translator.triggerLanguageChanged(language, Translator.language);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000FA0 RID: 4000 RVA: 0x000689E0 File Offset: 0x00066DE0
		// (remove) Token: 0x06000FA1 RID: 4001 RVA: 0x00068A14 File Offset: 0x00066E14
		public static event LanguageChangedHandler languageChanged;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000FA2 RID: 4002 RVA: 0x00068A48 File Offset: 0x00066E48
		// (remove) Token: 0x06000FA3 RID: 4003 RVA: 0x00068A7C File Offset: 0x00066E7C
		public static event TranslationRegisteredHandler translationRegistered;

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00068AB0 File Offset: 0x00066EB0
		public static Dictionary<string, Dictionary<string, Translation>> languages
		{
			get
			{
				return Translator._languages;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00068AB7 File Offset: 0x00066EB7
		public static HashSet<TranslationReference> misses
		{
			get
			{
				return Translator._misses;
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00068ABE File Offset: 0x00066EBE
		public static bool isOriginLanguage(string language)
		{
			return !string.IsNullOrEmpty(language) && language.Equals(Translator.ORIGIN_LANGUAGE, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00068AD9 File Offset: 0x00066ED9
		public static bool isCurrentLanguage(string language)
		{
			return !string.IsNullOrEmpty(language) && language.Equals(Translator.language, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00068AF4 File Offset: 0x00066EF4
		public static TranslationLeaf addLeaf(TranslationReference reference)
		{
			if (!reference.isValid)
			{
				return null;
			}
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(Translator.ORIGIN_LANGUAGE, out dictionary))
			{
				return null;
			}
			Translation translation;
			if (!dictionary.TryGetValue(reference.ns, out translation))
			{
				return null;
			}
			return translation.addLeaf(reference.token);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00068B4A File Offset: 0x00066F4A
		public static TranslationLeaf getLeaf(TranslationReference reference)
		{
			return Translator.getLeaf(Translator.language, reference);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00068B58 File Offset: 0x00066F58
		public static TranslationLeaf getLeaf(string language, TranslationReference reference)
		{
			if (string.IsNullOrEmpty(language) || !reference.isValid)
			{
				return null;
			}
			Dictionary<string, Translation> dictionary;
			TranslationLeaf translationLeaf;
			Translation translation;
			if (!Translator.languages.TryGetValue(language, out dictionary))
			{
				translationLeaf = null;
			}
			else if (!dictionary.TryGetValue(reference.ns, out translation))
			{
				translationLeaf = null;
			}
			else
			{
				translationLeaf = translation.getLeaf(reference.token);
			}
			if (translationLeaf != null)
			{
				return translationLeaf;
			}
			if (language == Translator.ORIGIN_LANGUAGE)
			{
				Translator.misses.Add(reference);
				return null;
			}
			return Translator.getLeaf(Translator.ORIGIN_LANGUAGE, reference);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00068BF4 File Offset: 0x00066FF4
		public static string translate(TranslationReference reference)
		{
			TranslationLeaf leaf = Translator.getLeaf(reference);
			if (leaf != null)
			{
				return leaf.text;
			}
			return reference.ToString();
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00068C24 File Offset: 0x00067024
		public static void registerTranslationDirectory(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			if (!Directory.Exists(path))
			{
				return;
			}
			foreach (string path2 in Directory.GetFiles(path, "*.translation"))
			{
				Translator.registerTranslationFile(path2);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00068C74 File Offset: 0x00067074
		public static void registerTranslationFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}
			if (!File.Exists(path))
			{
				return;
			}
			string text = null;
			string text2 = null;
			bool flag = false;
			using (StreamReader streamReader = new StreamReader(path))
			{
				IFormattedFileReader formattedFileReader = new LimitedKeyValueTableReader("Metadata", streamReader);
				formattedFileReader.readKey("Metadata");
				IFormattedFileReader formattedFileReader2 = formattedFileReader.readObject();
				if (formattedFileReader2 != null)
				{
					formattedFileReader2.readKey("Language");
					text = formattedFileReader2.readValue();
					formattedFileReader2.readKey("Namespace");
					text2 = formattedFileReader2.readValue();
					if (text != null && text2 != null)
					{
						text = text.ToLower();
						if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text))
						{
							Dictionary<string, Translation> dictionary;
							if (!Translator.languages.TryGetValue(text, out dictionary))
							{
								dictionary = new Dictionary<string, Translation>();
								Translator.languages.Add(text, dictionary);
							}
							if (!dictionary.ContainsKey(text2))
							{
								Translation value = new Translation(path, text, text2);
								dictionary.Add(text2, value);
								flag = true;
							}
						}
					}
				}
			}
			if (flag)
			{
				Translator.triggerTranslationRegistered(text, text2);
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00068D98 File Offset: 0x00067198
		public static void loadTranslations(string language)
		{
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(language, out dictionary))
			{
				return;
			}
			foreach (KeyValuePair<string, Translation> keyValuePair in dictionary)
			{
				keyValuePair.Value.load();
			}
			Terminal.print("Loaded \"" + language + "\" language", null, "Translations", "<color=#d58718>Translations</color>", true);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00068E28 File Offset: 0x00067228
		public static void loadTranslation(string language, string ns)
		{
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(language, out dictionary))
			{
				return;
			}
			Translation translation;
			if (!dictionary.TryGetValue(ns, out translation))
			{
				return;
			}
			translation.load();
			Terminal.print(string.Concat(new string[]
			{
				"Loaded \"",
				language,
				"\" language \"",
				ns,
				"\" namespace"
			}), null, "Translations", "<color=#d58718>Translations</color>", true);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00068E9C File Offset: 0x0006729C
		public static void unloadTranslations(string language)
		{
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(language, out dictionary))
			{
				return;
			}
			foreach (KeyValuePair<string, Translation> keyValuePair in dictionary)
			{
				keyValuePair.Value.unload();
			}
			Terminal.print("Unloaded \"" + language + "\" language", null, "Translations", "<color=#d58718>Translations</color>", true);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00068F2C File Offset: 0x0006732C
		public static void unloadTranslation(string language, string ns)
		{
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(language, out dictionary))
			{
				return;
			}
			Translation translation;
			if (!dictionary.TryGetValue(ns, out translation))
			{
				return;
			}
			translation.unload();
			Terminal.print(string.Concat(new string[]
			{
				"Unloaded \"",
				language,
				"\" language \"",
				ns,
				"\" namespace"
			}), null, "Translations", "<color=#d58718>Translations</color>", true);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00068F9D File Offset: 0x0006739D
		protected static void triggerLanguageChanged(string oldLanguage, string newLanguage)
		{
			if (Translator.languageChanged != null)
			{
				Translator.languageChanged(oldLanguage, newLanguage);
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00068FB5 File Offset: 0x000673B5
		protected static void triggerTranslationRegistered(string language, string ns)
		{
			if (Translator.translationRegistered != null)
			{
				Translator.translationRegistered(language, ns);
			}
		}

		// Token: 0x04000996 RID: 2454
		public static readonly string ORIGIN_LANGUAGE = "english";

		// Token: 0x04000997 RID: 2455
		protected static string _language = Translator.ORIGIN_LANGUAGE;

		// Token: 0x0400099A RID: 2458
		protected static Dictionary<string, Dictionary<string, Translation>> _languages = new Dictionary<string, Dictionary<string, Translation>>();

		// Token: 0x0400099B RID: 2459
		protected static HashSet<TranslationReference> _misses = new HashSet<TranslationReference>();
	}
}
