using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZone : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer mainR;
    private void OnMouseEnter()
    {
        mainR.material.color = Color.green;
    }

    private void OnMouseExit()
    {
        mainR.material.color = Color.red;
    }
}
