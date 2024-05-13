using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public void Play()
    {
        GameBehavior.IsNewGame = true;
        SceneManager.LoadScene("1v1");
    }
}
