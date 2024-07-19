using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS
{
    public class RoomTag
    {
        public string Layer { set; get; }
        public string RoomName { set; get; }
        public Point3d TextPoint { set; get; }
        public ObjectId TextId { set; get; }
    }

    public class Room
    {
        public string Name { set; get; }
        public string Handle { set; get; }
        public double Area { set; get; }
        public List<Wall> Walls = new List<Wall>();
    }

    public class Wall
    {
        public string RoomName { set; get; }
        public string Layer { set; get; }
        public string Name { set; get; }
        public Point3d StartPoint { set; get; }
        public Point3d EndPoint { set; get; }

        public string Handle { set; get; }
        public List<Window> Windows { set; get; }
        public string CommonWallName { set; get; }

        public double Length { set; get; }
        public double Angle { set; get; }
        //public ObjectId AcObjectId { set; get; }
        //public Entity AcEntity { set; get; }

        public Wall(string Layer, string WallName, string RoomName, Point3d StartPoint, Point3d EndPoint, string handle, List<Window> ListOfWindowData, string CommonWallName)
        {
            this.Layer = Layer;
            this.Name = WallName;
            this.RoomName = RoomName;

            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            this.Handle = handle;

            this.Windows = ListOfWindowData;

            this.CommonWallName = CommonWallName;
        }

        public Wall(Point3d StartPoint, Point3d EndPoint, double Length, double Angle, string handle)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            this.Length = Length;
            this.Angle = Angle;
            this.Handle = handle;
            //this.AcObjectId = AcObjectId;
            //this.AcEntity = AcEntity;
        }
        public Wall()
        {

        }
    }

    public class Window
    {
        public string Layer { set; get; }
        public string Name { set; get; }
        public double Width { set; get; }
        public double Length { set; get; }
        public string Handle { set; get; }

        public string SillHeight { set; get; }
        public string HeightOfVisionWindow { set; get; }
        public string HeightOfDaylightWindow { set; get; }
    }

    public class CornerData
    {
        public List<Point3d> ListOfCornerPoint { set; get; }
        public Wall MainLineData { set; get; }
        public int MainLineIndex { set; get; }
        public List<int> ListOfOtherLineIndex { set; get; }
        public List<Wall> ListOfOtherLineData { set; get; }
    }

    public class AECData
    {
        public List<Room> Rooms = new List<Room>();
    }

}
