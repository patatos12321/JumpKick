using UnityEngine;

public class GoalBehavior : MonoBehaviour
{
    public Direction Side;
    private GameBehavior GameBehavior;
    public Rigidbody2D OwnPlayer;

    private void Start()
    {
        GameBehavior = GameObject.FindObjectOfType<GameBehavior>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && (collision.gameObject.tag == "Ball" || collision.rigidbody == OwnPlayer))
        {
            GameBehavior.Score(Side);
        }
    }
}
