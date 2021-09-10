using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellClassifier : MonoBehaviour
{
    private float size = 350.0f;

    public string Classify(List<Vector2> points)
    {
        //Step 1. Resample a points path into n evenly spaced points.
        points = Resample(points, 64);
        //Step 2. Rotate points so that their indicative angle is at 0°
        points = RotateToZero(points);
        //Step 3. Scale points so that the resulting bounding box will be of size2 dimension;
        //then translate points to the origin
        points = ScaleToSquare(points, size);
        points = TranslateToOrigin(points);

        return "Circle";
    }

    public List<Vector2> Resample(List<Vector2> points, int n)
    {
        //1 I ← PATH-LENGTH(points) / (n – 1)
        float I = PathLength(points) / (n - 1);
        //2 D ← 0
        float D = 0;

        //3 newPoints ← points0
        List<Vector2> newPoints = new List<Vector2>();
        newPoints.Add(points[0]);

        //4 foreach point pi for i ≥ 1 in points do
        for (int i = 1; i < points.Count; i++)
        {
            //5 d ← DISTANCE(pi-1, pi) 
            float d = Vector2.Distance(points[i - 1], points[i]);
            //6 if (D + d) ≥ I then
            if ((D + d) >= I)
            {
                //7
                Vector2 q = new Vector2();
                q.x = points[i - 1].x + ((I - D) / d) * (points[i].x - points[i - 1].x);
                //8
                q.y = points[i - 1].y + ((I - D) / d) * (points[i].y - points[i - 1].y);
                //9 APPEND(newPoints, q) 
                newPoints.Add(q);
                //10 INSERT(points, i, q) // q will be the next pi
                points.Insert(i, q);
                //11 D ← 0 
                D = 0;
            } else
            {
                //12 else D ← D + d
                D = D + d;
            }
        }

        return newPoints;
    }

    private float PathLength(List<Vector2> A)
    {
        //1 d ← 0 
        float d = 0.0f;
        //2 for i from 1 to |A| step 1 do
        for (int i = 1; i < A.Count; i++)
        {
            //3 d ← d + DISTANCE(Ai-1, Ai)
            d = d + Vector2.Distance(A[i - 1], A[i]);
        }

        //4 return d
        return d;
    }

    public List<Vector2> RotateToZero(List<Vector2> points)
    {
        //1 c ← CENTROID(points) // computes (x¯, y¯)
        Vector2 c = new Vector2(0.0f, 0.0f);
        foreach (Vector2 p in points)
        {
            c.x += p.x;
            c.y += p.y;
        }
        c.x = c.x / points.Count;
        c.y = c.y / points.Count;

        //2 θ ← ATAN (cy – points0y, cx – points0x) // for -π ≤ θ ≤ π
        float theta = Mathf.Atan2(c.y - points[0].y, c.x - points[0].x);
        //3 newPoints ← ROTATE-BY(points, -θ)
        List<Vector2> newPoints = RotateBy(points, -theta, c);

        //4 return newPoints
        return newPoints;
    }

    private List<Vector2> RotateBy(List<Vector2> points, float theta, Vector2 c)
    {
        //1 c ← CENTROID(points)
        // just passed from RotateToZero

        List<Vector2> newPoints = new List<Vector2>();

        //2 foreach point p in points do
        foreach (Vector2 p in points)
        {
            Vector2 q = new Vector2(0.0f, 0.0f);

            //3 qx ← (px – cx) COS θ – (py – cy) SIN θ + cx
            q.x = ((p.x - c.x) * Mathf.Cos(theta)) - ((p.y - c.y) * Mathf.Sin(theta)) + c.x;
            //4 qy ← (px – cx) SIN θ + (py – cy) COS θ + cy
            q.y = ((p.x - c.x) * Mathf.Sin(theta)) + ((p.y - c.y) * Mathf.Cos(theta)) + c.y;
            //5 APPEND(newPoints, q)
            newPoints.Add(q);
        }

        return newPoints;
    }

    public List<Vector2> ScaleToSquare(List<Vector2> points, float size)
    {
        List<Vector2> newPoints = new List<Vector2>();

        //1 B ← BOUNDING-BOX(points)
        // B (xmin, xmax, ymin, ymax)
        Vector4 B = new Vector4(Mathf.Infinity, Mathf.NegativeInfinity, Mathf.Infinity, Mathf.NegativeInfinity);
        foreach (Vector2 p in points)
        {
            if (p.x < B.x)
            {
                B.x = p.x;
            }
            if (p.x > B.y)
            {
                B.y = p.x;
            }
            if (p.y < B.z)
            {
                B.z = p.x;
            }
            if (p.x > B.w)
            {
                B.w = p.x;
            }
        }
        float Bwidth = B.y - B.x;
        float Bheight = B.w - B.z;

        //2 foreach point p in points do
        foreach (Vector2 p in points)
        {
            Vector2 q = new Vector2(0.0f, 0.0f);

            //3 qx ← px × (size / Bwidth)
            q.x = p.x * (size / Bwidth);
            //4 qy ← py × (size / Bheight)
            q.y = p.y * (size / Bheight);

            //5 APPEND(newPoints, q) 
            newPoints.Add(q);
        }

        //6 return newPoints
        return newPoints;
    }

    public List<Vector2> TranslateToOrigin(List<Vector2> points)
    {

    }
}
