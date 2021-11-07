using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Demonstrations;
using Unity.MLAgents.Policies;
using Unity.Barracuda;
using UnityEngine.Serialization;

public class SpellClassifier : MonoBehaviour
{

    string[] spell = { "star", "triangle", "tornado", "zigzag", "square", "circle" };
    private readonly float size = 350.0f;
    private readonly float fi = 0.5f * (-1 + Mathf.Sqrt(5));

    public string bestGuess;
    [Serializable]
    public struct modelOutput
    {
        public string type;
        public float probability;
    }
    public modelOutput[] output;

    public NNModel modelFile;
    private Model m_RuntimeModel;
    private IWorker m_Worker;


    public Player player;

    private struct Classification
    {
        public List<Vector2> points;
        public string name;
        public float score;
    }

    private List<Classification> templates = new List<Classification>();

    private void Start()
    {
        m_RuntimeModel = ModelLoader.Load(modelFile);
        m_Worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, m_RuntimeModel);
        output = new modelOutput[spell.Length];

    }

    public void CreateTemplates(List<Vector2> points, string path)
    {
        //Step 1. Resample a points path into n evenly spaced points.
        points = Resample(points, 64);
        //Step 2. Rotate points so that their indicative angle is at 0°
        //points = RotateToZero(points);
        //Step 3. Scale points so that the resulting bounding box will be of size2 dimension
        //then translate points to the origin
        points = ScaleToSquare(points, size);
        points = TranslateToOrigin(points);

        //normalize
        points = Normalize(points);


        bool isNumeric = true;
        foreach (Vector2 pt in points)
        {
            float ptx = pt.x;
            float pty = pt.y;
            if (ptx < 0)
            {
                ptx *= -1;
            }
            if (pty < 0)
            {
                pty *= -1;
            }
            isNumeric = (!float.IsPositiveInfinity(ptx)) && (!float.IsNaN(ptx)) &&
                (!float.IsPositiveInfinity(pty)) && (!float.IsNaN(pty));
            if (!isNumeric)
            {
                break;
            }
        }

        if (isNumeric)
        {

            StreamWriter writer = new StreamWriter(path, false);

            foreach (Vector2 p in points)
            {
                writer.WriteLine(p.x + " " + p.y);
            }

            writer.Close();
            AssetDatabase.ImportAsset(path);
            print("Saved @ " + path);
        }
        else
        {
            Debug.Log("Data could not be saved.");
        }
    }

    private void ReadTemplate(string pathname, string spellname)
    {
        List<Vector2> points = new List<Vector2>();

        StreamReader reader = new StreamReader(pathname);
        var contents = reader.ReadToEnd();
        reader.Close();

        var lines = contents.Split('\n');
        foreach (string line in lines)
        {
            var coords = line.Split(' ');

            float x = float.Parse(coords[0]);
            float y = float.Parse(coords[1]);

            points.Add(new Vector2(x, y));
        }

        Classification T = new Classification();
        T.points = points;
        T.name = spellname;
        templates.Add(T);
    }

    public string Classify(List<Vector2> points)
    {

        bestGuess = "None";
        output = new modelOutput[spell.Length];
        //Step 1. Resample a points path into n evenly spaced points.
        points = Resample(points, 64);
        //Step 2. Rotate points so that their indicative angle is at 0°
        //points = RotateToZero(points);
        //Step 3. Scale points so that the resulting bounding box will be of size2 dimension
        //then translate points to the origin
        points = ScaleToSquare(points, size);
        points = TranslateToOrigin(points);

        //normalize
        points = Normalize(points);
        //create image

        //Step 0. Create 28 x 28 MNIST base
        float[,] img_array = new float[28, 28];

        //List to hold all points after connections made
        List<Vector2> matrix = new List<Vector2>(points);

        for (int i = 1; i < points.Count; i++)
        {
            Vector2 curPoint = points[i];
            Vector2 prevPoint = points[i - 1];
            matrix = new List<Vector2>(bresenham(matrix, (int)prevPoint.x, (int)prevPoint.y, (int)curPoint.x, (int)curPoint.y));
        }

        //Step 1. Round to nearest pixel in 28x28
        for (int i = 0; i < matrix.Count; i++)
        {
            Vector2 p = matrix[i];
            p.x = (int)(p.x * 28);
            p.y = (int)(p.y * 28);
            img_array[27 - (int)p.y, (int)p.x] = 1;
        }
        img_array[0, 0] = 0;

        print_coords(img_array);

        //Padding
        float[,] padded_img_array = new float[32, 32];
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                var ir = r - 2;
                var ic = c - 2;
                if (ir >= 0 && ir < 28 && ic >= 0 && ic < 28)
                {
                    padded_img_array[r, c] = img_array[ir, ic];
                }
            }
        }

        //To 1D
        var oned_points = new float[32 * 32];
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                oned_points[r * 32 + c] = padded_img_array[r, c];
            }
        }

        Tensor input = new Tensor(1, 32, 32, 1, oned_points);
        m_Worker.Execute(input);
        Tensor O = m_Worker.PeekOutput("dense_2");
        string selspell = spell[O.ArgMax()[0]];
        for(int s = 0; s < spell.Length; s++)
        {
            output[s] = new modelOutput();
            output[s].type = spell[s];
            output[s].probability = O.AsFloats()[s];
        }
        bestGuess = selspell;
        input.Dispose();

        player.SetActiveSpell(selspell);

        return selspell;
    }

    private List<Vector2> bresenham(List<Vector2> matrix, int x1, int y1, int x2,
                                        int y2)
    {
        int m_new = 2 * (y2 - y1);
        int slope_error_new = m_new - (x2 - x1);

        for (int x = x1, y = y1; x <= x2; x++)
        {
            matrix.Add(new Vector2(x, y));

            // Add slope to increment angle formed
            slope_error_new += m_new;

            // Slope error reached limit, time to
            // increment y and update slope error.
            if (slope_error_new >= 0)
            {
                y++;
                slope_error_new -= 2 * (x2 - x1);
            }
        }
        return matrix;
    }

    private void print_coords(float[,] img_array)
    {
        string output = "";
        for (int r = 0; r < 28; r++)
        {
            for (int c = 0; c < 28; c++)
            {
                output += (img_array[r, c] + " ");
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    private List<Vector2> Resample(List<Vector2> points, int n)
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
            }
            else
            {
                //12 else D ← D + d
                D += d;
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
            d += Vector2.Distance(A[i - 1], A[i]);
        }

        //4 return d
        return d;
    }

    private List<Vector2> RotateToZero(List<Vector2> points)
    {
        //1 c ← CENTROID(points) // computes (x¯, y¯)
        Vector2 c = Centroid(points);

        //2 θ ← ATAN (cy – points0y, cx – points0x) // for -π ≤ θ ≤ π
        float theta = Mathf.Atan2(c.y - points[0].y, c.x - points[0].x);
        //3 newPoints ← ROTATE-BY(points, -θ)
        List<Vector2> newPoints = RotateBy(points, -theta);

        //4 return newPoints
        return newPoints;
    }

    private List<Vector2> RotateBy(List<Vector2> points, float theta)
    {
        //1 c ← CENTROID(points)
        Vector2 c = Centroid(points);

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

    private Vector2 Centroid(List<Vector2> points)
    {
        Vector2 c = new Vector2(0.0f, 0.0f);
        foreach (Vector2 p in points)
        {
            c.x += p.x;
            c.y += p.y;
        }
        c.x /= points.Count;
        c.y /= points.Count;

        return c;
    }

    private List<Vector2> ScaleToSquare(List<Vector2> points, float size)
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
                B.z = p.y;
            }
            if (p.y > B.w)
            {
                B.w = p.y;
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

    private List<Vector2> TranslateToOrigin(List<Vector2> points)
    {
        List<Vector2> newPoints = new List<Vector2>();

        //1 c ← CENTROID(points)
        Vector2 c = Centroid(points);

        //2 foreach point p in points do
        foreach (Vector2 p in points)
        {
            Vector2 q = new Vector2(0.0f, 0.0f);

            //3 qx ← px – cx
            q.x = p.x - c.x;
            //4 qy ← py – cy
            //FIXME or not...
            q.y = p.y - c.y;

            //5 APPEND(newPoints, q)
            newPoints.Add(q);
        }

        //6 return newPoints
        return newPoints;
    }

    private Classification Recognize(List<Vector2> points, List<Classification> templates)
    {
        Classification Tprime = new Classification();

        //1 b ← +∞
        float b = Mathf.Infinity;

        //2 foreach template T in templates do
        foreach (Classification T in templates)
        {
            //3 d ← DISTANCE-AT-BEST-ANGLE(points, T, -θ, θ, θ∆)
            float d = DistanceAtBestAngle(points, T, -45.0f, 45.0f, 2.0f);

            //4 if d < b then
            if (d < b)
            {
                //5 b ← d
                b = d;
                //6 T′ ← T
                Tprime = T;
            }
        }
        //7 score ← 1 – b / 0.5√(size2+size2
        Tprime.score = 1 - (b / (0.5f * Mathf.Sqrt(Mathf.Pow(size, 2) + Mathf.Pow(size, 2))));

        //8 return 〈T′, score〉
        return Tprime;
    }

    private float DistanceAtBestAngle(List<Vector2> points, Classification T, float thetaA, float thetaB, float thetaDelta)
    {
        //1 x1 ← ϕθa + (1 – ϕ)θb
        float x1 = (fi * thetaA) + ((1 - fi) * thetaB);
        //2 f1 ← DISTANCE-AT-ANGLE(points, T, x1)
        float f1 = DistanceAtAngle(points, T, x1);
        //3 x2 ← (1 – ϕ)θa + ϕθb
        float x2 = ((1 - fi) * thetaA) + thetaB;
        //4 f2 ← DISTANCE-AT-ANGLE(points, T, x2)
        float f2 = DistanceAtAngle(points, T, x2);

        //5 while |θb – θa| > θ∆ do
        while (thetaB - thetaA > thetaDelta)
        {
            //6 if f1 < f2 then
            if (f1 > f2)
            {
                //7 θb ← x2
                thetaB = x2;
                //8 x2 ← x1
                x2 = x1;
                //9 f2 ← f1
                f2 = f1;
                //10 x1 ← ϕθa + (1 – ϕ)θb
                x1 = (fi * thetaA) + ((1 - fi) * thetaB);
                //11 f1 ← DISTANCE-AT-ANGLE(points, T, x1)
                f1 = DistanceAtAngle(points, T, x1);
            }
            else //12 else
            {
                //13 θa ← x1
                thetaA = x1;
                //14 x1 ← x2
                x1 = x2;
                //15 f1 ← f2
                f1 = f2;
                //16 x2 ← (1 – ϕ)θa + ϕθb
                x2 = ((1 - fi) * thetaA) + (fi * thetaB);
                //17 f2 ← DISTANCE-AT-ANGLE(points, T, x2)
                f2 = DistanceAtAngle(points, T, x2);
            }
        }

        //18 return MIN(f1, f2)
        return Mathf.Min(f1, f2);
    }

    private float DistanceAtAngle(List<Vector2> points, Classification T, float theta)
    {
        //1 newPoints ← ROTATE-BY(points, θ)
        List<Vector2> newPoints = RotateBy(points, theta);
        //2 d ← PATH-DISTANCE(newPoints, Tpoints)
        float d = PathDistance(newPoints, T.points);
        //3 return d
        return d;
    }

    private float PathDistance(List<Vector2> A, List<Vector2> B)
    {
        //1 d ← 0 
        float d = 0;

        //2 for i from 0 to |A| step 1 do
        for (int i = 0; i < A.Count; i++)
        {
            //3 d ← d + DISTANCE(Ai, Bi)
            d += Vector2.Distance(A[i], B[i]);
        }

        //4 return d / |A|
        return d / A.Count;
    }

    private float Get_Diagonal(List<Vector2> p, float min_x, float min_y)
    {
        float[] x_values = new float[p.Count];
        float[] y_values = new float[p.Count];

        for (int i = 0; i < p.Count; i++)
        {
            x_values[i] = Mathf.Abs(p[i].x);
            y_values[i] = Mathf.Abs(p[i].y);
        }

        float max_x = Mathf.Max(x_values);
        float max_y = Mathf.Max(y_values);

        return Mathf.Sqrt((max_x - min_x) * (max_x - min_x) + (max_y - min_y) * (max_y - min_y));
    }

    private List<Vector2> Normalize(List<Vector2> p)
    {
        float[] x_values = new float[p.Count];
        float[] y_values = new float[p.Count];

        for (int i = 0; i < p.Count; i++)
        {
            x_values[i] = Mathf.Abs(p[i].x);
            y_values[i] = Mathf.Abs(p[i].y);
        }

        float min_x = Mathf.Min(x_values);
        float min_y = Mathf.Min(y_values);

        float diagonal_len = Get_Diagonal(p, min_x, min_y);
        List<Vector2> newPoints = new List<Vector2>();

        foreach (Vector2 pt in p)
        {
            float x_val = pt.x;
            float y_val = pt.y;
            float x_norm = x_val < 0 ? (x_val + Mathf.Abs(min_x)) / diagonal_len : (x_val - Mathf.Abs(min_x)) / diagonal_len;
            float y_norm = y_val < 0 ? (y_val + Mathf.Abs(min_y)) / diagonal_len : (y_val - Mathf.Abs(min_y)) / diagonal_len;
            newPoints.Add(new Vector2(x_norm / 2.0f + 0.5f, y_norm / 2.0f + 0.5f));

        }

        return newPoints;

    }
}
