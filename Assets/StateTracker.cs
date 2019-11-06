using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTracker : MonoBehaviour
{

    public enum GameState { dialog, walk, choiceInteractions, cutscene };
    public GameState currentState = GameState.walk;
    public NPC[] NPCs;

    // Start is called before the first frame update
    void Start()
    {
        NPCs = GameObject.FindObjectsOfType<NPC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
