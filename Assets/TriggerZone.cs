using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public enum TriggerType { cutscene, other };
    TriggerType currentType = TriggerType.cutscene;



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by = " + other.transform.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered triggerzone => " + transform.name);

            switch (currentType)
            {
                case TriggerType.cutscene:
                    CutsceneLaunch();
                    break;
            }
        }
    }

    void CutsceneLaunch ()
    {
       StateTracker tracker = GameObject.Find("StateTracker").GetComponent<StateTracker>();
        tracker.currentState = StateTracker.GameState.cutscene;


    }
}
