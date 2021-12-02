using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ReturnToCompletionScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void exitGame()
    {
        SceneManager.LoadScene("CompletionScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
