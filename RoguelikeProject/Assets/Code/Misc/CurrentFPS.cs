using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentFPS : MonoBehaviour
{
    public Text fpsText;
    public float fpsUpdate;
    float fpsUpdateTime;

    private void Update()
    {
        fpsUpdateTime -= Time.deltaTime;
        if(fpsUpdateTime < 0)
        {
            fpsUpdateTime = fpsUpdate;
            fpsText.text = (1f / Time.unscaledDeltaTime).ToString("#");
        }
    }
}
