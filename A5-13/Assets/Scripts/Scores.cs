using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    public int coins;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        coins = 0;
        level = 0;
    }
}
