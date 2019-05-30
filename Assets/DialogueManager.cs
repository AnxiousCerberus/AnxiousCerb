using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro.Examples;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] TextAsset inkJSON;
    [SerializeField] RollingTextFade fadeController;

    private Story story;

    // Start is called before the first frame update
    void Awake()
    {
        story = new Story(inkJSON.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a") && fadeController.ready)
        {
            fadeController.GetComponent<TextMeshProUGUI>().text = story.Continue().Trim();
            fadeController.StartCoroutine(fadeController.AnimateVertexColors());
        }
    }
}
