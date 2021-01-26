using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Advertiser : MonoBehaviour
{
    public Rigidbody advertiser;

    public float position;
    //Max distance Agent can "See"
    public float MAX_SEE_AHEAD;

    //Max force 
    public Vector3 MAX_AVOID_FORCE;
    public float maxForce;
    public float maxSpeed;

    //Position that can be reached from other classes
    public Vector3 positionA;

    Vector3 velocity;
    public Vector3 force;
    public Vector3 target;
    Vector3 acceleration;

    //All shops
    public GameObject[] Shops;
    Vector3 ahead;
    Vector3 ahead2;

    //All obstacles
    public GameObject[] obstacles;
   
    void Start()

    {
        //Get Shops
        Shops = GameObject.FindGameObjectsWithTag("Store");

        //Advertisers and Tables(which includes planters and chairs) are obstacles
        string[] tagsToFind = { "Table", "Advertiser" };
        foreach (string s in tagsToFind)
		{
			obstacles = GameObject.FindGameObjectsWithTag(s);

		}

        //Get the advertiser object
        advertiser = GetComponent<Rigidbody>();

        //Give it a new target so it appears to wander
        InvokeRepeating("newTarget", 0.0f, 4.0f);
    }

    //New Target for wandering
    void newTarget()
    {
        int randX = Random.Range(-80, 80);
        int randZ = Random.Range(-40, 40);
        this.target = new Vector3(randX, 1, randZ);
    }


    void Update()
    {
        //positionA is used to be accessed from other classes
        positionA = transform.position;

        //Force is the combination of obstacle Avoidance and See
        force = obstacleAvoidance() + seek();
		//Update All
		acceleration = force; 
        advertiser.velocity += acceleration * Time.deltaTime;
        advertiser.position += advertiser.velocity * Time.deltaTime;

        //Make sure it's supposed to have a speed and normalize vector
        if (maxSpeed > 0.001f)
        {
            advertiser.velocity.Normalize();
        }

        //Make sure to fix rotation
        advertiser.rotation = Quaternion.identity;
    }

 
    Vector3 seek()
    {
        //Find velocity need to reach target
        Vector3 desiredVelocity = target - advertiser.position;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        //Return the seek velocity 
        return (desiredVelocity - advertiser.velocity);
    }


    Vector3 obstacleAvoidance()
    {
        //Find 2 see ahead vectors (scaled by max see ahad)
        ahead = advertiser.position + advertiser.velocity.normalized * MAX_SEE_AHEAD;
        ahead2 = advertiser.position + advertiser.velocity.normalized * MAX_SEE_AHEAD * 0.5f;

        //Get obstacles
        GameObject obstacle = new GameObject();

        //Find closest obstacle
        obstacle = closestObject();
        Vector3 avoidForce = new Vector3(0, 0, 0);

        //If there is an obstacle 
        if (obstacle != null)
        {
            //Find force to avoid obstacle 
            avoidForce.x = ahead.x - obstacle.transform.position.x;
            avoidForce.z = ahead.z - obstacle.transform.position.z;

            avoidForce.Normalize();
            avoidForce.Scale(MAX_AVOID_FORCE);
        }
        //If there is no obstacle 
        else 
        {
            //There is no avoid force
            Vector3 nullForce = new Vector3(0, 0, 0);
            avoidForce.Scale(nullForce);
        }
        return avoidForce;
    }


    GameObject closestObject()
    {
        GameObject closest = null;

        //For every obstacle in obstacles
        for (int i = 0; i < obstacles.Length; i++)
        {

            GameObject tempObject = obstacles[i];

 
            CapsuleCollider tempCollider = tempObject.GetComponent<CapsuleCollider>();

           //Check for collision
            bool collision;

            collision = checkCollision1(ahead, ahead2, tempCollider);

            //when you find the closest object 
            if (collision && (closest == null || euclideanDistance(tempObject)
                < euclideanDistance(closest)))
            {
                closest = tempObject;
            }
        }

        return closest;

    }

    //Euclidan distance between advertiser and an object 
    float euclideanDistance(GameObject obj2)
    {
        float eDistance = Mathf.Sqrt((advertiser.transform.position.x - obj2.transform.position.x) * (advertiser.transform.position.x - obj2.transform.position.x) +
            (advertiser.transform.position.y - obj2.transform.position.y) * (advertiser.transform.position.y - obj2.transform.position.y));
        return eDistance;
    }

    //Euclidan distance between two objects
    float euclideanDistance2(Vector3 obj1, Vector3 obj2)
    {

        float eDistance = Mathf.Sqrt((obj1.x - obj2.x) * (obj1.x - obj2.x) +
            (obj1.y - obj2.y) * (obj1.y - obj2.y));
        return eDistance;
    }

    //Check for collision using euclidean distance 
    bool checkCollision1(Vector3 tempAhead, Vector3 tempAhead2, CapsuleCollider obstacle)
    {
       
        bool collision = (euclideanDistance2(obstacle.gameObject.transform.position, tempAhead) <= obstacle.radius) ||
                (euclideanDistance2(obstacle.gameObject.transform.position, tempAhead2) <= obstacle.radius);   
        return collision;
    }
}