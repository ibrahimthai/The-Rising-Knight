using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEater : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

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
                Debug.Log("You got hit by Plant Eater");
            }
        }
        // If there's a lunge
        else if (LungeAttack.lungeKey == 1)
        {
            if (collision.gameObject.name == "Player")
            {
                Health.damageKey = 0;
            }
        }

        // Reset Key so player can also slash if they want to
        LungeAttack.lungeKey = 0;
    }
}
