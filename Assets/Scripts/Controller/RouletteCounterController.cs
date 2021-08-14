using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteCounterController : MonoBehaviour
{
    Vector3 startPosition;

    [SerializeField] float speed;
    [SerializeField] float amplitude;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float x = amplitude * Mathf.Sin(Time.time * speed);
        transform.localPosition = startPosition + new Vector3(x, 0, 0);
    }
}
