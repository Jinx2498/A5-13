using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMeshGenerator {    
    
    public float mazeWidth;     
    public float mazeHeight;    

    public MazeMeshGenerator() {

        mazeWidth = 3.75f;
        mazeHeight = 3.5f;

    }

    public Mesh FromData(int[,] data) {

        Mesh newMaze = new Mesh();

        List<Vector3> newVertices = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        newMaze.subMeshCount = 2;
        List<int> floorTriangles = new List<int>();
        List<int> wallTriangles = new List<int>();

        int maxRows = data.GetUpperBound(0);
        int maxColumns = data.GetUpperBound(1);
        float halfHeight = mazeHeight * .5f;

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {
                if (data[i, j] != 1) {
                
                    AddQuad(Matrix4x4.TRS(new Vector3(j * mazeWidth, 0, i * mazeWidth), Quaternion.LookRotation(Vector3.up), new Vector3(mazeWidth, mazeWidth, 1)), ref newVertices, ref newUVs, ref floorTriangles);

                    if (i - 1 < 0 || data[i-1, j] == 1) {

                        AddQuad(Matrix4x4.TRS(new Vector3(j * mazeWidth, halfHeight, (i-.5f) * mazeWidth), Quaternion.LookRotation(Vector3.forward), new Vector3(mazeWidth, mazeHeight, 1)), ref newVertices, ref newUVs, ref wallTriangles);

                    }

                    if (j + 1 > maxColumns || data[i, j+1] == 1) {
                    
                        AddQuad(Matrix4x4.TRS(new Vector3((j+.5f) * mazeWidth, halfHeight, i * mazeWidth), Quaternion.LookRotation(Vector3.left), new Vector3(mazeWidth, mazeHeight, 1)), ref newVertices, ref newUVs, ref wallTriangles);
                
                    }

                    if (j - 1 < 0 || data[i, j-1] == 1) {

                        AddQuad(Matrix4x4.TRS(new Vector3((j-.5f) * mazeWidth, halfHeight, i * mazeWidth), Quaternion.LookRotation(Vector3.right), new Vector3(mazeWidth, mazeHeight, 1)), ref newVertices, ref newUVs, ref wallTriangles);
                
                    }

                    if (i + 1 > maxRows || data[i+1, j] == 1) {

                        AddQuad(Matrix4x4.TRS(new Vector3(j * mazeWidth, halfHeight, (i+.5f) * mazeWidth), Quaternion.LookRotation(Vector3.back), new Vector3(mazeWidth, mazeHeight, 1)), ref newVertices, ref newUVs, ref wallTriangles);
                
                    }
                }
            }
        }

        newMaze.vertices = newVertices.ToArray();
        newMaze.uv = newUVs.ToArray();
    
        newMaze.SetTriangles(floorTriangles.ToArray(), 0);
        newMaze.SetTriangles(wallTriangles.ToArray(), 1);

        newMaze.RecalculateNormals();

        return newMaze;

    }

    private void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs, ref List<int> newTriangles) {
    
        int index = newVertices.Count;

        Vector3 vertex1 = new Vector3(-.5f, -.5f, 0);
        Vector3 vertex2 = new Vector3(-.5f, .5f, 0);
        Vector3 vertex3 = new Vector3(.5f, .5f, 0);
        Vector3 vertex4 = new Vector3(.5f, -.5f, 0);

        newVertices.Add(matrix.MultiplyPoint3x4(vertex1));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex2));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex3));
        newVertices.Add(matrix.MultiplyPoint3x4(vertex4));

        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        newTriangles.Add(index+2);
        newTriangles.Add(index+1);
        newTriangles.Add(index);

        newTriangles.Add(index+3);
        newTriangles.Add(index+2);
        newTriangles.Add(index);
    
    }

}