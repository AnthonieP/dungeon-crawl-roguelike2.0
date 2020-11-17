using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public UnityEvent m_Event;

    public void PlayEvent()
    {
        m_Event.Invoke();
    }

    public void TestEvent()
    {
        print("TestEvent");
    }
}
