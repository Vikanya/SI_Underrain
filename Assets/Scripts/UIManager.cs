using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField]
    SceneField titleSceen;

    int currentIndex = 0;
    public Level[] levels;

    [SerializeField]
    Image lvlImg;
    [SerializeField]
    TextMeshProUGUI lvlName;
    SceneField levelToStart;
    
    public void Start() {
        SelectLevel(currentIndex);
    }

    void SelectLevel(int index) {
        if (index >= levels.Length)
            index = 0;
        else if (index < 0)
            index = levels.Length - 1;

        currentIndex = index;

        if (levels[currentIndex].screen)
            lvlImg.sprite = levels[currentIndex].screen;

        lvlName.text = levels[currentIndex].name;
        levelToStart = levels[currentIndex].scene;
    }

    public void NextLevel() {
        SelectLevel(currentIndex + 1);
    }

    public void PreviousLevel() {
        SelectLevel(currentIndex - 1);
    }

    public void StartLevel() {
        SceneManager.LoadScene(levelToStart);
    }

    public void TitleScreen() {
        SceneManager.LoadScene(titleSceen);
    }

    public void Quit() {
        Application.Quit();
    }

    void OnValidate() {
        if (levels.Length > 0) {
            if (levels[levels.Length - 1].scene != null && levels[levels.Length-1].name == "") {
                levels[levels.Length - 1].name = levels[levels.Length - 1].scene;
            }
        }
    }
}

[System.Serializable]
public struct Level {
    public string name;
    public Sprite screen;
    public SceneField scene;
}
