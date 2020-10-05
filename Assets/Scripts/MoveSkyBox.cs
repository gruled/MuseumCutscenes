using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{

    [SerializeField] private Material _skyBox;
    [SerializeField] private float _speed;
    void Start()
    {
        _skyBox.SetFloat("_Rotation", 0.0f);
        StartCoroutine(_rotate());
    }

    private IEnumerator _rotate()
    {
        float rotation = 0.0f;
        float buff = 1.0f;
        while (true)
        {
            _skyBox.SetFloat("_Rotation", rotation);
            rotation += _speed*buff;
            if (rotation>30.0f||rotation<-30.0f)
            {
                buff *= -1;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
