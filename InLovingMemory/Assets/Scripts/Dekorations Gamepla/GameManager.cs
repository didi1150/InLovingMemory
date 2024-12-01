using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        _inventorySlots = GameObject.FindObjectsOfType<InventorySlot>().OrderBy(go => go.name).ToArray();
        
        System.Array.Sort(_inventorySlots, (a, b) => string.Compare(a.gameObject.name, b.gameObject.name, true));
        foreach (var inventorySlot in _inventorySlots)
        {
            Debug.Log("         slots : " +  inventorySlot.name);
        }
        if (_inventorySlots != null)
        {
            TaskEntry[] requiredDecorations = task.requiredDecoration.ToArray();
            TaskEntry[] negativeDecorations = task.negativeDecoration.ToArray();
            TaskEntry[] otherDecoration = task.otherDecoration.ToArray();
            TaskEntry[] relevantDecorations = new TaskEntry[_inventorySlots.Length];

            // int i = 0;
            // while (i < task.requiredDecoration.Count && i < _inventorySlots.Length)
            // {
            //     relevantDecorations[i] = requiredDecorations[i];
            //     i++;
            // }
            Debug.Log("length: " + relevantDecorations.Length);
            relevantDecorations = AddToArray(relevantDecorations,requiredDecorations,0);
            Debug.Log("required finished " + requiredDecorations.Length);
            relevantDecorations = AddToArray(relevantDecorations,negativeDecorations,requiredDecorations.Length);
            Debug.Log("negativ finished " + negativeDecorations.Length);
            relevantDecorations = AddToArray(relevantDecorations,otherDecoration,requiredDecorations.Length+negativeDecorations.Length);
            Debug.Log("other finished " + otherDecoration.Length);

            // int j = i;
            // i = 0;
            //
            // while (i < task.requiredDecoration.Count && i + j < _inventorySlots.Length)
            // {
            //     relevantDecorations[i + j] = requiredDecorations[i];
            //     i++;
            // }
            int i = 0;
            for (i = 0; i < relevantDecorations.Length; i++)
            {
                if (relevantDecorations[i] == null) break;
                _inventorySlots[i].setData(new DecorationData(relevantDecorations[i].decorationData.displayImage, relevantDecorations[i].decorationData.decorationImage, relevantDecorations[i].decorationData.type, relevantDecorations[i].decorationData.isGeneric));
                Debug.Log(relevantDecorations[i]);
                // _inventorySlots[i].GetComponent<Im>().enabled = true;
                _inventorySlots[i].gameObject.SetActive(true);
            }
            for (var j = i; j < relevantDecorations.Length; j++)
            {
                // _inventorySlots[i].GetComponent<Renderer>().enabled = false;
                _inventorySlots[j].gameObject.SetActive(false);
                Debug.Log("disabled: " + _inventorySlots[j].name);
            }
            // int k = 0;
            // for (k = 0; k < _inventorySlots.Length && k < relevantDecorations.Length; k++)
            // {
            //     if (relevantDecorations[k] == null) continue;
            //     _inventorySlots[k].setData(new DecorationData(relevantDecorations[k].decorationData.displayImage, relevantDecorations[k].decorationData.decorationImage, relevantDecorations[k].decorationData.type, relevantDecorations[k].decorationData.isGeneric));
            // }
            // for (int n = k; n < _inventorySlots.Length && n < negativeDecorations.Length; n++)
            // {
            //     if (negativeDecorations[k] == null) continue;
            //     _inventorySlots[n].setData(new DecorationData(negativeDecorations[k].decorationData.displayImage, negativeDecorations[k].decorationData.decorationImage, negativeDecorations[k].decorationData.type, negativeDecorations[k].decorationData.isGeneric));
            // }

        }
        //setup inventory

    }

    private TaskEntry[] AddToArray(TaskEntry[] relevantDecorations, TaskEntry[] deco, int index)
    {
        
        int i = 0;
        while (i < deco.Length && i < _inventorySlots.Length)
        {
            Debug.Log(i);
            relevantDecorations[index+i] = deco[i];
            i++;
        }
        return relevantDecorations;
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