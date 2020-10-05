using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeState : MonoBehaviour
{
    [SerializeField] private bool _isOuter;
    [SerializeField] private Camera _outerCamera;
    [SerializeField] private Camera _innerCamera;
    [SerializeField] private Image _image;
     void Start()
    {
        //_isOuter = true;
        _outerCamera.gameObject.SetActive(true);
        _innerCamera.gameObject.SetActive(false);
    }

    public void Revert()
    {
        _isOuter = !_isOuter;
        StartCoroutine(_action());
    }

    private IEnumerator _action()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, _image.DOColor(Color.black, 0.5f));
        sequence.InsertCallback(0.5f, () =>
        {
            if (_isOuter)
            {
                _innerCamera.gameObject.SetActive(true);
                _outerCamera.gameObject.SetActive(false);
            }
            else
            {
                _innerCamera.gameObject.SetActive(false);
                _outerCamera.gameObject.SetActive(true);
            }
        });
        sequence.Insert(0.5f, _image.DOColor(new Color(0.0f,0.0f,0.0f,0.0f), 0.5f));
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }

}
