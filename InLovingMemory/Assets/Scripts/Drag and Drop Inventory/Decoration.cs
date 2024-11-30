using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Decoration : MonoBehaviour, IPointerClickHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Image image;
    private bool isSelected = false;
    
    private DecorationData data;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void setData(DecorationData data)
    {
        this.data = data;
        image.sprite = data.decorationImage;
    }

    public void select()
    {
        isSelected = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void deselect()
    {
        isSelected = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.select(this);
    }

    public bool compareType(DecorationData.PlacementType type)
    {
        return type == data.type;
    }
    
    
}
