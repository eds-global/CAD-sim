# Project Installation Guide for CAD-sim

1. **Install Visual Studio 2022 Community Version**

   - Download Visual Studio 2022 from [here](https://visualstudio.microsoft.com/).

   ![Visual Studio 2022](images/visual_studio.png)

2. **Install ZWCAD 2024**

   - Download ZWCAD 2024 from [here](https://www.zwsoft.com/download).

   ![ZWCAD 2024](images/zwcad.png)

3. **Open the Source Code in Visual Studio**

4. **View Solution Explorer**

   If Solution Explorer is not visible:
   - Go to **View** > **Solution Explorer**.

5. **Add ZWCAD References to Projects**

   - Right-click on **References** > **Add References** > **Browse** tab > Click **Browse** > Locate `zwManaged.dll` and `zwDatabaseMgd.dll` in `C:\Program Files\ZWSOFT\ZWCAD 2024` > Click **OK**.

   ![Add References](images/add_references.png)

   Perform the above steps for both the 'EDS' and 'Zoom' projects.

6. **Build Application**

   - Right-click on **EDS** in Solution Explorer > **Build**.

7. **Access EDS.dll**

   The built `EDS.dll` file can be found at `<your folder location>\CAD Automation\EDS_AEC\EDS_AEC\EDS\bin\Debug\EDS.dll`.

8. **Integrate EDS.dll with ZWCAD**

9. **SDK Links**

   - [AutoCAD Developer Guide](https://help.autodesk.com/view/OARX/2023/ENU/?guid=GUID-390A47DB-77AF-433A-994C-2AFBBE9996AE)
