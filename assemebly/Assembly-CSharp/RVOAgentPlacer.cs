﻿using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class RVOAgentPlacer : MonoBehaviour
{
	// Token: 0x060003BB RID: 955 RVA: 0x0001C934 File Offset: 0x0001AD34
	private IEnumerator Start()
	{
		yield return null;
		for (int i = 0; i < this.agents; i++)
		{
			float num = (float)i / (float)this.agents * 3.14159274f * 2f;
			Vector3 vector = new Vector3((float)Math.Cos((double)num), 0f, (float)Math.Sin((double)num)) * this.ringSize;
			Vector3 target = -vector + this.goalOffset;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab, Vector3.zero, Quaternion.Euler(0f, num + 180f, 0f));
			RVOExampleAgent component = gameObject.GetComponent<RVOExampleAgent>();
			if (component == null)
			{
				Debug.LogError("Prefab does not have an RVOExampleAgent component attached");
				yield break;
			}
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = vector;
			component.repathRate = this.repathRate;
			component.SetTarget(target);
			component.SetColor(this.GetColor(num));
		}
		yield break;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001C94F File Offset: 0x0001AD4F
	public Color GetColor(float angle)
	{
		return RVOAgentPlacer.HSVToRGB(angle * 57.2957764f, 0.8f, 0.6f);
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001C968 File Offset: 0x0001AD68
	private static Color HSVToRGB(float h, float s, float v)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = s * v;
		float num5 = h / 60f;
		float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
		if (num5 < 1f)
		{
			num = num4;
			num2 = num6;
		}
		else if (num5 < 2f)
		{
			num = num6;
			num2 = num4;
		}
		else if (num5 < 3f)
		{
			num2 = num4;
			num3 = num6;
		}
		else if (num5 < 4f)
		{
			num2 = num6;
			num3 = num4;
		}
		else if (num5 < 5f)
		{
			num = num6;
			num3 = num4;
		}
		else if (num5 < 6f)
		{
			num = num4;
			num3 = num6;
		}
		float num7 = v - num4;
		num += num7;
		num2 += num7;
		num3 += num7;
		return new Color(num, num2, num3);
	}

	// Token: 0x0400031B RID: 795
	public int agents = 100;

	// Token: 0x0400031C RID: 796
	public float ringSize = 100f;

	// Token: 0x0400031D RID: 797
	public LayerMask mask;

	// Token: 0x0400031E RID: 798
	public GameObject prefab;

	// Token: 0x0400031F RID: 799
	public Vector3 goalOffset;

	// Token: 0x04000320 RID: 800
	public float repathRate = 1f;

	// Token: 0x04000321 RID: 801
	private const float rad2Deg = 57.2957764f;
}
