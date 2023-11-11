using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer mainR;

    private void Start()
    {
        mainR = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        transform.position = mousePosition;
    }
}
