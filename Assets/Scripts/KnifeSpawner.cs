using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    // нож для спавна
    public GameObject knife;
    // ссылка на объект скрипта
    public static KnifeSpawner instance;

    void Start()
    {
        instance = this; // записываем этот же объект в начале игры    
    }


    public void Spawn()
    {
        // спавним нож на своей позиции
        Instantiate(knife, transform.position, transform.rotation); 
    }

}
