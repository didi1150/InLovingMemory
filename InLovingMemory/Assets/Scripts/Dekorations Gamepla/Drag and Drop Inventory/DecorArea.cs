using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecorArea : MonoBehaviour, IPointerClickHandler
{
    private static List<DecorArea> _decorAreas = new List<DecorArea>();
    
    
    public DecorationData.PlacementType type;

    private CanvasGroup group;

    public void Awake()
    {
        group = GetComponent<CanvasGroup>();
        _decorAreas.Add(this);
    }

    public static void Disable()
    {
        for (int i = 0; i < _decorAreas.Count; i++)
        {
            _decorAreas[i].group.blocksRaycasts = false;
        }
    }
    
    public static void Enable()
    {
        for (int i = 0; i < _decorAreas.Count; i++)
        {
            _decorAreas[i].group.blocksRaycasts = true;
        }
    }


    public static void ClearList()
    {
        _decorAreas = new List<DecorArea>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.somethingSelected())
        {
            GameManager.checkPlacement(type);
        }
    }
}
