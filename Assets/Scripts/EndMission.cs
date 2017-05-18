using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMission : MonoBehaviour {

    [SerializeField]
    SceneField titleScreen;
    public GameObject winScreen, loseScreen;

    void Start() {
        GameManager.instance.endMission = gameObject;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        gameObject.SetActive(false);
    }
    
    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TitleScreen() {
        SceneManager.LoadScene(titleScreen);
    }

    public void Quit() {
        Application.Quit();
    }
}
