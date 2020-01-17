using UnityEngine;

/* This class creates an eagle prefab including the speed, damage, and collision */
public class eagle : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject explosion;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If there's no lunge
        if (LungeAttack.lungeKey == 0)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 10;
                Debug.Log("You got hit by Eagle");
            }
        }
        // If there's a lunge
        else if (LungeAttack.lungeKey == 1)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 0;
                Health.energyKey = 1;
                ExplosionEffect();
            }
        }

        // Reset Key so player can also slash if they want to
        LungeAttack.lungeKey = 0;
    }

    public void TakeDamage()
    {
        Debug.Log("You killed the Eagle");
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
