using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TaskData task;

    private static Decoration selectedDecoration;
    private static List<DecorationData> placedDecorations = new List<DecorationData>();

    private InventorySlot[] _inventorySlots;
    
    private static InventorySlot selectedInventorySlot;


    public bool debugGraveHitbox = false;
    public void Start()
    {
        //hide grave hitboxes
        if (!debugGraveHitbox)
        {
            DecorArea[] areas = GameObject.FindObjectsOfType<DecorArea>();
            foreach (var area in areas)
            {
                Image image = area.GetComponent<Image>();
                if (image)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                }
            }
        }
        // hide grave hitboxes
        

        //setup inventory
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>();

        if (_inventorySlots != null)
        {
            TaskEntry[] requiredDecorations = task.requiredDecoration.ToArray();
            TaskEntry[] negativeDecorations = task.negativeDecoration.ToArray();
            TaskEntry[] relevantDecorations = new TaskEntry[_inventorySlots.Length];

            int i = 0;
            while (i < task.requiredDecoration.Count && i < _inventorySlots.Length)
            {
                relevantDecorations[i] = requiredDecorations[i];
                i++;
            }

            int j = i;
            i = 0;
            TaskEntry[] otherDecoration = task.otherDecoration.ToArray();

            while (i < task.requiredDecoration.Count && i + j < _inventorySlots.Length)
            {
                relevantDecorations[i + j] = requiredDecorations[i];
                i++;
            }
            int k = 0;
            for (k = 0; k < _inventorySlots.Length && k < relevantDecorations.Length; k++)
            {
                if (relevantDecorations[k] == null) continue;
                _inventorySlots[k].setData(new DecorationData(relevantDecorations[k].decorationData.displayImage, relevantDecorations[k].decorationData.decorationImage, relevantDecorations[k].decorationData.type, relevantDecorations[k].decorationData.isGeneric));
            }
            for (int n = k; n < _inventorySlots.Length && n < negativeDecorations.Length; n++)
            {
                if (negativeDecorations[k] == null) continue;
                _inventorySlots[n].setData(new DecorationData(negativeDecorations[k].decorationData.displayImage, negativeDecorations[k].decorationData.decorationImage, negativeDecorations[k].decorationData.type, negativeDecorations[k].decorationData.isGeneric));
            }

        }
        //setup inventory

    }



    public static void checkPlacement(DecorationData.PlacementType type)
    {
        if (selectedDecoration.compareType(type))
        {
            placedDecorations.Add(selectedDecoration.getData());
            
            deselect();
            if (selectedInventorySlot.getData().isGeneric)
            {
                selectedInventorySlot.instantiateDecoration();
            }
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
        selectedDecoration = decoration;
        DecorationData data = decoration.getData();

        // Füge alle ausgewählten Objekte in Liste hinzu
        if (!placedDecorations.Find(p => p.uuid == decoration.getData().uuid))
            placedDecorations.Add(selectedDecoration.getData());
        decoration.select();
    }

    public static void deselect()
    {
        if (placedDecorations.Find(p => p.uuid == selectedDecoration.getData().uuid))
        {
            placedDecorations.Remove(selectedDecoration.getData());
        }
        selectedDecoration.deselect();
        selectedDecoration = null;
    }

    public static void deselectAndDestroy()
    {
        if (placedDecorations.Find(p => p.uuid == selectedDecoration.getData().uuid))
        {
            placedDecorations.Remove(selectedDecoration.getData());
        }
        selectedDecoration.deselect();
        Destroy(selectedDecoration.gameObject);
        selectedDecoration = null;
    }


    public static List<DecorationData> getPlacedDecoration()
    {
        return placedDecorations;
    }

    public static void setSelectedInventorySlot(InventorySlot slot)
    {
        selectedInventorySlot = slot;
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