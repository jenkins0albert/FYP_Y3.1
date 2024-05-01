using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;


public class Headbob : MonoBehaviour
{

    [SerializeField] private bool _enable = true; //camera shake -Adrian
    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f; //amplitude controls size -Adrian
    [SerializeField, Range(0, 30)] private float _frequency = 10f; //frequency controls speed -Adrian
    [SerializeField, Range(0, 30)] private float _smoothness = 10f; //frequency controls speed -Adrian

    Vector3 StartPos;

    private void Start()
    {
        StartPos = Vector3.zero;
    }

    private void Update()
    {
        CheckHeadbob();
        StopHeadbob();
    }

    private void CheckHeadbob()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (inputMagnitude > 0)
        {
            StartHeadbob();
            
        }
    }

    private Vector3 StartHeadbob()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * _frequency) * _amplitude * 1.4f, _smoothness * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * _frequency / 2f) * _amplitude * 1.6f, _smoothness * Time.deltaTime);
        transform.localPosition += pos;



        return pos;
    }

    private void StopHeadbob()
    {

        if (transform.localPosition == StartPos) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);

    }


}
