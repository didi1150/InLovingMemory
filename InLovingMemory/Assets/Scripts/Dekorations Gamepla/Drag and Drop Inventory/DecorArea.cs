using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecorArea : MonoBehaviour, IPointerClickHandler
{
    public DecorationData.PlacementType type;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.somethingSelected())
        {
            GameManager.checkPlacement(type);
        }
    }
}
