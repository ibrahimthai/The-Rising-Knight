using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock1 : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Health.damageKey = 15;
            Debug.Log("You got hit by a rocks");
        }
    }
}
