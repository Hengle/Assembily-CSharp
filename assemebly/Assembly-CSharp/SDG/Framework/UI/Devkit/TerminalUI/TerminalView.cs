using System;
using System.Collections.Generic;
using System.Text;
using SDG.Framework.Debug;
using SDG.Framework.UI.Components;
using SDG.Framework.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TerminalUI
{
	// Token: 0x0200028C RID: 652
	public class TerminalView : MonoBehaviour
	{
		// Token: 0x0600131C RID: 4892 RVA: 0x000799EB File Offset: 0x00077DEB
		private void onFiltersClicked()
		{
			this.separator.bActive = !this.separator.bActive;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00079A06 File Offset: 0x00077E06
		private void onClearAllClicked()
		{
			Terminal.clearAll();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00079A10 File Offset: 0x00077E10
		private void onCommandFieldChanged(string value)
		{
			this.arguments = TerminalUtility.splitArguments(value);
			if (this.arguments.Count == 0)
			{
				this.commands.Clear();
			}
			else if (this.arguments.Count == 1)
			{
				this.commands = TerminalUtility.filterCommands(this.arguments[0]);
			}
			for (int i = 0; i < this.hints.Count; i++)
			{
				TerminalHint terminalHint = this.hints[i];
				terminalHint.isVisible = (i < this.commands.Count);
				if (terminalHint.isVisible)
				{
					if (this.commands.Count == 1 && this.arguments.Count > 1 && this.arguments.Count <= this.commands[i].parameters.Length + 1)
					{
						StringBuilder instance = StringBuilderUtility.instance;
						instance.Append(this.commands[i].parameters[this.arguments.Count - 2].name);
						instance.Append(" - ");
						instance.Append(this.commands[i].parameters[this.arguments.Count - 2].type.Name.ToLower());
						instance.Append("\n");
						instance.Append("<color=#afafaf>");
						instance.Append(this.commands[i].parameters[this.arguments.Count - 2].description);
						if (this.commands[i].parameters[this.arguments.Count - 2].defaultValue != null)
						{
							string value2 = this.commands[i].parameters[this.arguments.Count - 2].defaultValue.ToString().ToLower();
							if (!string.IsNullOrEmpty(value2))
							{
								instance.Append(" [default: ");
								instance.Append(value2);
								instance.Append("]");
							}
						}
						if (this.commands[i].currentValue != null)
						{
							string value3 = this.commands[i].currentValue.Invoke(null, null).ToString().ToLower();
							if (!string.IsNullOrEmpty(value3))
							{
								instance.Append(" [current: ");
								instance.Append(value3);
								instance.Append("]");
							}
						}
						instance.Append("</color>");
						terminalHint.text = instance.ToString();
					}
					else
					{
						StringBuilder instance2 = StringBuilderUtility.instance;
						instance2.Append(this.commands[i].method.command);
						instance2.Insert(Mathf.Clamp(this.arguments[0].Length, 0, this.commands[i].method.command.Length), "</color>");
						instance2.Insert(0, ">");
						instance2.Insert(0, Terminal.highlightColor);
						instance2.Insert(0, "<color=");
						for (int j = 0; j < this.commands[i].parameters.Length; j++)
						{
							instance2.Append(" - ");
							instance2.Append(this.commands[i].parameters[j].type.Name.ToLower());
						}
						instance2.Append("\n");
						instance2.Append("<color=#afafaf>");
						instance2.Append(this.commands[i].method.description);
						instance2.Append("</color>");
						terminalHint.text = instance2.ToString();
					}
				}
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00079DFC File Offset: 0x000781FC
		private void execute()
		{
			string text = this.commandField.text;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			TerminalUtility.execute(text, this.arguments, this.commands);
			this.commandField.text = string.Empty;
			this.commandField.Select();
			this.commandField.ActivateInputField();
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00079E59 File Offset: 0x00078259
		private void onCommandEndEdit(string value)
		{
			this.execute();
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00079E61 File Offset: 0x00078261
		private void onExecuteClicked()
		{
			this.execute();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00079E69 File Offset: 0x00078269
		private void onTerminalFilterChanged(string category, bool value)
		{
			Terminal.toggleCategoryVisibility(category, value);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00079E72 File Offset: 0x00078272
		private void onTerminalFilterCleared(string category)
		{
			Terminal.clearCategory(category);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00079E7C File Offset: 0x0007827C
		private void addCategory(TerminalLogCategory category)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.filterTemplate);
			gameObject.name = "Category";
			gameObject.SetActive(true);
			Toggle component = gameObject.transform.FindChild("Toggle").GetComponent<Toggle>();
			component.isOn = category.isVisible;
			Text component2 = gameObject.transform.FindChild("Label").GetComponent<Text>();
			component2.text = category.displayName;
			Button component3 = gameObject.transform.FindChild("Clear_Button").GetComponent<Button>();
			RectTransform rectTransform = gameObject.transform as RectTransform;
			rectTransform.SetParent(this.filterContainer, false);
			rectTransform.offsetMin = new Vector2(5f, (float)this.filterToggles.Count * -50f - 45f);
			rectTransform.offsetMax = new Vector2(-5f, (float)this.filterToggles.Count * -50f - 5f);
			TerminalFilterToggle terminalFilterToggle = new TerminalFilterToggle(category.internalName, component, component3);
			terminalFilterToggle.onTerminalFilterChanged += this.onTerminalFilterChanged;
			terminalFilterToggle.onTerminalFilterCleared += this.onTerminalFilterCleared;
			this.filterToggles.Add(terminalFilterToggle);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00079FB0 File Offset: 0x000783B0
		private void addMessage(TerminalLogMessage message)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.logTemplate);
			gameObject.name = "Log";
			gameObject.SetActive(true);
			Text component = gameObject.GetComponent<Text>();
			component.text = message.category.displayName + ": " + message.displayText;
			RectTransform rectTransform = gameObject.transform as RectTransform;
			rectTransform.SetParent(this.logContainer, false);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0007A020 File Offset: 0x00078420
		private void refreshCategories()
		{
			for (int i = 0; i < this.filterToggles.Count; i++)
			{
				this.filterToggles[i].onTerminalFilterChanged -= this.onTerminalFilterChanged;
				this.filterToggles[i].onTerminalFilterCleared -= this.onTerminalFilterCleared;
			}
			this.filterToggles.Clear();
			for (int j = 0; j < this.filterContainer.childCount; j++)
			{
				if (!(this.filterContainer.GetChild(j).gameObject == this.filterTemplate))
				{
					UnityEngine.Object.Destroy(this.filterContainer.GetChild(j).gameObject);
				}
			}
			IList<TerminalLogCategory> logs = Terminal.getLogs();
			for (int k = 0; k < logs.Count; k++)
			{
				TerminalLogCategory category = logs[k];
				this.addCategory(category);
			}
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0007A114 File Offset: 0x00078514
		private void refreshMessages()
		{
			for (int i = 0; i < this.logContainer.childCount; i++)
			{
				if (!(this.logContainer.GetChild(i).gameObject == this.logTemplate))
				{
					UnityEngine.Object.Destroy(this.logContainer.GetChild(i).gameObject);
				}
			}
			this.logMessages.Clear();
			IList<TerminalLogCategory> logs = Terminal.getLogs();
			for (int j = 0; j < logs.Count; j++)
			{
				TerminalLogCategory terminalLogCategory = logs[j];
				if (terminalLogCategory.isVisible)
				{
					for (int k = 0; k < terminalLogCategory.messages.Count; k++)
					{
						TerminalLogMessage item = terminalLogCategory.messages[k];
						int num = this.logMessages.BinarySearch(item, this.logMessageTimestampComparer);
						if (num < 0)
						{
							num = ~num;
						}
						this.logMessages.Insert(num, item);
					}
				}
			}
			for (int l = 0; l < this.logMessages.Count; l++)
			{
				TerminalLogMessage message = this.logMessages[l];
				this.addMessage(message);
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0007A250 File Offset: 0x00078650
		private void onCategoryVisibilityChanged(TerminalLogCategory category)
		{
			this.refreshCategories();
			this.refreshMessages();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0007A25E File Offset: 0x0007865E
		private void onCategoriesCleared()
		{
			this.refreshCategories();
			this.refreshMessages();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0007A26C File Offset: 0x0007866C
		private void onCategoryCleared(TerminalLogCategory category)
		{
			if (!category.isVisible)
			{
				return;
			}
			this.refreshMessages();
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0007A280 File Offset: 0x00078680
		private void onCategoryAdded(TerminalLogCategory category)
		{
			this.refreshCategories();
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0007A288 File Offset: 0x00078688
		private void onMessageAdded(TerminalLogMessage message, TerminalLogCategory category)
		{
			if (!category.isVisible)
			{
				return;
			}
			this.addMessage(message);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0007A2A0 File Offset: 0x000786A0
		private void OnEnable()
		{
			this.refreshCategories();
			this.refreshMessages();
			this.filtersButton.onClick.AddListener(new UnityAction(this.onFiltersClicked));
			this.clearAllButton.onClick.AddListener(new UnityAction(this.onClearAllClicked));
			this.commandField.onValueChanged.AddListener(new UnityAction<string>(this.onCommandFieldChanged));
			this.commandField.onEndEdit.AddListener(new UnityAction<string>(this.onCommandEndEdit));
			this.executeButton.onClick.AddListener(new UnityAction(this.onExecuteClicked));
			Terminal.onCategoryVisibilityChanged += this.onCategoryVisibilityChanged;
			Terminal.onCategoriesCleared += this.onCategoriesCleared;
			Terminal.onCategoryCleared += this.onCategoryCleared;
			Terminal.onCategoryAdded += this.onCategoryAdded;
			Terminal.onMessageAdded += this.onMessageAdded;
			this.commandField.Select();
			this.commandField.ActivateInputField();
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0007A3B0 File Offset: 0x000787B0
		private void OnDisable()
		{
			this.filtersButton.onClick.RemoveListener(new UnityAction(this.onFiltersClicked));
			this.clearAllButton.onClick.RemoveListener(new UnityAction(this.onClearAllClicked));
			this.commandField.onValueChanged.RemoveListener(new UnityAction<string>(this.onCommandFieldChanged));
			this.commandField.onEndEdit.RemoveListener(new UnityAction<string>(this.onCommandEndEdit));
			this.executeButton.onClick.RemoveListener(new UnityAction(this.onExecuteClicked));
			Terminal.onCategoryVisibilityChanged -= this.onCategoryVisibilityChanged;
			Terminal.onCategoriesCleared -= this.onCategoriesCleared;
			Terminal.onCategoryCleared -= this.onCategoryCleared;
			Terminal.onCategoryAdded -= this.onCategoryAdded;
			Terminal.onMessageAdded -= this.onMessageAdded;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0007A4A0 File Offset: 0x000788A0
		private void Awake()
		{
			for (int i = 0; i < 8; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.hintTemplate);
				gameObject.name = "Hint";
				gameObject.SetActive(false);
				RectTransform rectTransform = gameObject.transform as RectTransform;
				rectTransform.SetParent(this.hintContainer, false);
				rectTransform.anchoredPosition += new Vector2(0f, (float)i * rectTransform.sizeDelta.y);
				Text component = rectTransform.FindChild("Text").GetComponent<Text>();
				TerminalHint item = new TerminalHint(gameObject, component);
				this.hints.Add(item);
			}
		}

		// Token: 0x04000AF2 RID: 2802
		public RectTransform filterContainer;

		// Token: 0x04000AF3 RID: 2803
		public GameObject filterTemplate;

		// Token: 0x04000AF4 RID: 2804
		public RectTransform logContainer;

		// Token: 0x04000AF5 RID: 2805
		public GameObject logTemplate;

		// Token: 0x04000AF6 RID: 2806
		public RectTransform hintContainer;

		// Token: 0x04000AF7 RID: 2807
		public GameObject hintTemplate;

		// Token: 0x04000AF8 RID: 2808
		public Separator separator;

		// Token: 0x04000AF9 RID: 2809
		public Button filtersButton;

		// Token: 0x04000AFA RID: 2810
		public Button clearAllButton;

		// Token: 0x04000AFB RID: 2811
		public InputField commandField;

		// Token: 0x04000AFC RID: 2812
		public Button executeButton;

		// Token: 0x04000AFD RID: 2813
		private List<TerminalHint> hints = new List<TerminalHint>();

		// Token: 0x04000AFE RID: 2814
		private List<string> arguments;

		// Token: 0x04000AFF RID: 2815
		private List<TerminalCommand> commands = new List<TerminalCommand>();

		// Token: 0x04000B00 RID: 2816
		private List<TerminalFilterToggle> filterToggles = new List<TerminalFilterToggle>();

		// Token: 0x04000B01 RID: 2817
		private List<TerminalLogMessage> logMessages = new List<TerminalLogMessage>();

		// Token: 0x04000B02 RID: 2818
		private TerminalLogMessageTimestampComparer logMessageTimestampComparer = new TerminalLogMessageTimestampComparer();
	}
}
