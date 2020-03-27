using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    struct CostAndProgress
    {
        public int Cost;
        public int Progress;
        public int Buttons;
    }

    public class GameControl : MonoBehaviour
    {
        public GameObject lightField, darkField, scoreField;
        public GameObject selectTileCanvas, advanceCanvas, allTiles;
        public GameObject[] tilesObjects;
        public TextMeshProUGUI playerTurnText;

        private Player player1, player2;
        private static GameObject playerChip1, playerChip2;

        private List<Tile> tiles;
        private List<CostAndProgress> costs = new List<CostAndProgress>();
        private int firstTilePosition = 0;

        public GameObject selectTile1Place, selectTile2Place, selectTile3Place;

        private int stageOfPlayerMove = 1;

        private int delta = 0;

        void Start()
        {
            this.player1 = new Player();
            this.player2 = new Player();

            player1.isActive = true;
            this.tiles = new List<Tile>();
            PrepareListOfTiles();
            ShowAvailableTiles();
        }

        void Update()
        {
            if (player1.finishOfGame && player2.finishOfGame)
            {
                if (player1.numberOfButtons > player2.numberOfButtons)
                {
                    playerTurnText.text = "Player 1 WIN!";
                }
                if (player2.numberOfButtons > player1.numberOfButtons)
                {
                    playerTurnText.text = "Player 2 WIN!";
                }
                else
                {
                    playerTurnText.text = "Draw";
                }
            }
            else if (stageOfPlayerMove == 1)
            {                
                if (player1.isActive)
                {
                    // put tile on light field 
                }
                else
                {
                    // put tile on dark field 
                }
            }
            // If click on Advance button
            else if (stageOfPlayerMove == 3)
            {
                if (player1.isActive)
                {
                    //if (playerChip1.GetComponent<ProgressInScoreBoard>().waypointIndex >
                    //    player1.position+ delta)
                    //{
                    //    playerChip1.GetComponent<ProgressInScoreBoard>().moveAllowed = false;
                    //    player1.position = playerChip1.GetComponent<ProgressInScoreBoard>().waypointIndex - 1;
                    //    HideScoreField();
                    //    PassTurnToAnotherPlayer();
                    //}
                    //if (playerChip1.GetComponent<ProgressInScoreBoard>().waypointIndex ==
                    //    playerChip1.GetComponent<ProgressInScoreBoard>().scoreboardWaypoints.Length)
                    //{
                    //    player1.finishOfGame = true;
                    //}
                }
                else
                {
                    //if (playerChip2.GetComponent<ProgressInScoreBoard>().waypointIndex >
                    //    player2.position + delta)
                    //{
                    //    playerChip2.GetComponent<ProgressInScoreBoard>().moveAllowed = false;
                    //    player2.position = playerChip2.GetComponent<ProgressInScoreBoard>().waypointIndex - 1;
                    //    HideScoreField();
                    //    PassTurnToAnotherPlayer();
                    //}
                    //if (playerChip2.GetComponent<ProgressInScoreBoard>().waypointIndex ==
                    //    playerChip2.GetComponent<ProgressInScoreBoard>().scoreboardWaypoints.Length)
                    //{
                    //    player2.finishOfGame = true;
                    //}
                }                
            }
        }

        void PrepareCostAndProgresses()
        {
            costs.Add(new CostAndProgress { Cost = 1, Progress = 2, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 4, Progress = 6, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 3, Progress = 6, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 2, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 5, Progress = 4, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 7, Progress = 1, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 7, Progress = 2, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 5, Progress = 3, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 5, Progress = 5, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 2, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 3, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 3, Progress = 3, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 8, Progress = 6, Buttons = 3 });
            costs.Add(new CostAndProgress { Cost = 1, Progress = 2, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 10, Progress = 4, Buttons = 3 });
            costs.Add(new CostAndProgress { Cost = 10, Progress = 5, Buttons = 3 });
        }

        void PrepareListOfTiles()
        {
            int i = 0;
            PrepareCostAndProgresses();
            foreach (var tile in tilesObjects)
            {
                tiles.Add(new Tile(costs[i].Cost, costs[i].Progress, costs[i].Buttons,
                    tile));
                i++;
            }
            tiles.Shuffle();
            Debug.Log("PrepareListOfTiles");
        }

        void ShowAvailableTiles()
        {
            List<Tile> avaiableTiles = FindAvailableTiles();
            Debug.Log("ShowAvailableTiles");
            float selectScale = 0.36f;
            avaiableTiles[0].tileObject.transform.position = selectTile1Place.transform.position;
            avaiableTiles[1].tileObject.transform.position = selectTile2Place.transform.position;
            avaiableTiles[2].tileObject.transform.position = selectTile3Place.transform.position;
            avaiableTiles[0].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            avaiableTiles[1].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            avaiableTiles[2].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            foreach (var tile in avaiableTiles)
            {
                tile.tileObject.gameObject.SetActive(true);

            }
        }

        List<Tile> FindAvailableTiles()
        {
            List<Tile> avaiableTiles = new List<Tile>();
            int i = 0;
            while (i < 3)
            {
                foreach (var tile in tiles.Skip(firstTilePosition))
                {
                    if (!tile.isUsed)
                    {
                        avaiableTiles.Add(tile);
                        i++;
                    }
                    if (i == 3)
                        break;
                }
            }
            return avaiableTiles;
        }

        public void ClickOnAdvanceButton()
        {
            Debug.Log("ClickOnAdvanceButton");
            stageOfPlayerMove = 3;
            int diffInButtons;
            if (player1.isActive)
            {
                diffInButtons = player2.position - player1.position + 1;
                player1.numberOfButtons += diffInButtons;
            }
            else
            {
                diffInButtons = player1.position - player2.position + 1;
                player2.numberOfButtons += diffInButtons;
            }
            //delta = diffInButtons;

            ShowScoreField();           

        }

        private void ShowScoreField()
        {
            selectTileCanvas.SetActive(false);
            advanceCanvas.SetActive(false);
            scoreField.SetActive(true);
            allTiles.SetActive(false);

        }

        private void HideScoreField()
        {
            selectTileCanvas.SetActive(true);
            advanceCanvas.SetActive(true);
            scoreField.SetActive(false);
            allTiles.SetActive(true);
        }

        private void PassTurnToAnotherPlayer()
        {
            if (player1.isActive && (!player2.finishOfGame))
            {
                player1.isActive = false;
                player2.isActive = true;
                playerTurnText.text = "Player 2 Turn";
            }
            else
            {
                if ((player1.finishOfGame))
                {
                    player1.isActive = true;
                    player2.isActive = false;
                    playerTurnText.text = "Player 1 Turn";
                }
            }
            stageOfPlayerMove = 1;
        }
    }
}