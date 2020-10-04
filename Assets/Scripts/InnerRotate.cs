using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerRotate : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Coroutine _coroutine;

    public void _run()
    {
        Manager.GetInstance()._camera.transform.SetParent(null);
        _coroutine = StartCoroutine(_activate());
    }


    public void _stop()
    {
        try
        {
            StopCoroutine(_coroutine);
        }
        catch (Exception e)
        {
        }
    }

    private IEnumerator _activate()
    {
        //Manager.GetInstance()._camera.transform.SetParent(_outerPivot.transform);
        while (true)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    Manager.GetInstance()._camera.transform.Rotate(new Vector3(-touch.deltaPosition.normalized.y, touch.deltaPosition.normalized.x, 0.0f), _speed);
                    
                    Manager.GetInstance()._camera.transform.rotation = Quaternion.Euler(Manager.GetInstance()._camera.transform.rotation.eulerAngles.x,Manager.GetInstance()._camera.transform.rotation.eulerAngles.y,0.0f);
                    //_outerPivot.transform.Rotate(
                        //new Vector3(-touch.deltaPosition.normalized.y, touch.deltaPosition.normalized.x, 0.0f), _speed);
                }
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
