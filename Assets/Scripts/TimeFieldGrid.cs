using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFieldGrid : MonoBehaviour
{
    [Range(0.5f, 2.0f)]
    public float playerSpeed = 0.5f;

    private const int cellCount = 64;
    public GameObject cellPrefab;
    public Transform gridGroup;
    private GameObject[] cellArray = new GameObject[cellCount];
    public List<GameObject> cellPath = new List<GameObject>();
    private int[] pathIds = { 0, 1, 2, 3, 4, 5, 6, 7,
                              15,23,31,39,47,55,63,
                              62,61,60,59,58,57,56,
                              48,40,32,24,16, 8,
                               9,10,11,12,13,14,
                              22,30,38,46,54,
                              53,52,51,50,49,
                              41,33,25,17,
                              18,19,20,21,
                              29,37,45,
                              44,43,42,
                              34,26,
                              27,28,
                              36,
                              35};

    private int[] cellsWithLeather = {22, 29, 36, 49, 56 };
    private int[] cellsWithButtons = {7, 13, 19, 26, 33, 39, 45, 53, 60};

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!player2.GetComponent<PlayerButton>().isMoving)
            {
                int currentCell = player2.GetComponent<PlayerButton>().cellNum;
                if (currentCell < 63) {
                    GameObject tmpCell = cellPath[currentCell + 1];
                    StartCoroutine(SmoothMove(player2, tmpCell, playerSpeed));
                }
            }
        }
    }

    public IEnumerator SmoothMove(GameObject player, GameObject newCell, float time)
    {
        {
            float currTime = 0;
            do
            {
                player.transform.position = Vector3.Lerp(player.transform.position, newCell.transform.position, currTime / time);
                currTime += Time.deltaTime;
                yield return null;
            }
            while (currTime <= time);
            player.GetComponent<Transform>().SetParent(newCell.transform, true);
            player.GetComponent<PlayerButton>().cellNum = newCell.GetComponent<TimeFieldCellScript>().id;
        }
    }



    public void GenerateCells()
    {
        for (int i = 0; i < cellCount; i++)
        {
            GameObject cell = Instantiate(cellPrefab);
            cell.transform.SetParent(gridGroup, false);

            cellArray[i] = cell;
        }

        MakePath();
    }

    private void MakePath()
    {
        int counter = 0;
        foreach (int i in pathIds)
        {
            cellPath.Add(cellArray[i]);

            cellArray[i].GetComponent<TimeFieldCellScript>().hasButton = cellsWithButtons.Contains(counter);
            cellArray[i].GetComponent<TimeFieldCellScript>().hasLeather = cellsWithLeather.Contains(counter);
            cellArray[i].GetComponent<Text>().text = counter.ToString();
            cellArray[i].GetComponent<TimeFieldCellScript>().id = counter;

            counter++;
        }
    }

    public void AddPlayer(GameObject player)
    {
        if (player1 == null)
        {
            player1 = player;
            player1.GetComponent<Transform>().SetParent(cellPath[1].transform, true);
            player1.transform.position = new Vector3(cellPath[1].transform.position.x, cellPath[1].transform.position.y);
        }
        else
        {
            player2 = player;
            player2.GetComponent<Transform>().SetParent(cellPath[2].transform, true);
            player2.transform.position = new Vector3(cellPath[2].transform.position.x, cellPath[2].transform.position.y);
        }
    }
}
