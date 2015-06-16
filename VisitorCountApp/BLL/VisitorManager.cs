using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorCountApp.DAL.Gateway;
using VisitorCountApp.DAL.Model;

namespace VisitorCountApp.BLL
{
    class VisitorManager
    {
        VisitorGateway visitorGateway = new VisitorGateway();

        public string SaveVisitor(Visitor aVisitor)
        {
            return visitorGateway.SaveVisitors(aVisitor);
        }
        public int GetNewSavedVisitorId() 
        {
            return visitorGateway.visitorId;
        }
        public void SaveAtRelationTable(int visitorId, int zoneId)
        {
            visitorGateway.SaveAtRelationTable(visitorId, zoneId);
        }
        public List<Visitor> GetVisitorList(int id)
        {
            return visitorGateway.GetVisitorList(id);
        }
        public List<int> GetVisitorIdList(int id)
        {
            return visitorGateway.GetVisitorIdList(id);
        }
    }
}
