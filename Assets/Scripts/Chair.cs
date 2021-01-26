using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    
    private int tablePosition;
    public Vector3 position;
    public bool occupied;
    
    // Start is called before the first frame update
    void Start()
    {
        //See if the chair is occupied
        occupied = false; 
        
    }

    // Update is called once per frame
    void Update()
    {
        //position to be accessed from other classes
        position = transform.position; 
    }
}
