using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDS.Models
{
    [XmlRoot(ElementName = "gbXML", Namespace = "http://www.gbxml.org/schema")]
    public class GbXml
    {
        [XmlAttribute("useSIUnitsForResults")]
        public bool UseSIUnitsForResults { get; set; }

        [XmlAttribute("lengthUnit")]
        public string LengthUnit { get; set; }

        [XmlAttribute("volumeUnit")]
        public string VolumeUnit { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("SurfaceReferenceLocation")]
        public string SurfaceReferenceLocation { get; set; }

        [XmlAttribute("areaUnit")]
        public string AreaUnit { get; set; }

        [XmlAttribute("temperatureUnit")]
        public string TemperatureUnit { get; set; }

        [XmlElement("Campus")]
        public Campus Campus { get; set; }


        [XmlElement("DocumentHistory")]
        public DocumentHistory DocumentHistory { get; set; }

        public XmlSerializerNamespaces Xmlns { get; set; }

        public GbXml()
        {
            Xmlns = new XmlSerializerNamespaces();
            Xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            Xmlns.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            Xmlns.Add("xhtml", "http://www.w3.org/1999/xhtml");
        }
    }
    [XmlRoot("DocumentHistory")]
    public class DocumentHistory
    {
        [XmlElement("CreatedBy")]
        public CreatedBy CreatedBy { get; set; }

        [XmlElement("ProgramInfo")]
        public ProgramInfo ProgramInfo { get; set; }

        [XmlElement("PersonInfo")]
        public PersonInfo PersonInfo { get; set; }
    }

    public class CreatedBy
    {
        [XmlAttribute("programId")]
        public string ProgramId { get; set; }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }

        [XmlAttribute("personId")]
        public string PersonId { get; set; }
    }

    public class ProgramInfo
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("ProductName")]
        public string ProductName { get; set; }

        [XmlElement("Version")]
        public string Version { get; set; }

        [XmlElement("Platform")]
        public string Platform { get; set; }

        [XmlElement("ProjectEntity")]
        public string ProjectEntity { get; set; } // Adjust type as needed
    }

    public class PersonInfo
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [XmlElement("LastName")]
        public string LastName { get; set; }
    }

    [XmlRoot("Campus")]
    public class Campus
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Building")]
        public Building Building { get; set; }

        [XmlElement("Surface")]
        public List<Surface> Surfaces { get; set; }
    }

    public class Building
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("buildingType")]
        public string BuildingType { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Area")]
        public double Area { get; set; }

        [XmlElement("Space")]
        public List<Space> Spaces { get; set; }

        [XmlElement("BuildingStorey")]
        public BuildingStorey BuildingStorey { get; set; }
    }

    public class Space
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("buildingStoreyIdRef")]
        public string BuildingStoreyIdRef { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Area")]
        public double Area { get; set; }

        [XmlElement("Volume")]
        public double Volume { get; set; }

        [XmlElement("PeopleNumber")]
        public PeopleNumber PeopleNumber { get; set; }

        [XmlElement("LightPowerPerArea")]
        public LightPowerPerArea LightPowerPerArea { get; set; }

        [XmlElement("EquipPowerPerArea")]
        public EquipPowerPerArea EquipPowerPerArea { get; set; }
    }

    public class PeopleNumber
    {
        [XmlAttribute("unit")]
        public string Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public class LightPowerPerArea
    {
        [XmlAttribute("unit")]
        public string Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public class EquipPowerPerArea
    {
        [XmlAttribute("unit")]
        public string Unit { get; set; }

        [XmlText]
        public double Value { get; set; }
    }

    public class BuildingStorey
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Level")]
        public double Level { get; set; }
    }

    public class Surface
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("surfaceType")]
        public string SurfaceType { get; set; }

        [XmlAttribute("constructionIdRef")]
        public string ConstructionIdRef { get; set; }

        [XmlElement("AdjacentSpaceId")]
        public List<AdjacentSpaceId> AdjacentSpaceId { get; set; }

        [XmlElement("RectangularGeometry")]
        public RectangularGeometry RectangularGeometry { get; set; }

        [XmlElement("PlanarGeometry")]
        public PlanarGeometry PlanarGeometry { get; set; }

        [XmlElement("Opening")]
        public List<Opening> Openings { get; set; }
    }

    public class AdjacentSpaceId
    {
        [XmlAttribute("spaceIdRef")]
        public string SpaceIdRef { get; set; }
    }

    public class RectangularGeometry
    {
        [XmlElement("Azimuth")]
        public double Azimuth { get; set; }

        [XmlElement("CartesianPoint")]
        public CartesianPoint CartesianPoint { get; set; }

        [XmlElement("Tilt")]
        public double Tilt { get; set; }

        [XmlElement("Width")]
        public double Width { get; set; }

        [XmlElement("Height")]
        public double Height { get; set; }
    }

    public class PlanarGeometry
    {
        [XmlElement("PolyLoop")]
        public PolyLoop PolyLoop { get; set; }
    }

    public class PolyLoop
    {
        [XmlElement("CartesianPoint")]
        public List<CartesianPoint> CartesianPoints { get; set; }
    }

    public class CartesianPoint
    {
        [XmlElement("Coordinate")]
        public List<double> Coordinates { get; set; }
    }

    public class Opening
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("openingType")]
        public string OpeningType { get; set; }

        [XmlAttribute("windowTypeIdRef")]
        public string WindowTypeIdRef { get; set; }

        [XmlElement("RectangularGeometry")]
        public RectangularGeometry RectangularGeometry { get; set; }

        [XmlElement("PlanarGeometry")]
        public PlanarGeometry PlanarGeometry { get; set; }
    }
}
