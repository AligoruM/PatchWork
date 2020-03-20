using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFieldController : MonoBehaviour
{
    public GameObject timeField;
    public GameObject timeFieldGrid;

    // Start is called before the first frame update
    void Start()
    {
        timeFieldGrid.GetComponent<TimeFieldGrid>().GenerateCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
