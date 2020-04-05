using UnityEngine;

namespace Assets.Scripts
{
    public class Tile
    {
        public GameObject tileObject;

        public int buttonCost;

        public int progressCost;

        public int buttonsOnTile;

        public int tileSize;

        public bool isUsed;

        public bool isActive;

        public Tile(int buttonCost, int progressCost, 
            int buttonsOnTile, GameObject tileObject, int tileSize)
        {
            this.buttonCost = buttonCost;
            this.progressCost = progressCost;
            this.buttonsOnTile = buttonsOnTile;
            this.tileObject = tileObject;
            this.isUsed = false;
            this.isActive = false;
            this.tileSize = tileSize;
        }
    }
}
