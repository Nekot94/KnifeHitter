using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Интерфейсы
using UnityEngine.SceneManagement; // Управление сценами

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int scores; // очки
    public int knifes = 6; // колличество ножей

    float timeToRestart = 1.5f; // время до рестарта

    public GameObject losePanel; // панель которая появится при проигрыше;
    public Text scoresText; // текст для очков
    public Text knifesText; // текст для колличества ножей

    static float speedModifier; // на сколько увеличить скорость каждый уровень 
    public float speedModifierGrowth = 20; // на сколько увеличить модификатор скорости каждый уровень 
    Circle circle;

    AudioSource source; // источник звука
    //public AudioClip win; // победа
    public AudioClip lose; // поражение

    void Start()
    {
        instance = this;

        // случайное колличество ножей для победы
        knifes = Random.Range(knifes, knifes * 2);
        knifesText.text = knifes.ToString();
        scoresText.text = "Очки: " + scores;

        circle = GameObject.FindGameObjectWithTag("Circle").GetComponent<Circle>();
        circle.rotationSpeed += speedModifier;

        source = GetComponent<AudioSource>(); // Получить компонент звука
    }

    // добавляет очки и меняет текст
    public void AddScores(int value)
    {
        scores += value;
        scoresText.text = "Очки: " + scores;
        Highscores.instance.AddHighscores(scores); // запись рекорда

    }


    // Уменьшает колличество ножей при попадании и запускает победу
    public void ChangeKnifes(int value)
    {
        knifes += value;
        knifesText.text = knifes.ToString();
        if (knifes == 0)
            Win();
        else
            circle.ChangeAfterHit();
    }

    // Проиграть
    public void Lose()
    {
        scores = 0;
        speedModifier = 0;
        losePanel.SetActive(true);     // Показать панель при поражении

        source.clip = lose; // выбираем звук 
        source.Play(); // проигрываем звук
    }

    // Победить
    void Win()
    {
        StartCoroutine(RestartTillTime()); // Запуск сопрограммы победы


        circle.SelfDestruction(); // взорвать сферу и откинуть ножи

        //увеличиваем модификатор скорости
        speedModifier += speedModifierGrowth;

        //source.clip = win; // выбираем звук 
        //source.Play(); // проигрываем звук

    }

    // Перезагрузить уровень
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Перезагрузить через определенное время
    IEnumerator RestartTillTime()
    {
        yield return new WaitForSeconds(timeToRestart);
        Restart();
    }


    public void ChangeSpeed()
    {
        float changeValue = Random.Range(0, 2) > 0 ? speedModifierGrowth / 2: -speedModifierGrowth / 2;
        circle.rotationSpeed += changeValue;
        circle.rotationSpeed = Random.Range(0, 5) > 0 ? circle.rotationSpeed : -circle.rotationSpeed;
        if (circle.rotationSpeed == 0)
            circle.rotationSpeed = speedModifierGrowth;
    }


    // меняет сцену с указаным именем
    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
