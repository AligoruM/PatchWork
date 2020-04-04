using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Android;

namespace Assets.Scripts
{
    public class TilesInteraction : MonoBehaviour
    {
        public bool isDragging;
        private Transform rt;
        private float maxSize = 0.5f;
        private Renderer rnd;
        private Material instMaterial;

        public void Start()
        {
            rnd = gameObject.GetComponent<Renderer>();
            instMaterial = rnd.material;
            rt = this.GetComponent<Transform>();
        }

        public void OnMouseDown()
        {
            isDragging = true;
            instMaterial.SetFloat("_OutlineEnabled", 1);
            if (rt.localScale.x < maxSize)
            {
                Debug.Log(rt.localScale.x);
                Debug.Log(maxSize);

                rt.localScale += new Vector3(0.14f, 0.14f, 0);
            }
        }

        public void OnMouseUp()
        {
            isDragging = false;
            instMaterial.SetFloat("_OutlineEnabled", 0);
            float minDistance = 5000;
            List<GameObject> cells = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cell"));
            int indOfGoodCell = 0;
            int i = 0;
            foreach (var cell in cells)
            {
                var tilePos = GetTopLeft(transform);
                var cellPos = cell.transform.position;
                cellPos.x -= cell.GetComponent<RectTransform>().rect.width / 2;
                cellPos.y += cell.GetComponent<RectTransform>().rect.height / 2;

                var dist = Math.Abs(Vector3.Distance(tilePos, cellPos));
                if (dist< minDistance)
                {
                    minDistance = dist;
                    indOfGoodCell = i;
                }
                i++;
            }
            if (minDistance < 2.5f)
            {
                var bestPos = cells[indOfGoodCell].transform.position;
                bestPos.x -= cells[indOfGoodCell].GetComponent<RectTransform>().rect.width / 3 * 2;
                bestPos.y += cells[indOfGoodCell].GetComponent<RectTransform>().rect.height / 3 * 2;
                float width = transform.GetComponent<Renderer>().bounds.size.x;
                float height = transform.GetComponent<Renderer>().bounds.size.y;
                bestPos = new Vector3(bestPos.x + width / 2, bestPos.y - height / 2, bestPos.z);
                transform.position = bestPos;
            }
        }

        private void Update()
        {
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition, Space.World);
            }
        }

        private Vector3 GetTopLeft(Transform obj)
        {
            float width = obj.GetComponent<Renderer>().bounds.size.x;
            float height = obj.GetComponent<Renderer>().bounds.size.y;
            Vector3 topLeft = obj.position;
            topLeft.x -= width / 2;
            topLeft.y += height / 2;
            return topLeft;
        }
    }
}