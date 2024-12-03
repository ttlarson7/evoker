using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject fireball;
    public GameObject wind;
    public Transform spawnPoint;
    public evilWizardHealth evilWizard;

    

    // Update is called once per frame
    void Update()
    {
    }

    public void SummonFireball()
    {
        Vector3 spawnPos = spawnPoint.position;
        GameObject fireBall = Instantiate(fireball, spawnPos, Quaternion.identity);
        Spell spellScript = fireBall.GetComponent<Spell>();
        if (spellScript != null)
        {
            spellScript.evilWizard = evilWizard;
        }
    }

    public void SummonWind()
    {
        Vector3 spawnPos = spawnPoint.position;
        Instantiate(wind, spawnPos, Quaternion.identity);
    }
}
