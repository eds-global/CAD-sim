using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EDS.Models
{
    [XmlRoot("Campus")]
    public class Campus
    {
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
        public AdjacentSpaceId AdjacentSpaceId { get; set; }

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
