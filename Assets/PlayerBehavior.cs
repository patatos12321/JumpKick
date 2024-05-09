using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float JumpHorizontalDistanceMultiplicator = 0.1f;
    public int JumpForce = 10;
    public int KickForce = 10;
    public int JumpDelay = 20;
    public KeyCode JumpKey = KeyCode.None;

    public Rigidbody2D Leg;
    public DirectionBehavior DirectionBehavior;
    public Direction PlayerDirection;

    private Rigidbody2D Me;

    private bool canJump = true;
    private bool doJumpKick = false;

    private int nbFrameSinceJump = 0;

    private float screenCenterX;

    // Start is called before the first frame update
    void Start()
    {
        Me = GetComponent<Rigidbody2D>();
        screenCenterX = Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch currentTouch = Input.GetTouch(i);

                // if it began this frame
                if (currentTouch.phase == TouchPhase.Began)
                {
                    if (currentTouch.position.x > screenCenterX && PlayerDirection == Direction.Left)
                    {
                        // if the touch position is to the right of center
                        // move right
                        if (canJump)
                        {
                            doJumpKick = true;
                        }
                    }
                    else if (currentTouch.position.x < screenCenterX && PlayerDirection == Direction.Right)
                    {
                        // if the touch position is to the left of center
                        // move left
                        if (canJump)
                        {
                            doJumpKick = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyUp(JumpKey))
        {
            if (canJump)
            {
                doJumpKick = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Insert Code Here (I.E. Load Scene, Etc)
                // OR Application.Quit();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void FixedUpdate()
    {
        if (doJumpKick)
        {
            Jump();
            Kick();

            canJump = false;
            doJumpKick= false;
        }

        nbFrameSinceJump++;

        if (nbFrameSinceJump > JumpDelay) 
        { canJump = true; nbFrameSinceJump = 0; }
    }

    private void Kick()
    {
        if (PlayerDirection == Direction.Right)
        {
            Leg.AddForce(new Vector2(KickForce, KickForce));
        }
        else // LEFT
        {
            Leg.AddForce(new Vector2(-KickForce, KickForce));
        }
    }

    private void Jump()
    {
        Me.AddForce(new Vector2(DirectionBehavior.GetPosition() * JumpHorizontalDistanceMultiplicator * JumpForce, JumpForce));
    }
}
