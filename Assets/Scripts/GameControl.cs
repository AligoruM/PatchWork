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
        public GameObject selectTileCanvas, advanceCanvas, tileMovementSection, allTiles, warningMessage;
        public GameObject[] tilesObjects;
        public TextMeshProUGUI playerTurnText, numberOfButtons, warningText;
        public Fields fields;
        public GameObject timeFieldGrid;
        private Player player1, player2;
        private static GameObject playerChip1, playerChip2;

        private List<Tile> tiles;
        private List<Tile> avaiableTiles;
        private List<CostAndProgress> costs = new List<CostAndProgress>();
        private int firstTilePosition = 0;

        public GameObject selectTile1Place, selectTile2Place, selectTile3Place;

        private int stageOfPlayerMove = 1;

        void Start()
        {
            fields = new Fields();
            this.player1 = new Player();
            this.player2 = new Player();

            StaticVariables.player1IsActive = true;
            this.tiles = new List<Tile>();
            PrepareListOfTiles();
            FindAvailableTiles();
            HideScoreField();
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
        }

        void ShowAvailableTiles()
        {
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
            foreach (var tile in tiles)
            {
                if (!tile.isUsed)
                {
                    tile.tileObject.SetActive(false);
                }
            }

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
                player1.position += diffInButtons;
                player1.numberOfButtons += diffInButtons;
            }
            else
            {
                diffInButtons = player1.position - player2.position + 1;
                player2.position += diffInButtons;
                player2.numberOfButtons += diffInButtons;
            }

            ShowScoreField();
            timeFieldGrid.GetComponent<TimeFieldGrid>().MoveActivePlayer(diffInButtons);
            PassTurnToAnotherPlayer();
        }

        private void ShowScoreField()
        {
            selectTileCanvas.SetActive(false);
            advanceCanvas.SetActive(false);
            scoreField.SetActive(true);
            allTiles.SetActive(false);
            timeFieldGrid.SetActive(true);            
        }

        public void HideScoreField()
        {
            selectTileCanvas.SetActive(true);
            advanceCanvas.SetActive(true);
            scoreField.SetActive(false);
            allTiles.SetActive(true);
        }

        private void PassTurnToAnotherPlayer()
        {
            if (StaticVariables.player1IsActive && !player2.finishOfGame)
            {
                if (player1.position > player2.position)
                {
                    StaticVariables.player1IsActive = false;
                    lightField.SetActive(false);
                    darkField.SetActive(true);
                    playerTurnText.text = "Player 2 Turn";
                    numberOfButtons.text = $"{player2.numberOfButtons}";
                    FindAvailableTiles();
                }
            }
            else if (!StaticVariables.player1IsActive && !player1.finishOfGame)
            {
                if (player2.position > player1.position)
                {
                    StaticVariables.player1IsActive = true;
                    lightField.SetActive(true);
                    darkField.SetActive(false);
                    playerTurnText.text = "Player 1 Turn";
                    numberOfButtons.text = $"{player1.numberOfButtons}";
                    FindAvailableTiles();
                }
            }
            stageOfPlayerMove = 1;
        }

        public void RotateActiveTile()
        {
            if (avaiableTiles[0].isActive)
            {
                avaiableTiles[0].tileObject.transform.Rotate(0, 0, -90);
            }
            if (avaiableTiles[1].isActive)
            {
                avaiableTiles[1].tileObject.transform.Rotate(0, 0, -90);
            }
            if (avaiableTiles[2].isActive)
            {
                avaiableTiles[2].tileObject.transform.Rotate(0, 0, -90);
            }
        }

        public void FlipActiveTile()
        {
            if (avaiableTiles[0].isActive)
            {
                avaiableTiles[0].tileObject.transform.Rotate(0, 180, 0);
            }
            if (avaiableTiles[1].isActive)
            {
                avaiableTiles[1].tileObject.transform.Rotate(0, 180, 0);
            }
            if (avaiableTiles[2].isActive)
            {
                avaiableTiles[2].tileObject.transform.Rotate(0, 180, 0);
            }
        }

        public void ClickAcceptButton()
        {

            int deltaPos = 0;
            if (avaiableTiles[0].isActive)
            {
                deltaPos = 1;
            }
            if (avaiableTiles[1].isActive)
            {
                deltaPos = 2;
            }
            if (avaiableTiles[2].isActive)
            {
                deltaPos = 3;
            }
            var acceptTile = avaiableTiles.Where(tile => tile.isActive).First();

            // Check limits of placement

            // Check avaliable money
            if (StaticVariables.player1IsActive)
            {
                if (acceptTile.buttonCost > player1.numberOfButtons)
                {
                    warningMessage.SetActive(true);
                    warningText.text = $"You don't have enough buttons to but this tile!";
                }
                else
                {
                    // If all good
                    ChangeFirstAvailableTile(deltaPos);
                    tileMovementSection.SetActive(false);
                    player1.numberOfButtons -= acceptTile.buttonCost;
                    player1.position += acceptTile.progressCost;
                    player1.numberOfButtonsOnField += acceptTile.buttonsOnTile;
                    acceptTile.isUsed = true;
                    acceptTile.isActive = false;
                    acceptTile.tileObject.transform.SetParent(lightField.transform);

                    ShowScoreField();
                    timeFieldGrid.GetComponent<TimeFieldGrid>().MoveActivePlayer(acceptTile.progressCost);
                    PassTurnToAnotherPlayer();
                }
            }
            else
            {
                if (acceptTile.buttonCost > player2.numberOfButtons)
                {
                    warningMessage.SetActive(true);
                    warningText.text = $"You don't have enough buttons to but this tile!";
                }
                else
                {
                    // If all good
                    ChangeFirstAvailableTile(deltaPos);
                    tileMovementSection.SetActive(false);
                    player2.numberOfButtons -= acceptTile.buttonCost;
                    player2.position += acceptTile.progressCost;
                    player2.numberOfButtonsOnField += acceptTile.buttonsOnTile;
                    acceptTile.isUsed = true;
                    acceptTile.isActive = false;
                    acceptTile.tileObject.transform.SetParent(darkField.transform);

                    ShowScoreField();
                    timeFieldGrid.GetComponent<TimeFieldGrid>().MoveActivePlayer(acceptTile.progressCost);
                    PassTurnToAnotherPlayer();
                }
            }
        }

        public void ChangeFirstAvailableTile(int posOfAcceptTile)
        {
            firstTilePosition = (int)((firstTilePosition + posOfAcceptTile) % tiles.Count);
        }

        public void ClickOkInWarningMessage()
        {
            warningMessage.SetActive(false);
        }
    }
}