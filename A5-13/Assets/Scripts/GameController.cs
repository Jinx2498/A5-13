using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour {
    
    [SerializeField] private PlayerMovement player;
    // [SerializeField] private Text scoreLabel;

    private MazeConstructor generator;

    // private int score;
    private bool goal;

    
    void Start() {

        generator = GetComponent<MazeConstructor>();
        StartNewGame();

    }

    private void StartNewGame() {

        // score = 0;
        // scoreLabel.text = score.ToString();

        StartNewMaze();

    }

    private void StartNewMaze() {

        generator.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);

        float x = generator.startColumn * generator.hallwayWidth;
        float y = 1;
        float z = generator.startRow * generator.hallwayWidth;
        player.transform.position = new Vector3(x, y, z);

        goal = false;
        player.enabled = true;

    }

    void Update() {

        if (!player.enabled) {

            return;

        }
    }

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("TODO next level");
        SceneManager.LoadScene("Level1");
    }

    private void OnStartTrigger(GameObject trigger, GameObject other) {

        if (goal) {

            Debug.Log("Finish!");
            player.enabled = false;

            Invoke("StartNewMaze", 4);

        }
    }
}
