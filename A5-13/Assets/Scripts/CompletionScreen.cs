using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scores score = GameObject.Find("Scores").GetComponent<Scores>();

        TMPro.TextMeshPro tm = GameObject.Find("Text/Coins").GetComponent<TMPro.TextMeshPro>();
        tm.text = score.coins.ToString() + "\tCoins Collected";
        tm = GameObject.Find("Text/Levels").GetComponent<TMPro.TextMeshPro>();
        tm.text = score.level.ToString() + "\tLevels Completed";

        Destroy(score.gameObject);
    }
}
