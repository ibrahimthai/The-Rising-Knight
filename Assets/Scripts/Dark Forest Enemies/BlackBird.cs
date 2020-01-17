using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Transform target;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If there's no lunge
        if (LungeAttack.lungeKey == 0)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 10;
                ExplosionEffect();
            }
        }
        // If there's a lunge
        else if (LungeAttack.lungeKey == 1)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 0;
                ExplosionEffect();
            }
        }

        // Resets the lunge key after a lunge is performed
        LungeAttack.lungeKey = 0;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        Health.energyKey = 1;
        ExplosionEffect();
    }

    public void ExplosionEffect()
    {
        GameObject e = Instantiate(explosion) as GameObject;
        e.transform.position = transform.position;
        Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

}
