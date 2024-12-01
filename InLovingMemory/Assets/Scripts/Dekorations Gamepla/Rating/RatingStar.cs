using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingStar : MonoBehaviour
{
    public Sprite empty, full;
    private Image image;

    public void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetFull()
    {
        image.sprite = full;
    }

    public void SetEmpty()
    {
        image.sprite = empty;
    }
}