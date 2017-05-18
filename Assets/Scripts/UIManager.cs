using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    SceneField levelToStart;

    public void StartLevel() {
        SceneManager.LoadScene(levelToStart);
    }

    public void Quit() {
        Application.Quit();
    }

}
