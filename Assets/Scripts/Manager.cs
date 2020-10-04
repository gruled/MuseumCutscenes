using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    [SerializeField] public bool isOuter;
    [SerializeField] public Camera _camera;

    private Manager()
    {
        
    }

    public static Manager GetInstance() => _instance;
    
    void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
        else if (_instance==this)
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {
        StartCoroutine(init());
    }

    private IEnumerator init()
    {
        yield return new WaitForSeconds(0.01f);
    }
}
