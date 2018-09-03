using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    // скорость ножа
    public float speed = 20;

    // метнкть нож только если true
    bool isGo;

    bool deactivate;

    // звуки
    public AudioSource source; // источник звука
    public AudioClip wood; // нож втыкается
    public AudioClip metal; // нож попадает в нож


	void Update ()
    {
        // если isGo true
        if (isGo)
            // Пепремещает объект
            transform.Translate(0, speed * Time.deltaTime, 0);

        // Если нажата левая кнопка мыши, то можно метать нож
        if (Input.GetMouseButtonDown(0))
            isGo = true;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если тэг равен Circle и нож не неактивен
        if (!deactivate && collision.gameObject.tag == "Circle")
        {
            speed = 0; // скорость равна 0
            transform.SetParent(collision.transform); // делаем его родителем круг
            KnifeSpawner.instance.Spawn(); // спавним новый нож

            GameController.instance.ChangeKnifes(-1); // уменьшить колличество ножей
            GameController.instance.AddScores(1); // увеличить колличество очков

            deactivate = true; // запретить снова сталкиваться с кругом 

            GameController.instance.ChangeSpeed(); // изменить скорость при попадании

            source.clip = wood; // выбираем звук 
            source.Play(); // проигрываем звук
        }

        // если столкнулся с другим ножом
        if (!deactivate && collision.gameObject.tag == "Knife")
        {
            speed = 0; // отключаем скорость
            GetComponent<Rigidbody2D>().gravityScale = 1; // включаем гравитацию
            deactivate = true;
            GameController.instance.Lose(); // проиграть

            source.clip = metal; // выбираем звук 
            source.Play(); // проигрываем звук
        }
    }
}
