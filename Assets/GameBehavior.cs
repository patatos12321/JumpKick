using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBehavior : MonoBehaviour
{
    public string DisplayScoreRight => scoreRight.ToString();
    public Text TextScoreRight;
    public string DisplayScoreLeft => scoreLeft.ToString();
    public Text TextScoreLeft;

    public static bool IsNewGame = false;

    private static int scoreLeft = 0;
    private static int scoreRight = 0;

    public static Direction ScoredDirection => scoredDirection;
    private static Direction scoredDirection = Direction.Left;

    void Start()
    {
        if (IsNewGame)
        {
            scoreLeft = 0;
            scoreRight = 0;
            IsNewGame = false;
        }

        TextScoreRight.text = DisplayScoreRight;
        TextScoreLeft.text = DisplayScoreLeft;
    }

    public void Score(Direction direction)
    {
        if (direction == Direction.Right) 
        { 
            scoreLeft++;
        }
        else
        {
            scoreRight++;
        }
        scoredDirection = direction;

        if (scoreLeft >=3 || scoreRight >= 3)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            SceneManager.LoadScene("Goal");
        }
    }
}
