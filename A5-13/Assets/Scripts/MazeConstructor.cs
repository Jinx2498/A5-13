using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeConstructor : MonoBehaviour {
    
    public bool showDebug;
    
    [SerializeField] private Material mazeMaterial1;
    [SerializeField] private Material mazeMaterial2;
    [SerializeField] private Material startMaterial;
    [SerializeField] private Material treasureMaterial;
    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;

    public NavMeshSurface surface;

    public int[,] data {

        get; private set;

    }

    public float hallwayWidth {
        
        get; private set;
    
    }

    public float hallwayHeight {
        
        get; private set;
    
    }

    public int startRow {
    
        get; private set;
    
    }

    public int startColumn {
    
        get; private set;
    
    }

    public int goalRow {
    
        get; private set;
    
    }

    public int goalColumn {
    
        get; private set;
    
    }

    void Start()
    {
        surface.BuildNavMesh();
    }

    void Awake() {

        meshGenerator = new MazeMeshGenerator();

        dataGenerator = new MazeDataGenerator();
        
        data = new int[,] {

            {1, 0, 1},
            {1, 1, 1}

        };

    }

    private void DisplayMaze() {
        
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        meshFilter.mesh = meshGenerator.FromData(data);
    
        MeshCollider meshCollider = go.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;

        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.materials = new Material[2] {mazeMaterial1, mazeMaterial2};

    }
    
    public void GenerateNewMaze(int sizeRows, int sizeColumns, TriggerEventHandler startCallback=null, TriggerEventHandler goalCallback=null) {
        
        if (sizeRows % 2 == 0 && sizeColumns % 2 == 0) {
            
            Debug.LogError("Odd numbers work better for dungeon size.");
        
        }

        DisposeOldMaze();

        data = dataGenerator.FromDimensions(sizeRows, sizeColumns);

        FindStartPosition();
        FindGoalPosition();

        hallwayWidth = meshGenerator.mazeWidth;
        hallwayHeight = meshGenerator.mazeHeight;

        DisplayMaze();

        PlaceStartTrigger(startCallback);
        PlaceGoalTrigger(goalCallback);

    }

    private void FindStartPosition() {

        int[,] startMaze = data;
        int maxRows = startMaze.GetUpperBound(0);
        int maxColumns = startMaze.GetUpperBound(1);

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {
                if (startMaze[i, j] == 0) {

                    startRow = i;
                    startColumn = j;
                    return;

                }
            }
        }
    }

    public void DisposeOldMaze() {

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Generated");
        
        foreach (GameObject go in objects) {
            
            Destroy(go);

        }
    }

    private void FindGoalPosition() {
    
        int[,] goalMaze = data;
        int maxRows = goalMaze.GetUpperBound(0);
        int maxColumns = goalMaze.GetUpperBound(1);

        for (int i = maxRows; i >= 0; i--) {
            for (int j = maxColumns; j >= 0; j--) {
                if (goalMaze[i, j] == 0) {

                    goalRow = i;
                    goalColumn = j;
                    return;

                }
            }
        }
    }

    private void PlaceStartTrigger(TriggerEventHandler callback) {
        
        GameObject startTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
        startTrigger.transform.position = new Vector3(startColumn * hallwayWidth, .5f, startRow * hallwayWidth);
        startTrigger.name = "Start Trigger";
        startTrigger.tag = "Generated";

        startTrigger.GetComponent<BoxCollider>().isTrigger = true;
        startTrigger.GetComponent<MeshRenderer>().sharedMaterial = startMaterial;

        TriggerEventRouter triggerEvent = startTrigger.AddComponent<TriggerEventRouter>();
        triggerEvent.callback = callback;
    
    }

    private void PlaceGoalTrigger(TriggerEventHandler callback) {

        GameObject goalTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
        goalTrigger.transform.position = new Vector3(goalColumn * hallwayWidth, .5f, goalRow * hallwayWidth);
        goalTrigger.name = "Treasure";
        goalTrigger.tag = "Generated";

        goalTrigger.GetComponent<BoxCollider>().isTrigger = true;
        goalTrigger.GetComponent<MeshRenderer>().sharedMaterial = treasureMaterial;

        TriggerEventRouter triggerEvent = goalTrigger.AddComponent<TriggerEventRouter>();
        triggerEvent.callback = callback;
    
    }

    void OnGUI() {

        if (!showDebug) {

            return;

        }

        int[,] mazeGUI = data;
        int maxRows = mazeGUI.GetUpperBound(0);
        int maxColumns = mazeGUI.GetUpperBound(1);

        string message = "";

        for (int i = maxRows; i >= 0; i--) {

            for (int j = 0; j <= maxColumns; j++) {

                if (mazeGUI[i, j] == 0) {

                    message += "....";

                } else {

                message += "==";

                }
            }

            message += "\n";

        }

        GUI.Label(new Rect(20, 20, 500, 500), message);

    }

}
