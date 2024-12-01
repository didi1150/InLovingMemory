using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decoration", menuName = "Drag and Drop Inventory/Decoration")]
public class DecorationData : ScriptableObject
{
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

    public DecorationData(Sprite displayImage, Sprite decorationImage, PlacementType placementType)
    {
        this.displayImage = displayImage;
        this.decorationImage = decorationImage;
        this.type = placementType;

        uuid = Guid.NewGuid().ToString();
        Debug.Log($"Generated UUID: {uuid} for {name}");
    }
}