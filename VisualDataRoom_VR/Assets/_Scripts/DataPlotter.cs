using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VRTK;

public class DataPlotter : MonoBehaviour
{
    
    // Name of the input CSV file, no extension
    public string inputfile;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Indices for columns to be assigned for plot
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // Full column names
    public string xName;
    public string yName;
    public string zName;
    
    //Scale of the scatter plot
    public float plotScale = 10.0f;

    //Position adjust variable for plot
    public Vector3 pointAdjust = new Vector3(0, 0, 0);

    // The prefab for the data points that will be instantiated
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;

    // Boolean to signal for plot finished
    public bool plotted = false;

    [HideInInspector]
    public GameObject Controllerhand;

    // Use this for initialization
    void Update()
    {
        Controllerhand = GameObject.Find("LeftController");
        if (Controllerhand.GetComponent<VRTK_ControllerEvents>().triggerPressed)
        {
            
            
            // Set pointlist to results of function Reader with argument inputfile
            pointList = CSVReader.Read(inputfile);

            //Log to console
            Debug.Log(pointList);

            // Declare list of strings, fill with keys (column names)
            List<string> columnList = new List<string>(pointList[1].Keys);

            // Print number of keys (using .count)
            Debug.Log("There are " + columnList.Count + " columns in the CSV");

            foreach (string key in columnList)
                Debug.Log("Column name is " + key);

            // Assign column name from columnList to Name variables
            xName = columnList[columnX];
            yName = columnList[columnY];
            zName = columnList[columnZ];

            // Get maxes of each axis
            float xMax = FindMaxValue(xName);
            float yMax = FindMaxValue(yName);
            float zMax = FindMaxValue(zName);

            // Get minimums of each axis
            float xMin = FindMinValue(xName);
            float yMin = FindMinValue(yName);
            float zMin = FindMinValue(zName);

            //float Xadj = (xMax - xMin) / 2 * xMax;
            //float Yadj = (yMax - xMin) / 2 * yMax;
            //float Zadj = (zMax - xMin) / 2 * zMax;
            float Xadj = 0.0f;
            float Yadj = 0.0f;
            float Zadj = 0.0f;
            //Debug.Log("This are"+Xadj+ Yadj+ Zadj);

            //Remove the previous plot if executed before
           
            if (plotted == true)
            {

                for (var i = 0; i < pointList.Count; i++)
                {
                    // Assigns original values to dataPointName
                    string dataPointNameused =
                        pointList[i][xName] + " "
                        + pointList[i][yName] + " "
                        + pointList[i][zName];

                    GameObject dataPointToDelete = GameObject.Find(dataPointNameused);
                    Destroy(dataPointToDelete);
                }
            }

            //Loop through Pointlist
            for (var i = 0; i < pointList.Count; i++)
            {
                // Get value in poinList at ith "row", in "column" Name, normalize
                float x =
                    ((System.Convert.ToSingle(pointList[i][xName]) - xMin)
                    / (xMax - xMin)) - Xadj;

                float y =
                    ((System.Convert.ToSingle(pointList[i][yName]) - yMin)
                    / (yMax - yMin)) - Yadj;

                float z =
                    ((System.Convert.ToSingle(pointList[i][zName]) - zMin)
                    / (zMax - zMin)) - Zadj;

                Debug.Log(new Vector3(xMax - xMin, yMax - yMin, zMax - zMin));


                // Instantiate as gameobject variable so that it can be manipulated within loop
                GameObject dataPoint = Instantiate(
                        PointPrefab,
                        (new Vector3(x, y, z) + pointAdjust) * plotScale,
                        Quaternion.identity);

                // Make child of PointHolder object, to keep points within container in hiearchy
                dataPoint.transform.parent = PointHolder.transform;

                // Assigns original values to dataPointName
                string dataPointName =
                    pointList[i][xName] + " "
                    + pointList[i][yName] + " "
                    + pointList[i][zName];

                // Assigns name to the prefab
                dataPoint.transform.name = dataPointName;

                // Gets material color and sets it to a new RGB color we define
                dataPoint.GetComponent<Renderer>().material.color =
                    new Color(x, y, z, 1.0f);
            }

            //Switch boolean because it is plotted
            plotted = true;
        }
    }

    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {

        float minValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }

        return minValue;
    }

}
