using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KnightPlayer : MonoBehaviour
{
    public float jumpForce = 200;
    public Collider2D attackTrigger;
    private bool isGrounded; //This makes it so that the knight can't only jump once.
    private bool isDead = false;
    private bool notDashing;

    private Animator anim; //We need to establish the variables anim and thisKnight out here so all functions can use them.
    private Rigidbody2D thisKnight;

    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;

    // Enemy Layers (List all enemies here)
    public LayerMask FrogEnemies;
    public LayerMask EagleEnemies;
    public LayerMask SkeletonEnemies;
    public LayerMask BlackBirdEnemies;

    public int damage;
    public bool timerRunning = true;
    public float dashUndo = -9.8f;
    public Collider2D lungeHitbox;
    
    // Start is called before the first frame update
    void Start()
    {
        thisKnight = GetComponent<Rigidbody2D>(); //Checks the game object that this script is attached to whether it has Rigidbody2d component/
        //Essentially, the above piece of code lets us modify whatever it is attached to by acting on the this Knight variable we created.
        anim = GetComponent<Animator>(); //Same thing with the animator here. anim.SetTrigger to make an animation play. Good for attacking.
        if (anim == null)
        {
            Debug.Log("Error: Did not find an Animator!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        thisKnight.AddForce(new Vector2(-9.8f, 0), ForceMode2D.Force);

        if (isDead == false) //PLACE ALL PLAYER CONTROL UNDER THIS STATEMENT. It means that the Knight is not dead, and should respond to player input
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Jump command. Checks if the Knight is on the ground. If he is, then it sets grounded to false
            {
                if (isGrounded == true)
                {
                    isGrounded = false;
                    thisKnight.velocity = Vector2.zero;//Everytime you jump the velocity is reset to zero, therefore any action after this is the same.
                    thisKnight.AddForce(new Vector2(0, jumpForce));
                }
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                // Do the Slash Animation
                anim.SetTrigger("Slash");
                EnemiesToAttack();
            }
            else if (Input.GetKeyDown(KeyCode.K)) //This is the Lunge button
            {
                if (notDashing == true)
                {
                    anim.SetTrigger("Lunge");

                    // Makes the lunge hitbox open
                    lungeHitbox.enabled = true;
                    thisKnight.velocity = Vector2.zero;//Everytime you jump the velocity is reset to zero, therefore any action after this is the same.
                    thisKnight.AddForce(new Vector2((jumpForce * 1.5f), 0)); //The Knight dashes forward about as strong as a jump.                    
                    notDashing = false;

                }
            }

        }
    }
     
    void OnCollisionEnter2D (Collision2D collidedObject) //Scan for the object that the Knight collided with. This distinguishes between the ground and the thing behind the Knight.
    {
        //Debug.Log("Knight colllided with " + collidedObject.gameObject.name);
        if (collidedObject.gameObject.name == "Ground1")
        {
            isGrounded = true;
        }
        else if (collidedObject.gameObject.name == "Ground2")
        {
            isGrounded = true;
        }
        else if (collidedObject.gameObject.name == "Ground3")
        {
            isGrounded = true;
        }
        else if (collidedObject.gameObject.name == "DarkForestTile")
        {
            isGrounded = true;
        }
        if (collidedObject.gameObject.name == "KnightPositionReset")
        {
            notDashing = true;
        }

    }

    public void EnemiesToAttack()
    {
        // Plains Enemies
        Collider2D[] frogsToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, FrogEnemies);
        Collider2D[] eaglesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EagleEnemies);

        // Dark Forest Enemies
        Collider2D[] skeletonToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, SkeletonEnemies);
        Collider2D[] blackBirdToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, BlackBirdEnemies);

        // IBRAHIM: Damage for Frogs. If damaged, gain one point
        for (int i = 0; i < frogsToDamage.Length; i++)
        {
            frogsToDamage[i].GetComponent<frog>().TakeDamage();
            ScoreScript.scoreValue += 1;
        }

        // IBRAHIM: Damage for Eagles. If damaged, gain one point
        for (int i = 0; i < eaglesToDamage.Length; i++)
        {
            eaglesToDamage[i].GetComponent<eagle>().TakeDamage();
            ScoreScript.scoreValue += 1;
        }

        // IBRAHIM: Damage for Skeleton. If damaged, gain one point
        for (int i = 0; i < skeletonToDamage.Length; i++)
        {
            skeletonToDamage[i].GetComponent<Skeleton>().TakeDamage();
            ScoreScript.scoreValue += 1;
        }

        // IBRAHIM: Damage for Skeleton. If damaged, gain one point
        for (int i = 0; i < blackBirdToDamage.Length; i++)
        {
            blackBirdToDamage[i].GetComponent<BlackBird>().TakeDamage();
            ScoreScript.scoreValue += 1;
        }
    }

    /* IBRAHIM: Draws out the collision circle area where the eagle or frogs should land on for a point */
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    
    public void disableLungeHitbox ()
    {
        lungeHitbox.enabled = false;
    }

}
