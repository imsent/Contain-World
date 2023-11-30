using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    // Start is called before the first frame update
    public void back()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }
}
