using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        transform.localPosition = new Vector3(x, y, originalPos.z);

        yield return null;
        Debug.Log("successful");
    }
}
