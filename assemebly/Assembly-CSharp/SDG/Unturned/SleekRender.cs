using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F8 RID: 1784
	public class SleekRender
	{
		// Token: 0x06003305 RID: 13061 RVA: 0x0014B54C File Offset: 0x0014994C
		public static int getScaledFontSize(int originalFontSize)
		{
			return Mathf.CeilToInt((float)originalFontSize * GraphicsSettings.uiTextScale);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x0014B55C File Offset: 0x0014995C
		public static void drawAngledImageTexture(Rect area, Texture texture, float angle, Color color)
		{
			if (texture != null)
			{
				if (!GUI.enabled)
				{
					color.a *= 0.5f;
				}
				GUI.color = color;
				Matrix4x4 matrix = GUI.matrix;
				GUIUtility.RotateAroundPivot(angle, area.center);
				GUI.DrawTexture(area, texture, ScaleMode.StretchToFill);
				GUI.matrix = matrix;
				GUI.color = Color.white;
			}
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x0014B5C4 File Offset: 0x001499C4
		public static void drawImageTexture(Rect area, Texture texture, Color color)
		{
			if (texture != null)
			{
				if (!GUI.enabled)
				{
					color.a *= 0.5f;
				}
				GUI.color = color;
				GUI.DrawTexture(area, texture, ScaleMode.StretchToFill);
				GUI.color = Color.white;
			}
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x0014B612 File Offset: 0x00149A12
		public static void drawImageMaterial(Rect area, Texture texture, Material material)
		{
			if (texture != null)
			{
				Graphics.DrawTexture(area, texture, material);
			}
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x0014B628 File Offset: 0x00149A28
		public static bool drawImageButton(Rect area, Texture texture, Color color)
		{
			if (texture != null)
			{
				if (!GUI.enabled)
				{
					color.a *= 0.5f;
				}
				GUI.color = color;
				GUI.DrawTexture(area, texture, ScaleMode.StretchToFill);
				GUI.color = Color.white;
				return SleekRender.allowInput && Event.current.mousePosition.x > area.xMin && Event.current.mousePosition.y > area.yMin && Event.current.mousePosition.x < area.xMax && Event.current.mousePosition.y < area.yMax && Event.current.type == EventType.MouseDown;
			}
			return false;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x0014B710 File Offset: 0x00149B10
		public static void drawTile(Rect area, Texture texture, Color color)
		{
			if (texture != null)
			{
				if (!GUI.enabled)
				{
					color.a *= 0.5f;
				}
				GUI.color = color;
				float uiLayoutScale = GraphicsSettings.uiLayoutScale;
				GUI.DrawTextureWithTexCoords(area, texture, new Rect(0f, 0f, area.width / (float)texture.width / uiLayoutScale, area.height / (float)texture.height / uiLayoutScale));
				GUI.color = Color.white;
			}
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x0014B794 File Offset: 0x00149B94
		public static bool drawGrid(Rect area, Texture texture, Color color)
		{
			if (texture != null)
			{
				if (!GUI.enabled)
				{
					color.a *= 0.5f;
				}
				GUI.color = color;
				float uiLayoutScale = GraphicsSettings.uiLayoutScale;
				GUI.DrawTextureWithTexCoords(area, texture, new Rect(0f, 0f, area.width / (float)texture.width / uiLayoutScale, area.height / (float)texture.height / uiLayoutScale));
				GUI.color = Color.white;
				return Event.current.type == EventType.MouseDown && (Event.current.mousePosition.x > area.xMin && Event.current.mousePosition.y > area.yMin && Event.current.mousePosition.x < area.xMax && Event.current.mousePosition.y < area.yMax);
			}
			return false;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x0014B8A7 File Offset: 0x00149CA7
		public static bool drawToggle(Rect area, Color color, bool state)
		{
			GUI.backgroundColor = color;
			state = GUI.Toggle(area, state, string.Empty);
			return state;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x0014B8C0 File Offset: 0x00149CC0
		public static bool drawButton(Rect area, Color color)
		{
			if (SleekRender.allowInput)
			{
				GUI.backgroundColor = color;
				return GUI.Button(area, string.Empty);
			}
			SleekRender.drawBox(area, color);
			return false;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x0014B8F4 File Offset: 0x00149CF4
		public static bool drawRepeat(Rect area, Color color)
		{
			if (SleekRender.allowInput)
			{
				GUI.backgroundColor = color;
				return GUI.RepeatButton(area, string.Empty);
			}
			SleekRender.drawBox(area, color);
			return false;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x0014B928 File Offset: 0x00149D28
		public static void drawBox(Rect area, Color color, GUIContent content)
		{
			if (content.tooltip != null && content.tooltip.Length > 0 && area.Contains(Event.current.mousePosition))
			{
				SleekRender.tooltip = color;
			}
			GUI.backgroundColor = color;
			GUI.Box(area, content);
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0014B97A File Offset: 0x00149D7A
		public static void drawBox(Rect area, Color color)
		{
			GUI.backgroundColor = color;
			GUI.Box(area, string.Empty);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x0014B990 File Offset: 0x00149D90
		public static void drawLabel(Rect area, FontStyle fontStyle, TextAnchor fontAlignment, int fontSize, GUIContent content2, Color color, GUIContent content)
		{
			if (content.tooltip != null && content.tooltip.Length > 0 && area.Contains(Event.current.mousePosition))
			{
				SleekRender.tooltip = color;
			}
			GUI.skin.label.fontStyle = fontStyle;
			GUI.skin.label.alignment = fontAlignment;
			GUI.skin.label.fontSize = SleekRender.getScaledFontSize(fontSize);
			bool richText = GUI.skin.label.richText;
			GUI.skin.label.richText = (content2 != null);
			Color outline = SleekRender.OUTLINE;
			outline.a *= color.a;
			GUI.contentColor = outline;
			if (content2 == null)
			{
				area.x -= 1f;
				GUI.Label(area, content);
				area.x += 2f;
				GUI.Label(area, content);
				area.x -= 1f;
				area.y -= 1f;
				GUI.Label(area, content);
				area.y += 2f;
				GUI.Label(area, content);
				area.y -= 1f;
			}
			else
			{
				area.x -= 1f;
				GUI.Label(area, content2);
				area.x += 2f;
				GUI.Label(area, content2);
				area.x -= 1f;
				area.y -= 1f;
				GUI.Label(area, content2);
				area.y += 2f;
				GUI.Label(area, content2);
				area.y -= 1f;
			}
			GUI.contentColor = color;
			GUI.Label(area, content);
			GUI.skin.label.richText = richText;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x0014BBA4 File Offset: 0x00149FA4
		public static void drawLabel(Rect area, FontStyle fontStyle, TextAnchor fontAlignment, int fontSize, bool isRich, Color color, string text)
		{
			GUI.skin.label.fontStyle = fontStyle;
			GUI.skin.label.alignment = fontAlignment;
			GUI.skin.label.fontSize = SleekRender.getScaledFontSize(fontSize);
			if (isRich)
			{
				bool richText = GUI.skin.label.richText;
				GUI.skin.label.richText = isRich;
				GUI.Label(area, text);
				GUI.skin.label.richText = richText;
			}
			else
			{
				Color outline = SleekRender.OUTLINE;
				outline.a *= color.a;
				GUI.contentColor = outline;
				area.x -= 1f;
				GUI.Label(area, text);
				area.x += 2f;
				GUI.Label(area, text);
				area.x -= 1f;
				area.y -= 1f;
				GUI.Label(area, text);
				area.y += 2f;
				GUI.Label(area, text);
				area.y -= 1f;
				GUI.contentColor = color;
				GUI.Label(area, text);
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x0014BCF0 File Offset: 0x0014A0F0
		public static string drawField(Rect area, FontStyle fontStyle, TextAnchor fontAlignment, int fontSize, Color color_0, Color color_1, string text, int maxLength, bool multiline)
		{
			return SleekRender.drawField(area, fontStyle, fontAlignment, fontSize, color_0, color_1, text, maxLength, string.Empty, multiline);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x0014BD18 File Offset: 0x0014A118
		public static string drawField(Rect area, FontStyle fontStyle, TextAnchor fontAlignment, int fontSize, Color color_0, Color color_1, string text, int maxLength, string hint, bool multiline)
		{
			GUI.skin.textArea.fontStyle = fontStyle;
			GUI.skin.textArea.alignment = fontAlignment;
			GUI.skin.textArea.fontSize = SleekRender.getScaledFontSize(fontSize);
			GUI.skin.textField.fontStyle = fontStyle;
			GUI.skin.textField.alignment = fontAlignment;
			GUI.skin.textField.fontSize = SleekRender.getScaledFontSize(fontSize);
			GUI.backgroundColor = color_0;
			GUI.contentColor = color_1;
			if (SleekRender.allowInput)
			{
				if (multiline)
				{
					text = GUI.TextArea(area, text, maxLength);
				}
				else
				{
					text = GUI.TextField(area, text, maxLength);
				}
				if (text == null)
				{
					text = string.Empty;
				}
				if (text.Length < 1)
				{
					SleekRender.drawLabel(area, fontStyle, fontAlignment, fontSize, false, color_1 * 0.5f, hint);
				}
				return text;
			}
			SleekRender.drawBox(area, color_0);
			SleekRender.drawLabel(area, fontStyle, fontAlignment, fontSize, false, color_1, text);
			return text;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x0014BE1C File Offset: 0x0014A21C
		public static string drawField(Rect area, FontStyle fontStyle, TextAnchor fontAlignment, int fontSize, Color color_0, Color color_1, string text, int maxLength, string hint, char replace)
		{
			GUI.skin.textField.fontStyle = fontStyle;
			GUI.skin.textField.alignment = fontAlignment;
			GUI.skin.textField.fontSize = SleekRender.getScaledFontSize(fontSize);
			GUI.backgroundColor = color_0;
			GUI.contentColor = color_1;
			if (SleekRender.allowInput)
			{
				text = GUI.PasswordField(area, text, replace, maxLength);
				if (text == null)
				{
					text = string.Empty;
				}
				if (text.Length < 1)
				{
					SleekRender.drawLabel(area, fontStyle, fontAlignment, fontSize, false, color_1 * 0.5f, hint);
				}
				return text;
			}
			SleekRender.drawBox(area, color_0);
			string text2 = string.Empty;
			for (int i = 0; i < text.Length; i++)
			{
				text2 += replace;
			}
			SleekRender.drawLabel(area, fontStyle, fontAlignment, fontSize, false, color_1, text2);
			return text;
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x0014BEFD File Offset: 0x0014A2FD
		public static float drawSlider(Rect area, ESleekOrientation orientation, float state, float size, Color color)
		{
			GUI.backgroundColor = color;
			if (orientation == ESleekOrientation.HORIZONTAL)
			{
				state = GUI.HorizontalScrollbar(area, state, size, 0f, 1f);
			}
			else
			{
				state = GUI.VerticalScrollbar(area, state, size, 0f, 1f);
			}
			return state;
		}

		// Token: 0x040022AD RID: 8877
		public static readonly int FONT_SIZE = 12;

		// Token: 0x040022AE RID: 8878
		private static readonly Color OUTLINE = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x040022AF RID: 8879
		public static bool allowInput;

		// Token: 0x040022B0 RID: 8880
		public static Color tooltip;
	}
}
