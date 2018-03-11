using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    GameObject[] enemies;
    private Transform enemyTransform;
    private Transform playerTransform;

    void Update()
    {
        // automate player moves
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        //
        
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        //if (playerTransform.position.x == enemyTransform.position.x)
        {
            nextFire = Time.time + fireRate;
            //    GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
            GetComponent<AudioSource>().Play();
        }

        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    private void FixedUpdate()
    {
        // For normal ship control 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().velocity = movement*speed;
        

        //while()
              
        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

}
