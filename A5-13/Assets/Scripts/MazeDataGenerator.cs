using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator {

    public float threshold;

    public MazeDataGenerator() {

        threshold = .1f;   
                                 
    }

    public int[,] FromDimensions(int rows_Size, int columns_Size) {

        int[,] mazeData = new int [rows_Size, columns_Size];
        
        int maxRows = mazeData.GetUpperBound(0);
        int maxColumns = mazeData.GetUpperBound(1);

        for (int i = 0; i <= maxRows; i++) {
            for (int j = 0; j <= maxColumns; j++) {
            
                if (i == 0 || j == 0 || i == maxRows || j == maxColumns) {

                    mazeData[i, j] = 1;

                } else if (i % 2 == 0 && j % 2 == 0) {

                    if (Random.value > threshold) {
                    
                        mazeData[i, j] = 1;

                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        mazeData[i+a, j+b] = 1;
                    }

                }

            }

        }

        return mazeData;

    }

}
