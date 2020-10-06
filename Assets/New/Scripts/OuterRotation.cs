using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OuterRotation : MonoBehaviour
{
    private Transform _parent;
    [SerializeField] private float _speed;
    [SerializeField] private Text _text;
    [SerializeField] private float _deltaMove;
    public float _moving;
    public float _currSpeed;
    [SerializeField] private float _threshold;
    [SerializeField] private float enertionModifier;

    private Vector3 _prevRotate;
    private float _prevAngle;
    void OnEnable()
    {
        _moving = 0;
        
        _parent = transform.parent;
        StartCoroutine(_rotation());
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

    private IEnumerator _enertion()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, DOTween.To(() => _prevAngle, x => _prevAngle = x, 0.0f, 1.0f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

    private IEnumerator _enertionMove()
    {
        float endTime = Time.time + 1.0f;
        while (endTime>Time.time)
        {
            _parent.Rotate(_prevRotate, _prevAngle*enertionModifier);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator _rotation()
    {
        while (true)
        {
            if (Input.touchCount==1)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        StartCoroutine(_speedUp());
                        break;
                    case TouchPhase.Moved:
                        if (touch.deltaPosition.magnitude>_threshold)
                        {
                            _prevRotate = new Vector3(-touch.deltaPosition.y, touch.deltaPosition.x, 0.0f);
                            _prevAngle = _currSpeed * (touch.deltaPosition.magnitude / touch.deltaTime);
                            _parent.Rotate(_prevRotate, _prevAngle);
                            _moving += touch.deltaPosition.magnitude;
                            if (_moving>_deltaMove)
                            {
                                StartCoroutine(_outText());
                            }   
                        }
                        break;
                    case TouchPhase.Ended:
                        StartCoroutine(_enertion());
                        StartCoroutine(_enertionMove());
                        break;
                }
            }
            yield return new WaitForSeconds(0.0001f);
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
        sequence.Insert(0.0f, _text.DOColor(new Color(0.0f,0.0f,0.0f,0.0f), 0.3f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }
}
