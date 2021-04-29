using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly. ");
                break;
            case "Finish":
                Debug.Log("You completed the level. ");
                break;
            case "Fuel":
                Debug.Log("You picked up some fuel. ");
                break;
            default:
                Debug.Log("You blew up. ");
                break;
        }

        string no = "false";
        if (true)
            DisplayMessage(no);
    }
    void DisplayMessage(string message)
    {
        Debug.Log(message);
    }
}
