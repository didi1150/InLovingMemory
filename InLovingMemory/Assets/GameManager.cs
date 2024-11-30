using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    private static Decoration selectedDecoration;
    private static List<Decoration> placedDecorations = new List<Decoration>();

    public static void checkPlacement(DecorationData.PlacementType type)
    {
        if (selectedDecoration.compareType(type))
        {
            placedDecorations.Add(selectedDecoration);
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
        selectedDecoration = decoration;
        print(selectedDecoration);
        decoration.select();
    }

    public static void deselect()
    {
        selectedDecoration.deselect();
        selectedDecoration = null;
    }

    public static void deselectAndDestroy()
    {
        Destroy(selectedDecoration.gameObject);
        selectedDecoration = null;
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
