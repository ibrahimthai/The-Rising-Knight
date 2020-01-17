using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject explosion;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        //screenBounds = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If there's no lunge
        if (LungeAttack.lungeKey == 0)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 20;
                Debug.Log("You got hit by Skeleton");
            }
        }
        // If there's a lunge
        else if (LungeAttack.lungeKey == 1)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 0;
                Health.energyKey = 1;
                Debug.Log("You lunged the Skeleton");
                DeathEffect();
                this.gameObject.SetActive(false);
            }
        }

        // Reset Key so player can also slash if they want to
        LungeAttack.lungeKey = 0;
    }

    public void TakeDamage()
    {
        DeathEffect();
        Health.energyKey = 1;
        this.gameObject.SetActive(false);
    }

    private void DeathEffect()
    {
        if (explosion != null)
        {
            GameObject effect = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }





}
