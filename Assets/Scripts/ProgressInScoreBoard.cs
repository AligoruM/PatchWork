using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressInScoreBoard : MonoBehaviour
{
    public Transform[] scoreboardWaypoints;

    [SerializeField]
    private float moveSpeed = 0.25f;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;

    void Start()
    {
        transform.position = scoreboardWaypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        if (moveAllowed)
            Move();
    }

    private void Move()
    {
        if (waypointIndex <= scoreboardWaypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                scoreboardWaypoints[waypointIndex].transform.position,
                moveSpeed * Time.deltaTime);

            if (transform.position == scoreboardWaypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
