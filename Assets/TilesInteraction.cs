using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesInteraction : MonoBehaviour
{
    private bool isDragging;
    private Transform rt;
    private float maxSize = 0.5f;
    private Renderer rnd;
    private Material instMaterial;

    public void Start()
    {
        rnd = gameObject.GetComponent<Renderer>();
        instMaterial = rnd.material;
        rt = this.GetComponent<Transform>();
    }

    public void OnMouseDown()
    {
        isDragging = true;
        instMaterial.SetFloat("_OutlineEnabled", 1);
        if (rt.localScale.x<maxSize)
        {
            Debug.Log(rt.localScale.x);
            Debug.Log(maxSize);
            
            rt.localScale += new Vector3(0.14f, 0.14f, 0);
        }
    }

    public void OnMouseUp()
    {
        isDragging = false;
        instMaterial.SetFloat("_OutlineEnabled", 0);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
