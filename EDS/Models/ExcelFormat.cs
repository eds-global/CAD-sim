﻿using ResourceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS.Models
{
    public class EDSExcel
    {
        public ProjectInformation projectInformation { get; set; }
        public List<EDSExcelRoom> roomList { get; set; }
    }

    public class EDSExcelRoom
    {
        public EDSRoomTag room { get; set; }
        public List<EDSExcelWall> walls { get; set; }
    }
    public class EDSExcelWall
    {
        public EDSWall wall { get; set; }
        public List<EDSWindow> windows { get; set; }
    }
}