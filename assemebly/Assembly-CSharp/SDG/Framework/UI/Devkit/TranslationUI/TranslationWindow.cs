using System;
using System.Collections.Generic;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000296 RID: 662
	public class TranslationWindow : Sleek2Window
	{
		// Token: 0x06001378 RID: 4984 RVA: 0x0007BA80 File Offset: 0x00079E80
		public TranslationWindow()
		{
			base.name = "Translation";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Title"));
			base.tab.label.translation.format();
			this.branch = null;
			this.treeMode = TranslationWindow.ETreeMode.ORIGIN;
			this.originButton = new Sleek2ImageTranslatedLabelButton();
			this.originButton.transform.anchorMin = new Vector2(0f, 1f);
			this.originButton.transform.anchorMax = new Vector2(0.25f, 1f);
			this.originButton.transform.pivot = new Vector2(0f, 1f);
			this.originButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.originButton.transform.offsetMax = new Vector2(0f, 0f);
			this.originButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Edit_Origin"));
			this.originButton.label.translation.format(Translator.ORIGIN_LANGUAGE);
			this.originButton.clicked += this.handleOriginButtonClicked;
			base.safePanel.addElement(this.originButton);
			this.translationButton = new Sleek2ImageTranslatedLabelButton();
			this.translationButton.transform.anchorMin = new Vector2(0.25f, 1f);
			this.translationButton.transform.anchorMax = new Vector2(0.5f, 1f);
			this.translationButton.transform.pivot = new Vector2(0f, 1f);
			this.translationButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.translationButton.transform.offsetMax = new Vector2(0f, 0f);
			this.translationButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Edit_Translation"));
			this.translationButton.label.translation.format(Translator.language);
			this.translationButton.clicked += this.handleTranslationButtonClicked;
			base.safePanel.addElement(this.translationButton);
			this.missesButton = new Sleek2ImageTranslatedLabelButton();
			this.missesButton.transform.anchorMin = new Vector2(0.5f, 1f);
			this.missesButton.transform.anchorMax = new Vector2(0.75f, 1f);
			this.missesButton.transform.pivot = new Vector2(0f, 1f);
			this.missesButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.missesButton.transform.offsetMax = new Vector2(0f, 0f);
			this.missesButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Origin_Misses"));
			this.missesButton.label.translation.format(Translator.ORIGIN_LANGUAGE);
			this.missesButton.clicked += this.handleMissesButtonClicked;
			base.safePanel.addElement(this.missesButton);
			this.deltaButton = new Sleek2ImageTranslatedLabelButton();
			this.deltaButton.transform.anchorMin = new Vector2(0.75f, 1f);
			this.deltaButton.transform.anchorMax = new Vector2(1f, 1f);
			this.deltaButton.transform.pivot = new Vector2(0f, 1f);
			this.deltaButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.deltaButton.transform.offsetMax = new Vector2(0f, 0f);
			this.deltaButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Origin_Translation_Delta"));
			this.deltaButton.label.translation.format(Translator.ORIGIN_LANGUAGE, Translator.language);
			this.deltaButton.clicked += this.handleDeltaButtonClicked;
			base.safePanel.addElement(this.deltaButton);
			this.view = new Sleek2Scrollview();
			this.view.transform.reset();
			this.view.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2 - 5));
			this.view.vertical = true;
			this.panel = new Sleek2Element();
			this.panel.name = "Panel";
			this.panel.transform.anchorMin = new Vector2(0f, 1f);
			this.panel.transform.anchorMax = new Vector2(1f, 1f);
			this.panel.transform.pivot = new Vector2(0f, 1f);
			this.panel.transform.sizeDelta = new Vector2(0f, 0f);
			this.view.panel = this.panel;
			base.safePanel.addElement(this.view);
			VerticalLayoutGroup verticalLayoutGroup = this.panel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.spacing = 5f;
			verticalLayoutGroup.childForceExpandWidth = true;
			verticalLayoutGroup.childForceExpandHeight = false;
			ContentSizeFitter contentSizeFitter = this.panel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.treePanel = new Sleek2Element();
			this.treePanel.name = "Origin";
			this.treePanel.transform.reset();
			this.treePanel.transform.pivot = new Vector2(0f, 1f);
			this.panel.addElement(this.treePanel);
			VerticalLayoutGroup verticalLayoutGroup2 = this.treePanel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup2.spacing = 5f;
			verticalLayoutGroup2.childForceExpandWidth = true;
			verticalLayoutGroup2.childForceExpandHeight = false;
			ContentSizeFitter contentSizeFitter2 = this.treePanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter2.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.treeHeader = new Sleek2Element();
			this.treeHeader.transform.anchorMin = new Vector2(0f, 1f);
			this.treeHeader.transform.anchorMax = new Vector2(1f, 1f);
			this.treeHeader.transform.pivot = new Vector2(0f, 1f);
			this.treeHeader.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			this.treeHeader.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			base.safePanel.addElement(this.treeHeader);
			this.backButton = new Sleek2ImageTranslatedLabelButton();
			this.backButton.transform.anchorMin = new Vector2(0f, 0f);
			this.backButton.transform.anchorMax = new Vector2(0.333f, 1f);
			this.backButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.backButton.clicked += this.handleBackButtonClicked;
			this.backButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Tree_Back"));
			this.backButton.label.translation.format();
			this.treeHeader.addElement(this.backButton);
			this.addLeafButton = new Sleek2ImageTranslatedLabelButton();
			this.addLeafButton.transform.anchorMin = new Vector2(0.333f, 0f);
			this.addLeafButton.transform.anchorMax = new Vector2(0.667f, 1f);
			this.addLeafButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.addLeafButton.clicked += this.handleAddLeafButtonClicked;
			this.addLeafButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Tree_Add_Leaf"));
			this.addLeafButton.label.translation.format();
			this.treeHeader.addElement(this.addLeafButton);
			this.addBranchButton = new Sleek2ImageTranslatedLabelButton();
			this.addBranchButton.transform.anchorMin = new Vector2(0.667f, 0f);
			this.addBranchButton.transform.anchorMax = new Vector2(1f, 1f);
			this.addBranchButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.addBranchButton.clicked += this.handleAddBranchButtonClicked;
			this.addBranchButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Tree_Add_Branch"));
			this.addBranchButton.label.translation.format();
			this.treeHeader.addElement(this.addBranchButton);
			this.missesPanel = new Sleek2Element();
			this.missesPanel.name = "Misses";
			this.missesPanel.transform.reset();
			this.missesPanel.transform.pivot = new Vector2(0f, 1f);
			this.panel.addElement(this.missesPanel);
			VerticalLayoutGroup verticalLayoutGroup3 = this.missesPanel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup3.spacing = 5f;
			verticalLayoutGroup3.childForceExpandWidth = true;
			verticalLayoutGroup3.childForceExpandHeight = false;
			ContentSizeFitter contentSizeFitter3 = this.missesPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter3.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter3.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.missesHeader = new Sleek2Element();
			this.missesHeader.transform.anchorMin = new Vector2(0f, 1f);
			this.missesHeader.transform.anchorMax = new Vector2(1f, 1f);
			this.missesHeader.transform.pivot = new Vector2(0f, 1f);
			this.missesHeader.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			this.missesHeader.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			base.safePanel.addElement(this.missesHeader);
			this.missesHeader.isVisible = false;
			this.clearMissesButton = new Sleek2ImageTranslatedLabelButton();
			this.clearMissesButton.transform.anchorMin = new Vector2(0f, 0f);
			this.clearMissesButton.transform.anchorMax = new Vector2(0.5f, 1f);
			this.clearMissesButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.clearMissesButton.clicked += this.handleClearMissesButtonClicked;
			this.clearMissesButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Clear_Misses"));
			this.clearMissesButton.label.translation.format();
			this.missesHeader.addElement(this.clearMissesButton);
			this.refreshMissesButton = new Sleek2ImageTranslatedLabelButton();
			this.refreshMissesButton.transform.anchorMin = new Vector2(0.5f, 0f);
			this.refreshMissesButton.transform.anchorMax = new Vector2(1f, 1f);
			this.refreshMissesButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.refreshMissesButton.clicked += this.handleRefreshMissesButtonClicked;
			this.refreshMissesButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Refresh_Misses"));
			this.refreshMissesButton.label.translation.format();
			this.missesHeader.addElement(this.refreshMissesButton);
			this.deltaPanel = new Sleek2Element();
			this.deltaPanel.name = "Delta";
			this.deltaPanel.transform.reset();
			this.deltaPanel.transform.pivot = new Vector2(0f, 1f);
			this.panel.addElement(this.deltaPanel);
			VerticalLayoutGroup verticalLayoutGroup4 = this.deltaPanel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup4.spacing = 5f;
			verticalLayoutGroup4.childForceExpandWidth = true;
			verticalLayoutGroup4.childForceExpandHeight = false;
			ContentSizeFitter contentSizeFitter4 = this.deltaPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter4.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
			contentSizeFitter4.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.deltaHeader = new Sleek2Element();
			this.deltaHeader.transform.anchorMin = new Vector2(0f, 1f);
			this.deltaHeader.transform.anchorMax = new Vector2(1f, 1f);
			this.deltaHeader.transform.pivot = new Vector2(0f, 1f);
			this.deltaHeader.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			this.deltaHeader.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			base.safePanel.addElement(this.deltaHeader);
			this.deltaHeader.isVisible = false;
			this.refreshDeltaButton = new Sleek2ImageTranslatedLabelButton();
			this.refreshDeltaButton.transform.anchorMin = new Vector2(0f, 0f);
			this.refreshDeltaButton.transform.anchorMax = new Vector2(1f, 1f);
			this.refreshDeltaButton.transform.sizeDelta = new Vector2(0f, 0f);
			this.refreshDeltaButton.clicked += this.handleRefreshDeltaButtonClicked;
			this.refreshDeltaButton.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Translation.Refresh_Delta"));
			this.refreshDeltaButton.label.translation.format();
			this.deltaHeader.addElement(this.refreshDeltaButton);
			this.climbBranch(null);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0007C98C File Offset: 0x0007AD8C
		protected virtual void climbBranch(TranslationBranch newBranch)
		{
			this.branch = newBranch;
			this.treePanel.clearElements();
			if (this.branch == null)
			{
				TranslationWindow.ETreeMode etreeMode = this.treeMode;
				string key;
				if (etreeMode != TranslationWindow.ETreeMode.ORIGIN)
				{
					if (etreeMode != TranslationWindow.ETreeMode.TRANSLATION)
					{
						Debug.LogError("Unknown translation UI tree mode: " + this.treeMode);
						return;
					}
					key = Translator.language;
				}
				else
				{
					key = Translator.ORIGIN_LANGUAGE;
				}
				Dictionary<string, Translation> dictionary;
				Translator.languages.TryGetValue(key, out dictionary);
				foreach (KeyValuePair<string, Translation> keyValuePair in dictionary)
				{
					TranslationNamespaceButton translationNamespaceButton = new TranslationNamespaceButton(keyValuePair.Value);
					translationNamespaceButton.clicked += this.handleNamespaceButtonClicked;
					this.treePanel.addElement(translationNamespaceButton);
				}
			}
			else
			{
				foreach (KeyValuePair<string, TranslationBranch> keyValuePair2 in this.branch.branches)
				{
					TranslationBranch value = keyValuePair2.Value;
					if (value.leaf != null)
					{
						TranslationLeafField element = new TranslationLeafField(value.leaf);
						this.treePanel.addElement(element);
					}
					else
					{
						TranslationBranchButton translationBranchButton = new TranslationBranchButton(value);
						translationBranchButton.climbButton.clicked += this.handleBranchClimbButtonClicked;
						this.treePanel.addElement(translationBranchButton);
					}
				}
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0007CB38 File Offset: 0x0007AF38
		protected virtual void refreshMisses()
		{
			this.missesPanel.clearElements();
			foreach (TranslationReference newReference in Translator.misses)
			{
				TranslationLeafMissField element = new TranslationLeafMissField(newReference);
				this.missesPanel.addElement(element);
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0007CBAC File Offset: 0x0007AFAC
		protected virtual void refreshDelta()
		{
			this.deltaPanel.clearElements();
			Dictionary<string, Translation> dictionary;
			if (!Translator.languages.TryGetValue(Translator.ORIGIN_LANGUAGE, out dictionary))
			{
				return;
			}
			Dictionary<string, Translation> dictionary2;
			if (!Translator.languages.TryGetValue(Translator.language, out dictionary2))
			{
				return;
			}
			foreach (KeyValuePair<string, Translation> keyValuePair in dictionary)
			{
				Translation value = keyValuePair.Value;
				Translation translation;
				if (dictionary2.TryGetValue(value.ns, out translation))
				{
					this.compareTranslationBranches(value.tree, translation.tree);
				}
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0007CC6C File Offset: 0x0007B06C
		protected virtual void compareTranslationBranches(TranslationBranch origin, TranslationBranch translation)
		{
			if (origin == null || translation == null)
			{
				return;
			}
			if (origin.leaf != null)
			{
				if (translation.leaf != null && origin.leaf.version == translation.leaf.version)
				{
					return;
				}
				if (translation.leaf == null)
				{
					translation.addLeaf();
				}
				TranslationLeafUpdateField element = new TranslationLeafUpdateField(origin.leaf, translation.leaf);
				this.deltaPanel.addElement(element);
			}
			else
			{
				foreach (KeyValuePair<string, TranslationBranch> keyValuePair in origin.branches)
				{
					TranslationBranch value = keyValuePair.Value;
					TranslationBranch translationBranch;
					if (!translation.branches.TryGetValue(value.key, out translationBranch))
					{
						translationBranch = translation.addBranch(value.key);
					}
					if (translationBranch.branches == null)
					{
						translationBranch.addBranches();
					}
					this.compareTranslationBranches(value, translationBranch);
				}
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0007CD80 File Offset: 0x0007B180
		protected virtual void changeMode(bool origin, bool miss, bool delta)
		{
			this.treePanel.isVisible = origin;
			this.treeHeader.isVisible = origin;
			this.missesPanel.isVisible = miss;
			this.missesHeader.isVisible = miss;
			this.deltaPanel.isVisible = delta;
			this.deltaHeader.isVisible = delta;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0007CDD5 File Offset: 0x0007B1D5
		protected virtual void handleOriginButtonClicked(Sleek2ImageButton button)
		{
			this.changeMode(true, false, false);
			if (this.treeMode != TranslationWindow.ETreeMode.ORIGIN)
			{
				this.treeMode = TranslationWindow.ETreeMode.ORIGIN;
				this.climbBranch(null);
			}
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0007CDF9 File Offset: 0x0007B1F9
		protected virtual void handleTranslationButtonClicked(Sleek2ImageButton button)
		{
			this.changeMode(true, false, false);
			if (this.treeMode != TranslationWindow.ETreeMode.TRANSLATION)
			{
				this.treeMode = TranslationWindow.ETreeMode.TRANSLATION;
				this.climbBranch(null);
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0007CE1E File Offset: 0x0007B21E
		protected virtual void handleMissesButtonClicked(Sleek2ImageButton button)
		{
			this.changeMode(false, true, false);
			this.refreshMisses();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0007CE2F File Offset: 0x0007B22F
		protected virtual void handleDeltaButtonClicked(Sleek2ImageButton button)
		{
			this.changeMode(false, false, true);
			this.refreshDelta();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0007CE40 File Offset: 0x0007B240
		protected virtual void handleBackButtonClicked(Sleek2ImageButton button)
		{
			if (this.branch == null || this.branch.parentBranch == null)
			{
				this.climbBranch(null);
				return;
			}
			this.climbBranch(this.branch.parentBranch);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0007CE78 File Offset: 0x0007B278
		protected virtual void handleAddLeafButtonClicked(Sleek2ImageButton button)
		{
			if (this.branch == null)
			{
				return;
			}
			if (this.branch.leaf != null)
			{
				return;
			}
			if (this.branch.branches == null)
			{
				this.branch.addBranches();
			}
			TranslationBranch translationBranch = this.branch.addBranch("new leaf");
			translationBranch.addLeaf();
			this.climbBranch(this.branch);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0007CEE0 File Offset: 0x0007B2E0
		protected virtual void handleAddBranchButtonClicked(Sleek2ImageButton button)
		{
			if (this.branch == null)
			{
				return;
			}
			if (this.branch.leaf != null)
			{
				return;
			}
			if (this.branch.branches == null)
			{
				this.branch.addBranches();
			}
			TranslationBranch translationBranch = this.branch.addBranch("new branch");
			translationBranch.addBranches();
			this.climbBranch(this.branch);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0007CF48 File Offset: 0x0007B348
		protected virtual void handleNamespaceButtonClicked(Sleek2ImageButton button)
		{
			TranslationNamespaceButton translationNamespaceButton = button as TranslationNamespaceButton;
			if (translationNamespaceButton == null || translationNamespaceButton.translation == null || translationNamespaceButton.translation.tree == null)
			{
				return;
			}
			this.climbBranch(translationNamespaceButton.translation.tree);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0007CF90 File Offset: 0x0007B390
		protected virtual void handleBranchClimbButtonClicked(Sleek2ImageButton button)
		{
			TranslationBranchButton translationBranchButton = button.parent as TranslationBranchButton;
			if (translationBranchButton == null || translationBranchButton.branch == null)
			{
				return;
			}
			this.climbBranch(translationBranchButton.branch);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0007CFC7 File Offset: 0x0007B3C7
		protected virtual void handleClearMissesButtonClicked(Sleek2ImageButton button)
		{
			Translator.misses.Clear();
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0007CFD3 File Offset: 0x0007B3D3
		protected virtual void handleRefreshMissesButtonClicked(Sleek2ImageButton button)
		{
			this.refreshMisses();
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0007CFDB File Offset: 0x0007B3DB
		protected virtual void handleRefreshDeltaButtonClicked(Sleek2ImageButton button)
		{
			this.refreshDelta();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0007CFE3 File Offset: 0x0007B3E3
		protected override void triggerDestroyed()
		{
			base.triggerDestroyed();
		}

		// Token: 0x04000B1D RID: 2845
		protected Sleek2ImageTranslatedLabelButton originButton;

		// Token: 0x04000B1E RID: 2846
		protected Sleek2ImageTranslatedLabelButton translationButton;

		// Token: 0x04000B1F RID: 2847
		protected Sleek2ImageTranslatedLabelButton missesButton;

		// Token: 0x04000B20 RID: 2848
		protected Sleek2ImageTranslatedLabelButton deltaButton;

		// Token: 0x04000B21 RID: 2849
		protected Sleek2Scrollview view;

		// Token: 0x04000B22 RID: 2850
		protected Sleek2Element panel;

		// Token: 0x04000B23 RID: 2851
		protected Sleek2Element treePanel;

		// Token: 0x04000B24 RID: 2852
		protected Sleek2Element treeHeader;

		// Token: 0x04000B25 RID: 2853
		protected Sleek2ImageTranslatedLabelButton backButton;

		// Token: 0x04000B26 RID: 2854
		protected Sleek2ImageTranslatedLabelButton addLeafButton;

		// Token: 0x04000B27 RID: 2855
		protected Sleek2ImageTranslatedLabelButton addBranchButton;

		// Token: 0x04000B28 RID: 2856
		protected Sleek2Element missesPanel;

		// Token: 0x04000B29 RID: 2857
		protected Sleek2Element missesHeader;

		// Token: 0x04000B2A RID: 2858
		protected Sleek2ImageTranslatedLabelButton clearMissesButton;

		// Token: 0x04000B2B RID: 2859
		protected Sleek2ImageTranslatedLabelButton refreshMissesButton;

		// Token: 0x04000B2C RID: 2860
		protected Sleek2Element deltaPanel;

		// Token: 0x04000B2D RID: 2861
		protected Sleek2Element deltaHeader;

		// Token: 0x04000B2E RID: 2862
		protected Sleek2ImageTranslatedLabelButton refreshDeltaButton;

		// Token: 0x04000B2F RID: 2863
		protected TranslationBranch branch;

		// Token: 0x04000B30 RID: 2864
		protected TranslationWindow.ETreeMode treeMode;

		// Token: 0x02000297 RID: 663
		protected enum ETreeMode
		{
			// Token: 0x04000B32 RID: 2866
			ORIGIN,
			// Token: 0x04000B33 RID: 2867
			TRANSLATION
		}
	}
}
