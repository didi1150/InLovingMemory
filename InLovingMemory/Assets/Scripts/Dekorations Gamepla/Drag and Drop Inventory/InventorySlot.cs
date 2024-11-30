using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Image displayImage;
    public DecorationData decoration;
    private static String decorationPrefabPath = "Prefabs/DecorationPrefab";
    private static GameObject decorationPrefab;

    public void Awake()
    {
        displayImage = GetComponent<Image>();
        displayImage.sprite = decoration.displayImage;
        decorationPrefab = Resources.Load(decorationPrefabPath) as GameObject;
        if (decorationPrefab == null)
        {
            print("Could not find decoration prefab");
        }
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
