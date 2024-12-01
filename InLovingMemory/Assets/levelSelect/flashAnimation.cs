using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class flashAnimation : MonoBehaviour
{
    private Color originalColor;
    public Color White = Color.white;
    public SpriteRenderer SpriteRenderer;
    public float duration = 0.1f;
    private bool isHovering = false;

    private Coroutine flashRoutine;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = SpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnMouseEnter()
    {
        isHovering = true;
        StartCoroutine(flash());
    }

    public void OnMouseExit()
    {
        isHovering = false;
        SpriteRenderer.color = originalColor;
    }

    
    private IEnumerator flash()
    {
        while (isHovering)
        {
            SpriteRenderer.color = White; 
            yield return new WaitForSeconds(duration);
            SpriteRenderer.color = originalColor;
            yield return new WaitForSeconds(duration);
        }
    }
}
