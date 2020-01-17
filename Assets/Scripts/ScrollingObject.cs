using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour{

    private Rigidbody2D rb2d;

    //THIS BABY IS THE CONTROLLER FOR ALL SCROLLING OBJECTS

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //Reference is stored in this variable
        rb2d.velocity = new Vector2(GameControl.loneInstance.scrollSpeed, 0); //Where is scroll speed? It's located in GameControl, so all scrolling objects can easily access the scrolling speed.
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.loneInstance.gameOver == true) //If we gameOver, then we should probably stop scrolling.
        {
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            rb2d.velocity = new Vector2(GameControl.loneInstance.scrollSpeed, 0); //Constantly update the scrolling speed if the speed changes
        }
    }
}
