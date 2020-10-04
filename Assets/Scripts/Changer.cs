using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

public class Changer : MonoBehaviour
{
    private OuterRotate _outerRotate;
    private Zoom _zoom;
    private InnerRotate _innerRotate;

    //private InnerRotate _innerRotate;
    [SerializeField] private Transform _innerPivot;
    [SerializeField] private Transform _outerPivot;
    [SerializeField] private Image _image;
    [SerializeField] private Light camLight;

    [SerializeField] private Transform InnerCapsule;
    [SerializeField] private Transform OuterCapsule;
        
        
    void Start()
    {
        _outerRotate = GetComponent<OuterRotate>();
        _zoom = GetComponent<Zoom>();
        _innerRotate = GetComponent<InnerRotate>();
        _outerRotate._run();
        _zoom._run();
    }

    public void Revert()
    {
        Manager.GetInstance().isOuter = !Manager.GetInstance().isOuter;
        if (Manager.GetInstance().isOuter)
        {
            _innerRotate._stop();
            _zoom._stop();
            StartCoroutine(_toOuter());
        }
        else
        {
            _outerRotate._stop();
            _zoom._stop();
            StartCoroutine(_toInner());
            
        }
    }

    private IEnumerator _toOuter()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, _image.DOColor(Color.black, 0.5f));
        sequence.Insert(0.4f, Manager.GetInstance()._camera.transform.DOMove(_outerPivot.position, 0.2f));
        sequence.Insert(0.5f, camLight.DOIntensity(20.0f, 0.1f));
        sequence.Insert(0.6f, Manager.GetInstance()._camera.transform.DOLookAt(OuterCapsule.transform.position, 0.1f));
        sequence.Insert(0.6f, _image.DOColor(new Color(0.0f,0.0f,0.0f,0.0f), 0.5f));
        sequence.Play();
        yield return  new WaitWhile(sequence.IsPlaying);
        _zoom._run();
        _outerRotate._run();
    }

    private IEnumerator _toInner()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, _image.DOColor(Color.black, 0.5f));
        sequence.Insert(0.4f, Manager.GetInstance()._camera.transform.DOMove(_innerPivot.position, 0.2f));
        sequence.Insert(0.5f, camLight.DOIntensity(0.0f, 0.1f));
        sequence.Insert(0.6f, _image.DOColor(new Color(0.0f,0.0f,0.0f,0.0f), 0.5f));
        sequence.Play();
        yield return  new WaitWhile(sequence.IsPlaying);
        _zoom._run();
        _innerRotate._run();
    }
}
