using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathfExtentions : MonoBehaviour {
}


public static class Vector2Extension {

	public static bool IsTrueNull(this UnityEngine.Object obj)
	 {
    	return (object)obj == null;
 	}

	public static float lenght(Vector2 v){
		return Mathf.Sqrt((v.x * v.x) + (v.y * v.y));
	}

	public static float VectorLenght(this Vector2 v){
		return Mathf.Sqrt((v.x * v.x) + (v.y * v.y));
	}

	public static float calculateMeshArea(this Mesh mesh){
		float area = 0;
		for (int i = 0; i < mesh.triangles.Length / 3; i++)
		{
			area += calculateTriangleArea(mesh.vertices[mesh.triangles[i * 3 + 0]],mesh.vertices[mesh.triangles[i * 3 + 1]],mesh.vertices[mesh.triangles[i * 3 + 2]]);
		}
		return area;
	}

	public static float calculateTriangleArea(Vector2 A, Vector2 B, Vector2 C){
		float a = lenght(A - B)
		,b = lenght(C - A)
		, c = lenght(C - B);

		float s = (a + b + c) / 2;

		return Mathf.Sqrt(Mathf.Abs(s*(s - a) * (s - b) * (s - c)));
	}

	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float radians = degrees * Mathf.Deg2Rad;
		float sin = Mathf.Sin(radians);
		float cos = Mathf.Cos(radians);
		
		float tx = v.x;
		float ty = v.y;

		return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
	}

	public static bool IsInsideTriangle(this Vector2 s, Vector2 a, Vector2 b, Vector2 c)
	{
		float as_x = s.x - a.x;
		float as_y = s.y - a.y;

		bool s_ab = (b.x-a.x)*as_y-(b.y-a.y)*as_x > 0;

		if((c.x-a.x)*as_y-(c.y-a.y)*as_x > 0 == s_ab) return false;

		if((c.x-b.x)*(s.y-b.y)-(c.y-b.y)*(s.x-b.x) > 0 != s_ab) return false;

		return true;
	}

	public static float ToAngle(this Vector2 v)
    {
		if (v.x < 0)
		{
			return 450 - (Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg);
		}
		else
		{
			return (Mathf.Atan2(v.x, v.y) * Mathf.Rad2Deg * -1) + 90;
		}
    }

	public static bool FloatingEquals(this Vector2 a,Vector2 b,float change){
		if(a.x - change < b.x && a.x + change > b.x && a.y - change < b.y && a.y + change > b.y)
			return true;
		return false;
	}

	public static Vector3[] MultiLerpLine(this Vector3[] points,float time)
	{
		float t = time * (points.Length - 1);

		Vector3 pointA = Vector3.zero;
		Vector3 pointB = Vector3.zero;

		List<Vector3> nodes = new List<Vector3>();

		for (int i = 0; i < points.Length; i++)
		{
			if(t < (float)i){
				pointA = points[i - 1];
				pointB = points[i];
				nodes.Add(Vector3.Lerp(pointA, pointB, t - (i - 1)));
				break;
			}
			else if(t == (float)i){
				nodes.Add(points[i]);
				break;
			}
			nodes.Add(points[i]);
		}

		if(nodes.Count != points.Length){
			for(int i = 0; i < points.Length - nodes.Count; i++){
				nodes.Add(nodes[nodes.Count - 1]);
			}
		}


		return nodes.ToArray();
	}

	public static Vector3 MultiLerp(this Vector3[] points,float time)
	{
		if (points.Length == 1)
			return points[0];
		else if (points.Length == 2)
			return Vector3.Lerp(points[0], points[1],time);

		if (time == 0)
			return points[0];

		if (time == 1)
			return points[points.Length - 1];

		float t = time * (points.Length - 1);

		Vector3 pointA = Vector3.zero;
		Vector3 pointB = Vector3.zero;

		for (int i = 0; i < points.Length; i++)
		{
			if(t < (float)i)
			{
			pointA = points[i - 1];
			pointB = points[i];
			return Vector3.Lerp(pointA, pointB, t - (i - 1));
			}
			else if(t == (float)i)
			{
			return points[i];
			}
		}
		return Vector3.zero;
	}
	
	
}