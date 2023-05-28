using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.3f);
    }
}
