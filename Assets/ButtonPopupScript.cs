using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonPopupScript : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private float disappearTimer = 1f;
    private float moveYSpeed = 0.5f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int amount)
    {
        textMesh.SetText("+" + amount);
        textMesh.faceColor = StaticVariables.player1IsActive ? new Color32(229, 62, 123, 255) : new Color32(59, 123, 191, 255);
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
