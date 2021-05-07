using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHandler : MonoBehaviour
{
    Vector3 eleStartPos; // elevator's start position
    Vector3 hEleStartPos; // horizontal elevator's start position
    [SerializeField] Vector3 eleMovementVector; // elevator's movement vector
    [SerializeField] Vector3 hEleMovementVector; // horizontal elevator's movement vector
    float movementFactor;
    float period = 6f;
    

    // Start is called before the first frame update
    void Start()
    {
        LocateElevatorPos();
    }

    // Update is called once per frame
    void Update()
    {
        SpinningObstacles();
        ProcessElevator();
    }
    void LocateElevatorPos()
    {
        foreach (var gameObject in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            switch (gameObject.name)
            {
                case "Elevator":
                    eleStartPos = gameObject.transform.position;
                    break;
                case "HorizontalElevator":
                    hEleStartPos = gameObject.transform.position;
                    break;
            }                
        }
    }
    void ProcessElevator()
    {
        if (period <= Mathf.Epsilon) return; // prevents from period reaching smallest throat (Mathf.Epsilon)
        float cycles = Time.time / period; // continually growing over time

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner
        Vector3 offset;
        
        foreach (var gameObject in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            switch (gameObject.name)
            {
                case "Elevator":
                    offset = eleMovementVector * movementFactor;
                    gameObject.transform.position = eleStartPos + offset;
                    break;
                case "HorizontalElevator":
                    offset = hEleMovementVector * movementFactor;
                    gameObject.transform.position = hEleStartPos + offset;
                    break;
            }                
        }

    }
    void SpinningObstacles()
    {
        foreach (var gameObject in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            if (gameObject.name == "SpinningObstacle")
                gameObject.transform.Rotate(0.0f, 0.0f, 0.2f);
        }
    }
}
