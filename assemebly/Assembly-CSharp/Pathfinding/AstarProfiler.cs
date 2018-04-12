using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000EF RID: 239
	public class AstarProfiler
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0004B00B File Offset: 0x0004940B
		private AstarProfiler()
		{
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0004B014 File Offset: 0x00049414
		[Conditional("ProfileAstar")]
		public static void InitializeFastProfile(string[] profileNames)
		{
			AstarProfiler.fastProfileNames = new string[profileNames.Length + 2];
			Array.Copy(profileNames, AstarProfiler.fastProfileNames, profileNames.Length);
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 2] = "__Control1__";
			AstarProfiler.fastProfileNames[AstarProfiler.fastProfileNames.Length - 1] = "__Control2__";
			AstarProfiler.fastProfiles = new AstarProfiler.ProfilePoint[AstarProfiler.fastProfileNames.Length];
			for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
			{
				AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0004B09B File Offset: 0x0004949B
		[Conditional("ProfileAstar")]
		public static void StartFastProfile(int tag)
		{
			AstarProfiler.fastProfiles[tag].watch.Start();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0004B0B0 File Offset: 0x000494B0
		[Conditional("ProfileAstar")]
		public static void EndFastProfile(int tag)
		{
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0004B0DE File Offset: 0x000494DE
		[Conditional("UNITY_PRO_PROFILER")]
		public static void EndProfile()
		{
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0004B0E0 File Offset: 0x000494E0
		[Conditional("ProfileAstar")]
		public static void StartProfile(string tag)
		{
			AstarProfiler.ProfilePoint profilePoint;
			AstarProfiler.profiles.TryGetValue(tag, out profilePoint);
			if (profilePoint == null)
			{
				profilePoint = new AstarProfiler.ProfilePoint();
				AstarProfiler.profiles[tag] = profilePoint;
			}
			profilePoint.tmpBytes = GC.GetTotalMemory(false);
			profilePoint.watch.Start();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0004B12C File Offset: 0x0004952C
		[Conditional("ProfileAstar")]
		public static void EndProfile(string tag)
		{
			if (!AstarProfiler.profiles.ContainsKey(tag))
			{
				UnityEngine.Debug.LogError("Can only end profiling for a tag which has already been started (tag was " + tag + ")");
				return;
			}
			AstarProfiler.ProfilePoint profilePoint = AstarProfiler.profiles[tag];
			profilePoint.totalCalls++;
			profilePoint.watch.Stop();
			profilePoint.totalBytes += GC.GetTotalMemory(false) - profilePoint.tmpBytes;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0004B1A0 File Offset: 0x000495A0
		[Conditional("ProfileAstar")]
		public static void Reset()
		{
			AstarProfiler.profiles.Clear();
			AstarProfiler.startTime = DateTime.UtcNow;
			if (AstarProfiler.fastProfiles != null)
			{
				for (int i = 0; i < AstarProfiler.fastProfiles.Length; i++)
				{
					AstarProfiler.fastProfiles[i] = new AstarProfiler.ProfilePoint();
				}
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0004B1F0 File Offset: 0x000495F0
		[Conditional("ProfileAstar")]
		public static void PrintFastResults()
		{
			for (int i = 0; i < 1000; i++)
			{
			}
			double num = AstarProfiler.fastProfiles[AstarProfiler.fastProfiles.Length - 2].watch.Elapsed.TotalMilliseconds / 1000.0;
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			stringBuilder.Append("Name\t\t|\tTotal Time\t|\tTotal Calls\t|\tAvg/Call\t|\tBytes");
			for (int j = 0; j < AstarProfiler.fastProfiles.Length; j++)
			{
				string text = AstarProfiler.fastProfileNames[j];
				AstarProfiler.ProfilePoint profilePoint = AstarProfiler.fastProfiles[j];
				int totalCalls = profilePoint.totalCalls;
				double num2 = profilePoint.watch.Elapsed.TotalMilliseconds - num * (double)totalCalls;
				if (totalCalls >= 1)
				{
					stringBuilder.Append("\n").Append(text.PadLeft(10)).Append("|   ");
					stringBuilder.Append(num2.ToString("0.0 ").PadLeft(10)).Append(profilePoint.watch.Elapsed.TotalMilliseconds.ToString("(0.0)").PadLeft(10)).Append("|   ");
					stringBuilder.Append(totalCalls.ToString().PadLeft(10)).Append("|   ");
					stringBuilder.Append((num2 / (double)totalCalls).ToString("0.000").PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0004B3D8 File Offset: 0x000497D8
		[Conditional("ProfileAstar")]
		public static void PrintResults()
		{
			TimeSpan timeSpan = DateTime.UtcNow - AstarProfiler.startTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("============================\n\t\t\t\tProfile results:\n============================\n");
			int num = 5;
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair in AstarProfiler.profiles)
			{
				num = Math.Max(keyValuePair.Key.Length, num);
			}
			stringBuilder.Append(" Name ".PadRight(num)).Append("|").Append(" Total Time\t".PadRight(20)).Append("|").Append(" Total Calls ".PadRight(20)).Append("|").Append(" Avg/Call ".PadRight(20));
			foreach (KeyValuePair<string, AstarProfiler.ProfilePoint> keyValuePair2 in AstarProfiler.profiles)
			{
				double totalMilliseconds = keyValuePair2.Value.watch.Elapsed.TotalMilliseconds;
				int totalCalls = keyValuePair2.Value.totalCalls;
				if (totalCalls >= 1)
				{
					string key = keyValuePair2.Key;
					stringBuilder.Append("\n").Append(key.PadRight(num)).Append("| ");
					stringBuilder.Append(totalMilliseconds.ToString("0.0").PadRight(20)).Append("| ");
					stringBuilder.Append(totalCalls.ToString().PadRight(20)).Append("| ");
					stringBuilder.Append((totalMilliseconds / (double)totalCalls).ToString("0.000").PadRight(20));
					stringBuilder.Append(AstarMath.FormatBytesBinary((int)keyValuePair2.Value.totalBytes).PadLeft(10));
				}
			}
			stringBuilder.Append("\n\n============================\n\t\tTotal runtime: ");
			stringBuilder.Append(timeSpan.TotalSeconds.ToString("F3"));
			stringBuilder.Append(" seconds\n============================");
			UnityEngine.Debug.Log(stringBuilder.ToString());
		}

		// Token: 0x0400068D RID: 1677
		private static Dictionary<string, AstarProfiler.ProfilePoint> profiles = new Dictionary<string, AstarProfiler.ProfilePoint>();

		// Token: 0x0400068E RID: 1678
		private static DateTime startTime = DateTime.UtcNow;

		// Token: 0x0400068F RID: 1679
		public static AstarProfiler.ProfilePoint[] fastProfiles;

		// Token: 0x04000690 RID: 1680
		public static string[] fastProfileNames;

		// Token: 0x020000F0 RID: 240
		public class ProfilePoint
		{
			// Token: 0x04000691 RID: 1681
			public Stopwatch watch = new Stopwatch();

			// Token: 0x04000692 RID: 1682
			public int totalCalls;

			// Token: 0x04000693 RID: 1683
			public long tmpBytes;

			// Token: 0x04000694 RID: 1684
			public long totalBytes;
		}
	}
}
