using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    private static Decoration selectedDecoration;
    private static Vector3 mouseOffset;
    private static List<Decoration> placedDecorations = new List<Decoration>();

    public static void checkPlacement(DecorationData.PlacementType type)
    {
        if (selectedDecoration.compareType(type))
        {
            deselect();
        }
        else
        {
            print("Destroy!!!");
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
        placedDecorations.Add(selectedDecoration);
        
        selectedDecoration = decoration;
        DecorationData data = decoration.getData();
        mouseOffset = new Vector3(data.decorationImage.rect.x, data.decorationImage.rect.y, 0);
        print(mouseOffset);
        
        decoration.select();
    }

    public static void deselect()
    {
        DecorArea.Disable();
        
        selectedDecoration.deselect();
        selectedDecoration = null;
    }

    public static void deselectAndDestroy()
    {
        
        Destroy(selectedDecoration.gameObject);
        selectedDecoration = null;
    }

    public static List<Decoration> getPlacedDecoration()
    {
        return placedDecorations;
    }
    
    public void Update()
    {
        
        if (selectedDecoration)
        {
            selectedDecoration.transform.position = Input.mousePosition + mouseOffset;

            if (Input.GetMouseButtonDown(1))
            {
                deselectAndDestroy();
            }
        }
    }
    
}
