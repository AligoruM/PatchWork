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
    public GameObject player2Prefab;
    
    private void Awake()
    {
        GetTimeFieldGrid().GenerateCells();
        this.player1 = Instantiate(playerPrefab);
        this.player2 = Instantiate(player2Prefab);
        GetTimeFieldGrid().AddPlayer(player1);
        GetTimeFieldGrid().AddPlayer(player2);
    }

    // Start is called before the first frame update
    void Start()
    {

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
