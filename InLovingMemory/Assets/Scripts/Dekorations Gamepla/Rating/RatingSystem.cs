using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rating : MonoBehaviour
{
    // Auftragdetails 
    private static List<Decoration> _placedDecoration;
    private static List<Decoration> _requestedDecorations;
    private static int _score = 0;
    private static bool _ratingPerformed = false;

    private void Awake()
    {
        _placedDecoration = GameManager.getPlacedDecoration();
    }

    // Abgleich mit requirement Liste (sind alle Objekte enthalten)
    private static void TestRequestedObjectsPresent()
    {
        int achivedScorePoints = 0;
        _score += achivedScorePoints;
    }
    
    
    // wird Mitte der Hitbox durch ein anderes Objekt verdeckt?
    private static void TestRequestedObjectsOcclusion()
    {
        // teste Occlussion für alle required Objects
        
        int achivedScorePoints = 0;
        // finde Mitte des Dekorechtecks
        _score += achivedScorePoints;
    }
    
    // ist der Name auf der Grabsteinplakette enthalten
    private static void TestCorrectNameOnGrave()
    {
        int achivedScorePoints = 0;
        _score += achivedScorePoints;
    }

    public static void DisableGameManager()
    {
        
    }
    
    public void PerformRating()
    {
        DisableGameManager();
        
        TestCorrectNameOnGrave();
        TestRequestedObjectsPresent();
        TestRequestedObjectsOcclusion();
        _ratingPerformed = true;
        Debug.Log("clicked");
    }
}
