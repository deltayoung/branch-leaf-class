#pragma once

#ifdef SHAPEGENERATOR_EXPORTS
#define SHAPEGENERATOR_API __declspec(dllexport)
#else
#define SHAPEGENERATOR_API __declspec(dllimport)
#endif



#include <vector> 
#include <map> 
#include <fstream>
#include <ANN/ANN.h>
#include "cVector.h" 



const int nbhoodBig = 16; // this refers to a general 16 point neighbourhood scan 
const int nbhoodSmall = 6; // this refers to a smaller 6 point neighbourhood 


struct SHAPEGENERATOR_API LineNode
{
	int id; // position in the original vector 
	int groupID; //this is its group id.. 
	cVector position;
	bool flag;
	int groupCount;

	vector<int> upward;  // possible to spilt up to branches 
	int downward; // only possible to have one root source. 

				  // there are 3 radii for each branch node. 
	float upper_radius, middle_radius, lower_radius;
	vector<cVector> lower_pts, middle_pts, upper_pts;


};


struct SHAPEGENERATOR_API VertexPoint
{
	int id; 
	cVector position;
	cVector color;
	cVector normal;
	float	 uv[2];
	float intensity;
	float returnCount; // the hit count number..  
	cVector nearestIntensity;
	cVector nearestDistances;
	float returnNumber;  // total count

	int groupID; // initialized to -1 at the start 

	float distance;
	bool flag;

};


// creating a seperate link, as we need to keep vertexPoint struct as small as possible, else the renderer cannot read in everything 
struct SHAPEGENERATOR_API nbLinks
{
	cVector nearestIntensity;
	cVector nearestDistances;

	vector<int> neighbours;   // this would be a distance measure neighbours, only include those that are within a certain measure. 
};





class SHAPEGENERATOR_API ShapeGenerator
{
public:
	
	ShapeGenerator(void);
	~ShapeGenerator(void);

	vector<VertexPoint> vp , lower_trunk_vp; // the data set is seperated into 2 different parts, for the lower and upper trunk ...  
	vector<nbLinks> vp_neighbours; 
	vector<LineNode> branchNode; 
	int gId; 
	vector<int>  pointIndices, lineIndices, triangleIndices; 


	vector<cVector> genericCirclePts ; 
	vector<int> genericTriangleIndexing , genericLineIndexing; 


	float neighbourDistanceLimit; // make sure that neighbours are only include those in these distances 

		
	int read_pointCloud(const char * filename ); 
			

	cVector intensityToColor(double intensity); 

	void filterPoints(float intensityLimit, float nDistanceLimit ); 
	void creepingSearch(); 
	void constructNearestNeighboursVer2(int K);
	void constructNearestNeighboursVer3(int K);
	

	
	void reducingNearPoints(); 

	
	// new set of functionality 
	void shortestPathBranchCreation(); 
	void constructDistanceGraph(bool singleConnect);
	void estimateParameters(float &intensityLimit, float &nDistanceLimit);

	bool connectionTest(int a , int b); 
	void findDistanceNeighbours(); 
	void findDistanceNeighboursVer2();
	void createCylindricalBranches(float binInterval, int &rootID); 
	float estimateRadius(cVector groupStartPosition, cVector groupEndPosition , vector<int> pts );
	float estimateRadius(cVector groupStartPosition, cVector groupEndPosition , vector<int> pts , float &base_value );

	float estimateRadius(cVector groupStartPosition, cVector groupEndPosition , vector<int> pts1 , vector<int> pts2 ); 
	void pruneBranches(int nodeNum, int intervalCount); 
	void traverseBranches(int rootNodeNum); 
	void mergeBranches(float binInterval); 
	
	void estimateAllBranches(int nodeNum, vector<vector<vector<int>>> &pointsBin , float base_value  ); 
	void reEstimateSplitBranchRadius(float base_value , vector<float> & radius_distribution); 

	
	void generateGenericCirclePoints(); 
	
	// to filter the points based on the trunk information 
	float trunkBasedFiltering(float adjustment);
	void separateTrunk(); 
	void breakLink(int upBranch, int downBranch);
	void separateUpperTree(); 
	void generateHistogram(); 
};

