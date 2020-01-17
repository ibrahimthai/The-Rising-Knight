using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject explosion;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Eagle collided with + " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            Health.damageKey = 20;
        }
        else if (collision.gameObject.name == "Ground1" || collision.gameObject.name == "Ground2" || collision.gameObject.name == "Ground3")
        {
            RockfallEffect();
        }
    }

    private void RockfallEffect()
    {
        if (explosion != null)
        {
            GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

}
