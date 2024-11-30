using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rating : MonoBehaviour
{
    private enum PlayerPerformance
    {
        Bad,
        Medium,
        Good
    }
    
    // Liste an platzierten Gegenständen aus GameManager
    private static List<Decoration> _placedDecoration; 
    
    // Auszuführender task
    [SerializeField] private TaskData _taskData;
    
    // score Punkte
    public int _pointsAddedForRightDecoration;
    public int _pointsAddedForRightName;

    private static int _mediumPerformanceScore;
    private static int _goodPerformanceScore;
    
    private static int _score = 0;
    private static bool _ratingPerformed = false;

    // eingegebener Text
    private static string _writtenOnGrave;
    
    
    //---------------------------------//
    private void Awake()
    {
        _placedDecoration = GameManager.getPlacedDecoration();
    }
    
    private  void TestRequestedObjectsPresent()
    {
        int achivedScorePoints = 0;
        
        // teste für alle wichtigen Objekten, ob sie plaziert wurden
        for (int i = 0; i < _taskData.requiredDecoration.Count; i++)
        {
            DecorationData requiredDecoration = _taskData.requiredDecoration[i];

            for (int j = 0; j < _placedDecoration.Count; j++)
            {
                if (requiredDecoration.Equals(_placedDecoration[j].getData()))
                {
                    achivedScorePoints += _pointsAddedForRightDecoration;
                }
            }
        }
        _score += achivedScorePoints;
        Debug.Log("score after TestRequestedObjectsPresent: " + _score);
    }
    
    public static void ReadInput(string s)
    {
        _writtenOnGrave = s;
        Debug.Log(_writtenOnGrave);
    }
    
    // ist der Name auf der Grabsteinplakette enthalten
    private void TestCorrectNameOnGrave()
    {
        if (_writtenOnGrave.ToLower().Contains(_taskData.name.ToLower()))
        {
            Debug.Log("contains name");
            _score += _pointsAddedForRightName;
        }
        else
        {
            Debug.Log("Does not contain name");
        }
    }

    private static PlayerPerformance EvaluatePlayerPerformance()
    {
        if (_score < _mediumPerformanceScore)
        {
            return PlayerPerformance.Bad;
        }
        else if (_score >= _mediumPerformanceScore && _score < _goodPerformanceScore)
        {
            return PlayerPerformance.Medium;
        }
        else
        {
            return PlayerPerformance.Good;
        }
    }
    
    public static void DisableGameManager()
    {
        
    }
    
    public void PerformRating()
    {
        DisableGameManager();
        
        TestCorrectNameOnGrave();
        TestRequestedObjectsPresent();

        EvaluatePlayerPerformance();
        _ratingPerformed = true;
    }
}
