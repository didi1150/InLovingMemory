using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image displayImage;
    
    [SerializeField]
    private DecorationData data;
    
    private static String decorationPrefabPath = "Prefabs/DecorationPrefab";
    private static GameObject decorationPrefab;

    public void Awake()
    {
        displayImage = transform.GetChild(0).GetComponent<Image>();
        decorationPrefab = Resources.Load(decorationPrefabPath) as GameObject;
    }

    public void setData(DecorationData data)
    {
        this.data = data;
        if (this.data)
        {
            displayImage.sprite = this.data.displayImage;
        }
    }

    public DecorationData getData()
    {
        return data;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.somethingSelected())
        {
            if (data != null)
            {
                instantiateDecoration();
            }
        }

    }

    public GameObject instantiateDecoration()
    {
        if (GameManager.somethingSelected()) return null;
        GameObject newDecoration = Instantiate(decorationPrefab, new Vector3(), Quaternion.identity);
        newDecoration.transform.SetParent(transform);
        Decoration dec = newDecoration.GetComponent<Decoration>();
        if (dec)
        {
            dec.setData(data);
            GameManager.select(dec);
            GameManager.setSelectedInventorySlot(this);
        }
        return newDecoration;
    }


}
