using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float speed;
    [SerializeField] private float enertionModifier;
    [SerializeField] private float threshold;
    private float _prevDist;
    private float _currDist;
    private Coroutine _enertion;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, (min+max)/2);
        _enertion = StartCoroutine(_move());
        try
        {
            StopCoroutine(_enertion);
        }
        catch (Exception e)
        {
        }
    }

    private IEnumerator _move()
    {
        float zShift = (_currDist - _prevDist) * speed;
        while (true)
        {
            zShift *= enertionModifier;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, transform.localPosition.z+zShift);
            if (transform.localPosition.z>max)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, max);
            }

            if (transform.localPosition.z<min)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, min);
            }
            if (Math.Abs(zShift)<threshold)
            {
                break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                _prevDist = Vector2.Distance(touch0.position, touch1.position);
                _currDist = _prevDist;
                try
                {
                    StopCoroutine(_enertion);
                }
                catch (Exception e)
                {
                }
            }

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                try
                {
                    StopCoroutine(_enertion);
                }
                catch (Exception e)
                {
                }
                _prevDist = _currDist;
                _currDist = Vector2.Distance(touch0.position, touch1.position);
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, transform.localPosition.z+(_currDist-_prevDist)*speed);
                if (transform.localPosition.z>max)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, max);
                }

                if (transform.localPosition.z<min)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.x, min);
                }
            }

            if (touch0.phase==TouchPhase.Ended||touch1.phase == TouchPhase.Ended)
            {
                try
                {
                    StopCoroutine(_enertion);
                }
                catch (Exception e)
                {
                }
                _enertion = StartCoroutine(_move());
            }
            
        }
    }
}
