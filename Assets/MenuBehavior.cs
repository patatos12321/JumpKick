using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("1v1");
    }
}