using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000435 RID: 1077
	public class HumanClothing
	{
		// Token: 0x06001DB4 RID: 7604 RVA: 0x0009FFD0 File Offset: 0x0009E3D0
		public HumanClothing()
		{
			this._texture = new Texture2D(128, 128, TextureFormat.RGBA32, true);
			this.texture.name = "Clothing_Albedo";
			this.texture.hideFlags = HideFlags.HideAndDontSave;
			this.texture.filterMode = FilterMode.Point;
			this._emission = new Texture2D(128, 128, TextureFormat.RGBA32, true);
			this.emission.name = "Clothing_Emission";
			this.emission.hideFlags = HideFlags.HideAndDontSave;
			this.emission.filterMode = FilterMode.Point;
			this._metallic = new Texture2D(128, 128, TextureFormat.RGBA32, true);
			this.metallic.name = "Clothing_Metallic";
			this.metallic.hideFlags = HideFlags.HideAndDontSave;
			this.metallic.filterMode = FilterMode.Point;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000A00A3 File Offset: 0x0009E4A3
		public Texture2D texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000A00AB File Offset: 0x0009E4AB
		public Texture2D emission
		{
			get
			{
				return this._emission;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x000A00B3 File Offset: 0x0009E4B3
		public Texture2D metallic
		{
			get
			{
				return this._metallic;
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x000A00BC File Offset: 0x0009E4BC
		public void apply()
		{
			for (int i = 0; i < this.texture.width; i++)
			{
				for (int j = 0; j < this.texture.height; j++)
				{
					this.texture.SetPixel(i, j, this.skin);
				}
			}
			if (this.face != null)
			{
				for (int k = 0; k < this.face.width; k++)
				{
					for (int l = 0; l < this.face.height; l++)
					{
						if (this.face.GetPixel(k, l).a > 0f)
						{
							this.texture.SetPixel(this.texture.width - 32 + k, this.texture.height - 16 + l, this.face.GetPixel(k, l));
						}
					}
				}
			}
			if (this.shirt != null)
			{
				for (int m = 0; m < this.shirt.width; m++)
				{
					for (int n = 0; n < this.shirt.height; n++)
					{
						if (this.shirt.GetPixel(m, n).a > 0f)
						{
							if (this.flipShirt && m < 32 && n > this.texture.height - 32)
							{
								this.texture.SetPixel(31 - m, n, this.shirt.GetPixel(m, n));
							}
							else
							{
								this.texture.SetPixel(m, n, this.shirt.GetPixel(m, n));
							}
						}
					}
				}
			}
			if (this.pants != null)
			{
				for (int num = 0; num < this.pants.width; num++)
				{
					for (int num2 = 0; num2 < this.pants.height; num2++)
					{
						if (this.pants.GetPixel(num, num2).a > 0f)
						{
							this.texture.SetPixel(num, num2, this.pants.GetPixel(num, num2));
						}
					}
				}
			}
			this.texture.Apply();
			for (int num3 = 0; num3 < this.emission.width; num3++)
			{
				for (int num4 = 0; num4 < this.emission.height; num4++)
				{
					this.emission.SetPixel(num3, num4, Color.black);
				}
			}
			if (this.faceEmission != null)
			{
				for (int num5 = 0; num5 < this.faceEmission.width; num5++)
				{
					for (int num6 = 0; num6 < this.faceEmission.height; num6++)
					{
						if (this.faceEmission.GetPixel(num5, num6).a > 0f)
						{
							this.emission.SetPixel(this.emission.width - 32 + num5, this.emission.height - 16 + num6, this.faceEmission.GetPixel(num5, num6));
						}
					}
				}
			}
			if (this.shirtEmission != null)
			{
				for (int num7 = 0; num7 < this.shirtEmission.width; num7++)
				{
					for (int num8 = 0; num8 < this.shirtEmission.height; num8++)
					{
						if (this.shirtEmission.GetPixel(num7, num8).a > 0f)
						{
							if (this.flipShirt && num7 < 32 && num8 > this.texture.height - 32)
							{
								this.emission.SetPixel(31 - num7, num8, this.shirtEmission.GetPixel(num7, num8));
							}
							else
							{
								this.emission.SetPixel(num7, num8, this.shirtEmission.GetPixel(num7, num8));
							}
						}
					}
				}
			}
			if (this.pantsEmission != null)
			{
				for (int num9 = 0; num9 < this.pantsEmission.width; num9++)
				{
					for (int num10 = 0; num10 < this.pantsEmission.height; num10++)
					{
						if (this.pantsEmission.GetPixel(num9, num10).a > 0f)
						{
							this.emission.SetPixel(num9, num10, this.pantsEmission.GetPixel(num9, num10));
						}
					}
				}
			}
			this.emission.Apply();
			for (int num11 = 0; num11 < this.metallic.width; num11++)
			{
				for (int num12 = 0; num12 < this.metallic.height; num12++)
				{
					this.metallic.SetPixel(num11, num12, new Color(0f, 0f, 0f, 0f));
				}
			}
			if (this.faceMetallic != null)
			{
				for (int num13 = 0; num13 < this.faceMetallic.width; num13++)
				{
					for (int num14 = 0; num14 < this.faceMetallic.height; num14++)
					{
						if (this.faceMetallic.GetPixel(num13, num14).a > 0f)
						{
							this.metallic.SetPixel(this.metallic.width - 32 + num13, this.metallic.height - 16 + num14, this.faceMetallic.GetPixel(num13, num14));
						}
					}
				}
			}
			if (this.shirtMetallic != null)
			{
				for (int num15 = 0; num15 < this.shirtMetallic.width; num15++)
				{
					for (int num16 = 0; num16 < this.shirtMetallic.height; num16++)
					{
						if (this.shirtMetallic.GetPixel(num15, num16).a > 0f)
						{
							if (this.flipShirt && num15 < 32 && num16 > this.texture.height - 32)
							{
								this.metallic.SetPixel(31 - num15, num16, this.shirtMetallic.GetPixel(num15, num16));
							}
							else
							{
								this.metallic.SetPixel(num15, num16, this.shirtMetallic.GetPixel(num15, num16));
							}
						}
					}
				}
			}
			if (this.pantsMetallic != null)
			{
				for (int num17 = 0; num17 < this.pantsMetallic.width; num17++)
				{
					for (int num18 = 0; num18 < this.pantsMetallic.height; num18++)
					{
						if (this.pantsMetallic.GetPixel(num17, num18).a > 0f)
						{
							this.metallic.SetPixel(num17, num18, this.pantsMetallic.GetPixel(num17, num18));
						}
					}
				}
			}
			this.metallic.Apply();
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000A0844 File Offset: 0x0009EC44
		public void destroy()
		{
			if (this.texture != null)
			{
				UnityEngine.Object.DestroyImmediate(this.texture);
				this._texture = null;
			}
			if (this.emission != null)
			{
				UnityEngine.Object.DestroyImmediate(this.emission);
				this._emission = null;
			}
			if (this.metallic != null)
			{
				UnityEngine.Object.DestroyImmediate(this.metallic);
				this._metallic = null;
			}
		}

		// Token: 0x040011BD RID: 4541
		protected Texture2D _texture;

		// Token: 0x040011BE RID: 4542
		protected Texture2D _emission;

		// Token: 0x040011BF RID: 4543
		protected Texture2D _metallic;

		// Token: 0x040011C0 RID: 4544
		public Texture2D face;

		// Token: 0x040011C1 RID: 4545
		public Texture2D faceEmission;

		// Token: 0x040011C2 RID: 4546
		public Texture2D faceMetallic;

		// Token: 0x040011C3 RID: 4547
		public Texture2D shirt;

		// Token: 0x040011C4 RID: 4548
		public Texture2D shirtEmission;

		// Token: 0x040011C5 RID: 4549
		public Texture2D shirtMetallic;

		// Token: 0x040011C6 RID: 4550
		public bool flipShirt;

		// Token: 0x040011C7 RID: 4551
		public Texture2D pants;

		// Token: 0x040011C8 RID: 4552
		public Texture2D pantsEmission;

		// Token: 0x040011C9 RID: 4553
		public Texture2D pantsMetallic;

		// Token: 0x040011CA RID: 4554
		public Color skin;
	}
}
