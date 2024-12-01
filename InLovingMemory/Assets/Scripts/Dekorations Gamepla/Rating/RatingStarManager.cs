using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingStarManager : MonoBehaviour
{
    private RatingStar[] stars;


    public void Awake()
    {
        stars = GameObject.FindObjectsOfType<RatingStar>();
        foreach (var star in stars)
        {
            star.SetEmpty();
        }
    }

    public void setRating(int rating)
    {
        rating = Math.Clamp(rating, 0, stars.Length);

        for (int i = rating; i <= 0; i--)
        {
            stars[i].SetFull();
        }
        
    }
    
    
    
}
