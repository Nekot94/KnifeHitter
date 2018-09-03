using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // скорость вращения. Можно менять в Unity потому что public
    public float rotationSpeed = 10;

    AudioSource source; // источник звука

    Color startColor;
    SpriteRenderer sprite;

    // Выполняется в начале игры
	void Start ()
    {
        // Debug.Log("Раз");
        source = GetComponent<AudioSource>(); // получить компонент звука

        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
	}
	
    // Выполняется каждый кадр
	void Update ()
    {
        // Debug.Log("Раз Раз");
        // Вращает объект
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

    }

    public void SelfDestruction()
    {
        GameObject[] knifesObjects = GameObject.FindGameObjectsWithTag("Knife"); // найти все ножи
        // Для каждого ножа
        foreach (var o in knifesObjects)
        {
            o.transform.SetParent(null);

            var r = o.GetComponent<Rigidbody2D>();
            r.gravityScale = 1; // включить гравитацию
            Vector2 dir = r.position - (Vector2)transform.position;
            r.AddForce(dir * 100);

            var k = o.GetComponent<Knife>(); // получить компонент ножа
            k.speed = 0; // выключить скорость
        }


        SpriteRenderer[] pices = GetComponentsInChildren<SpriteRenderer>();

        // откинуть каждый кусок
        foreach (var o in pices)
        {
            var r = o.GetComponent<Rigidbody2D>();
            r.bodyType = RigidbodyType2D.Dynamic;
            r.gravityScale = 1; // включить гравитацию
            Vector2 dir = r.position - (Vector2) transform.position;
            r.AddForce( 300 * new Vector2(Random.Range(-5,5), Random.Range(-5,5)));

            o.transform.SetParent(null);


        }

        source.Play(); // проиграть звук

        Destroy(gameObject.GetComponent<SpriteRenderer>());

    }
        
    // Эффект при попаднии
    public void ChangeAfterHit()
    {
        StartCoroutine(HitChange());
    }

    IEnumerator HitChange()
    {
        sprite.color += new Color(0.6f,0.6f, 0.6f);
        transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);
        yield return new WaitForSeconds(0.03f);
        sprite.color = startColor;
        transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);


    }

}
