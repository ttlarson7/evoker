using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private TrailRenderer trailRender;
    void Start()
    {
        trailRender = GetComponent<TrailRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {

        if(trailRender != null && trailRender.positionCount == 0)
        {
            Destroy(gameObject);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            if (trailRender != null)
            {
                trailRender.material.color = Color.red;
                int numPositions = trailRender.positionCount;

                Vector3[] positions = new Vector3[numPositions];

                trailRender.GetPositions(positions);

                foreach (Vector3 pos in positions)
                {
                    print(pos);
                }
            }
        }
    }
}
