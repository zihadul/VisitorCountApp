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
    public partial class VisitorEntryUI : Form
    {
        public VisitorEntryUI()
        {
            InitializeComponent();
            LoadGroupBoxWithZoneTypeCheckBox();
            
        }
        ZoneManager zoneManager = new ZoneManager();
        VisitorManager visitorManager = new VisitorManager();

        public void LoadGroupBoxWithZoneTypeCheckBox()
        {
            int position=30;
            List<Zone> zoneList = zoneManager.GetZoneTypeList();
            foreach (Zone zone in zoneList) 
            {
                CheckBox aCheckBox = new CheckBox();
                aCheckBox.Text = zone.ZoneType;
                aCheckBox.Tag = (Zone)zone;
                aCheckBox.Checked = true;
                aCheckBox.AutoSize = true;
                aCheckBox.Location = new Point(20, position);
                visitorGroupBox.Controls.Add(aCheckBox);
                position += 20;
            }
        }

        public void CheckedZoneSave(int visitorId) 
        {
            
            foreach (Control box in visitorGroupBox.Controls)
            {
                CheckBox tempBox = (CheckBox)box;
                if (tempBox.Checked)
                {
                    Zone selectedZone = (Zone)tempBox.Tag;
                    Zone aZone = new Zone();
                    aZone.Id = selectedZone.Id;
                    visitorManager.SaveAtRelationTable(visitorId, aZone.Id);
                    zoneManager.UpDateTotalZoneVisitor(aZone.Id);
                }
            }
        }
        public bool IsCheckBoxChecked()
        {
            bool check = false;
            foreach (Control box in visitorGroupBox.Controls)
            {
                CheckBox tempBox = (CheckBox)box;
                if (tempBox.Checked)
                {
                    check = true;
                }
            }
            return check;
        }
        private void visitorEntrySaveButton_Click(object sender, EventArgs e)
        {
            Visitor aVisitor = new Visitor();
            aVisitor.Name = nameTextBox.Text;
            aVisitor.Email = emailTextBox.Text;
            aVisitor.ContactNumber = contactNumberTextBox.Text;
            if (aVisitor.Name == "" || aVisitor.Email == "" || aVisitor.ContactNumber == "") {
                MessageBox.Show(@"Please Fillup all Information!", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!IsCheckBoxChecked()) 
            {
                MessageBox.Show("Select ZoneType!", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show( visitorManager.SaveVisitor(aVisitor));

            int visitorId = visitorManager.GetNewSavedVisitorId();
            CheckedZoneSave(visitorId);
            this.Close();
        }

       
        
    }
}
