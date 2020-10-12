using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float maxDistance;
    [SerializeField] private Image hand;
    private float _dist;
    void Start()
    {
        _dist = 0.0f;
        text = GetComponent<Text>();
        hand = GetComponentInChildren<Image>();
    }

    private IEnumerator _fade()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, text.DOColor(new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.5f));
        sequence.Insert(0.0f, hand.DOColor(new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.5f));
        sequence.InsertCallback(0.5f, () => {text.gameObject.SetActive(false);});
        sequence.Play();
        yield return new WaitWhile(sequence.IsPlaying);
    }
    
    void Update()
    {
        Touch touch;
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            _dist += touch.deltaPosition.magnitude;
            if (_dist>maxDistance)
            {
                StartCoroutine(_fade());
            }
        }
    }
}
