using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [SerializeField]
    GameObject player;

    [SerializeField]
    float speedOffset;

    [SerializeField]
    Vector2 posOffset;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Vector3 startPos
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -20);

    }
}
