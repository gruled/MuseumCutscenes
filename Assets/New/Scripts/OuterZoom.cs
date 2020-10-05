using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterZoom : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float FOVMax;
    [SerializeField] private float FOVMin;
    private float _prevDist;
    
    void OnEnable()
    {
        _camera = transform.GetComponent<Camera>();
        _camera.fieldOfView = (FOVMax + FOVMin) / 2.0f;
        StartCoroutine(_zoom());
    }

    private IEnumerator _zoom()
    {
        while (true)
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                if (touch0.phase==TouchPhase.Began||touch1.phase==TouchPhase.Began)
                {
                    _prevDist = Vector2.Distance(touch0.position, touch1.position);
                }
                else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    float dist = Vector2.Distance(touch0.position, touch1.position);
                    if (dist < _prevDist)
                    {
                        if (_camera.fieldOfView < FOVMax)
                        {
                            _camera.fieldOfView += _speed;
                        }
                    }
                    else
                    {
                        if (_camera.fieldOfView > FOVMin)
                        {
                            _camera.fieldOfView -= _speed;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

}
