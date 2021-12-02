using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Scores").GetComponent<Scores>().coins++;
        Destroy(gameObject);
    }
}
