using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    // Start is called before the first frame update
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -groundHorizontalLength) //If one of our objects have scrolled to its full length to the left, then it will be ready to reposition.
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 3f, 0);//Leapfrogs the new ground onto the other side.
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
