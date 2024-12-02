//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using System.Linq;
//using static UnityEditor.Progress;
//using Unity.VisualScripting;
//using static UnityEngine.EventSystems.EventTrigger;
//using UnityEditor.Experimental.GraphView;

//public class SpellData
//{
//    public List<List<Vector2>> points;
//}
//public class LineManager : MonoBehaviour
//{

//    public GameObject lineprefab;
//    List<List<Vector2>> userDrawnSpells = new List<List<Vector2>>();
//    private Line activeLine;
//    public GameObject spellPrefab;

//    private float timer;
//    private bool isTiming;
//    private int drawnIndex = 0;
//    // Update is called once per frame
//    void Update()
//    {
//        //reset timer and start new line
//        if (Input.GetMouseButtonDown(0))
//        {
//            timer = 0;
//            GameObject newLine = Instantiate(lineprefab);
//            activeLine = newLine.GetComponent<Line>();
//            userDrawnSpells.Add(new List<Vector2>());
           
//        }
//        //add line to total image, start timing
//        if (Input.GetMouseButtonUp(0))
//        {
//            if (activeLine != null)
//            {
//                userDrawnSpells[drawnIndex].AddRange(activeLine.GetPoints());
//                drawnIndex++;
//            }
//            isTiming = true;
//            activeLine = null;
//        }

//        //Update activeLine to current touch position
//        if (activeLine != null)
//        {
//            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            activeLine.UpdateLine(touchPos);
//        }
//        //If not drawing
//        else
//        {
//            //if currently timeing, update timer
//            if (isTiming)
//            {
//                timer += Time.deltaTime;
                
//            }
//        }

//        //Stop timing and cast spell when timer > 5
//        if (timer > 3)
//        {
//            isTiming = false;
//            timer = 0;
//            //SaveSpell();
//            userDrawnSpells.ForEach(s => s.Clear());
//            drawnIndex = 0;
//            userDrawnSpells.Clear();
//        }
//    }

//    private List<List<Vector2>> centerPoints(List<List<Vector2>> points)
//    {
//        List<float> centerXs = new List<float>();
//        List<float> centerYs = new List<float>();
//        foreach (var element in points)
//        {
//            float centerX = element.Average(p => p.x);
//            float centerY = element.Average(p => p.y);
//            centerXs.Add(centerX);
//            centerYs.Add(centerY);
//        }
//        Vector2 center = new Vector2(centerXs.Average(), centerYs.Average());
//        List<List<Vector2>> centeredSpells = new List<List<Vector2>>();
//        foreach (var element in points)
//        {
//            // Center each point in the list by subtracting the overall centroid
//            List<Vector2> centeredPoints = element.Select(p => p - center).ToList();
//            centeredSpells.Add(centeredPoints);
//        }
//        return centeredSpells;

//    }

//    private List<List<Vector2>> normalizePoints(List<List<Vector2>> points)
//    {
//        float maxDistance = 0f;
//        foreach (var spell in points)
//        {
//            foreach (var point in spell)
//            {
//                float magnitude = point.magnitude;
//                if (magnitude > maxDistance)
//                {
//                    maxDistance = magnitude;
//                }
//            }
//        }

//        // Normalize points directly without creating intermediate collections
//        var normalizedPoints = new List<List<Vector2>>(points.Count);
//        foreach (var spell in points)
//        {
//            var normalizedSpell = new List<Vector2>(spell.Count);
//            foreach (var point in spell)
//            {
//                normalizedSpell.Add(point / maxDistance);
//            }
//            normalizedPoints.Add(normalizedSpell);
//        }

//        return normalizedPoints;
//    }

//    void SaveSpell()
//    {
//        if (userDrawnSpells.Count > 0)
//        {
//            List<List<Vector2>> spellPoints = new List<List<Vector2>>();
//            spellPoints = centerPoints(userDrawnSpells);
//            spellPoints = normalizePoints(spellPoints);
//            DisplaySpell(spellPoints);
//            //print(Vector2.Distance(spellPoints[0], spellPoints[1]));
//            SpellData spellData = new SpellData { points = spellPoints };
//            string json = JsonUtility.ToJson(spellData);

//            string path = Application.persistentDataPath + "/spell.json";
//            File.WriteAllText(path, json);

//            Debug.Log("Spell saved to: " + path);
//        }
//    }
//    void DisplaySpell(List<List<Vector2>> spellPoints)
//    {
//        // Iterate over each individual spell (each list of Vector2 points)
//        foreach (var points in spellPoints)
//        {
//            // Create a new GameObject to hold the LineRenderer for each spell
//            GameObject spellLineObject = new GameObject("SpellVisualization");
//            LineRenderer lineRenderer = spellLineObject.AddComponent<LineRenderer>();
            
//            // Set LineRenderer properties
//            lineRenderer.startWidth = 0.1f;
//            lineRenderer.endWidth = 0.1f;
//            lineRenderer.positionCount = points.Count;
            
           
//            // Assign points to the LineRenderer
//            for (int i = 0; i < points.Count; i++)
//            {
//                lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0)); // Z = 0 for 2D
//            }

//            // Optional: Connect the last point back to the first to close the shape
//            if (points.Count > 2)
//            {
//                lineRenderer.loop = true; // Creates a closed loop (if more than 2 points)
//            }
//        }
//    }

//    //void LoadSpell()
//    //{

//    //    string path = Application.persistentDataPath + "/spell.json";
//    //    if (File.Exists(path))
//    //    {
//    //        string json = File.ReadAllText(path);
//    //        SpellData loadedSpell = JsonUtility.FromJson<SpellData>(json);

//    //        if (loadedSpell != null && loadedSpell.points.Count > 0)
//    //        {
//    //            Debug.Log("Spell loaded successfully. Points count: " + loadedSpell.points.Count);
//    //            CompareSpell(loadedSpell.points);
//    //        }
//    //        else
//    //        {
//    //            Debug.LogWarning("Loaded spell is empty or null.");
//    //        }
//    //    }
//    //    else
//    //    {
//    //        Debug.LogWarning("Spell file not found at: " + path);
//    //    }
//    //}





//}
