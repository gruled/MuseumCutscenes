using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{

    private Material _skyBox;
    [SerializeField] private float _speed;
    [SerializeField] private float defaultPos;
    void Start()
    {
        _skyBox = RenderSettings.skybox;
        _skyBox.SetFloat("_Rotation", defaultPos);
        StartCoroutine(_rotate());
    }

    private IEnumerator _rotate()
    {
        float rotation = _skyBox.GetFloat("_Rotation");
        float buff = 1.0f;
        while (true)
        {
            _skyBox.SetFloat("_Rotation", rotation);
            rotation += _speed*buff;
            if (rotation>defaultPos+.0f||rotation<defaultPos-15.0f)
            {
                buff *= -1;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
