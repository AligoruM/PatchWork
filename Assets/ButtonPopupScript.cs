using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonPopupScript : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float disappearTimer = 2f;
    private float moveYSpeed = 0.5f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int amount)
    {
        textMesh.SetText("+" + amount);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
           Destroy(gameObject);
        }
    }
}
