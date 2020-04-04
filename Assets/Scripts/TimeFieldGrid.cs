using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class TimeFieldGrid : MonoBehaviour
{
    [Range(0.1f, 1.0f)]
    public float playerSpeed = 0.1f;
    [Range(0.5f, 10.0f)]
    public float timeBeforeDisable = 6.5f;
    public GameObject cellPrefab;
    public Transform gridGroup;
    public GameObject gameContoller;

    private const int cellCount = 64;
    private GameObject[] cellArray = new GameObject[cellCount];
    private List<GameObject> cellPath = new List<GameObject>();
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

    private int[] cellsWithLeather = { 22, 29, 36, 49, 56 };
    private int[] cellsWithButtons = { 7, 13, 19, 26, 33, 39, 45, 53, 60 };

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
            GameObject playerToMove = StaticVariables.player1IsActive ? player1 : player2; 
            if (!playerToMove.GetComponent<PlayerButton>().isMoving)
            {
                int currentCell = playerToMove.GetComponent<PlayerButton>().cellNum;
                if (currentCell < 63)
                {
                    StartCoroutine(SmoothMove(playerToMove, 2));
                }
            }
        }
    }

    public void MoveActivePlayer(int cellCount)
    {
        GameObject playerToMove = StaticVariables.player1IsActive ? player1 : player2;
        if (!playerToMove.GetComponent<PlayerButton>().isMoving)
        {
            int currentCell = playerToMove.GetComponent<PlayerButton>().cellNum;
            if (currentCell < 63)
            {
                StartCoroutine(SmoothMove(playerToMove, cellCount));
            }
        }
    }

    public void MovePlayer(GameObject player, int cellcount)
    {

    }

    public IEnumerator SmoothMove(GameObject player, int cellCount)
    {
        int targetCellNum = player.GetComponent<PlayerButton>().cellNum + 1;
        if (targetCellNum < 64)
        {
            PlayerButton playerButton = player.GetComponent<PlayerButton>();
            playerButton.isMoving = true;
            GameObject tmpCell = getTargetCell(playerButton);
            float currTime = 0;
            do
            {
                player.transform.position = Vector3.Lerp(player.transform.position, tmpCell.transform.position, currTime / playerSpeed);
                currTime += Time.deltaTime;
                yield return null;
            }
            while (currTime <= playerSpeed);
            player.GetComponent<Transform>().SetParent(tmpCell.transform, true);
            playerButton.cellNum = tmpCell.GetComponent<TimeFieldCellScript>().id;
            if (cellsWithButtons.Contains(playerButton.cellNum))
            {
                gameContoller.GetComponent<GameControl>().ActivePlayerGetButtonsFromField();
            }
            if (cellCount > 1)
            {
                StartCoroutine(SmoothMove(player, cellCount - 1));
            }
            else
            {
                StartCoroutine(WaitAndDisableTimeFeild(timeBeforeDisable, player));
            }
        }

    }

    private GameObject getTargetCell(PlayerButton playerButton)
    {
        GameObject tmpCell;
        int currentCellId = playerButton.cellNum;
        if (cellsWithLeather.Contains(currentCellId + 1) || currentCellId == 0 || currentCellId == 1)
        {
            tmpCell = cellPath[currentCellId + 2];
        } else
        {
            tmpCell = cellPath[currentCellId + 1];
        }
        return tmpCell;
    }

    public IEnumerator WaitAndDisableTimeFeild(float time, GameObject player)
    {
        yield return new WaitForSeconds(time);
        player.GetComponent<PlayerButton>().isMoving = false;
        DisableField();
        gameContoller.GetComponent<GameControl>().PassTurnToAnotherPlayer();
    }

    private void DisableField()
    {
        gameContoller.GetComponent<GameControl>().HideScoreField();
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
            //cellArray[i].GetComponent<Text>().text = counter.ToString();
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
            player1.GetComponent<PlayerButton>().cellNum = cellPath[1].GetComponent<TimeFieldCellScript>().id;
        }
        else
        {
            player2 = player;
            player2.GetComponent<Transform>().SetParent(cellPath[2].transform, true);
            player2.transform.position = new Vector3(cellPath[2].transform.position.x, cellPath[2].transform.position.y);
            player2.GetComponent<PlayerButton>().cellNum = cellPath[2].GetComponent<TimeFieldCellScript>().id;
        }
    }
}
