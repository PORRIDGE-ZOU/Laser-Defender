using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMagnitude = .5f;

    Vector3 initialPosition;

    void Start()
    {

        initialPosition = transform.position;
        
    }


    public void Play()
    {
        StartCoroutine(Shake());
    }


    IEnumerator Shake()
    {

        float elapsedTime = 0;
        while(elapsedTime < shakeDuration)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        transform.position = initialPosition;

    }

    
}
