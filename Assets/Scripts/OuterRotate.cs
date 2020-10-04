using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterRotate : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _outerPivot;
    private Coroutine _coroutine;

    public void _run()
    {
        _coroutine = StartCoroutine(_activate());
    }


    public void _stop()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator _activate()
    {
        Manager.GetInstance()._camera.transform.SetParent(_outerPivot.transform);
        while (true)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    _outerPivot.transform.Rotate(
                        new Vector3(-touch.deltaPosition.normalized.y, touch.deltaPosition.normalized.x, 0.0f), _speed);
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
    
}
