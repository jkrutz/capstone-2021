using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellClassifier : MonoBehaviour
{
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
}
