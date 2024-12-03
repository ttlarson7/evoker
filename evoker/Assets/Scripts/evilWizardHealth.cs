using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class evilWizardHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxHealth;
    public Image HealthBar;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

    }
}
