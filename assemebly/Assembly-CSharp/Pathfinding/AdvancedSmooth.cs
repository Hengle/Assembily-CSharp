using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000C0 RID: 192
	[AddComponentMenu("Pathfinding/Modifiers/Advanced Smooth")]
	[Serializable]
	public class AdvancedSmooth : MonoModifier
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00040B89 File Offset: 0x0003EF89
		public override ModifierData input
		{
			get
			{
				return ModifierData.VectorPath;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00040B8C File Offset: 0x0003EF8C
		public override ModifierData output
		{
			get
			{
				return ModifierData.VectorPath;
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00040B90 File Offset: 0x0003EF90
		public override void Apply(Path p, ModifierData source)
		{
			Vector3[] array = p.vectorPath.ToArray();
			if (array == null || array.Length <= 2)
			{
				return;
			}
			List<Vector3> list = new List<Vector3>();
			list.Add(array[0]);
			AdvancedSmooth.TurnConstructor.turningRadius = this.turningRadius;
			for (int i = 1; i < array.Length - 1; i++)
			{
				List<AdvancedSmooth.Turn> turnList = new List<AdvancedSmooth.Turn>();
				AdvancedSmooth.TurnConstructor.Setup(i, array);
				this.turnConstruct1.Prepare(i, array);
				this.turnConstruct2.Prepare(i, array);
				AdvancedSmooth.TurnConstructor.PostPrepare();
				if (i == 1)
				{
					this.turnConstruct1.PointToTangent(turnList);
					this.turnConstruct2.PointToTangent(turnList);
				}
				else
				{
					this.turnConstruct1.TangentToTangent(turnList);
					this.turnConstruct2.TangentToTangent(turnList);
				}
				this.EvaluatePaths(turnList, list);
				if (i == array.Length - 2)
				{
					this.turnConstruct1.TangentToPoint(turnList);
					this.turnConstruct2.TangentToPoint(turnList);
				}
				this.EvaluatePaths(turnList, list);
			}
			list.Add(array[array.Length - 1]);
			p.vectorPath = list;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00040CAC File Offset: 0x0003F0AC
		private void EvaluatePaths(List<AdvancedSmooth.Turn> turnList, List<Vector3> output)
		{
			turnList.Sort();
			for (int i = 0; i < turnList.Count; i++)
			{
				if (i == 0)
				{
					turnList[i].GetPath(output);
				}
			}
			turnList.Clear();
			if (AdvancedSmooth.TurnConstructor.changedPreviousTangent)
			{
				this.turnConstruct1.OnTangentUpdate();
				this.turnConstruct2.OnTangentUpdate();
			}
		}

		// Token: 0x0400056D RID: 1389
		public float turningRadius = 1f;

		// Token: 0x0400056E RID: 1390
		public AdvancedSmooth.MaxTurn turnConstruct1 = new AdvancedSmooth.MaxTurn();

		// Token: 0x0400056F RID: 1391
		public AdvancedSmooth.ConstantTurn turnConstruct2 = new AdvancedSmooth.ConstantTurn();

		// Token: 0x020000C1 RID: 193
		[Serializable]
		public class MaxTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x00041118 File Offset: 0x0003F518
			public override void OnTangentUpdate()
			{
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.1415926535897931;
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00041198 File Offset: 0x0003F598
			public override void Prepare(int i, Vector3[] vectorPath)
			{
				this.preRightCircleCenter = this.rightCircleCenter;
				this.preLeftCircleCenter = this.leftCircleCenter;
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.preVaRight = this.vaRight;
				this.preVaLeft = this.vaLeft;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.1415926535897931;
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x00041248 File Offset: 0x0003F648
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				this.alfaRightRight = base.Atan2(this.rightCircleCenter - this.preRightCircleCenter);
				this.alfaLeftLeft = base.Atan2(this.leftCircleCenter - this.preLeftCircleCenter);
				this.alfaRightLeft = base.Atan2(this.leftCircleCenter - this.preRightCircleCenter);
				this.alfaLeftRight = base.Atan2(this.rightCircleCenter - this.preLeftCircleCenter);
				double num = (double)(this.leftCircleCenter - this.preRightCircleCenter).magnitude;
				double num2 = (double)(this.rightCircleCenter - this.preLeftCircleCenter).magnitude;
				bool flag = false;
				bool flag2 = false;
				if (num < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag = true;
				}
				if (num2 < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num2 = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag2 = true;
				}
				this.deltaRightLeft = ((!flag) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num)) : 0.0);
				this.deltaLeftRight = ((!flag2) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num2)) : 0.0);
				this.betaRightRight = base.ClockwiseAngle(this.preVaRight, this.alfaRightRight - 1.5707963267948966);
				this.betaRightLeft = base.ClockwiseAngle(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft);
				this.betaLeftRight = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight);
				this.betaLeftLeft = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966);
				this.betaRightRight += base.ClockwiseAngle(this.alfaRightRight - 1.5707963267948966, this.vaRight);
				this.betaRightLeft += base.CounterClockwiseAngle(this.alfaRightLeft + this.deltaRightLeft, this.vaLeft);
				this.betaLeftRight += base.ClockwiseAngle(this.alfaLeftRight - this.deltaLeftRight, this.vaRight);
				this.betaLeftLeft += base.CounterClockwiseAngle(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft);
				this.betaRightRight = base.GetLengthFromAngle(this.betaRightRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaRightLeft = base.GetLengthFromAngle(this.betaRightLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftRight = base.GetLengthFromAngle(this.betaLeftRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftLeft = base.GetLengthFromAngle(this.betaLeftLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				Vector3 a = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 a2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 a3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 a4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 b = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 b2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft + 3.1415926535897931) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				Vector3 b3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight + 3.1415926535897931) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 b4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				this.betaRightRight += (double)(a - b).magnitude;
				this.betaRightLeft += (double)(a2 - b2).magnitude;
				this.betaLeftRight += (double)(a3 - b3).magnitude;
				this.betaLeftLeft += (double)(a4 - b4).magnitude;
				if (flag)
				{
					this.betaRightLeft += 10000000.0;
				}
				if (flag2)
				{
					this.betaLeftRight += 10000000.0;
				}
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightRight, this, 2));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightLeft, this, 3));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftRight, this, 4));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftLeft, this, 5));
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x000417B4 File Offset: 0x0003FBB4
			public override void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				double num = (!flag) ? base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter) : 0.0;
				double num2 = (!flag) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude))) : 0.0;
				this.gammaRight = num + num2;
				double num3 = (!flag) ? base.ClockwiseAngle(this.gammaRight, this.vaRight) : 0.0;
				double num4 = (!flag2) ? base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter) : 0.0;
				double num5 = (!flag2) ? (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude))) : 0.0;
				this.gammaLeft = num4 - num5;
				double num6 = (!flag2) ? base.CounterClockwiseAngle(this.gammaLeft, this.vaLeft) : 0.0;
				if (!flag)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 0));
				}
				if (!flag2)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 1));
				}
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x00041988 File Offset: 0x0003FD88
			public override void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				if (!flag)
				{
					double num = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter);
					double num2 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude));
					this.gammaRight = num - num2;
					double num3 = base.ClockwiseAngle(this.vaRight, this.gammaRight);
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 6));
				}
				if (!flag2)
				{
					double num4 = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter);
					double num5 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude2));
					this.gammaLeft = num4 + num5;
					double num6 = base.CounterClockwiseAngle(this.vaLeft, this.gammaLeft);
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 7));
				}
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x00041AB8 File Offset: 0x0003FEB8
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				switch (turn.id)
				{
				case 0:
					base.AddCircleSegment(this.gammaRight, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 1:
					base.AddCircleSegment(this.gammaLeft, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 2:
					base.AddCircleSegment(this.preVaRight, this.alfaRightRight - 1.5707963267948966, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightRight - 1.5707963267948966, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 3:
					base.AddCircleSegment(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightLeft - this.deltaRightLeft + 3.1415926535897931, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 4:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftRight + this.deltaLeftRight + 3.1415926535897931, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 5:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 6:
					base.AddCircleSegment(this.vaRight, this.gammaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				case 7:
					base.AddCircleSegment(this.vaLeft, this.gammaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					break;
				}
			}

			// Token: 0x04000570 RID: 1392
			private Vector3 preRightCircleCenter = Vector3.zero;

			// Token: 0x04000571 RID: 1393
			private Vector3 preLeftCircleCenter = Vector3.zero;

			// Token: 0x04000572 RID: 1394
			private Vector3 rightCircleCenter;

			// Token: 0x04000573 RID: 1395
			private Vector3 leftCircleCenter;

			// Token: 0x04000574 RID: 1396
			private double vaRight;

			// Token: 0x04000575 RID: 1397
			private double vaLeft;

			// Token: 0x04000576 RID: 1398
			private double preVaLeft;

			// Token: 0x04000577 RID: 1399
			private double preVaRight;

			// Token: 0x04000578 RID: 1400
			private double gammaLeft;

			// Token: 0x04000579 RID: 1401
			private double gammaRight;

			// Token: 0x0400057A RID: 1402
			private double betaRightRight;

			// Token: 0x0400057B RID: 1403
			private double betaRightLeft;

			// Token: 0x0400057C RID: 1404
			private double betaLeftRight;

			// Token: 0x0400057D RID: 1405
			private double betaLeftLeft;

			// Token: 0x0400057E RID: 1406
			private double deltaRightLeft;

			// Token: 0x0400057F RID: 1407
			private double deltaLeftRight;

			// Token: 0x04000580 RID: 1408
			private double alfaRightRight;

			// Token: 0x04000581 RID: 1409
			private double alfaLeftLeft;

			// Token: 0x04000582 RID: 1410
			private double alfaRightLeft;

			// Token: 0x04000583 RID: 1411
			private double alfaLeftRight;
		}

		// Token: 0x020000C2 RID: 194
		[Serializable]
		public class ConstantTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06000688 RID: 1672 RVA: 0x00041CF4 File Offset: 0x000400F4
			public override void Prepare(int i, Vector3[] vectorPath)
			{
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x00041CF8 File Offset: 0x000400F8
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				Vector3 dir = Vector3.Cross(AdvancedSmooth.TurnConstructor.t1, Vector3.up);
				Vector3 vector = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.prev;
				Vector3 start = vector * 0.5f + AdvancedSmooth.TurnConstructor.prev;
				vector = Vector3.Cross(vector, Vector3.up);
				bool flag;
				this.circleCenter = Polygon.IntersectionPointOptimized(AdvancedSmooth.TurnConstructor.prev, dir, start, vector, out flag);
				if (!flag)
				{
					return;
				}
				this.gamma1 = base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.circleCenter);
				this.gamma2 = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.circleCenter);
				this.clockwise = !Polygon.Left(this.circleCenter, AdvancedSmooth.TurnConstructor.prev, AdvancedSmooth.TurnConstructor.prev + AdvancedSmooth.TurnConstructor.t1);
				double num = (!this.clockwise) ? base.CounterClockwiseAngle(this.gamma1, this.gamma2) : base.ClockwiseAngle(this.gamma1, this.gamma2);
				num = base.GetLengthFromAngle(num, (double)(this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				turnList.Add(new AdvancedSmooth.Turn((float)num, this, 0));
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x00041E30 File Offset: 0x00040230
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				base.AddCircleSegment(this.gamma1, this.gamma2, this.clockwise, this.circleCenter, output, (this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				AdvancedSmooth.TurnConstructor.normal = (AdvancedSmooth.TurnConstructor.current - this.circleCenter).normalized;
				AdvancedSmooth.TurnConstructor.t2 = Vector3.Cross(AdvancedSmooth.TurnConstructor.normal, Vector3.up).normalized;
				AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				if (!this.clockwise)
				{
					AdvancedSmooth.TurnConstructor.t2 = -AdvancedSmooth.TurnConstructor.t2;
					AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				}
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = true;
			}

			// Token: 0x04000584 RID: 1412
			private Vector3 circleCenter;

			// Token: 0x04000585 RID: 1413
			private double gamma1;

			// Token: 0x04000586 RID: 1414
			private double gamma2;

			// Token: 0x04000587 RID: 1415
			private bool clockwise;
		}

		// Token: 0x020000C3 RID: 195
		public abstract class TurnConstructor
		{
			// Token: 0x0600068C RID: 1676
			public abstract void Prepare(int i, Vector3[] vectorPath);

			// Token: 0x0600068D RID: 1677 RVA: 0x00040D25 File Offset: 0x0003F125
			public virtual void OnTangentUpdate()
			{
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00040D27 File Offset: 0x0003F127
			public virtual void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x00040D29 File Offset: 0x0003F129
			public virtual void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x00040D2B File Offset: 0x0003F12B
			public virtual void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06000691 RID: 1681
			public abstract void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output);

			// Token: 0x06000692 RID: 1682 RVA: 0x00040D30 File Offset: 0x0003F130
			public static void Setup(int i, Vector3[] vectorPath)
			{
				AdvancedSmooth.TurnConstructor.current = vectorPath[i];
				AdvancedSmooth.TurnConstructor.prev = vectorPath[i - 1];
				AdvancedSmooth.TurnConstructor.next = vectorPath[i + 1];
				AdvancedSmooth.TurnConstructor.prev.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.next.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.t1 = AdvancedSmooth.TurnConstructor.t2;
				AdvancedSmooth.TurnConstructor.t2 = (AdvancedSmooth.TurnConstructor.next - AdvancedSmooth.TurnConstructor.current).normalized - (AdvancedSmooth.TurnConstructor.prev - AdvancedSmooth.TurnConstructor.current).normalized;
				AdvancedSmooth.TurnConstructor.t2 = AdvancedSmooth.TurnConstructor.t2.normalized;
				AdvancedSmooth.TurnConstructor.prevNormal = AdvancedSmooth.TurnConstructor.normal;
				AdvancedSmooth.TurnConstructor.normal = Vector3.Cross(AdvancedSmooth.TurnConstructor.t2, Vector3.up);
				AdvancedSmooth.TurnConstructor.normal = AdvancedSmooth.TurnConstructor.normal.normalized;
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x00040E1A File Offset: 0x0003F21A
			public static void PostPrepare()
			{
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = false;
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x00040E24 File Offset: 0x0003F224
			public void AddCircleSegment(double startAngle, double endAngle, bool clockwise, Vector3 center, List<Vector3> output, float radius)
			{
				double num = 0.062831853071795868;
				if (clockwise)
				{
					while (endAngle > startAngle + 6.2831853071795862)
					{
						endAngle -= 6.2831853071795862;
					}
					while (endAngle < startAngle)
					{
						endAngle += 6.2831853071795862;
					}
				}
				else
				{
					while (endAngle < startAngle - 6.2831853071795862)
					{
						endAngle += 6.2831853071795862;
					}
					while (endAngle > startAngle)
					{
						endAngle -= 6.2831853071795862;
					}
				}
				if (clockwise)
				{
					for (double num2 = startAngle; num2 < endAngle; num2 += num)
					{
						output.Add(this.AngleToVector(num2) * radius + center);
					}
				}
				else
				{
					for (double num3 = startAngle; num3 > endAngle; num3 -= num)
					{
						output.Add(this.AngleToVector(num3) * radius + center);
					}
				}
				output.Add(this.AngleToVector(endAngle) * radius + center);
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x00040F44 File Offset: 0x0003F344
			public void DebugCircleSegment(Vector3 center, double startAngle, double endAngle, double radius, Color color)
			{
				double num = 0.062831853071795868;
				while (endAngle < startAngle)
				{
					endAngle += 6.2831853071795862;
				}
				Vector3 start = this.AngleToVector(startAngle) * (float)radius + center;
				for (double num2 = startAngle + num; num2 < endAngle; num2 += num)
				{
					Debug.DrawLine(start, this.AngleToVector(num2) * (float)radius + center);
				}
				Debug.DrawLine(start, this.AngleToVector(endAngle) * (float)radius + center);
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x00040FD4 File Offset: 0x0003F3D4
			public void DebugCircle(Vector3 center, double radius, Color color)
			{
				double num = 0.062831853071795868;
				Vector3 start = this.AngleToVector(-num) * (float)radius + center;
				for (double num2 = 0.0; num2 < 6.2831853071795862; num2 += num)
				{
					Vector3 vector = this.AngleToVector(num2) * (float)radius + center;
					Debug.DrawLine(start, vector, color);
					start = vector;
				}
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x00041042 File Offset: 0x0003F442
			public double GetLengthFromAngle(double angle, double radius)
			{
				return radius * angle;
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00041047 File Offset: 0x0003F447
			public double ClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(to - from);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x00041052 File Offset: 0x0003F452
			public double CounterClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(from - to);
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0004105D File Offset: 0x0003F45D
			public Vector3 AngleToVector(double a)
			{
				return new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a));
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x00041077 File Offset: 0x0003F477
			public double ToDegrees(double rad)
			{
				return rad * 57.295780181884766;
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00041084 File Offset: 0x0003F484
			public double ClampAngle(double a)
			{
				while (a < 0.0)
				{
					a += 6.2831853071795862;
				}
				while (a > 6.2831853071795862)
				{
					a -= 6.2831853071795862;
				}
				return a;
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x000410D4 File Offset: 0x0003F4D4
			public double Atan2(Vector3 v)
			{
				return Math.Atan2((double)v.z, (double)v.x);
			}

			// Token: 0x04000588 RID: 1416
			public float constantBias;

			// Token: 0x04000589 RID: 1417
			public float factorBias = 1f;

			// Token: 0x0400058A RID: 1418
			public static float turningRadius = 1f;

			// Token: 0x0400058B RID: 1419
			public const double ThreeSixtyRadians = 6.2831853071795862;

			// Token: 0x0400058C RID: 1420
			public static Vector3 prev;

			// Token: 0x0400058D RID: 1421
			public static Vector3 current;

			// Token: 0x0400058E RID: 1422
			public static Vector3 next;

			// Token: 0x0400058F RID: 1423
			public static Vector3 t1;

			// Token: 0x04000590 RID: 1424
			public static Vector3 t2;

			// Token: 0x04000591 RID: 1425
			public static Vector3 normal;

			// Token: 0x04000592 RID: 1426
			public static Vector3 prevNormal;

			// Token: 0x04000593 RID: 1427
			public static bool changedPreviousTangent;
		}

		// Token: 0x020000C4 RID: 196
		public struct Turn : IComparable<AdvancedSmooth.Turn>
		{
			// Token: 0x0600069F RID: 1695 RVA: 0x00041EEB File Offset: 0x000402EB
			public Turn(float length, AdvancedSmooth.TurnConstructor constructor, int id = 0)
			{
				this.length = length;
				this.id = id;
				this.constructor = constructor;
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00041F02 File Offset: 0x00040302
			public float score
			{
				get
				{
					return this.length * this.constructor.factorBias + this.constructor.constantBias;
				}
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x00041F22 File Offset: 0x00040322
			public void GetPath(List<Vector3> output)
			{
				this.constructor.GetPath(this, output);
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00041F36 File Offset: 0x00040336
			public int CompareTo(AdvancedSmooth.Turn t)
			{
				return (t.score <= this.score) ? ((t.score >= this.score) ? 0 : 1) : -1;
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00041F69 File Offset: 0x00040369
			public static bool operator <(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score < rhs.score;
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x00041F7B File Offset: 0x0004037B
			public static bool operator >(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score > rhs.score;
			}

			// Token: 0x04000594 RID: 1428
			public float length;

			// Token: 0x04000595 RID: 1429
			public int id;

			// Token: 0x04000596 RID: 1430
			public AdvancedSmooth.TurnConstructor constructor;
		}
	}
}
