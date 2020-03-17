using UnityEngine;

namespace Assets.Scripts
{
    public class Tile
    {
        public GameObject tileObject;

        public int buttonCost;

        public int progressCost;

        public int buttonsOnTile;

        public bool isUsed;

        public Tile(int buttonCost, int progressCost, 
            int buttonsOnTile, GameObject tileObject)
        {
            this.buttonCost = buttonCost;
            this.progressCost = progressCost;
            this.buttonsOnTile = buttonsOnTile;
            this.tileObject = tileObject;
            this.isUsed = false;
        }
    }
}
