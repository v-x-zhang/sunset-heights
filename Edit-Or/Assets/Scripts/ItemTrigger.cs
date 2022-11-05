using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{

    [SerializeField]
    private ApartmentDoor apartmentDoorScript;
    [SerializeField]
    private Computer computerScript;
    [SerializeField]
    private Paper paperScript;
    [SerializeField]
    private Elevator elevatorScript;

    [SerializeField]
    private bool isApartmentDoor;
    [SerializeField]
    private bool isComputer;
    [SerializeField]
    private bool isPaper;
    [SerializeField]
    private bool isElevatorButtons;

    private void OnTriggerEnter(Collider other)
    {
        if (isApartmentDoor)
        {
            if (other.gameObject.tag == "Player")
            {
                apartmentDoorScript.isInRange = true;
            }
        }else if (isComputer)
        {
            if(other.gameObject.tag == "Player")
            {
                computerScript.isInRange = true;
            }
        }
        else if (isPaper)
        {
            if (other.gameObject.tag == "Player")
            {
                paperScript.isInRange = true;
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                elevatorScript.isInRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isApartmentDoor)
        {
            if (other.gameObject.tag == "Player")
            {
                apartmentDoorScript.isInRange = false;
                apartmentDoorScript.isBeingLookedAt = false;
                apartmentDoorScript.apartmentText.SetActive(false); //Use animator here, which would check if it's being looked at to turn it off.

            }
        }
        else if (isComputer)
        {
            if (other.gameObject.tag == "Player")
            {
                computerScript.isInRange = false;
                computerScript.isBeingLookedAt = false;
                computerScript.computerText.SetActive(false); //Use animator here, which would check if it's being looked at to turn it off.
            }
        }
        else if(isPaper)
        {
            if (other.gameObject.tag == "Player")
            {
                paperScript.isInRange = false;
                paperScript.isBeingLookedAt = false;
                paperScript.paperText.SetActive(false); //Use animator here, which would check if it's being looked at to turn it off.
               
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                elevatorScript.isInRange = false;
                elevatorScript.isBeingLookedAt = false;
                elevatorScript.elevatorText.SetActive(false); //Use animator here, which would check if it's being looked at to turn it off.
            }
        }
    }
}
