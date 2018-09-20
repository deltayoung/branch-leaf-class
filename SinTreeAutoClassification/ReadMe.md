# Setup
1. `SinTreeAutoClassification` [project] > Properties > *Build* tab    
set **Output path** to the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64)    

2. `SinTreeAutoClassification` [project] > Properties > *Reference Paths* tab   
set **Reference paths** to the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64)  

3. `SinTreeAutoClassificationTester` [project] > Properties > *Build* tab    
set **Output path** to the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64)  

4. `SinTreeAutoClassificationTester` [project] > Properties > *Reference Paths* tab   
set **Reference paths** to the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64) 

5. In the targeted C++ project > Configuration Properties > General > **Output directory**    
set to the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64)  

6. Ensure `SinTreeAutoClassificationTester.exe.config` file is beeing copied during build 
into the PCS folder (C:\DC_LAS\PCS\PCS\bin\Release_HASP_x64). If not copy manualy from the
(..\branch-leaf-class\Release_HASP_x64) folder. 

# Skeleton description
## The sample C++ call `Classify` has 3 parameters:

1. `int itemNr`: number of point records in the input array (and in the output class array).  

2. `LASPointData* lasPointArray`: the input array from the LAS point records (reduced content converted to WIN32 types).   

3. `byte* classArray`: the output with the new values.
