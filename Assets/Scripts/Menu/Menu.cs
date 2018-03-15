
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Speech");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
