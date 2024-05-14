using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public float JumpHorizontalDistanceMultiplicator = 0.1f;
    public int JumpForce = 10;
    public int KickForce = 10;
    public KeyCode JumpKey = KeyCode.None;

    public Rigidbody2D Leg;
    private DirectionBehavior directionBehavior;
    public Direction PlayerDirection;

    public BoosterBehavior BoosterBehavior;

    private Rigidbody2D Me;

    private bool doJumpKick = false;

    private int nbFrameSinceJump = 0;

    private float screenCenterX;

    // Start is called before the first frame update
    void Start()
    {
        Me = GetComponent<Rigidbody2D>();
        directionBehavior = GameObject.FindObjectOfType<DirectionBehavior>();
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
            doJumpKick = true;
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
                    // if the touch position is to the right of center and I'm facing left
                    doJumpKick = currentTouch.position.x > screenCenterX && PlayerDirection == Direction.Left
                        // if the touch position is to the left of center and I'm facing right
                        || currentTouch.position.x < screenCenterX && PlayerDirection == Direction.Right;
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

            doJumpKick = false;
        }

        nbFrameSinceJump++;
    }

    private void Kick()
    {
        var sign = (PlayerDirection == Direction.Right ? 1 : -1);
        var myKickForce = KickForce * sign;
        Leg.rotation = 45f * sign;
        Leg.AddForce(new Vector2(myKickForce, KickForce));
    }

    private void Jump()
    {
        if (BoosterBehavior.Multiplicator == 1)
        {
            //Only Max Jumps reset velocity
            Me.velocity = new Vector2(0f, 0f);
        }

        var myBoostedJumpForce = JumpForce * BoosterBehavior.Multiplicator;
        Me.AddForce(new Vector2(directionBehavior.GetPosition() * myBoostedJumpForce, (1 - (Mathf.Abs(directionBehavior.GetPosition()) / 2)) * myBoostedJumpForce));

        BoosterBehavior.Boost();
    }
}
