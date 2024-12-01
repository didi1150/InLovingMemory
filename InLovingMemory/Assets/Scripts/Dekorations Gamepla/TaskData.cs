using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Task", menuName = "Drag and Drop Inventory/Task")]
public class TaskData : ScriptableObject
{
    public string name;

    public List<DecorationData> requiredDecoration;
    public List<DecorationData> otherDecoration;
}