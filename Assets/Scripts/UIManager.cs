using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    SceneField levelToStart;
    int currentIndex = 0;
    public Level[] levels;

    [SerializeField]
    Image lvlImg;
    [SerializeField]
    TextMeshProUGUI lvlName;

    public void Start() {

    }

    public void StartLevel() {
        SceneManager.LoadScene(levelToStart);
    }

    public void Quit() {
        Application.Quit();
    }

}

[System.Serializable]
public struct Level {
    public string name;
    public Sprite screen;
    public SceneField scene;
}
