using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkForestTile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
    }

}
