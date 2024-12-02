using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject fireball;
    public GameObject wind;
    public Transform spawnPoint;
    

    

    // Update is called once per frame
    void Update()
    {
    }

    public void SummonFireball()
    {
        Vector3 spawnPos = spawnPoint.position;
        Instantiate(fireball, spawnPos, Quaternion.identity);
    }

    public void SummonWind()
    {
        Vector3 spawnPos = spawnPoint.position;
        Instantiate(wind, spawnPos, Quaternion.identity);
    }
}
