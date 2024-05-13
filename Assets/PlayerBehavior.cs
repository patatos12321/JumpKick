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
        if (Application.platform == RuntimePlatform.Android)
        {
            StartAndroid();
        }
    }

    private void StartAndroid()
    {
        screenCenterX = Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Application.platform != RuntimePlatform.Android)
        {
            UpdateOthers();
        }
        else
        {
            UpdateAndroid();

        }
    }

    private void UpdateOthers()
    {
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
    }

    private void UpdateAndroid()
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
                        // if the touch position is to the right of center and I'm facing left
                        if (canJump)
                        {
                            doJumpKick = true;
                        }
                    }
                    else if (currentTouch.position.x < screenCenterX && PlayerDirection == Direction.Right)
                    {
                        // if the touch position is to the left of center and I'm facing right
                        if (canJump)
                        {
                            doJumpKick = true;
                        }
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            //Restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        if (doJumpKick)
        {
            Jump();
            Kick();

            canJump = false;
            doJumpKick = false;
        }

        nbFrameSinceJump++;

        if (nbFrameSinceJump > JumpDelay)
        { canJump = true; nbFrameSinceJump = 0; }
    }

    private void Kick()
    {
        var myKickForce = PlayerDirection == Direction.Right ? KickForce : -KickForce;
        Leg.AddForce(new Vector2(myKickForce, KickForce));
    }

    private void Jump()
    {
        Me.AddForce(new Vector2(DirectionBehavior.GetPosition() * JumpHorizontalDistanceMultiplicator * JumpForce, JumpForce));
    }
}
