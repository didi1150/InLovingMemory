using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Rating : MonoBehaviour
{

    public static Rating instance;
    private enum PlayerPerformance
    {
        Bad,
        Medium,
        Good
    }

    // Liste an platzierten Gegenständen aus GameManager
    private List<DecorationData> _placedDecoration;

    // Auszuführender task
    [SerializeField] private TaskData _taskData;

    // score Grenzen
    private int _mediumPerformanceScore;
    private int _goodPerformanceScore;

    private int _score = 0;
    private bool _ratingPerformed = false;

    // eingegebener Text
    private string _writtenOnGrave;

    //---------------------------------//
    private void Awake()
    {
        if (instance == null) instance = this;
        _placedDecoration = GameManager.getPlacedDecoration();
        _mediumPerformanceScore = 33;
        _goodPerformanceScore = 66;
    }

    private void TestRequestedObjectsPresent()
    {
        /*if (_placedDecoration == null)
        {
            Debug.Log("placedDecoration is null");
            return;
        }
        for (int i = 0; i < _taskData.requiredDecoration.Count(); i++)
        {
            DecorationData requiredDeco = _taskData.requiredDecoration[i];
            for (int j = 0; j < _placedDecoration.Count; j++)
            {
                if (_placedDecoration[j].Equals(requiredDeco))
                {
                    _score += _pointsAddedForRightDecoration;
                    Debug.Log("new score: " + _score);
                    break;
                }
            }
        }
        */

        if (_placedDecoration == null || _placedDecoration.Count == 0)
        {
            Debug.Log("placedDecoration is null or empty");
            return;
        }
        //foreach (var placedDecoration in _placedDecoration)
        //{
        //    //Debug.Log("Checking: " + placedDecoration.name);
        //    if (_taskData.requiredDecoration == null || _taskData.requiredDecoration.Count == 0)
        //    {
        //        Debug.Log("placedDecoration is null or empty");
        //        return;
        //    }

        //    for (int i = 0; i < _taskData.requiredDecoration.Count; i++)
        //    {

        //        //Debug.Log(i + ": " + _taskData.requiredDecoration[i].displayImage.name);
        //        //Debug.Log("Placed: " + placedDecoration.displayImage.name);
        //        if (placedDecoration.displayImage
        //            .Equals(_taskData.requiredDecoration[i].displayImage))
        //        {
        //            _score += _pointsAddedForRightDecoration;
        //            Debug.Log("score: " + _score);
        //        }
        //    }
        //}
        Debug.Log("Size: " + _placedDecoration.Count);
        List<TaskEntry> reqDecCopy = _taskData.requiredDecoration;
        List<DecorationData> placedDecCopy = _placedDecoration;
        Dictionary<string, int> matchCount = new Dictionary<string, int>();
        foreach (TaskEntry taskEntry in reqDecCopy)
        {
            string entryName = taskEntry.decorationData.displayImage.name;
            if (matchCount.GetValueOrDefault(entryName, 0) > taskEntry.maxAmount) continue;
            List<DecorationData> matches = placedDecCopy.FindAll(d => d.displayImage.name.Equals(entryName));
            if (matches.Count <= taskEntry.maxAmount) _score += taskEntry.pointsPerObject * matches.Count;
            else _score += taskEntry.pointsPerObject * taskEntry.maxAmount;
            if (matchCount.ContainsKey(entryName))
            {
                matchCount[entryName] += matches.Count;
            }
            else matchCount.Add(entryName, +matches.Count);
        }
        Debug.Log("Score Added: " + _score);
        List<TaskEntry> negativeDecCopy = _taskData.negativeDecoration;
        foreach (TaskEntry taskEntry in negativeDecCopy)
        {
            string entryName = taskEntry.decorationData.displayImage.name;
            if (matchCount.GetValueOrDefault(entryName, 0) > taskEntry.maxAmount) continue;
            List<DecorationData> matches = placedDecCopy.FindAll(d => d.displayImage.name.Equals(taskEntry.decorationData.displayImage.name));
            if (matches.Count == 0) continue;
            if (matches.Count <= taskEntry.maxAmount) _score -= taskEntry.pointsPerObject * matches.Count;
            else _score -= taskEntry.pointsPerObject * taskEntry.maxAmount;
            if (matchCount.ContainsKey(entryName))
            {
                matchCount[entryName] += matches.Count;
            }
            else matchCount.Add(entryName, matches.Count);
        }

        Debug.Log("Score Reduced: " + _score);

        _score *= 100;
        if (_score < 0) _score = 0;
        Debug.Log("Score: " + _score);


        /* HashSet<DecorationData> checkedDecorations = new HashSet<DecorationData>();

         foreach (var placedDecoration in _placedDecoration)
         {
         
        Debug.Log("size: " + _placedDecoration.Count);
             Debug.Log("placed: "+ _placedDecoration.Contains(placedDecoration));
             Debug.Log("checked" + !checkedDecorations.Contains(placedDecoration));
             if (_taskData.requiredDecoration.Contains(placedDecoration) && !checkedDecorations.Contains(placedDecoration))
             {
                 _score += _pointsAddedForRightDecoration;
                 checkedDecorations.Add(placedDecoration);
                 Debug.Log(" new score: " +  _score);
             }
         }
         */
    }

    public void ReadInput(string s)
    {
        _writtenOnGrave = s;
    }

    // ist der Name auf der Grabsteinplakette enthalten
    //private void TestCorrectNameOnGrave()
    //{
    //    if (_writtenOnGrave != null && _writtenOnGrave.ToLower().Contains(_taskData.name.ToLower()))
    //    {
    //        _score += _pointsAddedForRightName;
    //    }
    //}

    // überprüfe, ob Liste Null enthällt und bereinige sie
    private void RemoveNullInPlacedDecoration()
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

    private PlayerPerformance EvaluatePlayerPerformance()
    {
        int maxScore = 0;
        foreach (TaskEntry taskEntry in _taskData.requiredDecoration)
        {
            maxScore += taskEntry.pointsPerObject * taskEntry.maxAmount;
        }
        float percentage = (float)_score / maxScore;
        Debug.Log(_score + "/" + maxScore + " = " + percentage);

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

        //TestCorrectNameOnGrave();
        TestRequestedObjectsPresent();

        PlayerPerformance p = EvaluatePlayerPerformance();
        int rating = (int)p;
        Debug.Log("Enum: " + p.ToString());
        Debug.Log("Rating: " + rating);
        _ratingPerformed = true;
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        if (gameManager)
        {
            gameManager.ShowRating(rating);
        }
    }
}
