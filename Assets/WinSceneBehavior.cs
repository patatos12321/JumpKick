using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneBehavior : MonoBehaviour
{
    public Text ProudText;
    private Stopwatch sw = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {
        if (GameBehavior.ScoredDirection == Direction.Right)
        {
            ProudText.color = Color.blue;
        }
        else
        {
            ProudText.color = Color.red;
        }
        sw.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (sw.ElapsedMilliseconds > 10000)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
