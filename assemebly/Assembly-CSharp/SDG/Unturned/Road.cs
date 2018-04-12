using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x0200056F RID: 1391
	public class Road
	{
		// Token: 0x06002658 RID: 9816 RVA: 0x000E15E4 File Offset: 0x000DF9E4
		public Road(byte newMaterial, ushort newRoadIndex) : this(newMaterial, newRoadIndex, false, new List<RoadJoint>())
		{
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x000E15F4 File Offset: 0x000DF9F4
		public Road(byte newMaterial, ushort newRoadIndex, bool newLoop, List<RoadJoint> newJoints)
		{
			this.material = newMaterial;
			this.roadIndex = newRoadIndex;
			this._road = new GameObject().transform;
			this.road.name = "Road";
			this.road.parent = LevelRoads.models;
			this.road.tag = "Environment";
			this.road.gameObject.layer = LayerMasks.ENVIRONMENT;
			this._isLoop = newLoop;
			this._joints = newJoints;
			this.samples = new List<RoadSample>();
			this.trackSamples = new List<TrackSample>();
			if (Level.isEditor)
			{
				this.line = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Road"))).transform;
				this.line.name = "Line";
				this.line.parent = LevelRoads.models;
				this._paths = new List<RoadPath>();
				this.lineRenderer = this.line.GetComponent<LineRenderer>();
				for (int i = 0; i < this.joints.Count; i++)
				{
					Transform transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Path"))).transform;
					transform.name = "Path_" + i;
					transform.parent = this.line;
					RoadPath item = new RoadPath(transform);
					this.paths.Add(item);
				}
				if (LevelGround.terrain != null)
				{
					this.updatePoints();
				}
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x000E1775 File Offset: 0x000DFB75
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x000E177D File Offset: 0x000DFB7D
		public ushort roadIndex { get; protected set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x000E1786 File Offset: 0x000DFB86
		public Transform road
		{
			get
			{
				return this._road;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000E178E File Offset: 0x000DFB8E
		// (set) Token: 0x0600265E RID: 9822 RVA: 0x000E1796 File Offset: 0x000DFB96
		public bool isLoop
		{
			get
			{
				return this._isLoop;
			}
			set
			{
				this._isLoop = value;
				this.updatePoints();
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600265F RID: 9823 RVA: 0x000E17A5 File Offset: 0x000DFBA5
		public List<RoadJoint> joints
		{
			get
			{
				return this._joints;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000E17AD File Offset: 0x000DFBAD
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x000E17B5 File Offset: 0x000DFBB5
		public float trackSampledLength { get; protected set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000E17BE File Offset: 0x000DFBBE
		public List<RoadPath> paths
		{
			get
			{
				return this._paths;
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000E17C8 File Offset: 0x000DFBC8
		public void setEnabled(bool isEnabled)
		{
			this.line.gameObject.SetActive(isEnabled);
			for (int i = 0; i < this.paths.Count; i++)
			{
				this.paths[i].vertex.gameObject.SetActive(isEnabled);
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000E1820 File Offset: 0x000DFC20
		public void getTrackData(float trackPosition, out Vector3 position, out Vector3 normal, out Vector3 direction)
		{
			if (this.trackSamples.Count > 1)
			{
				TrackSample trackSample = this.trackSamples[0];
				for (int i = 1; i < this.trackSamples.Count; i++)
				{
					TrackSample trackSample2 = this.trackSamples[i];
					if (trackPosition >= trackSample.distance && trackPosition <= trackSample2.distance)
					{
						float t = (trackPosition - trackSample.distance) / (trackSample2.distance - trackSample.distance);
						position = Vector3.Lerp(trackSample.position, trackSample2.position, t);
						normal = Vector3.Lerp(trackSample.normal, trackSample2.normal, t);
						direction = Vector3.Lerp(trackSample.direction, trackSample2.direction, t);
						return;
					}
					trackSample = trackSample2;
				}
				if (this.isLoop)
				{
					TrackSample trackSample3 = this.trackSamples[0];
					if (trackSample != trackSample3)
					{
						float t2 = (trackPosition - trackSample.distance) / (this.trackSampledLength - trackSample.distance);
						position = Vector3.Lerp(trackSample.position, trackSample3.position, t2);
						normal = Vector3.Lerp(trackSample.normal, trackSample3.normal, t2);
						direction = Vector3.Lerp(trackSample.direction, trackSample3.direction, t2);
						return;
					}
				}
			}
			position = Vector3.zero;
			normal = Vector3.up;
			direction = Vector3.forward;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000E199C File Offset: 0x000DFD9C
		public void getTrackPosition(int index, float t, out Vector3 position, out Vector3 normal)
		{
			position = this.getPosition(index, t);
			normal = Vector3.up;
			if (!this.joints[index].ignoreTerrain)
			{
				position.y = LevelGround.getHeight(position);
				normal = LevelGround.getNormal(position);
			}
			position += normal * (LevelRoads.materials[(int)this.material].depth + LevelRoads.materials[(int)this.material].offset);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000E1A3C File Offset: 0x000DFE3C
		public void getTrackPosition(float t, out int index, out Vector3 position, out Vector3 normal)
		{
			position = this.getPosition(t, out index);
			normal = Vector3.up;
			if (!this.joints[index].ignoreTerrain)
			{
				position.y = LevelGround.getHeight(position);
				normal = LevelGround.getNormal(position);
			}
			position += normal * (LevelRoads.materials[(int)this.material].depth + LevelRoads.materials[(int)this.material].offset);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000E1AE0 File Offset: 0x000DFEE0
		public Vector3 getPosition(float t)
		{
			int num;
			return this.getPosition(t, out num);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000E1AF8 File Offset: 0x000DFEF8
		public Vector3 getPosition(float t, out int index)
		{
			if (this.isLoop)
			{
				index = (int)(t * (float)this.joints.Count);
				t = t * (float)this.joints.Count - (float)index;
				return this.getPosition(index, t);
			}
			index = (int)(t * (float)(this.joints.Count - 1));
			t = t * (float)(this.joints.Count - 1) - (float)index;
			return this.getPosition(index, t);
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000E1B74 File Offset: 0x000DFF74
		public Vector3 getPosition(int index, float t)
		{
			index = Mathf.Clamp(index, 0, this.joints.Count - 1);
			t = Mathf.Clamp01(t);
			RoadJoint roadJoint = this.joints[index];
			RoadJoint roadJoint2;
			if (index == this.joints.Count - 1)
			{
				roadJoint2 = this.joints[0];
			}
			else
			{
				roadJoint2 = this.joints[index + 1];
			}
			Vector3 tangent = roadJoint.getTangent(1);
			Vector3 tangent2 = roadJoint2.getTangent(0);
			if (Vector3.Dot(tangent.normalized, tangent2.normalized) < -0.999f)
			{
				return Vector3.Lerp(roadJoint.vertex, roadJoint2.vertex, t);
			}
			return BezierTool.getPosition(roadJoint.vertex, roadJoint.vertex + tangent, roadJoint2.vertex + tangent2, roadJoint2.vertex, t);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000E1C4C File Offset: 0x000E004C
		public Vector3 getVelocity(float t)
		{
			if (this.isLoop)
			{
				int num = (int)(t * (float)this.joints.Count);
				t = t * (float)this.joints.Count - (float)num;
				return this.getVelocity(num, t);
			}
			int num2 = (int)(t * (float)(this.joints.Count - 1));
			t = t * (float)(this.joints.Count - 1) - (float)num2;
			return this.getVelocity(num2, t);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000E1CC0 File Offset: 0x000E00C0
		public Vector3 getVelocity(int index, float t)
		{
			index = Mathf.Clamp(index, 0, this.joints.Count - 1);
			t = Mathf.Clamp01(t);
			RoadJoint roadJoint = this.joints[index];
			RoadJoint roadJoint2;
			if (index == this.joints.Count - 1)
			{
				roadJoint2 = this.joints[0];
			}
			else
			{
				roadJoint2 = this.joints[index + 1];
			}
			return BezierTool.getVelocity(roadJoint.vertex, roadJoint.vertex + roadJoint.getTangent(1), roadJoint2.vertex + roadJoint2.getTangent(0), roadJoint2.vertex, t);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x000E1D64 File Offset: 0x000E0164
		public float getLengthEstimate()
		{
			double num = 0.0;
			for (int i = 0; i < this.joints.Count - 1 + ((!this.isLoop) ? 0 : 1); i++)
			{
				num += (double)this.getLengthEstimate(i);
			}
			return (float)num;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000E1DBC File Offset: 0x000E01BC
		public float getLengthEstimate(int index)
		{
			index = Mathf.Clamp(index, 0, this.joints.Count - 1);
			RoadJoint roadJoint = this.joints[index];
			RoadJoint roadJoint2;
			if (index == this.joints.Count - 1)
			{
				roadJoint2 = this.joints[0];
			}
			else
			{
				roadJoint2 = this.joints[index + 1];
			}
			Vector3 tangent = roadJoint.getTangent(1);
			Vector3 tangent2 = roadJoint2.getTangent(0);
			if (Vector3.Dot(tangent.normalized, tangent2.normalized) < -0.999f)
			{
				return (roadJoint2.vertex - roadJoint.vertex).magnitude;
			}
			return BezierTool.getLengthEstimate(roadJoint.vertex, roadJoint.vertex + tangent, roadJoint2.vertex + tangent2, roadJoint2.vertex);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x000E1E94 File Offset: 0x000E0294
		[Obsolete]
		public Transform addPoint(Transform origin, Vector3 point)
		{
			RoadJoint roadJoint = new RoadJoint(point);
			if (origin == null || origin == this.paths[this.paths.Count - 1].vertex)
			{
				if (this.joints.Count > 0)
				{
					roadJoint.setTangent(0, (this.joints[this.joints.Count - 1].vertex - point).normalized * 2.5f);
				}
				this.joints.Add(roadJoint);
				Transform transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Path"))).transform;
				transform.name = "Path_" + (this.joints.Count - 1);
				transform.parent = this.line;
				RoadPath roadPath = new RoadPath(transform);
				this.paths.Add(roadPath);
				this.updatePoints();
				return roadPath.vertex;
			}
			if (origin == this.paths[0].vertex)
			{
				for (int i = 0; i < this.joints.Count; i++)
				{
					this.paths[i].vertex.name = "Path_" + (i + 1);
				}
				if (this.joints.Count > 0)
				{
					roadJoint.setTangent(1, (this.joints[0].vertex - point).normalized * 2.5f);
				}
				this.joints.Insert(0, roadJoint);
				Transform transform2 = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Path"))).transform;
				transform2.name = "Path_0";
				transform2.parent = this.line;
				RoadPath roadPath2 = new RoadPath(transform2);
				this.paths.Insert(0, roadPath2);
				this.updatePoints();
				return roadPath2.vertex;
			}
			return null;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x000E20AC File Offset: 0x000E04AC
		public Transform addVertex(int vertexIndex, Vector3 point)
		{
			RoadJoint roadJoint = new RoadJoint(point);
			for (int i = vertexIndex; i < this.joints.Count; i++)
			{
				this.paths[i].vertex.name = "Path_" + (i + 1);
			}
			if (this.joints.Count == 1)
			{
				this.joints[0].setTangent(1, (point - this.joints[0].vertex).normalized * 2.5f);
				roadJoint.setTangent(0, (this.joints[0].vertex - point).normalized * 2.5f);
			}
			else if (this.joints.Count > 1)
			{
				if (vertexIndex == 0)
				{
					if (this.isLoop)
					{
						RoadJoint roadJoint2 = this.joints[this.joints.Count - 1];
						RoadJoint roadJoint3 = this.joints[0];
						roadJoint.setTangent(1, (roadJoint3.vertex - roadJoint2.vertex).normalized * 2.5f);
					}
					else
					{
						roadJoint.setTangent(1, (this.joints[0].vertex - point).normalized * 2.5f);
					}
				}
				else if (vertexIndex == this.joints.Count)
				{
					if (this.isLoop)
					{
						RoadJoint roadJoint4 = this.joints[this.joints.Count - 1];
						RoadJoint roadJoint5 = this.joints[0];
						roadJoint.setTangent(1, (roadJoint5.vertex - roadJoint4.vertex).normalized * 2.5f);
					}
					else
					{
						roadJoint.setTangent(0, (this.joints[this.joints.Count - 1].vertex - point).normalized * 2.5f);
					}
				}
				else
				{
					RoadJoint roadJoint6 = this.joints[vertexIndex - 1];
					RoadJoint roadJoint7 = this.joints[vertexIndex];
					roadJoint.setTangent(1, (roadJoint7.vertex - roadJoint6.vertex).normalized * 2.5f);
				}
			}
			this.joints.Insert(vertexIndex, roadJoint);
			Transform transform = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Path"))).transform;
			transform.name = "Path_" + vertexIndex;
			transform.parent = this.line;
			RoadPath roadPath = new RoadPath(transform);
			this.paths.Insert(vertexIndex, roadPath);
			this.updatePoints();
			return roadPath.vertex;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000E23AC File Offset: 0x000E07AC
		[Obsolete]
		public void removePoint(Transform select)
		{
			if (this.joints.Count < 2)
			{
				LevelRoads.removeRoad(this);
				return;
			}
			for (int i = 0; i < this.paths.Count; i++)
			{
				if (this.paths[i].vertex == select)
				{
					for (int j = i + 1; j < this.paths.Count; j++)
					{
						this.paths[j].vertex.name = "Path_" + (j - 1);
					}
					UnityEngine.Object.Destroy(select.gameObject);
					this.joints.RemoveAt(i);
					this.paths.RemoveAt(i);
					this.updatePoints();
					return;
				}
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x000E247C File Offset: 0x000E087C
		public void removeVertex(int vertexIndex)
		{
			if (this.joints.Count < 2)
			{
				LevelRoads.removeRoad(this);
				return;
			}
			for (int i = vertexIndex + 1; i < this.paths.Count; i++)
			{
				this.paths[i].vertex.name = "Path_" + (i - 1);
			}
			this.paths[vertexIndex].remove();
			this.paths.RemoveAt(vertexIndex);
			this.joints.RemoveAt(vertexIndex);
			this.updatePoints();
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000E2516 File Offset: 0x000E0916
		public void remove()
		{
			UnityEngine.Object.Destroy(this.road.gameObject);
			UnityEngine.Object.Destroy(this.line.gameObject);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000E2538 File Offset: 0x000E0938
		[Obsolete]
		public void movePoint(Transform select, Vector3 point)
		{
			for (int i = 0; i < this.paths.Count; i++)
			{
				if (this.paths[i].vertex == select)
				{
					this.joints[i].vertex = point;
					this.updatePoints();
					return;
				}
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000E2596 File Offset: 0x000E0996
		public void moveVertex(int vertexIndex, Vector3 point)
		{
			this.joints[vertexIndex].vertex = point;
			this.updatePoints();
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000E25B0 File Offset: 0x000E09B0
		public void moveTangent(int vertexIndex, int tangentIndex, Vector3 point)
		{
			this.joints[vertexIndex].setTangent(tangentIndex, point);
			this.updatePoints();
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000E25CB File Offset: 0x000E09CB
		public void splitPoint(Transform select)
		{
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000E25D0 File Offset: 0x000E09D0
		public void buildMesh()
		{
			for (int i = 0; i < this.road.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.road.GetChild(i).gameObject);
			}
			if (this.joints.Count < 2)
			{
				return;
			}
			this.updateSamples();
			if (!Level.isEditor)
			{
				bool flag = false;
				foreach (LevelTrainAssociation levelTrainAssociation in Level.info.configData.Trains)
				{
					if (levelTrainAssociation.RoadIndex == this.roadIndex)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.updateTrackSamples();
				}
			}
			Vector3[] array = new Vector3[this.samples.Count * 4 + ((!this.isLoop) ? 8 : 0)];
			Vector3[] array2 = new Vector3[this.samples.Count * 4 + ((!this.isLoop) ? 8 : 0)];
			Vector2[] array3 = new Vector2[this.samples.Count * 4 + ((!this.isLoop) ? 8 : 0)];
			float num = 0f;
			Vector3 b = Vector3.zero;
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			Vector3 vector3 = Vector3.zero;
			Vector3 a = Vector3.zero;
			Vector2 b2 = Vector2.zero;
			int j;
			for (j = 0; j < this.samples.Count; j++)
			{
				RoadSample roadSample = this.samples[j];
				RoadJoint roadJoint = this.joints[roadSample.index];
				vector = this.getPosition(roadSample.index, roadSample.time);
				if (!roadJoint.ignoreTerrain)
				{
					vector.y = LevelGround.getHeight(vector);
				}
				vector2 = this.getVelocity(roadSample.index, roadSample.time).normalized;
				if (roadJoint.ignoreTerrain)
				{
					vector3 = Vector3.up;
				}
				else
				{
					vector3 = LevelGround.getNormal(vector);
				}
				a = Vector3.Cross(vector2, vector3);
				if (!roadJoint.ignoreTerrain)
				{
					Vector3 point = vector + a * LevelRoads.materials[(int)this.material].width;
					float num2 = LevelGround.getHeight(point) - point.y;
					if (num2 > 0f)
					{
						vector.y += num2;
					}
					Vector3 point2 = vector - a * LevelRoads.materials[(int)this.material].width;
					float num3 = LevelGround.getHeight(point2) - point2.y;
					if (num3 > 0f)
					{
						vector.y += num3;
					}
				}
				if (roadSample.index < this.joints.Count - 1)
				{
					vector.y += Mathf.Lerp(roadJoint.offset, this.joints[roadSample.index + 1].offset, roadSample.time);
				}
				else if (this.isLoop)
				{
					vector.y += Mathf.Lerp(roadJoint.offset, this.joints[0].offset, roadSample.time);
				}
				else
				{
					vector.y += roadJoint.offset;
				}
				array[((!this.isLoop) ? 4 : 0) + j * 4] = vector + a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset;
				array[((!this.isLoop) ? 4 : 0) + j * 4 + 1] = vector + a * LevelRoads.materials[(int)this.material].width + vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset;
				array[((!this.isLoop) ? 4 : 0) + j * 4 + 2] = vector - a * LevelRoads.materials[(int)this.material].width + vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset;
				array[((!this.isLoop) ? 4 : 0) + j * 4 + 3] = vector - a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset;
				array2[((!this.isLoop) ? 4 : 0) + j * 4] = vector3;
				array2[((!this.isLoop) ? 4 : 0) + j * 4 + 1] = vector3;
				array2[((!this.isLoop) ? 4 : 0) + j * 4 + 2] = vector3;
				array2[((!this.isLoop) ? 4 : 0) + j * 4 + 3] = vector3;
				if (j == 0)
				{
					b = vector;
					array3[((!this.isLoop) ? 4 : 0) + j * 4] = Vector2.zero;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 1] = Vector2.zero;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 2] = Vector2.right;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 3] = Vector2.right;
				}
				else
				{
					num += (vector - b).magnitude;
					b = vector;
					b2 = Vector2.up * num / (float)LevelRoads.materials[(int)this.material].material.mainTexture.height * LevelRoads.materials[(int)this.material].height;
					array3[((!this.isLoop) ? 4 : 0) + j * 4] = Vector2.zero + b2;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 1] = Vector2.zero + b2;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 2] = Vector2.right + b2;
					array3[((!this.isLoop) ? 4 : 0) + j * 4 + 3] = Vector2.right + b2;
				}
			}
			if (!this.isLoop)
			{
				array[4 + j * 4] = vector + a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset + vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[4 + j * 4 + 1] = vector + a * LevelRoads.materials[(int)this.material].width - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset + vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[4 + j * 4 + 2] = vector - a * LevelRoads.materials[(int)this.material].width - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset + vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[4 + j * 4 + 3] = vector - a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset + vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array2[4 + j * 4] = vector3;
				array2[4 + j * 4 + 1] = vector3;
				array2[4 + j * 4 + 2] = vector3;
				array2[4 + j * 4 + 3] = vector3;
				b2 = Vector2.up * num / (float)LevelRoads.materials[(int)this.material].material.mainTexture.height * LevelRoads.materials[(int)this.material].height;
				array3[4 + j * 4] = Vector2.zero + b2;
				array3[4 + j * 4 + 1] = Vector2.zero + b2;
				array3[4 + j * 4 + 2] = Vector2.right + b2;
				array3[4 + j * 4 + 3] = Vector2.right + b2;
				j = 0;
				vector = this.getPosition(this.samples[0].index, this.samples[0].time);
				if (!this.joints[0].ignoreTerrain)
				{
					vector.y = LevelGround.getHeight(vector);
				}
				vector2 = this.getVelocity(this.samples[0].index, this.samples[0].time).normalized;
				if (this.joints[0].ignoreTerrain)
				{
					vector3 = LevelGround.getNormal(this.joints[0].vertex);
				}
				else
				{
					vector3 = LevelGround.getNormal(vector);
				}
				a = Vector3.Cross(vector2, vector3);
				if (!this.joints[0].ignoreTerrain)
				{
					Vector3 point3 = vector + a * LevelRoads.materials[(int)this.material].width;
					float num4 = LevelGround.getHeight(point3) - point3.y;
					if (num4 > 0f)
					{
						vector.y += num4;
					}
					Vector3 point4 = vector - a * LevelRoads.materials[(int)this.material].width;
					float num5 = LevelGround.getHeight(point4) - point4.y;
					if (num5 > 0f)
					{
						vector.y += num5;
					}
				}
				vector.y += this.joints[0].offset;
				array[j * 4] = vector + a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset - vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[j * 4 + 1] = vector + a * LevelRoads.materials[(int)this.material].width - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset - vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[j * 4 + 2] = vector - a * LevelRoads.materials[(int)this.material].width - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset - vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array[j * 4 + 3] = vector - a * (LevelRoads.materials[(int)this.material].width + LevelRoads.materials[(int)this.material].depth * 2f) - vector3 * LevelRoads.materials[(int)this.material].depth + vector3 * LevelRoads.materials[(int)this.material].offset - vector2 * LevelRoads.materials[(int)this.material].depth * 4f;
				array2[j * 4] = vector3;
				array2[j * 4 + 1] = vector3;
				array2[j * 4 + 2] = vector3;
				array2[j * 4 + 3] = vector3;
				array3[j * 4] = Vector2.zero;
				array3[j * 4 + 1] = Vector2.zero;
				array3[j * 4 + 2] = Vector2.right;
				array3[j * 4 + 3] = Vector2.right;
			}
			int num6 = 0;
			for (int k = 0; k < this.samples.Count; k += 20)
			{
				int num7 = Mathf.Min(k + 20, this.samples.Count - 1);
				int num8 = num7 - k + 1;
				if (!this.isLoop)
				{
					if (k == 0)
					{
						num8++;
					}
					if (num7 == this.samples.Count - 1)
					{
						num8++;
					}
				}
				Vector3[] array4 = new Vector3[num8 * 4];
				Vector3[] array5 = new Vector3[num8 * 4];
				Vector2[] array6 = new Vector2[num8 * 4];
				int[] array7 = new int[num8 * 18];
				int num9 = k;
				if (!this.isLoop && k > 0)
				{
					num9++;
				}
				Array.Copy(array, num9 * 4, array4, 0, array4.Length);
				Array.Copy(array2, num9 * 4, array5, 0, array4.Length);
				Array.Copy(array3, num9 * 4, array6, 0, array4.Length);
				for (int l = 0; l < num8 - 1; l++)
				{
					array7[l * 18] = l * 4 + 5;
					array7[l * 18 + 1] = l * 4 + 1;
					array7[l * 18 + 2] = l * 4 + 4;
					array7[l * 18 + 3] = l * 4;
					array7[l * 18 + 4] = l * 4 + 4;
					array7[l * 18 + 5] = l * 4 + 1;
					array7[l * 18 + 6] = l * 4 + 6;
					array7[l * 18 + 7] = l * 4 + 2;
					array7[l * 18 + 8] = l * 4 + 5;
					array7[l * 18 + 9] = l * 4 + 1;
					array7[l * 18 + 10] = l * 4 + 5;
					array7[l * 18 + 11] = l * 4 + 2;
					array7[l * 18 + 12] = l * 4 + 7;
					array7[l * 18 + 13] = l * 4 + 3;
					array7[l * 18 + 14] = l * 4 + 6;
					array7[l * 18 + 15] = l * 4 + 2;
					array7[l * 18 + 16] = l * 4 + 6;
					array7[l * 18 + 17] = l * 4 + 3;
				}
				Transform transform = new GameObject().transform;
				transform.name = "Segment_" + num6;
				transform.parent = this.road;
				transform.tag = "Environment";
				transform.gameObject.layer = LayerMasks.ENVIRONMENT;
				transform.gameObject.AddComponent<MeshCollider>();
				if (!Dedicator.isDedicated)
				{
					transform.gameObject.AddComponent<MeshFilter>();
					MeshRenderer meshRenderer = transform.gameObject.AddComponent<MeshRenderer>();
					meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Simple;
					meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				}
				if (LevelRoads.materials[(int)this.material].isConcrete)
				{
					transform.GetComponent<Collider>().sharedMaterial = (PhysicMaterial)Resources.Load("Physics/Concrete_Static");
				}
				else
				{
					transform.GetComponent<Collider>().sharedMaterial = (PhysicMaterial)Resources.Load("Physics/Gravel_Static");
				}
				Mesh mesh = new Mesh();
				mesh.name = "Road_Segment_" + num6;
				mesh.vertices = array4;
				mesh.normals = array5;
				mesh.uv = array6;
				mesh.triangles = array7;
				transform.GetComponent<MeshCollider>().sharedMesh = mesh;
				if (!Dedicator.isDedicated)
				{
					transform.GetComponent<MeshFilter>().sharedMesh = mesh;
					transform.GetComponent<Renderer>().sharedMaterial = LevelRoads.materials[(int)this.material].material;
				}
				num6++;
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000E3A00 File Offset: 0x000E1E00
		private void updateSamples()
		{
			this.samples.Clear();
			float num = 0f;
			for (int i = 0; i < this.joints.Count - 1 + ((!this.isLoop) ? 0 : 1); i++)
			{
				float lengthEstimate = this.getLengthEstimate(i);
				float num2;
				for (num2 = num; num2 < lengthEstimate; num2 += 5f)
				{
					float time = num2 / lengthEstimate;
					RoadSample roadSample = new RoadSample();
					roadSample.index = i;
					roadSample.time = time;
					this.samples.Add(roadSample);
				}
				num = num2 - lengthEstimate;
			}
			if (this.isLoop)
			{
				RoadSample roadSample2 = new RoadSample();
				roadSample2.index = 0;
				roadSample2.time = 0f;
				this.samples.Add(roadSample2);
			}
			else
			{
				RoadSample roadSample3 = new RoadSample();
				roadSample3.index = this.joints.Count - 2;
				roadSample3.time = 1f;
				this.samples.Add(roadSample3);
			}
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000E3B08 File Offset: 0x000E1F08
		private void updateTrackSamples()
		{
			this.trackSamples.Clear();
			if (this.samples.Count < 2)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			Vector3 up = Vector3.up;
			double num = 0.0;
			int num2 = (!this.isLoop) ? this.samples.Count : (this.samples.Count - 1);
			for (int i = 1; i < num2; i++)
			{
				RoadSample roadSample = this.samples[i];
				TrackSample trackSample = null;
				if (i == 1)
				{
					RoadSample roadSample2 = this.samples[0];
					this.getTrackPosition(roadSample2.index, roadSample2.time, out vector, out up);
					trackSample = new TrackSample();
					trackSample.position = vector;
					trackSample.normal = up;
					this.trackSamples.Add(trackSample);
				}
				Vector3 vector2;
				Vector3 normal;
				this.getTrackPosition(roadSample.index, roadSample.time, out vector2, out normal);
				Vector3 a = vector2 - vector;
				float magnitude = a.magnitude;
				Vector3 direction = a / magnitude;
				TrackSample trackSample2 = new TrackSample();
				trackSample2.distance = (float)num;
				trackSample2.position = vector2;
				trackSample2.normal = normal;
				trackSample2.direction = direction;
				this.trackSamples.Add(trackSample2);
				if (trackSample != null)
				{
					trackSample.direction = direction;
				}
				vector = vector2;
				num += (double)magnitude;
			}
			if (this.isLoop)
			{
				num += (double)(this.trackSamples[0].position - vector).magnitude;
			}
			this.trackSampledLength = (float)num;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000E3CAC File Offset: 0x000E20AC
		public void updatePoints()
		{
			for (int i = 0; i < this.joints.Count; i++)
			{
				RoadJoint roadJoint = this.joints[i];
				if (!roadJoint.ignoreTerrain)
				{
					roadJoint.vertex.y = LevelGround.getHeight(roadJoint.vertex);
				}
			}
			for (int j = 0; j < this.joints.Count; j++)
			{
				RoadPath roadPath = this.paths[j];
				roadPath.vertex.position = this.joints[j].vertex;
				roadPath.tangents[0].gameObject.SetActive(j > 0 || this.isLoop);
				roadPath.tangents[1].gameObject.SetActive(j < this.joints.Count - 1 || this.isLoop);
				roadPath.setTangent(0, this.joints[j].getTangent(0));
				roadPath.setTangent(1, this.joints[j].getTangent(1));
			}
			if (this.joints.Count < 2)
			{
				this.lineRenderer.numPositions = 0;
				return;
			}
			this.updateSamples();
			this.lineRenderer.numPositions = this.samples.Count;
			for (int k = 0; k < this.samples.Count; k++)
			{
				RoadSample roadSample = this.samples[k];
				RoadJoint roadJoint2 = this.joints[roadSample.index];
				Vector3 position = this.getPosition(roadSample.index, roadSample.time);
				if (!roadJoint2.ignoreTerrain)
				{
					position.y = LevelGround.getHeight(position);
				}
				if (roadSample.index < this.joints.Count - 1)
				{
					position.y += Mathf.Lerp(roadJoint2.offset, this.joints[roadSample.index + 1].offset, roadSample.time);
				}
				else if (this.isLoop)
				{
					position.y += Mathf.Lerp(roadJoint2.offset, this.joints[0].offset, roadSample.time);
				}
				else
				{
					position.y += roadJoint2.offset;
				}
				this.lineRenderer.SetPosition(k, position);
			}
		}

		// Token: 0x04001804 RID: 6148
		public byte material;

		// Token: 0x04001806 RID: 6150
		private Transform _road;

		// Token: 0x04001807 RID: 6151
		private Transform line;

		// Token: 0x04001808 RID: 6152
		private LineRenderer lineRenderer;

		// Token: 0x04001809 RID: 6153
		private bool _isLoop;

		// Token: 0x0400180A RID: 6154
		private List<RoadJoint> _joints;

		// Token: 0x0400180B RID: 6155
		private List<RoadSample> samples;

		// Token: 0x0400180C RID: 6156
		private List<TrackSample> trackSamples;

		// Token: 0x0400180E RID: 6158
		private List<RoadPath> _paths;
	}
}
