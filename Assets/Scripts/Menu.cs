using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text highscoresText;

    void Start()
    {
        highscoresText.text = "Рекорд: " + Highscores.instance.GetHighscores();
    }

    // меняет сцену с указаным именем
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
