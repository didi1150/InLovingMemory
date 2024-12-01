using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image displayImage;
    private DecorationData decoration;
    private static String decorationPrefabPath = "Prefabs/DecorationPrefab";
    private static GameObject decorationPrefab;

    public void Awake()
    {
        displayImage = transform.GetChild(0).GetComponent<Image>();
        decorationPrefab = Resources.Load(decorationPrefabPath) as GameObject;
    }

    public void setData(DecorationData data)
    {
        decoration = data;
        if (decoration)
        {
            displayImage.sprite = decoration.displayImage;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.somethingSelected())
        {
            if (decoration != null)
            {
                instantiateDecoration(eventData.position);
            }
        }

    }

    public GameObject instantiateDecoration(Vector3 position)
    {
        if (GameManager.somethingSelected()) return null;
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
