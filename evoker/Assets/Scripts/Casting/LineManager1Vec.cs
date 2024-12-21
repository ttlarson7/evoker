using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using static UnityEditor.Progress;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.Experimental.GraphView;


public class LineManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject lineprefab;
    public wizardController wizard;
    
    List<Vector2> userDrawnSpell = new List<Vector2>();
    private List<GameObject> linesOnScreen = new List<GameObject>();
    private Line activeLine;
    private List<Line> activeLines = new List<Line>();
    private float timer;
    private bool isTiming;
    float maxDrawBoundary = Screen.height * .66f;
    // Update is called once per frame
    void Update()
    {
        Vector2 touchPosScreen = Input.mousePosition;
        if(touchPosScreen.y > maxDrawBoundary)
        {
            if (activeLine != null)
            {
                activeLines.Add(activeLine); // Save the active line
                activeLine = null;          // Clear the current active line
            }
            return;
        }
        //if (Input.GetMouseButton(0) && touchPosScreen.y < maxDrawBoundary && activeLine == null)
        //{
        //    // Start a new line when re-entering the drawable area
        //    GameObject newLine = Instantiate(lineprefab);
        //    activeLine = newLine.GetComponent<Line>();
        //    linesOnScreen.Add(newLine);
        //}
        //reset timer and start new line
        if (Input.GetMouseButton(0) && touchPosScreen.y < maxDrawBoundary)
        {
            
            timer = 0;
            if(activeLine == null)
            {
                GameObject newLine = Instantiate(lineprefab);
                activeLine = newLine.GetComponent<Line>();
                linesOnScreen.Add(newLine);
            }
            
        }
        //add line to total image, start timing
        if (Input.GetMouseButtonUp(0))
        {
            if (activeLine != null || activeLines != null)
            {
                for (int i = 0; i < activeLines.Count; i++)
                {
                    userDrawnSpell.AddRange(activeLines[i].GetPoints());
                }
                if(activeLine != null)
                {
                    userDrawnSpell.AddRange(activeLine.GetPoints());
                }
                
            }
            isTiming = true;
            activeLine = null;
        }

        //Update activeLine to current touch position
        if (activeLine != null)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(touchPos);
        }
        //If not drawing
        else
        {
            //if currently timeing, update timer
            if (isTiming)
            {
                timer += Time.deltaTime;

            }
        }

        //Stop timing and cast spell when timer > 5
        if (timer > 1)
        {
            wizard.startAnimation();
            isTiming = false;
            timer = 0;
            userDrawnSpell = processPoints(userDrawnSpell);
            //SaveSpell();
            LoadSpell();
            
            ClearLines();
            userDrawnSpell.Clear();
        }
    }

    private void ClearLines()
    {
        foreach (GameObject line in linesOnScreen)
        {
            if (line != null)
            {
                Destroy(line); // Destroy the line GameObject
            }
        }
        linesOnScreen.Clear(); // Clear the list of lines
    }

    private List<Vector2> centerPoints(List<Vector2> points)
    {
        int n = userDrawnSpell.Count;
        float centerX = points.Average(p => p.x);
        float centerY = points.Average(p => p.y);

        Vector2 center = new Vector2(centerX, centerY);
        List<Vector2> centerPoints = points.Select(p => p - center).ToList();
        return centerPoints;


    }

    private List<Vector2> normalizePoints(List<Vector2> points)
    {
        float maxDistance = points.Max(p => p.magnitude);
        return points.Select(p => p / maxDistance).ToList();
    }

    private List<Vector2> processPoints(List<Vector2> points)
    {
        points = centerPoints(points);
        points = normalizePoints(points);
        
        return points;
    }


    void SaveSpell()
    {
        if (userDrawnSpell.Count > 0)
        {
            List<Vector2> spellPoints = new List<Vector2>();
            print(Vector2.Distance(userDrawnSpell[0], userDrawnSpell[1]));
            spellPoints = centerPoints(userDrawnSpell);
            print(Vector2.Distance(spellPoints[0], spellPoints[1]));
            spellPoints = normalizePoints(spellPoints);
            print(Vector2.Distance(spellPoints[0], spellPoints[1]));
           
            //DisplaySpell(spellPoints);
            print(Vector2.Distance(spellPoints[0], spellPoints[1]));
            SpellData spellData = new SpellData { points = spellPoints };
            string json = JsonUtility.ToJson(spellData);

            string path = Application.persistentDataPath + "/wind.json";
            File.WriteAllText(path, json);

            Debug.Log("Spell saved to: " + path);
        }
    }
    void DisplaySpell(List<Vector2> spellPoints)
    {
        // Iterate over each individual spell (each list of Vector2 points)
        
        // Create a new GameObject to hold the LineRenderer for each spell
        GameObject spellLineObject = new GameObject("SpellVisualization");
        LineRenderer lineRenderer = spellLineObject.AddComponent<LineRenderer>();

        // Set LineRenderer properties
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = spellPoints.Count;


        // Assign points to the LineRenderer
        for (int i = 0; i < spellPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(spellPoints[i].x, spellPoints[i].y, 0)); // Z = 0 for 2D
        }

       
        
    }

    void LoadSpell()
    {

        string path = Application.persistentDataPath + "/spell.json";
        string path2 = Application.persistentDataPath + "/wind.json";
        string spell = "no cast";
        float maxSim = 0;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SpellData fireball = JsonUtility.FromJson<SpellData>(json);
            float similarity = compareSpells(fireball.points, userDrawnSpell);
            if (similarity > maxSim)
            {
                maxSim = similarity;
                spell = "fireball";
            }
            json = File.ReadAllText(path2);
            SpellData windSpell = JsonUtility.FromJson<SpellData>(json);
            similarity = compareSpells(windSpell.points, userDrawnSpell);
            if (similarity > maxSim)
            {
                maxSim = similarity;
                spell = "wind";
            }
            if (maxSim > 90)
            {
                StartCoroutine(WaitAndCast(spell));
                //if (spell == "fireball")
                //{
                //    gameManager.SummonFireball();
                //}else if (spell == "wind")
                //{
                //    gameManager.SummonWind();
                //}
            }

            print(maxSim);
            
            

        }
        else
        {
            Debug.LogWarning  ("Spell file not found at: " + path);
        }
    }
    private IEnumerator WaitAndCast(string spell)
    {
        yield return new WaitForSeconds(0.2f); // Wait for 0.5 seconds

        if (spell == "fireball")
        {
            gameManager.SummonFireball();
        }
        else if (spell == "wind")
        {
            gameManager.SummonWind();
        }
    }


    private float compareSpells(List<Vector2> definedSpell, List<Vector2> userDrawn)
    {
        // Step 1: Get the lengths of both sequences
        int len1 = definedSpell.Count;
        int len2 = userDrawn.Count;

        // Step 2: Initialize the DTW matrix with dimensions [len1, len2]
        float[,] dtw = new float[len1, len2];

        // Step 3: Initialize the first cell of the matrix (top-left corner)
        dtw[0, 0] = Vector2.Distance(definedSpell[0], userDrawn[0]);

        // Initialize the first column (i.e., matching with the user drawing)
        for (int i = 1; i < len1; i++)
            dtw[i, 0] = dtw[i - 1, 0] + Vector2.Distance(definedSpell[i], userDrawn[0]);

        // Initialize the first row (i.e., matching with the predefined drawing)
        for (int j = 1; j < len2; j++)
            dtw[0, j] = dtw[0, j - 1] + Vector2.Distance(definedSpell[0], userDrawn[j]);

        // Step 4: Fill the rest of the DTW matrix
        for (int i = 1; i < len1; i++)
        {
            for (int j = 1; j < len2; j++)
            {
                // Compute the cost (Euclidean distance) between the two points
                float cost = Vector2.Distance(definedSpell[i], userDrawn[j]);

                // Get the minimum cost of moving in one of the three directions
                dtw[i, j] = cost + Mathf.Min(dtw[i - 1, j], Mathf.Min(dtw[i, j - 1], dtw[i - 1, j - 1]));
            }
        }

        // Step 5: Normalize the score to 0-100 range
        // Calculate maximum possible distance
        float maxPossibleDistance = CalculateMaxPossibleDistance(definedSpell, userDrawn);

        // Normalize the DTW distance
        float normalizedScore = 100f * (1f - (dtw[len1 - 1, len2 - 1] / maxPossibleDistance));

        // Clamp the score between 0 and 100
        return Mathf.Clamp(normalizedScore, 0f, 100f);
    }

    // Helper method to calculate maximum possible distance
    private float CalculateMaxPossibleDistance(List<Vector2> definedSpell, List<Vector2> userDrawn)
    {
        // Calculate the maximum possible distance between the two sequences
        float maxDistance = 0f;

        // Find the maximum distance between points
        for (int i = 0; i < definedSpell.Count; i++)
        {
            for (int j = 0; j < userDrawn.Count; j++)
            {
                float distance = Vector2.Distance(definedSpell[i], userDrawn[j]);
                maxDistance = Mathf.Max(maxDistance, distance);
            }
        }

        // Scale the max distance by the total number of points
        return maxDistance * (definedSpell.Count + userDrawn.Count);
    }





}
public class SpellData
{
    public List<Vector2> points;
}