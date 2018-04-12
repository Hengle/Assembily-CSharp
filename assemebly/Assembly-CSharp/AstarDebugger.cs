using System;
using System.Runtime.CompilerServices;
using System.Text;
using Pathfinding;
using Pathfinding.Util;
using UnityEngine;

// Token: 0x02000019 RID: 25
[AddComponentMenu("Pathfinding/Debugger")]
[ExecuteInEditMode]
public class AstarDebugger : MonoBehaviour
{
	// Token: 0x0600014A RID: 330 RVA: 0x0000EF20 File Offset: 0x0000D320
	public AstarDebugger()
	{
		AstarDebugger.PathTypeDebug[] array = new AstarDebugger.PathTypeDebug[1];
		int num = 0;
		string name = "ABPath";
		if (AstarDebugger.<>f__mg$cache0 == null)
		{
			AstarDebugger.<>f__mg$cache0 = new Func<int>(PathPool<ABPath>.GetSize);
		}
		Func<int> getSize = AstarDebugger.<>f__mg$cache0;
		if (AstarDebugger.<>f__mg$cache1 == null)
		{
			AstarDebugger.<>f__mg$cache1 = new Func<int>(PathPool<ABPath>.GetTotalCreated);
		}
		array[num] = new AstarDebugger.PathTypeDebug(name, getSize, AstarDebugger.<>f__mg$cache1);
		this.debugTypes = array;
		base..ctor();
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000F008 File Offset: 0x0000D408
	public void Start()
	{
		base.useGUILayout = false;
		this.fpsDrops = new float[this.fpsDropCounterSize];
		if (base.GetComponent<Camera>() != null)
		{
			this.cam = base.GetComponent<Camera>();
		}
		else
		{
			this.cam = Camera.main;
		}
		this.graph = new AstarDebugger.GraphPoint[this.graphBufferSize];
		for (int i = 0; i < this.fpsDrops.Length; i++)
		{
			this.fpsDrops[i] = 1f / Time.deltaTime;
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000F098 File Offset: 0x0000D498
	public void Update()
	{
		if (!this.show || (!Application.isPlaying && !this.showInEditor))
		{
			return;
		}
		int num = GC.CollectionCount(0);
		if (this.lastCollectNum != (float)num)
		{
			this.lastCollectNum = (float)num;
			this.delta = Time.realtimeSinceStartup - this.lastCollect;
			this.lastCollect = Time.realtimeSinceStartup;
			this.lastDeltaTime = Time.deltaTime;
			this.collectAlloc = this.allocMem;
		}
		this.allocMem = (int)GC.GetTotalMemory(false);
		bool flag = this.allocMem < this.peakAlloc;
		this.peakAlloc = (flag ? this.peakAlloc : this.allocMem);
		if (Time.realtimeSinceStartup - this.lastAllocSet > 0.3f || !Application.isPlaying)
		{
			int num2 = this.allocMem - this.lastAllocMemory;
			this.lastAllocMemory = this.allocMem;
			this.lastAllocSet = Time.realtimeSinceStartup;
			this.delayedDeltaTime = Time.deltaTime;
			if (num2 >= 0)
			{
				this.allocRate = num2;
			}
		}
		if (Application.isPlaying)
		{
			this.fpsDrops[Time.frameCount % this.fpsDrops.Length] = ((Time.deltaTime == 0f) ? float.PositiveInfinity : (1f / Time.deltaTime));
			int num3 = Time.frameCount % this.graph.Length;
			this.graph[num3].fps = ((Time.deltaTime >= Mathf.Epsilon) ? (1f / Time.deltaTime) : 0f);
			this.graph[num3].collectEvent = flag;
			this.graph[num3].memory = (float)this.allocMem;
		}
		if (Application.isPlaying && this.cam != null && this.showGraph)
		{
			this.graphWidth = (float)this.cam.pixelWidth * 0.8f;
			float num4 = float.PositiveInfinity;
			float num5 = 0f;
			float num6 = float.PositiveInfinity;
			float num7 = 0f;
			for (int i = 0; i < this.graph.Length; i++)
			{
				num4 = Mathf.Min(this.graph[i].memory, num4);
				num5 = Mathf.Max(this.graph[i].memory, num5);
				num6 = Mathf.Min(this.graph[i].fps, num6);
				num7 = Mathf.Max(this.graph[i].fps, num7);
			}
			int num8 = Time.frameCount % this.graph.Length;
			Matrix4x4 m = Matrix4x4.TRS(new Vector3(((float)this.cam.pixelWidth - this.graphWidth) / 2f, this.graphOffset, 1f), Quaternion.identity, new Vector3(this.graphWidth, this.graphHeight, 1f));
			for (int j = 0; j < this.graph.Length - 1; j++)
			{
				if (j != num8)
				{
					this.DrawGraphLine(j, m, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, AstarMath.MapTo(num4, num5, this.graph[j].memory), AstarMath.MapTo(num4, num5, this.graph[j + 1].memory), Color.blue);
					this.DrawGraphLine(j, m, (float)j / (float)this.graph.Length, (float)(j + 1) / (float)this.graph.Length, AstarMath.MapTo(num6, num7, this.graph[j].fps), AstarMath.MapTo(num6, num7, this.graph[j + 1].fps), Color.green);
				}
			}
		}
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000F48F File Offset: 0x0000D88F
	public void DrawGraphLine(int index, Matrix4x4 m, float x1, float x2, float y1, float y2, Color col)
	{
		Debug.DrawLine(this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x1, y1))), this.cam.ScreenToWorldPoint(m.MultiplyPoint3x4(new Vector3(x2, y2))), col);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000F4D0 File Offset: 0x0000D8D0
	public void Cross(Vector3 p)
	{
		p = this.cam.cameraToWorldMatrix.MultiplyPoint(p);
		Debug.DrawLine(p - Vector3.up * 0.2f, p + Vector3.up * 0.2f, Color.red);
		Debug.DrawLine(p - Vector3.right * 0.2f, p + Vector3.right * 0.2f, Color.red);
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000F55C File Offset: 0x0000D95C
	public void OnGUI()
	{
		if (!this.show || (!Application.isPlaying && !this.showInEditor))
		{
			return;
		}
		if (this.style == null)
		{
			this.style = new GUIStyle();
			this.style.normal.textColor = Color.white;
			this.style.padding = new RectOffset(5, 5, 5, 5);
		}
		if (Time.realtimeSinceStartup - this.lastUpdate > 0.5f || this.cachedText == null || !Application.isPlaying)
		{
			this.lastUpdate = Time.realtimeSinceStartup;
			this.boxRect = new Rect(5f, (float)this.yOffset, 310f, 40f);
			this.text.Length = 0;
			this.text.AppendLine("A* Pathfinding Project Debugger");
			this.text.Append("A* Version: ").Append(AstarPath.Version.ToString());
			if (this.showMemProfile)
			{
				this.boxRect.height = this.boxRect.height + 200f;
				this.text.AppendLine();
				this.text.AppendLine();
				this.text.Append("Currently allocated".PadRight(25));
				this.text.Append(((float)this.allocMem / 1000000f).ToString("0.0 MB"));
				this.text.AppendLine();
				this.text.Append("Peak allocated".PadRight(25));
				this.text.Append(((float)this.peakAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Last collect peak".PadRight(25));
				this.text.Append(((float)this.collectAlloc / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Allocation rate".PadRight(25));
				this.text.Append(((float)this.allocRate / 1000000f).ToString("0.0 MB")).AppendLine();
				this.text.Append("Collection frequency".PadRight(25));
				this.text.Append(this.delta.ToString("0.00"));
				this.text.Append("s\n");
				this.text.Append("Last collect fps".PadRight(25));
				this.text.Append((1f / this.lastDeltaTime).ToString("0.0 fps"));
				this.text.Append(" (");
				this.text.Append(this.lastDeltaTime.ToString("0.000 s"));
				this.text.Append(")");
			}
			if (this.showFPS)
			{
				this.text.AppendLine();
				this.text.AppendLine();
				this.text.Append("FPS".PadRight(25)).Append((1f / this.delayedDeltaTime).ToString("0.0 fps"));
				float num = float.PositiveInfinity;
				for (int i = 0; i < this.fpsDrops.Length; i++)
				{
					if (this.fpsDrops[i] < num)
					{
						num = this.fpsDrops[i];
					}
				}
				this.text.AppendLine();
				this.text.Append(("Lowest fps (last " + this.fpsDrops.Length + ")").PadRight(25)).Append(num.ToString("0.0"));
			}
			if (this.showPathProfile)
			{
				AstarPath active = AstarPath.active;
				this.text.AppendLine();
				if (active == null)
				{
					this.text.Append("\nNo AstarPath Object In The Scene");
				}
				else
				{
					if (ListPool<Vector3>.GetSize() > this.maxVecPool)
					{
						this.maxVecPool = ListPool<Vector3>.GetSize();
					}
					if (ListPool<GraphNode>.GetSize() > this.maxNodePool)
					{
						this.maxNodePool = ListPool<GraphNode>.GetSize();
					}
					this.text.Append("\nPool Sizes (size/total created)");
					for (int j = 0; j < this.debugTypes.Length; j++)
					{
						this.debugTypes[j].Print(this.text);
					}
				}
			}
			this.cachedText = this.text.ToString();
		}
		if (this.font != null)
		{
			this.style.font = this.font;
			this.style.fontSize = this.fontSize;
		}
		this.boxRect.height = this.style.CalcHeight(new GUIContent(this.cachedText), this.boxRect.width);
		GUI.Box(this.boxRect, string.Empty);
		GUI.Label(this.boxRect, this.cachedText, this.style);
		if (this.showGraph)
		{
			float num2 = float.PositiveInfinity;
			float num3 = 0f;
			float num4 = float.PositiveInfinity;
			float num5 = 0f;
			for (int k = 0; k < this.graph.Length; k++)
			{
				num2 = Mathf.Min(this.graph[k].memory, num2);
				num3 = Mathf.Max(this.graph[k].memory, num3);
				num4 = Mathf.Min(this.graph[k].fps, num4);
				num5 = Mathf.Max(this.graph[k].fps, num5);
			}
			GUI.color = Color.blue;
			float num6 = (float)Mathf.RoundToInt(num3 / 100000f);
			GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num2, num3, this.graphOffset, this.graphHeight + this.graphOffset, num6 * 1000f * 100f) - 10f, 100f, 20f), (num6 / 10f).ToString("0.0 MB"));
			num6 = Mathf.Round(num2 / 100000f);
			GUI.Label(new Rect(5f, (float)Screen.height - AstarMath.MapTo(num2, num3, this.graphOffset, this.graphHeight + this.graphOffset, num6 * 1000f * 100f) - 10f, 100f, 20f), (num6 / 10f).ToString("0.0 MB"));
			GUI.color = Color.green;
			num6 = Mathf.Round(num5);
			GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num4, num5, this.graphOffset, this.graphHeight + this.graphOffset, num6) - 10f, 100f, 20f), num6.ToString("0 FPS"));
			num6 = Mathf.Round(num4);
			GUI.Label(new Rect(55f, (float)Screen.height - AstarMath.MapTo(num4, num5, this.graphOffset, this.graphHeight + this.graphOffset, num6) - 10f, 100f, 20f), num6.ToString("0 FPS"));
		}
	}

	// Token: 0x04000107 RID: 263
	public int yOffset = 5;

	// Token: 0x04000108 RID: 264
	public bool show = true;

	// Token: 0x04000109 RID: 265
	public bool showInEditor;

	// Token: 0x0400010A RID: 266
	public bool showFPS;

	// Token: 0x0400010B RID: 267
	public bool showPathProfile;

	// Token: 0x0400010C RID: 268
	public bool showMemProfile;

	// Token: 0x0400010D RID: 269
	public bool showGraph;

	// Token: 0x0400010E RID: 270
	public int graphBufferSize = 200;

	// Token: 0x0400010F RID: 271
	public Font font;

	// Token: 0x04000110 RID: 272
	public int fontSize = 12;

	// Token: 0x04000111 RID: 273
	private StringBuilder text = new StringBuilder();

	// Token: 0x04000112 RID: 274
	private string cachedText;

	// Token: 0x04000113 RID: 275
	private float lastUpdate = -999f;

	// Token: 0x04000114 RID: 276
	private AstarDebugger.GraphPoint[] graph;

	// Token: 0x04000115 RID: 277
	private float delayedDeltaTime = 1f;

	// Token: 0x04000116 RID: 278
	private float lastCollect;

	// Token: 0x04000117 RID: 279
	private float lastCollectNum;

	// Token: 0x04000118 RID: 280
	private float delta;

	// Token: 0x04000119 RID: 281
	private float lastDeltaTime;

	// Token: 0x0400011A RID: 282
	private int allocRate;

	// Token: 0x0400011B RID: 283
	private int lastAllocMemory;

	// Token: 0x0400011C RID: 284
	private float lastAllocSet = -9999f;

	// Token: 0x0400011D RID: 285
	private int allocMem;

	// Token: 0x0400011E RID: 286
	private int collectAlloc;

	// Token: 0x0400011F RID: 287
	private int peakAlloc;

	// Token: 0x04000120 RID: 288
	private int fpsDropCounterSize = 200;

	// Token: 0x04000121 RID: 289
	private float[] fpsDrops;

	// Token: 0x04000122 RID: 290
	private Rect boxRect;

	// Token: 0x04000123 RID: 291
	private GUIStyle style;

	// Token: 0x04000124 RID: 292
	private Camera cam;

	// Token: 0x04000125 RID: 293
	private LineRenderer lineRend;

	// Token: 0x04000126 RID: 294
	private float graphWidth = 100f;

	// Token: 0x04000127 RID: 295
	private float graphHeight = 100f;

	// Token: 0x04000128 RID: 296
	private float graphOffset = 50f;

	// Token: 0x04000129 RID: 297
	private int maxVecPool;

	// Token: 0x0400012A RID: 298
	private int maxNodePool;

	// Token: 0x0400012B RID: 299
	private AstarDebugger.PathTypeDebug[] debugTypes;

	// Token: 0x0400012C RID: 300
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache0;

	// Token: 0x0400012D RID: 301
	[CompilerGenerated]
	private static Func<int> <>f__mg$cache1;

	// Token: 0x0200001A RID: 26
	private struct GraphPoint
	{
		// Token: 0x0400012E RID: 302
		public float fps;

		// Token: 0x0400012F RID: 303
		public float memory;

		// Token: 0x04000130 RID: 304
		public bool collectEvent;
	}

	// Token: 0x0200001B RID: 27
	private struct PathTypeDebug
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000FD05 File Offset: 0x0000E105
		public PathTypeDebug(string name, Func<int> getSize, Func<int> getTotalCreated)
		{
			this.name = name;
			this.getSize = getSize;
			this.getTotalCreated = getTotalCreated;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000FD1C File Offset: 0x0000E11C
		public void Print(StringBuilder text)
		{
			int num = this.getTotalCreated();
			if (num > 0)
			{
				text.Append("\n").Append(("  " + this.name).PadRight(25)).Append(this.getSize()).Append("/").Append(num);
			}
		}

		// Token: 0x04000131 RID: 305
		private string name;

		// Token: 0x04000132 RID: 306
		private Func<int> getSize;

		// Token: 0x04000133 RID: 307
		private Func<int> getTotalCreated;
	}
}
