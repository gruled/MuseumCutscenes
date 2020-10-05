using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExposureSkyBox : MonoBehaviour
{
    [SerializeField] private Material _skyBox;
    private float _currExposure;
    
    void Start()
    {
        _currExposure = _skyBox.GetFloat("_Exposure");
        StartCoroutine(_exposure());
    }

    private IEnumerator _exposure()
    {
        while (true)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Insert(0.0f, _skyBox.DOFloat(_currExposure+0.05f, "_Exposure",0.2f));
            sequence.Insert(0.2f, _skyBox.DOFloat(_currExposure+0.025f, "_Exposure",0.1f));
            sequence.Insert(0.3f, _skyBox.DOFloat(_currExposure+0.05f, "_Exposure",0.1f));
            sequence.Insert(0.4f, _skyBox.DOFloat(_currExposure, "_Exposure",0.2f));
            sequence.Play();
            yield return new WaitWhile(sequence.IsPlaying);
            yield return new WaitForSeconds(Random.Range(1.0f,3.0f));
        }
    }

}
