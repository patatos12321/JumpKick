using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalSceneBehavior : MonoBehaviour
{
    public Text GoalText;
    private Stopwatch sw = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        var sb = new StringBuilder();
        var rand = new System.Random();

        for (int i = 0; i < 100; i++)
        {
            var nb = rand.Next(10);
            sb.Append('G',nb);
            nb = rand.Next(10);
            sb.Append('O', nb);
            nb = rand.Next(10);
            sb.Append('A', nb);
            nb = rand.Next(10);
            sb.Append('L', nb);
        }

        if (GameBehavior.ScoredDirection == Direction.Right)
        {
            GoalText.color = Color.blue;
        }
        else
        {
            GoalText.color = Color.red;
        }
        GoalText.text = sb.ToString();
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (sw.ElapsedMilliseconds > 3000)
        {
            SceneManager.LoadScene("1v1");
        }
    }
}
