using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryTrigger : MonoBehaviour
{
    [SerializeField]
    private bool isElevatorTrigger;
    [SerializeField]
    private bool isFirstTrigger;
    [SerializeField]
    private bool isSoundTrigger;
    [SerializeField]
    private bool isScareTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isElevatorTrigger)
            {
                if (!ScaryManager.instance.beenToElevator)
                {
                    ScaryManager.instance.beenToElevator = true;
                    ScaryManager.instance.PlayDoorSound();
                }
            }
            else if (isFirstTrigger)
            {
                if (ScaryManager.instance.beenToElevator)
                {
                    ScaryManager.instance.beenBackToStart = true;
                }
            }
            else if (isSoundTrigger)
            {
                if (ScaryManager.instance.doorsHaveOpened && !ScaryManager.instance.hasPlayedSound)
                {
                    ScaryManager.instance.hasPlayedSound = true;
                    ScaryManager.instance.PlaySound();
                }
            }
            else if (isScareTrigger)
            {
                if (ScaryManager.instance.hasPlayedSound)
                {
                    ScaryManager.instance.JumpScare();
                }
            }
        }
    }
}
