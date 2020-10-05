using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class InnerRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Camera _camera;
    [SerializeField] private Text _text;
    [SerializeField] private float _deltaMove;
    public float _moving;
    public float _currSpeed;
    [SerializeField] private float _threshold;

    void OnEnable()
    {
        _currSpeed = 0.0f;
        _moving = 0;
        _camera = transform.GetComponent<Camera>();
        StartCoroutine(_rotate());
        StartCoroutine(_toText());
    }

    private IEnumerator _speedUp()
    {
        _currSpeed = 0.0f;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, DOTween.To(() => _currSpeed, x => _currSpeed = x, _speed, 1.0f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

    
    

    private IEnumerator _rotate()
    {
        while (true)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        StartCoroutine(_speedUp());
                        break;
                    case TouchPhase.Moved:
                        _camera.transform.Rotate(new Vector3(touch.deltaPosition.y, -touch.deltaPosition.x, 0.0f),
                            _currSpeed * (touch.deltaPosition.magnitude / touch.deltaTime));
                        _camera.transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles.x,
                            _camera.transform.rotation.eulerAngles.y, 0.0f);
                        _moving += touch.deltaPosition.magnitude;
                        if (_moving > _deltaMove)
                        {
                            StartCoroutine(_outText());
                        }
                        break;
                }
            }

            yield return new WaitForSeconds(0.001f);
        }
    }


    private IEnumerator _toText()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, _text.DOColor(Color.white, 0.3f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

    private IEnumerator _outText()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, _text.DOColor(new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.3f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }
}
