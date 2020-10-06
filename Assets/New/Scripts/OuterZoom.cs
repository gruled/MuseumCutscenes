using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OuterZoom : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float FOVMax;
    [SerializeField] private float FOVMin;
    private float _prevDist;
    private float _dist;
    private float _elderDist;

    private float _speedModifier;

    void OnEnable()
    {
        _camera = transform.GetComponent<Camera>();
        _camera.fieldOfView = (FOVMax + FOVMin) / 2.0f;
        StartCoroutine(_zoom());
    }

    private IEnumerator _increaseSpeedZoom()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, DOTween.To(() => _speedModifier, x => _speedModifier = x, 1.0f, 0.5f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

    private IEnumerator _enertion()
    {
        Sequence sequence = DOTween.Sequence();
        float endValue = _camera.fieldOfView - (_prevDist - _elderDist) * 0.3f;
        if (endValue > FOVMax)
        {
            endValue = FOVMax;
        }

        if (endValue < FOVMin)
        {
            endValue = FOVMin;
        }

        sequence.Insert(0.0f, _camera.DOFieldOfView(endValue, 1.0f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

    private IEnumerator _zoom()
    {
        while (true)
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);
                if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                {
                    _speedModifier = 0.0f;
                    StartCoroutine(_increaseSpeedZoom());
                    _prevDist = Vector2.Distance(touch0.position, touch1.position);
                }
                else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                {
                    _dist = Vector2.Distance(touch0.position, touch1.position);
                    float d = Math.Abs(_dist - _prevDist);
                    if (_dist < _prevDist)
                    {
                        _camera.fieldOfView += _speed * _speedModifier*d;
                        if (_camera.fieldOfView > FOVMax)
                        {
                            _camera.fieldOfView = FOVMax;
                        }
                    }
                    else
                    {
                        _camera.fieldOfView -= _speed * _speedModifier*d;
                        if (_camera.fieldOfView < FOVMin)
                        {
                            _camera.fieldOfView = FOVMin;
                        }
                    }

                    _elderDist = _prevDist;
                    _prevDist = _dist;
                }
                if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
                {
                    StartCoroutine(_enertion());
                }
            }

            yield return new WaitForSeconds(0.001f);
        }
    }

}
