using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    public Vector3 target;
    public float lerpSpeed;
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, lerpSpeed * Time.deltaTime);
    }
}
