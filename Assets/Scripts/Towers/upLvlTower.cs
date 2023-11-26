using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upLvlTower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject towerNext;

    public GameObject towerNow;

    public void towerUP()
    {
        Instantiate(towerNext, towerNow.transform.position, Quaternion.identity);
        Destroy(towerNow);
    }
}
