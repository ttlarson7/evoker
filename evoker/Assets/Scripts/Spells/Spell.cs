using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity;
    public float damage;
    public evilWizardHealth evilWizard;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(Vector2.up * velocity);
        SetStraightVelocity();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > 5.5f)
        {
            Destroy(gameObject);
        }

    }



    private void SetStraightVelocity()
    {
        rb.velocity = transform.up * velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("EvilWizard"))
        {
            print("HERE");
            evilWizard.health -= damage;
            Destroy(gameObject);
        }else if (other.gameObject.CompareTag("WhirlWind"))
        {
            Destroy(gameObject);
        }
    }
}
