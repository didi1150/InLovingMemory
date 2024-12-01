using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private static List<DecorationData> _placedDecoration;

    // Auszuführender task
    [SerializeField] private TaskData _taskData;

    // score Punkte
    public int _pointsAddedForRightDecoration;
    public int _pointsAddedForRightName;

    // score Grenzen
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

    private void TestRequestedObjectsPresent()
    {
        if (_placedDecoration == null)
        {
            Debug.Log("placedDecoration is null");
        }
        for (int i = 0; i < _taskData.requiredDecoration.Count(); i++)
        {
            DecorationData requiredDeco = _taskData.requiredDecoration[i];
            if (_placedDecoration != null && _placedDecoration.Contains(requiredDeco))
            {
                _score += _pointsAddedForRightDecoration;
                Debug.Log(_score + "new");
            }
        }
        
        
        /*Debug.Log(_placedDecoration.Count + "before");
        RemoveNullInPlacedDecoration();
        Debug.Log(_placedDecoration.Count + "after");
        

        if (_placedDecoration == null)
        {
            Debug.Log("placedDecoration is null");
        }
        
        Debug.Log("list size: " + _placedDecoration.Count);
        int achivedScorePoints = 0;

        //_placedDecoration = _placedDecoration.OfType<Decoration>().ToList();
        
=======
        RemoveNullInPlacedDecoration();

        int achivedScorePoints = 0;
        _placedDecoration = _placedDecoration.OfType<DecorationData>().ToList();

>>>>>>> 426c5b475940a952a222fc33e61ce13c19922f80
        // teste für alle wichtigen Objekten, ob sie plaziert wurden
        for (int i = 0; i < _taskData.requiredDecoration.Count; i++)
        {
            DecorationData requiredDecoration = _taskData.requiredDecoration[i];

            for (int j = 0; j < _placedDecoration.Count; j++)
            {
                if (requiredDecoration.Equals(_placedDecoration[j]))
                {
                    achivedScorePoints += _pointsAddedForRightDecoration;
                }
            }
        }
        _score += achivedScorePoints;
        Debug.Log("score after TestRequestedObjectsPresent: " + _score);
        */
    }

    public static void ReadInput(string s)
    {
        _writtenOnGrave = s;
    }

    // ist der Name auf der Grabsteinplakette enthalten
    private void TestCorrectNameOnGrave()
    {
        if (_writtenOnGrave != null &&_writtenOnGrave.ToLower().Contains(_taskData.name.ToLower()))
        {
            Debug.Log("contains name");
            _score += _pointsAddedForRightName;
            Debug.Log(_score);
        }
    }

    // überprüfe, ob Liste Null enthällt und bereinige sie
    private static void RemoveNullInPlacedDecoration()
    {
        if (_placedDecoration == null)
        {
            return;
        }

       for (int i = 0; i < _placedDecoration.Count; i++)
        {
            if (_placedDecoration[i] == null)
            {
                _placedDecoration.RemoveAt(i);
            }
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


    public void PerformRating()
    {

        TestCorrectNameOnGrave();
        TestRequestedObjectsPresent();

        EvaluatePlayerPerformance();
        _ratingPerformed = true;
    }
}