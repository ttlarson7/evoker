using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject fireball;
    public Transform spawnPoint;
    

    

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    fireBall();
        //}
    }

    private void fireBall()
    {
        Vector3 spawnPos = spawnPoint.position;
        Instantiate(fireball, spawnPos, Quaternion.identity);
    }
}
