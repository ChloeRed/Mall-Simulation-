using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCourt : MonoBehaviour
{
    public GameObject table;
    public GameObject planter;
    private Vector3 firstTablePos;

    // Start is called before the first frame update
    void Start()
    {
        //Either 3 or 4 Tables
        int numTables = Random.Range(3, 5);
        int numPlanters = Random.Range(2, 6);

        //RandomX and Z for the first table
        int randX = Random.Range(-45, 45);
        int randZ = Random.Range(-15, 15);
        for (int i = 0; i < numTables; i++)
        {
            //Randomly Place the First Table
            if (i == 0)
            {
                Vector3 position = new Vector3(randX, 1, randZ);
                Instantiate(table, position, Quaternion.identity);
                firstTablePos = position;
            }

            //Place Table 2
            if (i == 1)
            {
                int x = randX + Random.Range(10, 15);
                int z = randZ + Random.Range(10, 15);
                Vector3 position = new Vector3(x, 1, z);
              
                Instantiate(table, position, Quaternion.identity);
            }

            //Place Table 3
            if (i == 2)
            {
                int x = randX + Random.Range(-10, -15);
                int z = randZ + Random.Range(10, 15);
                Vector3 position = new Vector3(x, 1, z);
                Instantiate(table, position, Quaternion.identity);
            }

            //Place Table 4 (if it exists)
            if (i == 2)
            {
                int x = randX + Random.Range(-10, -15);
                int z = randZ + Random.Range(-10, -15);
                Vector3 position = new Vector3(x, 1, z);
                Instantiate(table, position, Quaternion.identity);
            }

        }

        //Adding Planters
        for (int i = 0; i < numPlanters; i++)
        {
            //Place Planter 1 
            if (i == 0)
            {
                float x = firstTablePos.x + Random.Range(10, 16);
                float z = firstTablePos.z + Random.Range(-3, 3);
                Instantiate(planter, new Vector3(x, 1, z), Quaternion.identity);
            }

            //Place Planter 2
            if (i == 1)
            {
                float x = firstTablePos.x + Random.Range(-3, 3);
                float z = firstTablePos.z + Random.Range(10, 16);
                Instantiate(planter, new Vector3(x, 1, z), Quaternion.identity);
            }

            //Place Planter 3 
            if (i == 2)
            {
                float x = firstTablePos.x - Random.Range(10, 16);
                float z = firstTablePos.z + Random.Range(-3, 3);
                Instantiate(planter, new Vector3(x, 1, firstTablePos.z), Quaternion.identity);
            }

            //Place Planter 4 (if it exists)
            if (i == 3)
            {
                float x = firstTablePos.x + Random.Range(-3, 3);
                float z = firstTablePos.z - Random.Range(10, 16);
                Instantiate(planter, new Vector3(x, 1, z), Quaternion.identity);
            }

            //Place Planter 5 (if it exists)
            if (i == 4)
            {
                float x = 0;
                float z = 0;
                int randomNegativeX = Random.Range(0, 2);
                int randomNegativeZ = Random.Range(0, 2);

                if (randomNegativeX == 0)
                {
                    x = firstTablePos.x + 20;
                }
                if (randomNegativeX == 1)
                {
                    x = firstTablePos.x - 20;
                }
                if (randomNegativeZ == 0)
                {
                    z = firstTablePos.z + 20;
                }
                if (randomNegativeZ == 1)
                {
                    z = firstTablePos.z - 20;
                }
                Instantiate(planter, new Vector3(x, 1, z), Quaternion.identity);

            }
        }
    }


    // Update is called once per frame
    void Update()
    {


    }
}
