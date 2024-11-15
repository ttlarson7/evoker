using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject drawPrefab;
    private GameObject theTrail;
    Plane planeObj;
    Vector3 startPos;
    private TrailRenderer trailRender;
    void Start()
    {
        planeObj = new Plane(Camera.main.transform.position, this.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            theTrail = (GameObject)Instantiate(drawPrefab, this.transform.position, Quaternion.identity);
            trailRender = theTrail.GetComponent<TrailRenderer>();
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (planeObj.Raycast(mouseRay, out _dis))
            {
                startPos = mouseRay.GetPoint(_dis);
            }


        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (planeObj.Raycast(mouseRay, out _dis))
            {
                theTrail.transform.position = mouseRay.GetPoint(_dis);
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
        {
            if (trailRender != null)
            {
                
            }
        }

    }
}
