using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Decoration", menuName = "Drag and Drop Inventory/Decoration")]
public class DecorationData : ScriptableObject
{
    public PlacementType type;
    public enum PlacementType
    {
        Ground,
        Gravestone,
        Borderstone
    }
    
    public Sprite displayImage;
    public Sprite decorationImage;
}