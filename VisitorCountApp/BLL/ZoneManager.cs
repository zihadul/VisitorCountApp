using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorCountApp.DAL.Gateway;
using VisitorCountApp.DAL.Model;

namespace VisitorCountApp.BLL
{
    class ZoneManager
    {
        ZoneGateway zoneGateway = new ZoneGateway();


        public string SaveZoneType(Zone aZone)
        {
             return zoneGateway.SaveZone(aZone);
        }

        public List<Zone> GetZoneTypeList()
        {
            return zoneGateway.GetZoneTypeList();
        }
        public decimal GetTotalNumberOfVisitors() 
        {
            return zoneGateway.totalNumberOfVisitors;
        }
        public void UpDateTotalZoneVisitor(int zoneId)
        {
            zoneGateway.UpDateTotalZoneVisitor(zoneId);
        }
        public decimal GetTotalVisitor(int id)
        {
            return zoneGateway.GetTotalVisitors(id);
        }
        public bool IsZoneExixts(string name)
        {
            return zoneGateway.IsZoneTypeExists(name);
        }
    }
}
