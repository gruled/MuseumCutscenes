using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OuterRotation : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float enertionThreshold;
   [SerializeField] private float enertionModifier;
   private float _angle;
   private Vector3 _axis;
   private Coroutine _coroutine;

   private void Start()
   {
      _coroutine = StartCoroutine(_enertion());
      _stop();
   }


   private IEnumerator _enertion()
   {
      float angleShift = _angle;
      while (true)
      {
         transform.parent.Rotate(_axis,angleShift);
         angleShift *= enertionModifier;
         if (Math.Abs(angleShift)<enertionThreshold)
         {
            break;
         }
         yield return new WaitForSeconds(Time.fixedDeltaTime);
      }
   }
   
   private void Update()
   {
      Touch touch;
      if (Input.touchCount==1)
      {
         touch = Input.GetTouch(0);
         switch (touch.phase)
         {
            case TouchPhase.Began:
               _stop();
               _axis = Vector3.zero;
               break;
            case TouchPhase.Moved:
               _stop();
               _angle = speed * (touch.deltaPosition.magnitude / touch.deltaTime);
               _axis = new Vector3(-touch.deltaPosition.normalized.y, touch.deltaPosition.normalized.x, 0.0f);
               transform.parent.Rotate(_axis, _angle);
               break;
            case TouchPhase.Ended:
               _stop();
               _coroutine = StartCoroutine(_enertion());
               break;
         }
      }
   }

   private void _stop()
   {
      try
      {
         StopCoroutine(_coroutine);
      }
      catch (Exception e)
      {
      }
   }
}
