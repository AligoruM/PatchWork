using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour { 

    public bool isMoving;
    public int cellNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateZPos();
    }
    //тупой костыль для меня, т.к. впадлу разбираться почему он вечно уезжает в жопу
    private void updateZPos()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
