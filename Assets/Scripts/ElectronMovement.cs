using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectronMovement : MonoBehaviour
{

    private float Y;
    private float Z;

    private const float e = 1.6e-19f;
    private const float mass = 9.109e-17f;
    private const float distance = 0.003f;
    //private const float deltaY = 0.834f;
    private const float deltaY = 0.247f;
    //private const float deltaZ = 0.858f;
    private const float deltaZ = 0.247f;
    private const float deltaX = 0.7497f;

    
    private float deltaTime = 0;
    private float timeStart = 0;
    private float timeElapsed = 0;


    private static float initialVelocity;

    public static float frecuencyx = 3.0f;
    public static float frecuencyy = 2.0f;
    public static float xphase = Mathf.PI/4;
    public static float yphase = 0.0f;
    public static float EVoltaje = 1000.0f;
    public static float amplitudex = 200.0f;
    public static float apmlitudey = 200.0f;
    public static float latency = 20.0f;

    public static bool senx = true;
    public static bool seny = true;
    public static bool isSin = true;

    private Rigidbody electron;


    private bool finish = false;
    private bool inZone = false;
  
 
    // Start is called before the first frame update
    void Start() 
    {
        
        //start simulation with initial data


        initialVelocity = Mathf.Sqrt((2 * e * EVoltaje) / mass);
        electron = GetComponent<Rigidbody>();
        electron.velocity = new Vector3(initialVelocity, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (isSin)
        {
            if (senx)
            {
                Z = amplitudex * Mathf.Sin(frecuencyx * Time.time - xphase);
            }
            else
            {
                Z = amplitudex * Mathf.Cos(frecuencyx * Time.time - xphase);
            }

            if (seny)
            {
                Y = apmlitudey * Mathf.Sin(frecuencyy * Time.time - yphase);
            }
            else
            {
                Y = apmlitudey * Mathf.Cos(frecuencyy * Time.time - yphase);
            }
        }
        else
        {
            Y = apmlitudey;
            Z = amplitudex;
        }
        
        

        if (!finish)
        {
            if (inZone)
            {
                voltageY();
                voltageZ();
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Delete"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            electron.velocity = new Vector3(0, 0, 0);
            finish = true;
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject, latency);
        }
        if (other.tag.Equals("VoltageZone"))
        {
            timeStart = Time.time;
            inZone = true;
            
        }
            
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("VoltageZone"))
        {
            timeElapsed = Time.time;
            deltaTime = timeStart - timeElapsed;
            //electron.velocity = new Vector3(0, 0, 0);
            inZone = false;
        }
            
    }

    private void voltageY()
    {
        float a = accelerationY();
        float newYV = a*(Time.time-timeStart);
        electron.velocity = new Vector3(electron.velocity.x, newYV, electron.velocity.z);
    }

    private float accelerationY()
    {
        float electricField = Y / deltaY;
        float electricForce = e * electricField;
        float acceleration = electricForce / mass;
        return acceleration;
    }

    private void voltageZ()
    {
        float a = accelerationZ();
        float newZV = a * (Time.time - timeStart); ;
        electron.velocity = new Vector3(electron.velocity.x, electron.velocity.y, newZV);
    }

    private float accelerationZ()
    {
        float electricField = Z / deltaZ;
        float electricForce = e * electricField;
        float acceleration = electricForce / mass;
        return acceleration;
    }
    
    public static float maxVoltageY()
    {
        float max = (Mathf.Pow(deltaY, 2) * Mathf.Pow(initialVelocity,2) * mass) / (e * Mathf.Pow(deltaX, 2));
        return max;
    }

    public static float maxVoltageX()
    {
        float max = (Mathf.Pow(deltaZ, 2) * Mathf.Pow(initialVelocity, 2) * mass) / (e * Mathf.Pow(deltaX,2));
        return max;
    }
}
