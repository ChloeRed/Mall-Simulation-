using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopper : MonoBehaviour
{
    public Rigidbody shopper;
    public float MAX_SEE_AHEAD;
    public Vector3 MAX_AVOID_FORCE; 
    public float maxForce;
    public float maxSpeed; 
    Vector3 position;
    Vector3 velocity;
    public Vector3 force;
    public Vector3 target;
    Vector3 acceleration;
    public GameObject[] Shops;
    Vector3 ahead;
    Vector3 ahead2;
    public GameObject[] obstacles;
    bool madeIt;
    public Vector3 positionS;
    public float timer;
    public bool closeEnough = false; 



    // Start is called before the first frame update
    void Start()

    {
        Shops = GameObject.FindGameObjectsWithTag("Store");
		string[] tagsToFind = { "Table", "Advertiser", "Shopper" };
		foreach (string s in tagsToFind)
		{
			obstacles = GameObject.FindGameObjectsWithTag(s);

		}
		shopper = GetComponent<Rigidbody>();
        float randZ = position.z;
    
        int decision = Random.Range(0, 2);
        if (decision == 0)
        //Shoppers who will go straight across
        {
            target = new Vector3(99, 1, randZ);
        }
        else if (decision == 1)
        {
            //send them to a store first
            int randShop = Random.Range(0, 20);
            target = new Vector3(Shops[randShop].transform.position.x, 1, Shops[randShop].transform.position.z) ;
            float waitTime = 1.0f;
            waitTime -= Time.deltaTime;
            //Debug.Log(Shops[randShop].transform.position);
             madeIt = false; 
            if (position.z < -40 || position.z > 40)

            {
                madeIt = true;
                Debug.Log(":(");
               

            }
            if (madeIt)
            {
                Debug.Log(":)"); 
                target = new Vector3(99, 1, randZ);
                madeIt = false; 
            }


        }
     
    }

    

    void Update()
    {
        positionS = transform.position;
        transform.position = positionS; 
        force = obstacleAvoidance() + seek();
        acceleration = force;
        shopper.velocity += acceleration * Time.deltaTime;
        shopper.position += shopper.velocity * Time.deltaTime;
        if (maxSpeed > 0.001f)
        {
            shopper.velocity.Normalize();
        }
        shopper.rotation = Quaternion.identity;
    }
     
    Vector3 seek()
    {
        Vector3 desiredVelocity = target - shopper.position;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;
        return (desiredVelocity - shopper.velocity);
    }


    Vector3 obstacleAvoidance()
        {
            ahead = shopper.position + shopper.velocity.normalized * MAX_SEE_AHEAD;
            ahead2 = shopper.position + shopper.velocity.normalized * MAX_SEE_AHEAD * 0.5f;
            GameObject obstacle = new GameObject();
            obstacle = closestObject();
            Vector3 avoidForce = new Vector3(0,0,0);
            if (obstacle != null)
            {
                avoidForce.x = ahead.x - obstacle.transform.position.x;
                avoidForce.z = ahead.z - obstacle.transform.position.z;

               avoidForce.Normalize();
               avoidForce.Scale(MAX_AVOID_FORCE);
     
            }else
            {
                Vector3 nullForce = new Vector3(0, 0, 0);
                avoidForce.Scale(nullForce); 
            }
            
            return avoidForce; 
        }
        
    GameObject closestObject()
        {
            GameObject closest = null;
            
            for (int i = 0; i < obstacles.Length; i++)
            {
  
                GameObject tempObject = obstacles[i];
           
                CapsuleCollider tempCollider = tempObject.GetComponent<CapsuleCollider>();
            
                bool collision;
               
                collision = checkCollision1(ahead, ahead2, tempCollider);
 
                if (collision && (closest == null ||euclideanDistance(tempObject)
                    < euclideanDistance(closest)))
                {
                    closest = tempObject; 
                }
           
            }
             
            return closest; 

        }
    float euclideanDistance (GameObject obj2) 
        {
            float eDistance = Mathf.Sqrt((shopper.transform.position.x - obj2.transform.position.x) * (shopper.transform.position.x - obj2.transform.position.x) +
                (shopper.transform.position.y - obj2.transform.position.y) * (shopper.transform.position.y - obj2.transform.position.y));
            return eDistance; 
        }
    float euclideanDistance2(Vector3 obj1, Vector3 obj2)
        { 
    
        float eDistance = Mathf.Sqrt((obj1.x - obj2.x) * (obj1.x - obj2.x) +
            (obj1.y - obj2.y) * (obj1.y - obj2.y));
        return eDistance;
    }

        bool checkCollision1(Vector3 tempAhead, Vector3 tempAhead2, CapsuleCollider obstacle)
        {
            
            bool collision = (euclideanDistance2(obstacle.gameObject.transform.position, tempAhead) <= obstacle.radius) ||
                    (euclideanDistance2(obstacle.gameObject.transform.position, tempAhead2) <= obstacle.radius);
            
            return collision; 
        }

    }

