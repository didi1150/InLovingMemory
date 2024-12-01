using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TaskEntry
{
    public DecorationData decorationData;
    public int maxAmount = 1;
    public int pointsPerObject = 1;
}

[CreateAssetMenu(fileName = "New Task", menuName = "Drag and Drop Inventory/Task")]
public class TaskData : ScriptableObject
{
    public string name;

    public List<TaskEntry> requiredDecoration;
    public List<TaskEntry> otherDecoration;
    public List<TaskEntry> negativeDecoration;
}