using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TRACKS THE STATE OF THE GAME AND MAKES IT AVAILABLE FOR OTHER SCRIPTS.

public class GameControl : MonoBehaviour
{
    public static GameControl loneInstance; //loneInstance instead of doing static variables, we can hit up GameControl.loneInstance to do references and changes.
    public GameObject gameOverText;
    public GameObject StartText;
    //public Text scoreText;
    public bool gameOver = false;

    public float scrollSpeed = -1.5f; //THIS IS THE SCROLLING SPEED

    private int score = 0;

    // Start is called before the first frame update
    void Awake() //Awake? Awake means whatever is in this block runs even before start.
    {
        if (loneInstance == null) 
        {
            loneInstance = this;
        }
        else if (loneInstance != this) //If there is another instance of GameControl around
        {
            Destroy(gameObject); //Oh, there's already another guy active. Let's destroy ourselves.
        }
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Time.timeScale == 0 && Input.anyKey)
        {
            StartText.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void KnightScored()
    {
        if (gameOver == true) //Game's over, you can't score
            return;
        ++score;
        //scoreText.text = "Score " + score.ToString();   
    }

    public void KnightDied() //Call this boy
    {
        //gameOverText.SetActive(true); //This reveals the game over text that was always there, but hidden the whole time.
        //gameOver = true;
    }
}
