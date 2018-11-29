using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VisualDataLoader : MonoBehaviour {

	// Name of the input CSV file, no extension
	private string inputfile = "iris";

	// List for holding data from CSV reader
	private List<Dictionary<string, object>> pointList;
    private List<float> yList = new List<float>();

    // Indices for columns to be assigned for plot
    public int columnX = 0;
	public int columnY = 1;
	public int columnZ = 2;

	// Full column names
	private string xName;
	private string yName;
	private string zName;

	//Scale of the scatter plot
	private float plotScale = 5.0f;

	//Position adjust variable for plot
	public Vector3 pointAdjust = new Vector3(0, 0, 0);

	//The prefab for the data points that will be instantiated
	public GameObject PointPrefab;

	//Object which will contain instantiated prefabs in hierarchy
	public GameObject PointHolder;

	// Boolean to signal for plot finished
	private bool plotted = false;

    public OVRInput.Controller controller;

    // Use this for initialization
    void Start () {

	    
    }

    // Update is called once per frame
    void Update()
	{
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0.5f && plotted == false)
		{
		    Debug.Log("Trigger Pressed");
            // Set pointlist to results of function Reader with argument inputfile
            pointList = CSVReader.Read(inputfile);

			//Log to console
			Debug.Log("The CSV file was read ");
			// Declare list of strings, fill with keys (column names)
			List<string> columnList = new List<string>(pointList[1].Keys);

            // Print number of keys (using .count)
            Debug.Log("There are " + columnList.Count + " columns in the CSV file");

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

            //Loop through Pointlist
            for (var i = 0; i < pointList.Count; i++)
		    {
		        // Get value in poinList at ith "row", in "column" Name, normalize
		        float xpercent =
		            ((Convert.ToSingle(pointList[i][xName]) - xMin)
		             / (xMax - xMin));

		        float ypercent = 0.05f; //Make the Databalls stay on the floor
		        yList.Add((Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin));

		        float zpercent =
		            ((Convert.ToSingle(pointList[i][zName]) - zMin)
		             / (zMax - zMin));

		        // Instantiate as gameobject variable so that it can be manipulated within loop
		        GameObject dataPoint = Instantiate(
		            PointPrefab,
		            (new Vector3(xpercent, ypercent, zpercent)) * plotScale,
		            Quaternion.identity);

		        // Make child of PointHolder object, to keep points within container in hierarchy
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
		            new Color(xpercent, yList[i], zpercent, 1.0f);
                
		        //Flag the plot as done
		        plotted = true;
		    }
        }

	    if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.5f && plotted == true)
        {
            Debug.Log("Grip Pressed & boolean true");
            for (var i = 0; i < pointList.Count; i++)
	        {
	            
                // Takes the name of prefab
                string dataPointName = pointList[i][xName] + " "
	                                                       + pointList[i][yName] + " "
	                                                       + pointList[i][zName];

	            GameObject dataPointToMove = GameObject.Find(dataPointName);

	            iTween.MoveTo(dataPointToMove,
	                iTween.Hash(
	                    "position", new Vector3(dataPointToMove.transform.position.x,
	                        (yList[i] * plotScale),
	                        dataPointToMove.transform.position.z),
	                    "time", 2f,
	                    "easetype", "easeInOutQuad"
	                )
	            );
	        }
            Debug.Log(+ yList.Count + " Datapoints positioned");
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
