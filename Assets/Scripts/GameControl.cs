using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

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
        public GameObject startNewGameWindow;
        public GameObject lightField, darkField, scoreField;
        public GameObject selectTileCanvas, advanceCanvas, tileMovementSection, allTiles, warningMessage;
        public GameObject[] tilesObjects;
        public TextMeshProUGUI playerTurnText, numberOfRedButtons, numberOfBlueButtons, warningText;
        public Fields fields;
        public GameObject timeFieldGrid;
        public GameObject playerButton;
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
            numberOfRedButtons.text = $"{player1.numberOfButtons}";
            numberOfBlueButtons.text = $"{player2.numberOfButtons}";

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
            }
        }

        int FindActiveTile()
        {
            int i = 0;
            foreach (var tile in avaiableTiles)
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
                for (int i = 0; i < avaiableTiles.Count; i++)
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
            costs.Add(new CostAndProgress { Cost = 2, Progress = 2, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 10, Progress = 3, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 1, Progress = 5, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 0, Progress = 3, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 7, Progress = 4, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 6, Progress = 5, Buttons = 2 });
            costs.Add(new CostAndProgress { Cost = 3, Progress = 4, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 7, Progress = 6, Buttons = 3 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 1, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 1, Progress = 3, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 1, Progress = 4, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 1, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 3, Progress = 1, Buttons = 0 });
            costs.Add(new CostAndProgress { Cost = 4, Progress = 2, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 2, Progress = 3, Buttons = 1 });
            costs.Add(new CostAndProgress { Cost = 3, Progress = 2, Buttons = 1 });
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
                selectTile1Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("Progress: {0}\nCost: {1}",  avaiableTiles[0].progressCost, avaiableTiles[0].buttonCost));
            }
            else
            {
                selectTile1Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Empty);
            }
            if (!avaiableTiles[1].isActive)
            {
                avaiableTiles[1].tileObject.transform.position = selectTile2Place.transform.position;
                avaiableTiles[1].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
                selectTile2Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("Progress: {0}\nCost: {1}", avaiableTiles[1].progressCost, avaiableTiles[1].buttonCost));
            }
            else
            {
                selectTile2Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Empty);
            }
            if (!avaiableTiles[2].isActive)
            {
                avaiableTiles[2].tileObject.transform.position = selectTile3Place.transform.position;
                avaiableTiles[2].tileObject.transform.localScale = new Vector3(selectScale, selectScale, 0);
                selectTile3Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Format("Progress: {0}\nCost: {1}", avaiableTiles[2].progressCost, avaiableTiles[2].buttonCost));
            }
            else
            {
                selectTile3Place.GetComponentInChildren<TextMeshProUGUI>().SetText(string.Empty);
            }

            if (avaiableTiles[0].isActive || avaiableTiles[1].isActive || avaiableTiles[2].isActive)
            {
                advanceCanvas.SetActive(false);
                tileMovementSection.SetActive(true);
            }
            else
            {
                //advanceCanvas.SetActive(true);
                //tileMovementSection.SetActive(false);
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
            timeFieldGrid.SetActive(false);
        }

        public void PassTurnToAnotherPlayer()
        {
            if (StaticVariables.player1IsActive && !player2.finishOfGame)
            {
                if (player1.position > player2.position)
                {
                    StaticVariables.player1IsActive = false;
                    lightField.SetActive(false);
                    darkField.SetActive(true);
                    playerTurnText.text = "Player 2 Turn";
                    playerTurnText.faceColor = new Color32(59, 123, 191, 255);
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
                    playerTurnText.faceColor = new Color32(229, 62, 123, 255);
                }
            }
            FindAvailableTiles();
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

            if (StaticVariables.player1IsActive)
            {
                if (acceptTile.buttonCost > player1.numberOfButtons)
                {
                    warningMessage.SetActive(true);
                    warningText.text = $"Warning\nYou don't have enough buttons to put this tile!";
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
                    stageOfPlayerMove = 3;
                    timeFieldGrid.GetComponent<TimeFieldGrid>().MoveActivePlayer(acceptTile.progressCost);
                }
            }
            else
            {
                if (acceptTile.buttonCost > player2.numberOfButtons)
                {
                    warningMessage.SetActive(true);
                    warningText.text = $"Warning\nYou don't have enough buttons to put this tile!";
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
                    stageOfPlayerMove = 3;
                    timeFieldGrid.GetComponent<TimeFieldGrid>().MoveActivePlayer(acceptTile.progressCost);
                }
            }
        }

        public void ClickDeclineButton()
        {
            avaiableTiles[0].isActive = false;
            avaiableTiles[1].isActive = false;
            avaiableTiles[2].isActive = false;
            ShowAvailableTiles();
            advanceCanvas.SetActive(true);
            tileMovementSection.SetActive(false);
        }

        public void ChangeFirstAvailableTile(int posOfAcceptTile)
        {
            firstTilePosition = (int)((firstTilePosition + posOfAcceptTile) % tiles.Count);
        }

        public void ClickOkInWarningMessage()
        {
            warningMessage.SetActive(false);
        }

        public int ActivePlayerGetButtonsFromField()
        {
            int numberOfButtons;
            if (StaticVariables.player1IsActive)
            {
                player1.numberOfButtons += player1.numberOfButtonsOnField;
                numberOfButtons = player1.numberOfButtonsOnField;
            }
            else
            {
                player2.numberOfButtons += player2.numberOfButtonsOnField;
                numberOfButtons = player2.numberOfButtonsOnField;
            }
            return numberOfButtons;
        }

        public void ShowScoreFieldButton()
        {
            advanceCanvas.SetActive(false);
            ShowScoreField();
            StartCoroutine(WaitAndDisableTimeFeild(3));
        }

        public IEnumerator WaitAndDisableTimeFeild(float time)
        {
            yield return new WaitForSeconds(time);
            HideScoreField();
        }

        public void ShowOpponentFieldButton()
        {
            if (StaticVariables.player1IsActive)
            {
                lightField.SetActive(false);
                darkField.SetActive(true);
                playerTurnText.text = "Player 2 Field";
                playerTurnText.faceColor = new Color32(59, 123, 191, 255);
            }
            else
            {
                lightField.SetActive(true);
                darkField.SetActive(false);
                playerTurnText.text = "Player 1 Field";
                playerTurnText.faceColor = new Color32(229, 62, 123, 255);
            }
            StartCoroutine(WaitAndDisableOpponentFeild(3));
        }

        public IEnumerator WaitAndDisableOpponentFeild(float time)
        {
            yield return new WaitForSeconds(time);

            if (StaticVariables.player1IsActive)
            {
                lightField.SetActive(true);
                darkField.SetActive(false);
                playerTurnText.text = "Player 1 Turn";
                playerTurnText.faceColor = new Color32(229, 62, 123, 255);
            }
            else
            {
                lightField.SetActive(false);
                darkField.SetActive(true);
                playerTurnText.text = "Player 2 Turn";
                playerTurnText.faceColor = new Color32(59, 123, 191, 255);
            }
        }

        public void StartNewGame()
        {
            startNewGameWindow.SetActive(false);
        }

    }
}