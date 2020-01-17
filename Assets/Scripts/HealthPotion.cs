using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    //public GameObject healEffect;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //HealEffect();
            Health.damageKey = 1;
            Debug.Log("You got a health potion: 20+");
            this.gameObject.SetActive(false);
        }
            
    }

    /*
    private void HealEffect()
    {
        if (healEffect != null)
        {
            GameObject effect = Instantiate(healEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
    */
}
