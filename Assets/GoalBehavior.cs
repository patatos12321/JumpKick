using UnityEngine;

public class GoalBehavior : MonoBehaviour
{
    public Direction Side;
    private GameBehavior GameBehavior;

    private void Start()
    {
        GameBehavior = GameObject.FindObjectOfType<GameBehavior>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Ball")
        {
            GameBehavior.Score(Side);
        }
    }
}
