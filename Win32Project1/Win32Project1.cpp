// Win32Project1.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Win32Project1.h"
#include "cVector.h"
#include "ShapeGenerator.h"
#include <iostream>
#include <fstream>




// This is an example of an exported function.

// itemNr = size of the data 
// lasPointArray is the vector of data... 
// classArray is what i need to identify/classify into trunk or crown... 

// this is where my code comes in... 
// end of the day just modify the classArray 
WIN32PROJECT1_API int Classify(int itemNr, float intensityEstimation, float neighbourhoodEstimation, LASPointData* lasPointArray, unsigned char* classArray)
{

	/*
		Here are the various parameters 

		IntensityEstimation - this parameter is an multiplier to the max intensity of the points located in the trunk. 
								Lower the value if no branches is identified. 
								Raise the value if a lot of the leaves are identified. 
								Default = 0.49f 

		NeighbourhoodEstimation - this parameter is an indication of how far to extend the search, after the seed points are 
								identified. 
								Raise the value if no branches are indentified
								Lower the value if a lot of the leaves are identified. 
								Default = 0.25f 

		
		Note: NeighbourhoodEstimation is for fine tuning, most of the time using the intensityEstimation should do the job 
	
		
		
	*/

	intensityEstimation -= 0.01f; 
	int a,  originalSize, fliteredSize;

		intensityEstimation += 0.01f; // adding 0.01f each time. 

		
		ShapeGenerator sg;
		VertexPoint vptemp;
		cVector tempVector, max, min, centroid;


		// reading from data lasPointArray 
		for (a = 0; a < itemNr; a++)
		{
			//classArray[a] = 0; // initializing to type 0

			vptemp.id = a;
			vptemp.intensity = lasPointArray[a].intensity;
			tempVector.x() = lasPointArray[a].x;
			tempVector.y() = lasPointArray[a].y;
			tempVector.z() = lasPointArray[a].z;

			vptemp.position.set(tempVector);

			// indicator for the flags 
			if (lasPointArray[a].isTrunk == 1)
				vptemp.flag = true;
			else
				vptemp.flag = false;


			if (a == 0) // first 
			{
				max.set(tempVector);
				min.set(tempVector);
			}
			else
			{
				if (tempVector.x() < min.x()) min.x() = tempVector.x();
				if (tempVector.y() < min.y()) min.y() = tempVector.y();
				if (tempVector.z() < min.z()) min.z() = tempVector.z();

				if (tempVector.x() > max.x()) max.x() = tempVector.x();
				if (tempVector.y() > max.y()) max.y() = tempVector.y();
				if (tempVector.z() > max.z()) max.z() = tempVector.z();
			}
			sg.vp.push_back(vptemp);
		}

		// recentralizing 
		centroid = (max + min) / 2.0f;

		for (a = 0; a < sg.vp.size(); a++)
		{
			sg.vp[a].position -= centroid;
			sg.vp[a].position.ref = a; // record the index position 
		}

		// neighbourhood construction
		sg.constructNearestNeighboursVer2(7);

		// getting the intensity limit estimation from trunk 
		float intensityLimit = sg.trunkBasedFiltering(intensityEstimation);


		originalSize = sg.vp.size(); // this is the segmented point cloud consisting of just the trees. (no ground, no other objects) 

		// set the lower trunk classifier 
		for (a = 0; a < sg.lower_trunk_vp.size(); a++)
			classArray[sg.lower_trunk_vp[a].id] = 40;

		
		// this is the function for the filtering of points 
		sg.constructDistanceGraph(true);
		sg.estimateParameters(intensityLimit, neighbourhoodEstimation);

		sg.filterPoints(intensityLimit, neighbourhoodEstimation);
		//sg.neighbourDistanceLimit = neighbourhoodEstimation * 0.5f;
		//sg.shortestPathBranchCreation();
	 	 

		fliteredSize = sg.vp.size(); 
		// pushing the classification results back to classArray
			for (a = 0; a < sg.vp.size(); a++)
			{
				classArray[sg.vp[a].id] = 40;
			}
		

	return 0; 
}


