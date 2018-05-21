// cVector.h
//#ifndef CVECTOR_H
//#define CVECTOR_H
#pragma once

#ifdef SHAPEGENERATOR_EXPORTS
#define SHAPEGENERATOR_API __declspec(dllexport)
#else
#define SHAPEGENERATOR_API __declspec(dllimport)
#endif

#include <iostream>
#include <vector> 
#include "time.h" 

#define PI 3.1415

using namespace std;

/*////////////////////////////////////////////////////
some of the implementation was done for 2D computation, however, no distinction is made between 2D vector and 3D vector
in the event when 2D vector are used, the z dimension will be ignored. 
For ease of implementation. 
/*////////////////////////////////////////////////////

#define EPS 0.0001f   // 3D cases where it's not absolute intersection , due to floating point inaccuracies 

//extern int global_int ; 

//int global_int ; 


class SHAPEGENERATOR_API cVector
{

	// do not add anything in front!! 

public:


	double vec[3] ; 
	int ref;		// ref refers to reference int, which might be a good container when trying to do back referencing. Keeping it as compact as possible
	double intensity; 
	
	//added by cy
	int type;
	int posInDown;

	// Constructor
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	cVector();
	cVector( double a, double b, double c );
	cVector( double xyz[3] );
	
	// Destructor
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	~cVector();


	// GET and SET functions - apparently, this 2 set of function allows ppl to set it when necessary 
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	double x() const { return vec[ 0 ]; }
	double y() const { return vec[ 1 ]; }
	double z() const { return vec[ 2 ]; }

	double& x() { return vec[ 0 ]; }
	double& y() { return vec[ 1 ]; }
	double& z() { return vec[ 2 ]; }

	//added by cy
	void setRef(int id)
	{
		ref = id;
	}
	int getRef()
	{
		return ref;
	}
	void setType(int t)
	{
		type = t;
	}
	int getType()
	{
		return type;
	}
	void setPosInDown()
	{
		posInDown++;
	}
	void setPosInDown(int pos)
	{
		posInDown = pos;
	}
	int getPosInDown()
	{
		return posInDown;
	}
	/*void setPosInInput(int pos)
	{
		posInInput = pos;
	}
	int getPosInInput()
	{
		return posInInput;
	}*/


	// utility functions. 
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void print();
	void blockingprint(); 
	double length() const;								// Returns the length of the vector 
	double lengthWithoutSqrt() const ;				// returns a parameter to state the magnitude of the vector, it's not the actual length
	cVector normalize();									// Normalize this vector
	void zero();											// Initialises the vector to {0,0,0}
	double distanceTo(cVector cv) const ; 
	double distanceToWithoutSqrt(cVector cv) const;		// measure the distance between points, for comparison sake, hence dun need the squareroot. 
	static double toDegree(double radian);
	static double toRadian(double degree);
	static void pre_Randomizing() ;		// call this before generating a whole list of random cVectors; 
	void randomGenerator() ;						// utility function to generate a random vector. , but remember to call pre_Randomizing once first. 
	static bool isPointOnLine(cVector lineStart, cVector lineEnd, cVector testPt ) ;


	void mergeSmall(cVector cv) ; // using for bounding Box computation, return the mimium in the 3 dimensions. 
	void mergeLarge(cVector cv); // using for bounding box computation, return the maximum in all 3 dimensions. 

	cVector calculate_NelsonMax_normal( std::vector<cVector> locs ) ; // assuming this vector contains the actual location of the vertex, and the vector contains the list of location of its neighbors


	// static functions for the computation of commonly used vector mathematics 
	// functions for 2D will have a 2D name behind, in the case where no 2D naming convention is employed, 3D case
	// will be assumed. 
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	// Intersection between 2 lines. 
	static bool doeslineIntersect2D(cVector start1 , cVector end1 , cVector start2, cVector end2); 
	static bool getLineIntersection2D(cVector start1 , cVector end1 , cVector start2, cVector end2, cVector *intersect) ; 
	static bool linelineIntersection3D(cVector line1Start , cVector line1End, cVector line2Start, cVector line2End , cVector &jointPt); 
	static bool doesLineIntersection2DInfinity(cVector start1 , cVector direction1 , cVector start2, cVector end2 , cVector *intersect ); 
	static bool linelineIntersection3D(cVector line1Start , cVector line1End, cVector line2Start, cVector line2End , cVector &jointPt1 , cVector &jointPt2); 
	static bool calculateFixedLineToLineIntersection3D(cVector start, cVector direction, cVector start2, cVector end2, cVector &intersect); // the two ends of the line is fixed. 
	static bool calculateFixedLineIntersection3D(cVector start, cVector direction, cVector start2, cVector end2, cVector &intersect);
	static bool linelineIntersection3DConstrained(cVector line1Start , cVector line1End, cVector line2Start, cVector line2End , cVector &jointPt1 , cVector &jointPt2, float closestDistance); 
	static float getKfactor(cVector point, cVector direction, cVector test); 
	
	
	// point-line functions. 
	static double distancePtLine(cVector line1, cVector line2 , cVector point ) ; 
	static cVector projectPtLineInfinity(cVector line1, cVector line2 , cVector point );
	static cVector projectPtLine(cVector line1, cVector line2 , cVector point );
	static double perpendicularDistancePtLine(cVector Line1, cVector Line2 , cVector point ); 
	
	// angle at 2 vectors. isn;t
	static double angleDiffDegree(cVector a , cVector b);
	static double angleDiffRadian(cVector a , cVector b) ; 
	static double getAcuteAngleDegree(cVector prev, cVector origin, cVector next);
	static double getAcuteAngleRadian(cVector prev, cVector origin, cVector next);
	static double antiClockwiseAngleDegree(cVector a , cVector b , cVector norm) ; 
	static double antiClockwiseAngleRadian(cVector a , cVector b , cVector norm) ; 

	// 2D point in 2D triangle test functions. 
	bool lineDivide2D(cVector start, cVector end, cVector test );
	bool SameSideTest2D(cVector start, cVector end, cVector test1, cVector test2 );
    bool PointInTriangleTest2D(cVector v1, cVector v2, cVector v3, cVector t1) ; 
	static double getCircumRadius(cVector v1, cVector v2, cVector v3 );

	// 3D point in 3D triangle test function, assumes that 3 vertices plus test point all lies on a plane. 
	static bool sameSide3D( cVector p1 , cVector p2 , cVector t1 , cVector t2 ) ; 
	static bool isPointWithinTriangle3D(cVector one, cVector two, cVector three, cVector testpoint ) ; 

	// point - plane functions, in 3D 
	static double distancePtPlane(cVector norm, cVector ptOnPlane, cVector point ) ; 
	static double signedDistancePtPlane(cVector norm, cVector ptOnPlane, cVector point ) ; 
	static bool absoluteDistancePtPlane(cVector norm, cVector ptOnPlane, cVector point ) ; 
	static cVector projectPtToPlane(cVector norm, cVector ptOnPlane, cVector point) ; 

	// line-plane functions, in 3D of course. 
	static bool getLinePlaneIntersection(cVector planePt1 , cVector realNorm , cVector lineNorm , cVector linePt1, cVector &intersectPoint ); 
	static bool doesLineIntersectTriangle(cVector v1, cVector v2, cVector v3 , cVector lineNorm , cVector linePt1) ; 
	static bool doesTriangle1IntersectTriangle2(cVector v1, cVector v2, cVector v3, cVector t1, cVector t2, cVector t3 ) ;      
	static bool tri_tri_intersection(cVector v1, cVector v2, cVector v3, cVector t1, cVector t2, cVector t3 ) ;     
	static cVector projectVectorOnPlane(cVector planeNormal, cVector v); 

	// line-sphere intersection in 3D 
	static cVector getLineCircleIntersectionSimplified(cVector p1, cVector p2, cVector centre, double radius , bool &solutionFound ); 
	static bool doesLineIntersectCircle(cVector p1, cVector p2, cVector centre, double radius ); 

	// circle functions. 
	static double getCircumCircleRadius(cVector one, cVector two, cVector three ) ; // get the radius of a circle that goes thru 3 points of the triangle.
	static double getInscribedCircleRadius(cVector one, cVector two, cVector three ) ; // get the radius of a circle that touches each of the 3 sides of the triangle 

		
	// edgeFlip in 3D
	static bool should3DEdgeFlip(cVector originalStart, cVector originalEnd, cVector newStart, cVector newEnd ); 
	static bool isPossible2DEdgeFlip(cVector v1, cVector v2, cVector v3, cVector newVector) ;  // this just ask if it is possible to edgeflip, not whether should it... 

	// given a vector of 2D cVector, find the best set out of the set such that the angle spacing is the best. 
	// it returns a value that determines the square difference to 90 degrees 
	// used as a recursive function too. 
	static bool selectBestSet(vector<cVector> &originalSet, vector<double> &angleSet, vector<int> &removeIndex) ; 

	//angle based smoothing algorithm 
	static void angleSmoothingComputation(cVector &node, vector<cVector> ccwList );
	static void angleSmoothingComputationVer2(cVector &node, vector<cVector> ccwList , vector<vector<double>> angleList);


	/*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	A little tutorial on function calls and const keyword. 

	C++ way of passing reference 

	void functionA( int &A )  { blah blah blah }
	usage: functionA( a )  // no need for *ptr or anything. 

	Reason for using passing by reference approach 
	1) modification of the class in the function
	2) prevent making a unnecessary copy of the class for the function. Esp when the class is big, might take too much time. 

	However, if taking the 2nd reason for passing by reference, if u want to prevent modification of the class within 
	the function, need to include the "const" keyword. 

	for example 
	const classA functionABC ( const int &a ) const 
	
	1st const : prevent u from doing... 
	       functionABC(some_int_variable).classA_variableB = 5 ;

    2nd const: the variable a that is passed into the function will not be changed 

	3rd const: the functionABC will not change anything in it's own class. 		

	/*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
		
	// operator overloading functions. 
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Comparison
	bool operator==( const cVector& cVtr ) const;
	bool operator!=( const cVector& cVtr) const;
	void operator=(const cVector& cVtr) ;		// replaces set function  

	//CY added
	bool operator<( const cVector& cVtr) const;
	bool isEqual(double, double) const;	

	// Point and Vector Operations
	cVector operator+( cVector& cVtr);				// Vector addition
	cVector operator+( double xyz[3] );
	void operator+=( cVector& cVtr);				// addition direct to the class itself. 

	cVector operator-( cVector& cVtr) const;		// Vector subtraction
	cVector operator-( double xyz[3] );
	void operator-=( cVector& cVtr) ;				// subtraction direct to the class itself. 


	cVector operator*(double fval);						// multiplication, different from the vector dot product 
	void operator*=( double fval );						// self multiplication  

	cVector operator/(double fval);						// division 
	void operator/=(double fval );						// self division

	double operator*( cVector &cVtr);					// Vector dot product

	// cross product follows the right hand rule. if C = A X B, the the right hand follows from A to B.. then the thumb will be where C is
	cVector operator^( cVector& cVtr);				// Vector cross product
	void operator^=(cVector	& cVtr );				// self vector cross product; 

	cVector operator~();									// self negation , returns itself and a return value
	cVector operator!();									// returns a negated version of itself 

	void set (double a, double b , double c );
	void set(double abc[3]);
	void set(cVector cv);									// will try not to use this function whenever possible, cumbersome. 

	//added by cy
	void set (double a, double b , double c, int d );


	

	// list of dubious functions, will clean up once verified it's uses... 
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	bool signedDistanceValue(cVector cV) ; 
	double distanceTo(cVector v1, cVector v2, cVector v3);				// should be set to.. distance to  (feFace) or something like dat
	bool does2DEdgeFlip(cVector start1 , cVector end1 , cVector start2, cVector end2);  // should be located in feEdge or something
	
	//TY
	int grid_idx;


	static double averageMovement; 



};



// this is for CGAL to access the 3 variables of this cVector. 
struct SHAPEGENERATOR_API Construct_coord_iterator {

	typedef  const double* result_type;

  const double* operator()(const cVector& p) const
  { return static_cast<const double*>(p.vec); }

  const double* operator()(const cVector& p, int)  const
  { return static_cast<const double*>(p.vec+3); }
};




/*

// with this, i'm officially making cVector a possible kernel-traits .. like double and int. 
//////////////////////////////////////////////////////////////

namespace CGAL 
{
	template <> 
	struct Kernel_traits<cVector>
	{
		struct Kernel 
		{
		  typedef double FT;
		  typedef double RT;
		};
	};
}

// this is for CGAL to access the 3 variables of this cVector. 
struct Construct_coord_iterator {

	typedef  const double* result_type;

  const double* operator()(const cVector& p) const
  { return static_cast<const double*>(p.vec); }

  const double* operator()(const cVector& p, int)  const
  { return static_cast<const double*>(p.vec+3); }
};


*/ 



//#endif
