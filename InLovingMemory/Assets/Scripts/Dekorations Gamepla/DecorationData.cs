using System;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decoration", menuName = "Drag and Drop Inventory/Decoration")]
public class DecorationData : ScriptableObject
{
    public bool isGeneric;
    public string uuid;
    public PlacementType type;
    public enum PlacementType
    {
        Ground,
        Gravestone,
        Borderstone
    }

    public Sprite displayImage;
    public Sprite decorationImage;

    public DecorationData(Sprite displayImage, Sprite decorationImage, PlacementType placementType, bool isGeneric)
    {
        this.displayImage = displayImage;
        this.decorationImage = decorationImage;
        this.type = placementType;
        this.isGeneric = isGeneric;

        uuid = Guid.NewGuid().ToString();
        Debug.Log($"Generated UUID: {uuid} for {name}");
    }
}