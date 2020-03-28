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
        public GameObject selectTileCanvas, advanceCanvas, tileMovementSection, allTiles;
        public GameObject[] tilesObjects;
        public TextMeshProUGUI playerTurnText;
        public Fields fields;
        private Player player1, player2;
        private static GameObject playerChip1, playerChip2;

        private List<Tile> tiles;
        private List<Tile> avaiableTiles;
        private List<CostAndProgress> costs = new List<CostAndProgress>();
        private int firstTilePosition = 0;

        public GameObject selectTile1Place, selectTile2Place, selectTile3Place;

        private int stageOfPlayerMove = 1;

        private int delta = 0;

        void Start()
        {
            fields = new Fields();
            this.player1 = new Player();
            this.player2 = new Player();

            StaticVariables.player1IsActive = true;
            this.tiles = new List<Tile>();
            PrepareListOfTiles();
            FindAvailableTiles();
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
                CheckActiveStatusOfAvailableTiles();
                ShowAvailableTiles();
                if (StaticVariables.player1IsActive)
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
                if (StaticVariables.player1IsActive)
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

        int FindActiveTile()
        {
            int i = 0;
            foreach(var tile in avaiableTiles)
            {
                if (tile.tileObject.GetComponent<TilesInteraction>().isDragging)
                {
                    tile.isActive = true;
                    break;
                }
                i++;
            }
            return i;
        }

        void CheckActiveStatusOfAvailableTiles()
        {
            int actTileIndex = FindActiveTile();
            if (actTileIndex != 3)
            {
                for (int i=0; i<avaiableTiles.Count;i++)
                {
                    if (i != actTileIndex)
                        avaiableTiles[i].isActive = false;
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
            //Debug.Log("PrepareListOfTiles");
        }

        void ShowAvailableTiles()
        {
            //Debug.Log("ShowAvailableTiles");
            float selectScale = 0.36f;
            if (!avaiableTiles[0].isActive)
            {
                avaiableTiles[0].tileObject.transform.position = selectTile1Place.transform.position;
                avaiableTiles[0].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            }
            if (!avaiableTiles[1].isActive)
            {
                avaiableTiles[1].tileObject.transform.position = selectTile2Place.transform.position;
                avaiableTiles[1].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            }
            if (!avaiableTiles[2].isActive)
            {
                avaiableTiles[2].tileObject.transform.position = selectTile3Place.transform.position;
                avaiableTiles[2].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
            }

            if (avaiableTiles[0].isActive || avaiableTiles[1].isActive || avaiableTiles[2].isActive)
            {
                advanceCanvas.SetActive(false);
                tileMovementSection.SetActive(true);
            }

            foreach (var tile in avaiableTiles)
            {
                tile.tileObject.gameObject.SetActive(true);
            }
        }

        private void FindAvailableTiles()
        {
            avaiableTiles = new List<Tile>();
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
        }

        public void ClickOnAdvanceButton()
        {
            Debug.Log("ClickOnAdvanceButton");
            stageOfPlayerMove = 3;
            int diffInButtons;
            if (StaticVariables.player1IsActive)
            {
                diffInButtons = player2.position - player1.position + 1;
                player1.numberOfButtons += diffInButtons;
            }
            else
            {
                diffInButtons = player1.position - player2.position + 1;
                player2.numberOfButtons += diffInButtons;
            }

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
            if (StaticVariables.player1IsActive && (!player2.finishOfGame))
            {
                StaticVariables.player1IsActive = false;
                playerTurnText.text = "Player 2 Turn";
            }
            else
            {
                if ((player1.finishOfGame))
                {
                    StaticVariables.player1IsActive = true;
                    playerTurnText.text = "Player 1 Turn";
                }
            }
            stageOfPlayerMove = 1;
        }
    }
}