using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public static int lungeKey = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb = this.GetComponent<Rigidbody2D>();

        if (collision.gameObject.name == "Black Bird(Clone)")
        {
            //lungeKey = 1;
            Debug.Log("BLACK BIRD!!");
            ScoreScript.scoreValue += 1;
        }
        else if (collision.gameObject.name == "frog(Clone)")
        {
            //lungeKey = 1;
            Debug.Log("FROG!!");
            ScoreScript.scoreValue += 1;
        }
        else if (collision.gameObject.name == "eagle(Clone)")
        {
            //lungeKey = 1;
            Debug.Log("EAGLE!!");
            ScoreScript.scoreValue += 1;
        }
        else if (collision.gameObject.name == "Skeleton(Clone)")
        {
            //lungeKey = 1;
            Debug.Log("SKELETON!!");
            ScoreScript.scoreValue += 1;
        }
        else if (collision.gameObject.name == "Plant Eater Trap(Clone)")
        {
            //lungeKey = 1;
            Debug.Log("PLANT EATER!!");
        }

    }
}
