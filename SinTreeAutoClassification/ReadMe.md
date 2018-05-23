# Setup
1. `SinTreeAutoClassification` [project] > Properties > *Build* tab    
set **Output path** to PCS folder (Release_HASP_x64)    

2. `SinTreeAutoClassification` [project] > Properties > *Reference Paths* tab   
set **Reference paths** to PCS folder (Release_HASP_x64)  

3. `SinTreeAutoClassificationTester` [project] > Properties > *Build* tab    
set **Output path** to PCS folder (Release_HASP_x64)  

4. In the targeted C++ project > Configuration Properties > General > **Output directory**    
set to PCS folder (Release_HASP_x64)  

5. Create `SinTreeAutoClassificationTester.exe.config`  (Or just simply copy the existing one)
fill with 
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.serviceModel>
    <bindings>
      <basicHttpBinding>
        <binding name="LicenseServiceSoap1" />
        <binding name="LicenseServiceSoap2" />
      </basicHttpBinding>
      <customBinding>
        <binding name="LicenseServiceSoap12">
          <textMessageEncoding messageVersion="Soap12" />
          <httpTransport />
        </binding>
      </customBinding>
    </bindings>
    <client>
      <endpoint address="http://localhost:8080/PCSLicenseServer/LicenseService" binding="basicHttpBinding" bindingConfiguration="LicenseServiceSoap1" contract="LicenseServiceReferencePi.LicenseServiceSoap" name="LicenseServiceSoap1" />
      <endpoint address="http://www.digicart.hu/pcs/LicenseService.asmx" binding="customBinding" bindingConfiguration="LicenseServiceSoap12" contract="LicenseServiceReferencePi.LicenseServiceSoap" name="LicenseServiceSoap12" />
      <endpoint address="http://www.digicart.hu/pcs/LicenseService.asmx" binding="basicHttpBinding" bindingConfiguration="LicenseServiceSoap2" contract="LicenseServiceReferenceDC.LicenseServiceSoap" name="LicenseServiceSoap2" />
    </client>
  </system.serviceModel>
</configuration> 
```
# Skeleton description
## The sample C++ call `Classify` has 3 parameters:

1. `int itemNr`: number of point records in the input array (and in the output class array).  

2. `LASPointData* lasPointArray`: the input array from the LAS point records (reduced content converted to WIN32 types).   

3. `byte* classArray`: the output with the new values.
