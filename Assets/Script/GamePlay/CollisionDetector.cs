using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    //CollisionDetector is belong to sumo
    public SumoController SumoController;

    private void OnTriggerEnter(Collider other)
    {
        // If the striking object is the ForegroundCollisionArea, someone has hit this sumo
        if (other.gameObject.CompareTag("FrontCollisionArea"))
        {
            // If the sumo striking area is the BackCollisionArea, someone has hit this sumo from back
            if (gameObject.CompareTag("BackCollisionArea"))
            {
                var pushMultiplier = other.gameObject.GetComponent<CollisionDetector>().SumoController.PushMultiplier;
                //If the direction is positive, sumo moves forward. If the direction is negative, sumo moves back
                SumoController.SumoAddForce(1, pushMultiplier);
            }
            // If the sumo striking area is the FrontCollisionArea, someone has hit this sumo from front
            else if (gameObject.CompareTag("FrontCollisionArea"))
            {
                var pushMultiplier = other.gameObject.GetComponent<CollisionDetector>().SumoController.PushMultiplier;
                //If the direction is positive, sumo moves forward. If the direction is negative, sumo moves back
                SumoController.SumoAddForce(-1, pushMultiplier);
            }
        }
    }
}