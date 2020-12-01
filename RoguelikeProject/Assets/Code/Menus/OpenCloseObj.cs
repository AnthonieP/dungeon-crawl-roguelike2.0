using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseObj : MonoBehaviour
{
    public GameObject turnOnObj;
    public GameObject turnOffObj;

    public void TurnOff()
    {
        turnOffObj.SetActive(false);
    }

    public void TurnOn()
    {
        turnOnObj.SetActive(true);
    }
}
