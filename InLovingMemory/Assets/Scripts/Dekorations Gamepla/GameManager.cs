using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TaskData task;
    
    private static Decoration selectedDecoration;
    private static List<DecorationData> placedDecorations = new List<DecorationData>();

    private InventorySlot[] _inventorySlots;


    public void Awake()
    {
        
        //setup inventory
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();
        
        if (_inventorySlots != null)
        {
            DecorationData[] requiredDecorations = task.requiredDecoration.ToArray();
            DecorationData[] relevantDecorations = new DecorationData[_inventorySlots.Length];
            
            int i = 0;
            while ( i < task.requiredDecoration.Count && i < _inventorySlots.Length)
            {
                relevantDecorations[i] = requiredDecorations[i];
                i++;
            }

            int j = i;
            i = 0;
            DecorationData[] otherDecoration = task.otherDecoration.ToArray();
            
            while (i < task.requiredDecoration.Count && i+j < _inventorySlots.Length)
            {
                relevantDecorations[i+j] = requiredDecorations[i];
                i++;
            }

            for (int k = 0; k < _inventorySlots.Length && k < relevantDecorations.Length; k++)
            {
                _inventorySlots[k].setData(relevantDecorations[k]);   
            }
        }
        //setup inventory

    }
    
    
    
    public static void checkPlacement(DecorationData.PlacementType type)
    {
        if (selectedDecoration.compareType(type))
        {
            placedDecorations.Add(selectedDecoration.getData());
        }
        else
        {
            deselectAndDestroy();
        }
    }

    public static bool somethingSelected()
    {
        return selectedDecoration != null;
    }
    
    public static void select(Decoration decoration)
    {
        DecorArea.Enable();
        
        // Füge alle ausgewählten Objekte in Liste hinzu
        placedDecorations.Add(selectedDecoration.getData());
        
        selectedDecoration = decoration;
        DecorationData data = decoration.getData();
        
        decoration.select();
    }

    public static void deselect()
    {
        if (placedDecorations.Contains(selectedDecoration.getData()))
        {
            placedDecorations.Remove(selectedDecoration.getData());
        }
        selectedDecoration.deselect();
        selectedDecoration = null;
    }

    public static void deselectAndDestroy()
    {
        
        Destroy(selectedDecoration.gameObject);
        selectedDecoration = null;
    }
    

    public static List<DecorationData> getPlacedDecoration()
    {
        return placedDecorations;
    }
    
    public void Update()
    {
        
        if (selectedDecoration)
        {
            selectedDecoration.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(1))
            {
                deselectAndDestroy();
            }
        }
    }
    
}
