using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnLongButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool pointerDown;
    float PointerDownTimer;

    public UnityEvent OnLongClick;
  
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        //Debug.Log("PointerDown");
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        //Debug.Log("PointerDown");
    }

    private void Update()
    {
        if (pointerDown)
        {
            if (OnLongClick != null) OnLongClick.Invoke(); 
        }
    }

    private void Reset()
    {
        pointerDown = false;
    }

}
