using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RatingStarManager : MonoBehaviour
{
    private RatingStar[] stars;


    public void Start()
    {
        stars = GameObject.FindObjectsOfType<RatingStar>();
        foreach (var star in stars)
        {
            if (star != null)
            {
                star.SetEmpty();
            }
        }
    }

    public void setRating(int rating)
    {
        rating = Math.Clamp(rating, 0, stars.Length - 1);

        for (int i = rating; i >= 0; i--)
        {
            stars[i].SetFull();

        }
    }
    
    
    
}
