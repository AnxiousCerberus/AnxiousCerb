using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeZoneTrigger : MonoBehaviour
{

    [SerializeField]
    string SceneToLoad = "None";
    bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Triggered by = " + other.transform.name);

        if (other.CompareTag("Player") && Input.GetAxisRaw("Vertical") > 0 && SceneManager.GetActiveScene().name != SceneToLoad && !isLoading)
        {
            isLoading = true;
            Debug.Log("Loading next scene...");
            StartCoroutine (LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneToLoad));
        isLoading = false;
    }
}
