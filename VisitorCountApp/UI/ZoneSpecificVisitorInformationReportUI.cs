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
using Microsoft.Office.Interop.Excel;

namespace VisitorCountApp.UI
{
    public partial class ZoneSpecificVisitorInformationReportUI : Form
    {
        public ZoneSpecificVisitorInformationReportUI()
        {
            InitializeComponent();
            LoadZoneTypeInComboBox();
          
        }
        ZoneManager zoneManager = new ZoneManager();
        VisitorManager visitorManager=new VisitorManager();
        public void LoadZoneTypeInComboBox()
        {
            selectZoneComboBox.DataSource = zoneManager.GetZoneTypeList();
            selectZoneComboBox.DisplayMember = "ZoneType";
            selectZoneComboBox.ValueMember = "Id";
        }

        public void ShowSelectedZoneVisitors()
        {
            Zone aZone = (Zone)selectZoneComboBox.SelectedItem;
            int zoneId = aZone.Id;
            totalZoneSpecificTextBox.Text = zoneManager.GetTotalVisitor(zoneId).ToString();
            List<int> visitorIdList = visitorManager.GetVisitorIdList(zoneId);
            zoneSpecificVisitorInformationListView.Items.Clear();
            foreach (int visitorId in visitorIdList)
            {
                List<Visitor> totalVisitorList = visitorManager.GetVisitorList(visitorId);
                foreach (Visitor visitor in totalVisitorList)
                {
                    ListViewItem items = new ListViewItem();
                    items.SubItems.Add(visitor.Name);
                    items.SubItems.Add(visitor.Email);
                    items.SubItems.Add(visitor.ContactNumber);
                    zoneSpecificVisitorInformationListView.Items.Add(items);
                }
            }
        }

        private void showButton_Click(object sender, EventArgs e)
        {
            ShowSelectedZoneVisitors();
        }

        private void exportToExcelButton_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            Workbook xlWorkBook = xlApp.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet worksheet = (Worksheet)xlApp.ActiveSheet;

            xlApp.Visible = true;
         
            worksheet.Cells[1, 1] = "Name";
            worksheet.Cells[1, 2] = "Email";
            worksheet.Cells[1, 3] = "Contact Number";

            Zone aZone = (Zone)selectZoneComboBox.SelectedItem;
            int zoneId = aZone.Id;
        
            List<int> visitorIdList = visitorManager.GetVisitorIdList(zoneId);
            int row = 2;
            foreach (int visitorId in visitorIdList)
            {
                
                List<Visitor> totalVisitorList = visitorManager.GetVisitorList(visitorId);
                foreach (Visitor visitor in totalVisitorList)
                {
                    worksheet.Cells[row, 1] = visitor.Name;
                    worksheet.Cells[row, 2] = visitor.Email;
                    worksheet.Cells[row, 3] = visitor.ContactNumber;
                    row++;
                }
            }
            worksheet.Cells[row, 1] = "Total Visitor";
            worksheet.Cells[row, 2] = totalZoneSpecificTextBox.Text;
            string zoneType = aZone.ZoneType;
            xlWorkBook.SaveAs(@"G:\"+zoneType);
        
        }
    }
}
