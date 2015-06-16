using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorCountApp.BLL;
using VisitorCountApp.DAL.Model;

namespace VisitorCountApp.UI
{
    public partial class ZoneTypeWiseVisitorsNumberReportUI : Form
    {
        public ZoneTypeWiseVisitorsNumberReportUI()
        {
            InitializeComponent();
            LoadAllZoneTypeWithTotalVisitors();
        }
        ZoneManager zoneManager = new ZoneManager();
        public void LoadAllZoneTypeWithTotalVisitors() {

            List<Zone> zoneList = zoneManager.GetZoneTypeList();
            foreach (Zone zone in zoneList) 
            {
                ListViewItem items = new ListViewItem();
                items.SubItems.Add(zone.ZoneType);
                items.SubItems.Add(zone.Total.ToString());
                zoneTypeWiseVisitorNumberListView.Items.Add(items);
            }
            totalVisitorTextBox.Text =zoneManager.GetTotalNumberOfVisitors().ToString();
        }
    }
}
