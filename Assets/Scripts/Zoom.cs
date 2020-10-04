using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Coroutine _coroutine;
    private float _prevDist;
    private Camera _camera;
    public float speed;
    public float FOVMax;
    public float FOVMin;

    public void _run()
    {
        _camera = Manager.GetInstance()._camera;
        _coroutine = StartCoroutine(_activate());
    }

    public void _stop()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator _activate()
    {
        
        while (true)
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    _prevDist = Vector2.Distance(touch0.position, touch1.position);
                }

                if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    float currentDist = Vector2.Distance(touch0.position, touch1.position);
                    if (currentDist < _prevDist)
                    {
                        if (_camera.fieldOfView < FOVMax)
                        {
                            _camera.fieldOfView += speed;
                        }
                    }

                    if (currentDist > _prevDist)
                    {
                        if (_camera.fieldOfView > FOVMin)
                        {
                            _camera.fieldOfView -= speed;
                        }
                    }

                    _prevDist = currentDist;
                }

            }

            yield return new WaitForSeconds(0.01f);
        }
    }
    
}
