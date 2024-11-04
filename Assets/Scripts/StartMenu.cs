using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void LoadScene(int idx) => SceneManager.LoadScene(idx);
    public void Exit() => Application.Quit();
}
