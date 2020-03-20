using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFieldController : MonoBehaviour
{
    public GameObject timeField;
    public GameObject timeFieldGrid;

    public GameObject player1;
    public GameObject player2;

    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GetTimeFieldGrid().GenerateCells();
        this.player1 = Instantiate(playerPrefab);
        this.player2 = Instantiate(playerPrefab);
        GetTimeFieldGrid().AddPlayer(player1);
        GetTimeFieldGrid().AddPlayer(player2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private TimeFieldGrid GetTimeFieldGrid()
    {
        return timeFieldGrid.GetComponent<TimeFieldGrid>();
    }
}
