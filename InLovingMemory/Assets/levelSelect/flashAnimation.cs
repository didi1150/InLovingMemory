using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("FlashAnimation Start - Original Color: " + originalColor);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
        
        if (hit.collider != null)
        {
            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);
            Debug.Log("Current GameObject: " + gameObject.name);
            Debug.Log("Scene1Done Status: " + levelSelect.scene1done);
        }

        if (hit.collider != null && hit.collider.gameObject == gameObject && !levelSelect.scene1done)
        {
            if (!isHovering)
            {
                Debug.Log("Starting Flash Animation");
                isHovering = true;
                flashRoutine = StartCoroutine(flash());
            }
        }
        else
        {
            if (isHovering)
            {
                Debug.Log("Stopping Flash Animation");
                isHovering = false;
                if (flashRoutine != null)
                {
                    StopCoroutine(flash());
                }
                SpriteRenderer.color = originalColor;
            }
        }
    }
    
    void OnMouseExit()
    {
        isHovering = false;
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        SpriteRenderer.color = originalColor;
    }
    
    public IEnumerator flash()
    {
        Debug.Log("Flash Coroutine Started");
        while (isHovering)
        {
            SpriteRenderer.color = White; 
            yield return new WaitForSeconds(duration);
            SpriteRenderer.color = originalColor;
            yield return new WaitForSeconds(duration);
        }
        Debug.Log("Flash Coroutine Ended");
    }
}
