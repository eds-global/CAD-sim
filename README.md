# CAD-sim
1. Install Visual Studio 2022 Community Version.  Link to download https://visualstudio.microsoft.com/ 
    I tried on Visual studio 2022, we can also try it on latest Visual studio version.
image.png

2. Install ZWCAD 2024. Link to download https://www.zwsoft.com/download 
3. Open the source code in visual studio.
4. From the side bar, open Solution Explorer. If Solution Explorer is not showing in side panel, go to <View Menu> and select Solution Explorer.  
5. There two main projects in Solution Explorer - 'EDS' and 'Zoom'. Expand Both Project and check for 'References'. 
    You will see there are two zwcad references added- ZwManaged, database
image.png
 
3.  We need to add both the references from the ZWCAD installed location.
4. To do this, Right click on Reference -> Add References -> Select Browse tab -> Click Browse Button at bottom - > locate both .dll (zwManaged.dll, zwDatabaseMgd.all) in installation folder (C:\Program Files\ZWSOFT\ZWCAD 2024) one by one. -> Click OK.
image.png
5. Perform step 4 for both the project EDS and ZOOM. 
6. To Build application - right click EDS in Solution Explorer -> Click Build or same can be performed from Build Menu. 
 7. Now you can access the EDS.dll file (to include in zwcad) in the folder where the source code is kept. Path of dll file is '<your folder location>\CAD Automation\EDS_AEC\EDS_AEC\EDS\bin\Debug\EDS.dll'
8. Access the EDS.dll file and use in ZWCAD.

SDK link for AutoCAD Developer guide. https://help.autodesk.com/view/OARX/2023/ENU/?guid=GUID-390A47DB-77AF-433A-994C-2AFBBE9996AE
AutoCAD and ZwCAD is a similar software, Programming for these are more or less similar. 
