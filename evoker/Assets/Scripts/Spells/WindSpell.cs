using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity;
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

        if (other.gameObject.CompareTag("FireBall"))
        {
            print("HERE");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        
        
    }
}
