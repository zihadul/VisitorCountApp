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
    public partial class ZoneTypeEntryUI : Form
    {
        public ZoneTypeEntryUI()
        {
            InitializeComponent();
            LoadAllZoneType();
        }

        ZoneManager zoneManager = new ZoneManager();

        private void zoneTypeSaveButton_Click(object sender, EventArgs e)
        {
            Zone aZone = new Zone();
            aZone.ZoneType = typeNameTextBox.Text;
            if (aZone.ZoneType == "")
            {
                MessageBox.Show("Enter Zone type!");
                return;
            }
            if (zoneManager.IsZoneExixts(aZone.ZoneType))
            {
                MessageBox.Show("This Zone Type Already Exists!");
                return;
            }
            MessageBox.Show(zoneManager.SaveZoneType(aZone));
            typeNameTextBox.Clear();
            LoadAllZoneType();
        }

        public void LoadAllZoneType()
        {
            List<Zone> zoneList = zoneManager.GetZoneTypeList();
            zoneTypeListView.Items.Clear();
            foreach (Zone zone in zoneList) 
            {
                ListViewItem items = new ListViewItem();
                items.SubItems.Add(zone.ZoneType);
                zoneTypeListView.Items.Add(items);
            }
        }
         
    }
}
