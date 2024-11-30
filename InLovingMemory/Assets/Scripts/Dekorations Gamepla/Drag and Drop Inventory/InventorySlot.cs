using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Image displayImage;
    private DecorationData decoration;
    private static String decorationPrefabPath = "Prefabs/DecorationPrefab";
    private static GameObject decorationPrefab;

    public void Awake()
    {
        displayImage = GetComponent<Image>();
        decorationPrefab = Resources.Load(decorationPrefabPath) as GameObject;
    }

    public void setData(DecorationData data)
    {
        decoration = data;
        displayImage.sprite = decoration.displayImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.somethingSelected())
        {
            instantiateDecoration(eventData.position);
        }

    }

    public GameObject instantiateDecoration(Vector3 position)
    {
        GameObject newDecoration = Instantiate(decorationPrefab, position, Quaternion.identity);
        newDecoration.transform.SetParent(transform);
        Decoration dec = newDecoration.GetComponent<Decoration>();
        if (dec)
        {
            dec.setData(decoration);
            GameManager.select(dec);
        }
        return newDecoration;
    }
    
    
}
