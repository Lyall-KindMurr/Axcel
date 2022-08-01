using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public float parallaxFactor;
    public GameObject cam;

    void Start()
    {
        cam = Camera.main.gameObject;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;

        Vector2 newPosition = new Vector2(startpos + distance, transform.position.y);

        transform.position = newPosition;

        if (temp > startpos + (length / 2)) startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }
}

/*
{
public Transform subject;

Vector2 startPosition;
[Range(-1.0f, 1.0f)]
public float parallax = 0.0f;

Vector2 travel => (Vector2)subject.transform.position - startPosition;


private void Start()
{
    startPosition = transform.position;
}

// Update is called once per frame
void Update()
{
    Vector2 newPos = new Vector2(startPosition.x + travel.x * parallax, startPosition.y);
    transform.position = newPos;
}
}
*/
