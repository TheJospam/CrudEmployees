﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudEmployees
{
    public partial class Tables : Form
    {
        Conexion c = null;
        DataSet ds = null;
        List<String> deptnums;
        List<String> deptnames;
        List<String> bonustype;
        List<String> bonustypeno;
        List<String> deducttype;
        List<String> deducttypeno;
        
        Panel[] panels;
        Button[] addButtons;
        DataGridView[] tables;
        public Tables()
        {
            InitializeComponent();

            
            
            //Datos del Form y tamaños
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            /*employeesTable.Width = this.Width - this.Width/28;
            employeesTable.Height = this.Height / 3;
            employeesTable.Location = new Point(this.Width/56,this.Height*2/5);*/

            //Datos del registro de empleados
            /*employeesPanel.Visible = false;
            employeesPanel.Width = this.Width - this.Width / 27;
            employeesPanel.Height = this.Height / 5;
            employeesPanel.Location = new Point(this.Width / 54, this.Height / 5);
            fnEText.Width = employeesPanel.Width / 7;
            lnEText.Width = employeesPanel.Width / 7;
            genECombo.Width = employeesPanel.Width / 7;
            bdEPicker.Width = employeesPanel.Width / 7;
            hdEPicker.Width = employeesPanel.Width / 7;
            addRecord.Width = employeesPanel.Width / 7;
            addRecord.Height = employeesPanel.Height / 3;
            hideFields.Height = employeesPanel.Height / 3;
            hideFields.Width = employeesPanel.Width / 7;
            fnELabel.Location = new Point(employeesPanel.Width / 49, 0);
            fnEText.Location = new Point(employeesPanel.Width / 49, fnELabel.Height*16/3);
            lnELabel.Location = new Point(employeesPanel.Width *9/ 49, 0 );
            lnEText.Location = new Point(employeesPanel.Width *9/ 49, lnELabel.Height * 16 / 3);
            genELabel.Location = new Point(employeesPanel.Width *17/ 49, 0);
            genECombo.Location = new Point(employeesPanel.Width*17 / 49, genELabel.Height * 16 / 3);
            bdELabel.Location = new Point(employeesPanel.Width *25/ 49, 0);
            bdEPicker.Location = new Point(employeesPanel.Width *25/ 49, bdELabel.Height * 16 / 3);
            hdELabel.Location = new Point(employeesPanel.Width *33/ 49, 0);
            hdEPicker.Location = new Point(employeesPanel.Width *33/ 49, hdELabel.Height * 16 / 3);
            addRecord.Location = new Point(employeesPanel.Width * 41 / 49, 0);
            hideFields.Location = new Point(employeesPanel.Width * 41 / 49, hdELabel.Height * 16 / 3);*/

            //Datos del panel de Búsqueda
            /*searchPanel.Visible = false;
            searchPanel.Width = this.Width - this.Width / 27;
            searchPanel.Height = this.Height / 5;
            searchPanel.Location = new Point(this.Width / 54, this.Height / 5);
            enSText.Width = searchPanel.Width / 5;
            fnSText.Width = searchPanel.Width / 5;
            lnSText.Width = searchPanel.Width / 5;
            search.Width = searchPanel.Width / 5;
            searchInfo.Location = new Point(0, 0);
            enSearch.Location = new Point(0, searchInfo.Height * 17 / 15);
            enSText.Location = new Point(0, enSearch.Location.Y + enSearch.Height * 17 / 15);
            */



            //Datos del panel de control
            /*panel2.Width = this.Width - this.Width / 27;
            panel2.Height = this.Height / 5;
            panel2.Location = new Point(this.Width / 54, this.Height*4 / 5);
            showFields.Height = panel2.Height / 3;
            showFields.Width = panel2.Width / 7;
            deleteRecord.Height = panel2.Height / 3;
            deleteRecord.Width = panel2.Width / 7;
            editRecord.Height = panel2.Height / 3;
            editRecord.Width = panel2.Width / 7;
            showSearch.Height = panel2.Height / 3;
            showSearch.Width = panel2.Width / 7;
            showSearch.Location = new Point(0,0);
            showFields.Location = new Point(showSearch.Width + showSearch.Width / 2 , 0);
            editRecord.Location = new Point(showFields.Location.X + showFields.Width*3/2, 0);
            deleteRecord.Location = new Point(panel2.Width-deleteRecord.Width, 0);*/
            panels = new Panel[]{employeesPanel, departmentsPanel, managerPanel, deptempPanel, titlesPanel, salariesPanel,
                                 bonusPanel, deductionPanel, holidayPanel, sickleavePanel};

            addButtons = new Button[] {addEmployee,addDepartment,addManager,addDeptEmp,addTitle,addSalary,
                                       addBonus, addDeductions, addHolidays, addSickleave};

            tables = new DataGridView[] { employeesTable, departmentsTable, deptmanagerTable, deptempTable, titlesTable, salariesTable,
                                         bonusTable, deductionsTable, holidayTable, sickleaveTable, paydetailsTable, payhistoryTable};

            searchBar.GotFocus += search_GotFocus;
            searchBar.LostFocus += search_LostFocus;
            
            c = new Conexion();
            this.setData("employees");

            ds.Dispose();
            bonustype = c.getBonusType();
            bonustype = c.getNames("bonustype");
            btnBCombo.Items.Clear();
            foreach(String btn in bonustype)
            {
                btnBCombo.Items.Add(btn);
            }
            deducttype = c.getDeductType();
            deducttype = c.getNames("deducttype");
            dtnDSCombo.Items.Clear();
            foreach (String btn in deducttype)
            {
                dtnDSCombo.Items.Add(btn);
            }

            
            deptnums = c.getDeptId();
            deptnames = c.getNames("departments");
            foreach (String deptno in deptnames)
            {
                Console.WriteLine(deptno);
            }
            
        }
        private void setData(string table)
        {
            switch (table)
            {
                case "employees":
                    ds = new DataSet();
                    ds = c.getData(table);

                    employeesTable.DataSource = ds.Tables[table].DefaultView;
                    employeesTable.Columns[0].HeaderText = "Employee Number";
                    employeesTable.Columns[1].HeaderText = "First Name";
                    employeesTable.Columns[2].HeaderText = "Last Name";
                    employeesTable.Columns[3].HeaderText = "Gender";
                    employeesTable.Columns[4].HeaderText = "Birth Date";
                    employeesTable.Columns[5].HeaderText = "Hire Date";
                    showingEmployees.Text = "Showing last 100 hired employees";
                    break;
                case "departments":
                    ds = new DataSet();
                    ds = c.getData(table);
                    departmentsTable.DataSource = ds.Tables[table].DefaultView;
                    departmentsTable.Columns[0].HeaderText = "Department Number";
                    departmentsTable.Columns[1].HeaderText = "Department Name";
                    showingDepartments.Text = "Showing all departments";
                    break;
                case "dept_manager":
                    ds = new DataSet();
                    ds = c.getData(table);
                    deptmanagerTable.DataSource = ds.Tables[table].DefaultView;
                    deptmanagerTable.Columns[0].HeaderText = "Employee Number";
                    deptmanagerTable.Columns[1].HeaderText = "First Name";
                    deptmanagerTable.Columns[2].HeaderText = "Last Name";
                    deptmanagerTable.Columns[3].HeaderText = "Department Number";
                    deptmanagerTable.Columns[4].HeaderText = "From Date";
                    deptmanagerTable.Columns[5].HeaderText = "To Date";
                    showingManagers.Text = "Showing all managers";
                    break;
                case "dept_emp":
                    ds = new DataSet();
                    ds = c.getData(table);
                    deptempTable.DataSource = ds.Tables[table].DefaultView;
                    deptempTable.Columns[0].HeaderText = "Employee Number";
                    deptempTable.Columns[1].HeaderText = "First Name";
                    deptempTable.Columns[2].HeaderText = "Last Name";
                    deptempTable.Columns[3].HeaderText = "Department Number";
                    deptempTable.Columns[4].HeaderText = "From Date";
                    deptempTable.Columns[5].HeaderText = "To Date";
                    showingDeptemp.Text = "Showing last 100 department changes";
                    break;
                case "titles":
                    ds = new DataSet();
                    ds = c.getData(table);
                    titlesTable.DataSource = ds.Tables[table].DefaultView;
                    titlesTable.Columns[0].HeaderText = "Employee Number";
                    titlesTable.Columns[1].HeaderText = "First Name";
                    titlesTable.Columns[2].HeaderText = "Last Name";
                    titlesTable.Columns[3].HeaderText = "Title";
                    titlesTable.Columns[4].HeaderText = "From Date";
                    titlesTable.Columns[5].HeaderText = "To Date";
                    showingTitles.Text = "Showing last 100 title changes";
                    break;
                case "salaries":
                    ds = new DataSet();
                    ds = c.getData(table);
                    salariesTable.DataSource = ds.Tables[table].DefaultView;
                    salariesTable.Columns[0].HeaderText = "Employee Number";
                    salariesTable.Columns[1].HeaderText = "First Name";
                    salariesTable.Columns[2].HeaderText = "Last Name";
                    salariesTable.Columns[3].HeaderText = "Salary";
                    salariesTable.Columns[4].HeaderText = "From Date";
                    salariesTable.Columns[5].HeaderText = "To Date";
                    showingSalaries.Text = "Showing last 100 salary changes";
                    break;
                case "bonus":
                    ds = new DataSet();
                    ds = c.getData(table);
                    bonusTable.DataSource = ds.Tables[table].DefaultView;
                    bonusTable.Columns[0].HeaderText = "Employee Number";
                    bonusTable.Columns[1].HeaderText = "Bonus Date";
                    bonusTable.Columns[2].HeaderText = "Bonus Amount";
                    bonusTable.Columns[3].HeaderText = "Bonus Type";
                    showingBonus.Text = "Showing last 100 bonuses";
                    break;
                case "deduction":
                    ds = new DataSet();
                    ds = c.getData(table);
                    deductionsTable.DataSource = ds.Tables[table].DefaultView;
                    deductionsTable.Columns[0].HeaderText = "Employee Number";
                    deductionsTable.Columns[1].HeaderText = "Deduct Date";
                    deductionsTable.Columns[2].HeaderText = "Deduct Amount";
                    deductionsTable.Columns[3].HeaderText = "Deduct Type";
                    showingDeductions.Text = "Showing last 100 deductions";
                    break;
                case "holiday":
                    ds = new DataSet();
                    ds = c.getData(table);
                    holidayTable.DataSource = ds.Tables[table].DefaultView;
                    holidayTable.Columns[0].HeaderText = "Employee Number";
                    holidayTable.Columns[1].HeaderText = "Start Date";
                    holidayTable.Columns[2].HeaderText = "End Date";
                    showingHolidays.Text = "Showing last 100 holidays";
                    break;
                case "sickleave":
                    ds = new DataSet();
                    ds = c.getData(table);
                    sickleaveTable.DataSource = ds.Tables[table].DefaultView;
                    sickleaveTable.Columns[0].HeaderText = "Employee Number";
                    sickleaveTable.Columns[1].HeaderText = "Start Date";
                    sickleaveTable.Columns[2].HeaderText = "End Date";
                    sickleaveTable.Columns[3].HeaderText = "Reason";
                    showingSickleaves.Text = "Showing last 100 sick leaves";
                    break;
                case "paydetails":
                    ds = new DataSet();
                    ds = c.getData(table);
                    paydetailsTable.DataSource = ds.Tables[table].DefaultView;
                    paydetailsTable.Columns[0].HeaderText = "Employee Number";
                    paydetailsTable.Columns[1].HeaderText = "Start Date";
                    paydetailsTable.Columns[2].HeaderText = "Routing Number";
                    paydetailsTable.Columns[3].HeaderText = "Account Type";
                    paydetailsTable.Columns[4].HeaderText = "Bank Name";
                    paydetailsTable.Columns[5].HeaderText = "Bank Address";
                    paydetailsTable.Columns[6].HeaderText = "Pay Type";
                    showingPaydetails.Text = "Showing last 100 paid details";
                    break;
                case "payhistory":
                    ds = new DataSet();
                    ds = c.getData(table);
                    payhistoryTable.DataSource = ds.Tables[table].DefaultView;
                    payhistoryTable.Columns[0].HeaderText = "Pay Number";
                    payhistoryTable.Columns[1].HeaderText = "Employee Number";
                    payhistoryTable.Columns[2].HeaderText = "Pay Date";
                    payhistoryTable.Columns[3].HeaderText = "Check Number";
                    payhistoryTable.Columns[4].HeaderText = "Pay Amount";
                    showingPayhistory.Text = "Showing last 100 payments";
                    break;
            }
        }
        private void InsertData()
        {
            Object[] row;
            int index = tabControl1.SelectedIndex;
            switch (index)
            {
                case 0:
                    row = new Object[5];


                    row[0] = fnEText.Text;
                    row[1] = lnEText.Text;
                    row[2] = genECombo.SelectedItem.ToString();
                    row[3] = bdEPicker.Value;
                    row[4] = hdEPicker.Value;
                    c.Insert("employees", row);
                    ds = new DataSet();
                    ds = c.getData("employees");
                    employeesTable.DataSource = ds.Tables["employees"].DefaultView;
                    employeesTable.Refresh();
                    ds.Dispose();
                    fnEText.Text = "";
                    lnEText.Text = "";
                    genECombo.SelectedIndex = -1;
                    bdEPicker.Value = DateTime.Now;
                    hdEPicker.Value = DateTime.Now;
                    break;
                case 1:
                    row = new Object[2];
                    row[0] = dnoDText.Text;
                    row[1] = dnDText.Text;
                    c.Insert("departments", row);
                    ds = new DataSet();
                    ds = c.getData("departments");
                    departmentsTable.DataSource = ds.Tables["departments"].DefaultView;
                    departmentsTable.Refresh();
                    ds.Dispose();
                    dnoDText.Text = "";
                    dnDText.Text = "";
                    break;
                case 2:
                    row = new Object[4];
                    row[0] = enDMText.Text;
                    //row[1] = dnoDMText.Text;
                    row[1] = c.getID("departments",dnoDMCombo.SelectedItem.ToString());
                    //row[1] = dnoDMCombo.SelectedItem.ToString()
                    row[2] = fdDMPicker.Value;
                    row[3] = tdDMPicker.Value;
                    c.Insert("dept_manager", row);
                    ds = new DataSet();
                    ds = c.getData("dept_manager");
                    deptmanagerTable.DataSource = ds.Tables["dept_manager"].DefaultView;
                    deptmanagerTable.Refresh();
                    ds.Dispose();
                    enDMText.Text = "";
                    //dnoDMText.Text = "";
                    dnoDMCombo.Items.Clear();
                    deptnums = c.getDeptId();
                    deptnames = c.getNames("departments");
                    
                    foreach (String dno in deptnames)
                    {
                        dnoDMCombo.Items.Add(dno);
                    }
                    dnoDMCombo.SelectedIndex = -1;
                    fdDMPicker.Value = DateTime.Now;
                    tdDMPicker.Value = DateTime.Now;
                    break;
                case 3:
                    row = new object[4];
                    row[0] = enDEText.Text;
                    //row[1] = dnoDEText.Text;
                    row[1] = c.getID("departments",dnoDECombo.SelectedItem.ToString());
                    //row[1] = dnoDECombo.SelectedItem.ToString();
                    row[2] = fdDEPicker.Value;
                    row[3] = tdDEPicker.Value;
                    c.Insert("dept_emp", row);
                    ds = new DataSet();
                    ds = c.getData("dept_emp");
                    deptempTable.DataSource = ds.Tables["dept_emp"].DefaultView;
                    deptempTable.Refresh();
                    ds.Dispose();
                    enDEText.Text = "";
                    //dnoDEText.Text = "";
                    dnoDECombo.Items.Clear();
                    deptnums = c.getDeptId();

                    foreach (String dno in deptnums)
                    {
                        dnoDECombo.Items.Add(dno);
                    }
                    dnoDECombo.SelectedIndex = -1;
                    fdDEPicker.Value = DateTime.Now;
                    tdDEPicker.Value = DateTime.Now;
                    break;
                case 4:
                    row = new Object[4];
                    row[0] = enTText.Text;
                    row[1] = tiTText.Text;
                    row[2] = fdTPicker.Value;
                    row[3] = tdTPicker.Value;
                    c.Insert("titles", row);
                    ds = new DataSet();
                    ds = c.getData("titles");
                    deptmanagerTable.DataSource = ds.Tables["titles"].DefaultView;
                    deptmanagerTable.Refresh();
                    ds.Dispose();
                    enTText.Text = "";
                    tiTText.Text = "";
                    fdTPicker.Value = DateTime.Now;
                    tdTPicker.Value = DateTime.Now;
                    break;
                case 5:
                    row = new Object[4];
                    row[0] = enSText.Text;
                    row[1] = salSText.Text;
                    row[2] = fdSPicker.Value;
                    row[3] = tdSPicker.Value;
                    c.Insert("salaries", row);
                    ds = new DataSet();
                    ds = c.getData("salaries");
                    deptmanagerTable.DataSource = ds.Tables["salaries"].DefaultView;
                    deptmanagerTable.Refresh();
                    ds.Dispose();
                    enSText.Text = "";
                    salSText.Text = "";
                    fdSPicker.Value = DateTime.Now;
                    tdSPicker.Value = DateTime.Now;
                    break;
                case 6:
                    row = new Object[4];
                    row[0] = enBText.Text;
                    row[1] = bodBPicker.Value;
                    row[2] = baBText.Text;
                    row[3] = c.getID("bonustype",btnBCombo.SelectedItem.ToString());
                    //row[3] = btnBCombo.SelectedIndex + 1001;
                    c.Insert("bonus", row);
                    ds = new DataSet();
                    ds = c.getData("bonus");
                    bonusTable.DataSource = ds.Tables["bonus"].DefaultView;
                    bonusTable.Refresh();
                    ds.Dispose();
                    enBText.Text = "";
                    bodBPicker.Value = DateTime.Now;
                    baBText.Text = "";
                    btnBCombo.SelectedIndex = -1;
                    break;
                case 7:
                    row = new Object[4];
                    row[0] = enDSText.Text;
                    row[1] = ddDSPicker.Value;
                    row[2] = daDSText.Text;
                    row[3] = c.getID("deducttype", dtnDSCombo.SelectedItem.ToString());
                    //row[3] = dtnDSCombo.SelectedIndex + 3001;
                    c.Insert("deduction", row);
                    ds = new DataSet();
                    ds = c.getData("deduction");
                    deductionsTable.DataSource = ds.Tables["deduction"].DefaultView;
                    deductionsTable.Refresh();
                    ds.Dispose();
                    enDSText.Text = "";
                    ddDSPicker.Value = DateTime.Now;
                    daDSText.Text = "";
                    dtnDSCombo.SelectedIndex = -1;
                    break;
                case 8:
                    row = new Object[3];
                    row[0] = enHText.Text;
                    row[1] = sdHPicker.Value;
                    row[2] = edHPicker.Value;
                    c.Insert("holiday", row);
                    ds = new DataSet();
                    ds = c.getData("holiday");
                    holidayTable.DataSource = ds.Tables["holiday"].DefaultView;
                    holidayTable.Refresh();
                    ds.Dispose();
                    enHText.Text = "";
                    sdHPicker.Value = DateTime.Now;
                    edHPicker.Value = DateTime.Now;
                    break;
                case 9:
                    row = new Object[4];
                    row[0] = enSLText.Text;
                    row[1] = sdSLPicker.Value;
                    row[2] = edSLPicker.Value;
                    row[3] = reSLText.Text;
                    c.Insert("sickleave", row);
                    ds = new DataSet();
                    ds = c.getData("sickleave");
                    sickleaveTable.DataSource = ds.Tables["sickleave"].DefaultView;
                    sickleaveTable.Refresh();
                    ds.Dispose();
                    enSLText.Text = "";
                    sdSLPicker.Value = DateTime.Now;
                    edSLPicker.Value = DateTime.Now;
                    reSLText.Text = "";
                    break;

            }
            tables[index].Refresh();
        }

        private void EditData()
        {
            int index = tabControl1.SelectedIndex;
            Object[] row;
            if (tables[index].SelectedCells.Count > 0)
            {
                int selectedrowindex = tables[index].SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tables[index].Rows[selectedrowindex];

                switch (index)
                {
                    case 0:
                        row = new object[6];
                        row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                        row[1] = fnEText.Text;
                        row[2] = lnEText.Text;
                        row[3] = genECombo.SelectedItem.ToString();
                        row[4] = bdEPicker.Value;
                        row[5] = hdEPicker.Value;
                        c.Edit("employees", row);
                        break;
                    case 1:
                        row = new object[2];
                        row[0] = dnoDText.Text;
                        row[1] = dnDText.Text;
                        c.Edit("departments", row);
                        break;
                    case 2:
                        row = new object[4];
                        row[0] = enDMText.Text;
                        //row[1] = dnoDMText.Text;
                        row[1] = c.getID("departments", dnoDMCombo.SelectedItem.ToString());
                        //row[1] = dnoDMCombo.SelectedItem.ToString();
                        row[2] = fdDMPicker.Value;
                        row[3] = tdDMPicker.Value;
                        c.Edit("dept_manager", row);
                        break;
                    case 3:
                        row = new object[4];
                        row[0] = enDEText.Text;
                        //row[1] = dnoDEText.Text;
                        row[1] = c.getID("departments", dnoDECombo.SelectedItem.ToString());
                        //row[1] = dnoDECombo.SelectedItem.ToString();
                        row[2] = fdDEPicker.Value;
                        row[3] = tdDEPicker.Value;
                        c.Edit("dept_emp", row);
                        break;
                    case 4:
                        row = new object[4];
                        row[0] = enTText.Text;
                        row[1] = tiTText.Text;
                        row[2] = fdTPicker.Value;
                        row[3] = tdTPicker.Value;
                        c.Edit("titles", row);
                        break;
                    case 5:
                        row = new object[4];
                        row[0] = enSText.Text;
                        row[1] = salSText.Text;
                        row[2] = fdSPicker.Value;
                        row[3] = tdSPicker.Value;
                        c.Edit("salaries", row);
                        break;
                    case 6:
                        row = new Object[4];
                        row[0] = enBText.Text;
                        row[1] = bodBPicker.Value;
                        row[2] = baBText.Text;
                        row[3] = c.getID("bonustype", btnBCombo.SelectedItem.ToString());
                        //row[3] = btnBCombo.SelectedIndex + 1001;
                        c.Edit("bonus", row);
                        break;
                    case 7:
                        row = new Object[4];
                        row[0] = enDSText.Text;
                        row[1] = ddDSPicker.Value;
                        row[2] = daDSText.Text;
                        row[3] = c.getID("deducttype", dtnDSCombo.SelectedItem.ToString());
                        //row[3] = dtnDSCombo.SelectedIndex + 3001;
                        c.Edit("deduction", row);
                        break;
                    case 8:
                        row = new Object[3];
                        row[0] = enHText.Text;
                        row[1] = sdHPicker.Value;
                        row[2] = edHPicker.Value;
                        c.Edit("holiday", row);
                        break;
                    case 9:
                        row = new Object[4];
                        row[0] = enSLText.Text;
                        row[1] = sdSLPicker.Value;
                        row[2] = edSLPicker.Value;
                        row[3] = reSLText.Text;
                        c.Edit("sickleave", row);
                        break;

                }
                //Object[] row = new object[6];
                string tableName = tabControl1.SelectedTab.Name;
                this.setData(tableName);
                this.hideFields_Click(this, new EventArgs());

               /* c.Edit("employees", row);
                ds = new DataSet();
                ds = c.getData("employees");
                employeesTable.DataSource = ds.Tables["employees"].DefaultView;
                employeesTable.Refresh();
                ds.Dispose();
                fnEText.Text = "";
                lnEText.Text = "";
                genECombo.SelectedIndex = -1;
                bdEPicker.Value = DateTime.Now;
                hdEPicker.Value = DateTime.Now;*/
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void showFields_Click(object sender, EventArgs e)
        {
            //employeesPanel.Visible = true;
            int index = tabControl1.SelectedIndex;
            panels[index].Visible = true;
            tables[index].Enabled = true;
            addButtons[index].Text = "Add";
            switch (index)
            {
                case 0:
                    fnEText.Text = "";
                    lnEText.Text = "";
                    genECombo.SelectedIndex = -1;
                    bdEPicker.Value = DateTime.Now;
                    hdEPicker.Value = DateTime.Now;
                    break;
                case 1:
                    String lastdno = deptnums[deptnums.Count-1];
                    lastdno = lastdno.Remove(0, 1);
                    int newdno = int.Parse(lastdno) + 1;
                    //int newdno = 1000;
                    lastdno = newdno.ToString("000");
                    dnoDText.Text = "d"+lastdno;
                    dnDText.Text = "";
                    break;
                case 2:
                    //dnoDMText.Text = "";
                    deptnums = c.getDeptId();
                    deptnames = c.getNames("departments");
                    dnoDMCombo.Items.Clear();
                    foreach(String dno in deptnames)
                    {
                        dnoDMCombo.Items.Add(dno);
                    }
                    dnoDMCombo.Enabled = true;
                    //dnoDMText.Enabled = true;
                   // dnoDMCombo.Enabled = true;
                    enDMText.Text = "";
                    enDMText.Enabled = true;
                    fdDMPicker.Value = DateTime.Now;
                    tdDMPicker.Value = DateTime.Now;
                    break;
                case 3:
                   // dnoDEText.Text = "";
                    //dnoDEText.Enabled = true;
                    deptnums = c.getDeptId();
                    deptnames = c.getNames("departments");
                    dnoDECombo.Items.Clear();
                    foreach (String dno in deptnames)
                    {
                        dnoDECombo.Items.Add(dno);
                    }
                    dnoDECombo.Enabled = true;
                    //dnoDMText.Enabled = true;
                    //dnoDMCombo.Enabled = true;
                    enDEText.Text = "";
                    enDEText.Enabled = true;
                    fdDEPicker.Value = DateTime.Now;
                    tdDEPicker.Value = DateTime.Now;
                    break;
                case 4:
                    enTText.Text = "";
                    enTText.Enabled = true;
                    tiTText.Text = "";
                    tiTText.Enabled = true;
                    fdTPicker.Value = DateTime.Now;
                    fdTPicker.Enabled = true;
                    tdTPicker.Value = DateTime.Now;
                    break;
                case 5:
                    enSText.Text = "";
                    enSText.Enabled = true;
                    salSText.Text = "";
                    fdSPicker.Value = DateTime.Now;
                    fdSPicker.Enabled = true;
                    tdSPicker.Value = DateTime.Now;
                    break;
                case 6:
                    enBText.Text = "";
                    enBText.Enabled = true;
                    bodBPicker.Value = DateTime.Now;
                    bodBPicker.Enabled = true;
                    baBText.Text = "";
                    baBText.Enabled = true;
                    btnBCombo.SelectedIndex = -1;
                    btnBCombo.Enabled = true;
                    break;
                case 7:
                    enDSText.Text = "";
                    ddDSPicker.Value = DateTime.Now;
                    daDSText.Text = "";
                    dtnDSCombo.SelectedIndex = -1;
                    enDSText.Enabled = true;
                    ddDSPicker.Enabled = true;
                    daDSText.Enabled = true;
                    dtnDSCombo.Enabled = true;
                    break;
                case 8:
                    enHText.Text = "";
                    sdHPicker.Value = DateTime.Now;
                    edHPicker.Value = DateTime.Now;
                    enHText.Enabled = true;
                    sdHPicker.Enabled = true;
                    edHPicker.Enabled = true;
                    break;
                case 9:
                    enSLText.Text = "";
                    sdSLPicker.Value = DateTime.Now;
                    edSLPicker.Value = DateTime.Now;
                    reSLText.Text = "";
                    enSLText.Enabled = true;
                    sdSLPicker.Enabled = true;
                    edSLPicker.Enabled = true;
                    reSLText.Enabled = true;
                    break;


            }
        }

        private void hideFields_Click(object sender, EventArgs e)
        {

            int index = tabControl1.SelectedIndex;
            if(index < 9)
            {
                panels[index].Visible = false;

            }
            tables[index].Enabled = true;
            switch (index)
            {
                case 0:
                    fnEText.Text = "";
                    lnEText.Text = "";
                    genECombo.SelectedIndex = -1;
                    bdEPicker.Value = DateTime.Now;
                    hdEPicker.Value = DateTime.Now;
                    break;
                case 1:
                    dnoDText.Text = "";
                    dnDText.Text = "";
                    break;
                case 2:
                    //dnoDMText.Text = "";
                   // dnoDMText.Enabled = true;
                    
                    deptnums = c.getDeptId();
                    deptnames = c.getNames("departments");
                    dnoDMCombo.Items.Clear();
                    foreach (String dno in deptnames)
                    {
                        dnoDMCombo.Items.Add(dno);
                    }
                    dnoDMCombo.SelectedIndex = -1;

                    dnoDMCombo.Enabled = true;
                    enDMText.Text = "";
                    enDMText.Enabled = true;
                    fdDMPicker.Value = DateTime.Now;
                    tdDMPicker.Value = DateTime.Now;
                    break;
                case 3:
                    //dnoDEText.Text = "";
                    //dnoDEText.Enabled = true;
                    deptnums = c.getDeptId();
                    deptnames = c.getNames("departments");
                    dnoDECombo.Items.Clear();
                    foreach (String dno in deptnames)
                    {
                        dnoDECombo.Items.Add(dno);
                    }
                    dnoDECombo.SelectedIndex = -1;
                    dnoDECombo.Enabled = true;
                    enDEText.Text = "";
                    enDEText.Enabled = true;
                    fdDEPicker.Value = DateTime.Now;
                    tdDEPicker.Value = DateTime.Now;
                    break;
                case 4:
                    enTText.Text = "";
                    enTText.Enabled = true;
                    tiTText.Text = "";
                    tiTText.Enabled = true;
                    fdTPicker.Value = DateTime.Now;
                    fdTPicker.Enabled = true;
                    tdTPicker.Value = DateTime.Now;
                    break;
                case 5:
                    enSText.Text = "";
                    enSText.Enabled = true;
                    salSText.Text = "";
                    fdSPicker.Value = DateTime.Now;
                    fdSPicker.Enabled = true;
                    tdSPicker.Value = DateTime.Now;
                    break;
                case 6:
                    enBText.Text = "";
                    enBText.Enabled = true;
                    bodBPicker.Value = DateTime.Now;
                    bodBPicker.Enabled = true;
                    baBText.Text = "";
                    baBText.Enabled = true;
                    btnBCombo.SelectedIndex = -1;
                    btnBCombo.Enabled = true;
                    break;
                case 7:
                    enDSText.Text = "";
                    ddDSPicker.Value = DateTime.Now;
                    daDSText.Text = "";
                    dtnDSCombo.SelectedIndex = -1;
                    enDSText.Enabled = true;
                    ddDSPicker.Enabled = true;
                    daDSText.Enabled = true;
                    dtnDSCombo.Enabled = true;
                    break;
                case 8:
                    enHText.Text = "";
                    sdHPicker.Value = DateTime.Now;
                    edHPicker.Value = DateTime.Now;
                    enHText.Enabled = true;
                    sdHPicker.Enabled = true;
                    edHPicker.Enabled = true;
                    break;
                case 9:
                    enSLText.Text = "";
                    sdSLPicker.Value = DateTime.Now;
                    edSLPicker.Value = DateTime.Now;
                    reSLText.Text = "";
                    enSLText.Enabled = true;
                    sdSLPicker.Enabled = true;
                    edSLPicker.Enabled = true;
                    reSLText.Enabled = true;
                    break;

            }

            

            



        }

        private void AddRecord_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            int index = tabControl1.SelectedIndex;
            Boolean validateEmpty = true;
            Boolean validateDate = true;
            switch (index)
            {
                case 0:
                    validateEmpty = string.IsNullOrWhiteSpace(fnEText.Text)
                                || string.IsNullOrWhiteSpace(lnEText.Text)
                                || genECombo.SelectedIndex == -1;
                    validateDate = DateTime.Compare(hdEPicker.Value, bdEPicker.Value) <= 0;
                    break;
                case 1:
                    validateEmpty = string.IsNullOrWhiteSpace(dnDText.Text);
                    validateDate = false;
                    break;
                case 2:
                    validateEmpty = dnoDMCombo.SelectedIndex == -1
                                || string.IsNullOrWhiteSpace(enDMText.Text);
                    validateDate = DateTime.Compare(tdDMPicker.Value, fdDMPicker.Value) <= 0;
                    break;
                case 3:
                    validateEmpty = dnoDECombo.SelectedIndex == -1
                                || string.IsNullOrWhiteSpace(enDEText.Text);
                    validateDate = DateTime.Compare(tdDEPicker.Value, fdDEPicker.Value) <= 0;

                    break;
                case 4:
                    validateEmpty = string.IsNullOrWhiteSpace(tiTText.Text)
                                || string.IsNullOrWhiteSpace(enDEText.Text);
                    validateDate = DateTime.Compare(tdTPicker.Value, fdTPicker.Value) <= 0;

                    break;
                case 5:
                    validateEmpty = string.IsNullOrWhiteSpace(salSText.Text)
                                || string.IsNullOrWhiteSpace(enSText.Text);
                    validateDate = DateTime.Compare(tdSPicker.Value, fdSPicker.Value) <= 0;

                    break;
                case 6:
                    validateEmpty = string.IsNullOrWhiteSpace(enBText.Text)
                                || string.IsNullOrWhiteSpace(baBText.Text)
                                || btnBCombo.SelectedIndex == -1;
                    validateDate = false;
                    break;
                case 7:
                    validateEmpty = string.IsNullOrWhiteSpace(enDSText.Text)
                                || string.IsNullOrWhiteSpace(daDSText.Text)
                                || dtnDSCombo.SelectedIndex == -1;
                    validateDate = false;
                    break;
                case 8:
                    validateEmpty = string.IsNullOrWhiteSpace(enHText.Text);
                    validateDate = DateTime.Compare(edHPicker.Value, sdHPicker.Value) <= 0;
                    break;
                case 9:
                    validateEmpty = string.IsNullOrWhiteSpace(enSLText.Text)
                                || string.IsNullOrWhiteSpace(reSLText.Text);
                    validateDate = DateTime.Compare(edSLPicker.Value, sdSLPicker.Value) <= 0;
                    break;
                
            }
            if (validateEmpty)
            {
                MessageBox.Show("Fill all the fields.");
            }
            else if (validateDate)
            {
                MessageBox.Show("Inserted dates aren't congruent");
            }
            else
            {
                if (addButtons[index].Text.Equals("Add"))
                {
                    this.InsertData();
                    addButtons[index].Text = "Add";
                    panels[index].Visible = false;
                }
                else
                {
                    this.EditData();
                    addButtons[index].Text = "Add";
                    panels[index].Visible = false;

                }
            }
            validateEmpty = true;
            validateDate = true;

            Cursor.Current = Cursors.Arrow;
            //
        }

        private void DeleteRecord_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record? It might delete some records in other tables", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                //do something
                int index = tabControl1.SelectedIndex;
                Object[] row;
                if (tables[index].SelectedCells.Count > 0)
                {
                    int selectedrowindex = tables[index].SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = tables[index].Rows[selectedrowindex];
                    switch (index)
                    {
                        case 0:
                            row = new object[1];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            c.Delete("employees", row);
                            break;
                        case 1:
                            row = new object[1];
                            row[0] = Convert.ToString(selectedRow.Cells[0].Value);
                            c.Delete("departments", row);
                            break;
                        case 2:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToString(selectedRow.Cells[3].Value);
                            c.Delete("dept_manager", row);
                            break;
                        case 3:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToString(selectedRow.Cells[3].Value);
                            c.Delete("dept_emp", row);
                            break;
                        case 4:
                            row = new object[3];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToString(selectedRow.Cells[3].Value);
                            row[2] = Convert.ToDateTime(selectedRow.Cells[4].Value);
                            c.Delete("titles", row);
                            break;
                        case 5:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToDateTime(selectedRow.Cells[4].Value);
                            break;
                        case 6:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToDateTime(selectedRow.Cells[1].Value);
                            break;
                        case 7:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToDateTime(selectedRow.Cells[1].Value);
                            break;
                        case 8:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToDateTime(selectedRow.Cells[1].Value);
                            break;
                        case 9:
                            row = new object[2];
                            row[0] = Convert.ToInt32(selectedRow.Cells[0].Value);
                            row[1] = Convert.ToDateTime(selectedRow.Cells[1].Value);
                            break;
                    }




                    string tableName = tabControl1.SelectedTab.Name;
                    this.setData(tableName);
                    tables[index].Refresh();
                    //c.Delete("employees", empno);
                    //ds = new DataSet();
                    //ds = c.getData("employees");
                    //employeesTable.DataSource = ds.Tables["employees"].DefaultView;
                    //employeesTable.Refresh();
                    //ds.Dispose();

                }
                Cursor.Current = Cursors.Arrow;
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            
        }

        private void EditRecord_Click(object sender, EventArgs e)
        {
            Object[] row = new object[6];
            int index = tabControl1.SelectedIndex;
            int typeIndex = 0;
            DateTime temptd;
            if (tables[index].SelectedCells.Count > 0)
            {
                
                int selectedrowindex = tables[index].SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tables[index].Rows[selectedrowindex];
                switch (index)
                {
                    case 0:
                        fnEText.Text = Convert.ToString(selectedRow.Cells[1].Value);
                        lnEText.Text = Convert.ToString(selectedRow.Cells[2].Value);
                        string selectedgender = Convert.ToString(selectedRow.Cells[3].Value);
                        if (selectedgender.Equals("M"))
                        {
                            genECombo.SelectedIndex = 0;
                        }
                        else
                        {
                            genECombo.SelectedIndex = 1;
                        }
                        bdEPicker.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                        hdEPicker.Value = Convert.ToDateTime(selectedRow.Cells[5].Value);
                        break;
                    case 1:
                        dnoDText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        dnoDText.Enabled = false;
                        dnDText.Text = Convert.ToString(selectedRow.Cells[1].Value);
                        break;
                    case 2:
                        enDMText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enDMText.Enabled = false;
                        //dnoDMText.Text = Convert.ToString(selectedRow.Cells[3].Value);
                        //dnoDMText.Enabled = false;
                        deptnums = c.getDeptId();
                        deptnames = c.getNames("departments");
                        dnoDMCombo.Items.Clear();
                        foreach (String dno in deptnames)
                        {
                            dnoDMCombo.Items.Add(dno);
                        }
                        
                        typeIndex = deptnums.IndexOf(Convert.ToString(selectedRow.Cells[3].Value));
                        dnoDMCombo.SelectedIndex = dnoDMCombo.Items.IndexOf(deptnames[typeIndex]);
                        dnoDMCombo.Enabled = false;
                        fdDMPicker.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                        temptd = Convert.ToDateTime(selectedRow.Cells[5].Value);
                        
                        if (temptd.Year >= 9999)
                        {
                            temptd = new DateTime(9998, temptd.Month, temptd.Day);
                            
                        }
                        tdDMPicker.Value = temptd;
                        break;
                    case 3:
                        enDEText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enDEText.Enabled = false;
                        //dnoDEText.Text = Convert.ToString(selectedRow.Cells[3].Value);
                        //dnoDEText.Enabled = false;
                        deptnums = c.getDeptId();
                        deptnames = c.getNames("departments");
                        dnoDECombo.Items.Clear();
                        foreach (String dno in deptnames)
                        {
                            dnoDECombo.Items.Add(dno);
                        }
                        typeIndex = deptnums.IndexOf(Convert.ToString(selectedRow.Cells[3].Value));
                        dnoDECombo.SelectedIndex = dnoDECombo.Items.IndexOf(deptnames[typeIndex]);
                        dnoDECombo.Enabled = false;
                        fdDEPicker.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                        temptd = Convert.ToDateTime(selectedRow.Cells[5].Value);

                        if (temptd.Year >= 9999)
                        {
                            temptd = new DateTime(9998, temptd.Month, temptd.Day);

                        }
                        tdDEPicker.Value = temptd;
                        break;
                    case 4:
                        enTText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enTText.Enabled = false;
                        tiTText.Text = Convert.ToString(selectedRow.Cells[3].Value);
                        tiTText.Enabled = false;
                        fdTPicker.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                        fdTPicker.Enabled = false;
                        temptd = Convert.ToDateTime(selectedRow.Cells[5].Value);

                        if (temptd.Year >= 9999)
                        {
                            temptd = new DateTime(9998, temptd.Month, temptd.Day);

                        }
                        tdTPicker.Value = temptd;
                        break;
                    case 5:
                        enSText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enSText.Enabled = false;
                        salSText.Text = Convert.ToString(selectedRow.Cells[3].Value);
                        fdSPicker.Value = Convert.ToDateTime(selectedRow.Cells[4].Value);
                        fdSPicker.Enabled = false;
                        temptd = Convert.ToDateTime(selectedRow.Cells[5].Value);

                        if (temptd.Year >= 9999)
                        {
                            temptd = new DateTime(9998, temptd.Month, temptd.Day);

                        }
                        tdSPicker.Value = temptd;
                        break;
                    case 6:
                        enBText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enBText.Enabled = false;
                        bodBPicker.Value = Convert.ToDateTime(selectedRow.Cells[1].Value);
                        bodBPicker.Enabled = false;
                        baBText.Text = Convert.ToString(selectedRow.Cells[2].Value);
                        bonustypeno = c.getAllids("bonustype");
                        typeIndex = bonustypeno.IndexOf(Convert.ToString(selectedRow.Cells[3].Value));
                        btnBCombo.SelectedIndex = btnBCombo.Items.IndexOf(bonustype[typeIndex]);
                        //btnBCombo.SelectedIndex = Convert.ToInt32(selectedRow.Cells[3].Value) - 1001;
                        break;
                    case 7:
                        enDSText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enDSText.Enabled = false;
                        ddDSPicker.Value = Convert.ToDateTime(selectedRow.Cells[1].Value);
                        ddDSPicker.Enabled = false;
                        daDSText.Text = Convert.ToString(selectedRow.Cells[2].Value);
                        deducttypeno = c.getAllids("deducttype");
                        typeIndex = deducttypeno.IndexOf(Convert.ToString(selectedRow.Cells[3].Value));
                        dtnDSCombo.SelectedIndex = dtnDSCombo.Items.IndexOf(deducttype[typeIndex]);
                        //dtnDSCombo.SelectedIndex = Convert.ToInt32(selectedRow.Cells[3].Value) - 3001;
                        break;
                    case 8:
                        enHText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enHText.Enabled = false;
                        sdHPicker.Value = Convert.ToDateTime(selectedRow.Cells[1].Value);
                        sdHPicker.Enabled = false;
                        edHPicker.Value = Convert.ToDateTime(selectedRow.Cells[2].Value);
                        break;
                    case 9:
                        enSLText.Text = Convert.ToString(selectedRow.Cells[0].Value);
                        enSLText.Enabled = false;
                        sdSLPicker.Value = Convert.ToDateTime(selectedRow.Cells[1].Value);
                        sdSLPicker.Enabled = false;
                        edSLPicker.Value = Convert.ToDateTime(selectedRow.Cells[2].Value);
                        reSLText.Text = Convert.ToString(selectedRow.Cells[3].Value);
                        break;

                }
                
                string a = Convert.ToString(selectedRow.Cells[0].Value);
                //int index = tabControl1.SelectedIndex;
                addButtons[index].Text = "Edit";
                panels[index].Visible = true;
                tables[index].Enabled = false;

            }
            
            
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string tableName = tabControl1.SelectedTab.Name;
            int index = tabControl1.SelectedIndex;
            this.setData(tableName);
            this.hideFields_Click(this, new EventArgs());
            if(tableName == "departments")
            {
                cancelSearch.Enabled = false;
                search.Enabled = false;
            }
            else
            {
                cancelSearch.Enabled = true;
                search.Enabled = true;
            }
            if(index > 9)
            {
                showFields.Enabled = false;
                editRecord.Enabled = false;
                deleteRecord.Enabled = false;
            }
            else
            {
                showFields.Enabled = true;
                editRecord.Enabled = true;
                deleteRecord.Enabled = true;

            }
            Cursor.Current = Cursors.Arrow;
        }

        private void search_GotFocus(object sender, EventArgs e)
        {
            if(searchBar.Text == "Search by employee number")
            {
                searchBar.Text = "";
                
            }
            searchBar.ForeColor = Color.Black;
        }

        public void search_LostFocus(object sender, EventArgs e)
        {
            if(searchBar.Text == "")
            {
                searchBar.Text = "Search by employee number";
            }
            searchBar.ForeColor = Color.DarkGray;
        }

        private void SearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                
                //search.Enabled = true;
            }
            
        }

        private void Search_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string table = tabControl1.SelectedTab.Name;
            int searched;
            search.Enabled = false;
            cancelSearch.Enabled = true;
            try
            {
                searched = int.Parse(searchBar.Text);

            }
            catch (FormatException)
            {
                searched = 0;
            }
             
            switch (table)
            {
                case "employees":
                    ds = new DataSet();
                    ds = c.Search(table,searched);

                    employeesTable.DataSource = ds.Tables[table].DefaultView;
                    employeesTable.Columns[0].HeaderText = "Employee Number";
                    employeesTable.Columns[1].HeaderText = "First Name";
                    employeesTable.Columns[2].HeaderText = "Last Name";
                    employeesTable.Columns[3].HeaderText = "Gender";
                    employeesTable.Columns[4].HeaderText = "Birth Date";
                    employeesTable.Columns[5].HeaderText = "Hire Date";
                    if(searched == 0)
                    {
                        showingEmployees.Text = "No records found";
                    }
                    else
                    {
                        showingEmployees.Text = "Showing info of employee " + searched;
                    }
                    
                    break;
                /*case "departments":
                    ds = new DataSet();
                    ds = c.getData(table);
                    departmentsTable.DataSource = ds.Tables[table].DefaultView;
                    departmentsTable.Columns[0].HeaderText = "Department Number";
                    departmentsTable.Columns[1].HeaderText = "Department Name";
                    break;*/
                case "dept_manager":
                    ds = new DataSet();
                    ds = c.Search(table, searched);
                    deptmanagerTable.DataSource = ds.Tables[table].DefaultView;
                    deptmanagerTable.Columns[0].HeaderText = "Employee Number";
                    deptmanagerTable.Columns[1].HeaderText = "First Name";
                    deptmanagerTable.Columns[2].HeaderText = "Last Name";
                    deptmanagerTable.Columns[1].HeaderText = "Department Number";
                    deptmanagerTable.Columns[2].HeaderText = "From Date";
                    deptmanagerTable.Columns[3].HeaderText = "To Date";
                    if (searched == 0)
                    {
                        showingManagers.Text = "No records found";
                    }
                    else
                    {
                        showingManagers.Text = "Showing info of employee " + searched;
                    }
                    break;
                case "dept_emp":
                    ds = new DataSet();
                    ds = c.Search(table,searched);
                    deptempTable.DataSource = ds.Tables[table].DefaultView;
                    deptempTable.Columns[0].HeaderText = "Employee Number";
                    deptempTable.Columns[1].HeaderText = "First Name";
                    deptempTable.Columns[2].HeaderText = "Last Name";
                    deptempTable.Columns[3].HeaderText = "Department Number";
                    deptempTable.Columns[4].HeaderText = "From Date";
                    deptempTable.Columns[5].HeaderText = "To Date";
                    if (searched == 0)
                    {
                        showingDeptemp.Text = "No records found";
                    }
                    else
                    {
                        showingDeptemp.Text = "Showing current department of employee " + searched;
                    }
                    break;
                case "titles":
                    ds = new DataSet();
                    ds = c.Search(table,searched);
                    titlesTable.DataSource = ds.Tables[table].DefaultView;
                    titlesTable.Columns[0].HeaderText = "Employee Number";
                    titlesTable.Columns[1].HeaderText = "First Name";
                    titlesTable.Columns[2].HeaderText = "Last Name";
                    titlesTable.Columns[3].HeaderText = "Title";
                    titlesTable.Columns[4].HeaderText = "From Date";
                    titlesTable.Columns[5].HeaderText = "To Date";
                    if (searched == 0)
                    {
                        showingTitles.Text = "No records found";
                    }
                    else
                    {
                        showingTitles.Text = "Showing current title of employee " + searched;
                    }
                    break;
                case "salaries":
                    ds = new DataSet();
                    ds = c.Search(table,searched);
                    salariesTable.DataSource = ds.Tables[table].DefaultView;
                    salariesTable.Columns[0].HeaderText = "Employee Number";
                    salariesTable.Columns[1].HeaderText = "First Name";
                    salariesTable.Columns[2].HeaderText = "Last Name";
                    salariesTable.Columns[3].HeaderText = "Salary";
                    salariesTable.Columns[4].HeaderText = "From Date";
                    salariesTable.Columns[5].HeaderText = "To Date";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "bonus":
                    ds = new DataSet();
                    ds = c.getData(table);
                    bonusTable.DataSource = ds.Tables[table].DefaultView;
                    bonusTable.Columns[0].HeaderText = "Employee Number";
                    bonusTable.Columns[1].HeaderText = "Bonus Date";
                    bonusTable.Columns[2].HeaderText = "Bonus Amount";
                    bonusTable.Columns[3].HeaderText = "Bonus Type";
                    showingBonus.Text = "Showing last 100 bonuses";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "deduction":
                    ds = new DataSet();
                    ds = c.getData(table);
                    deductionsTable.DataSource = ds.Tables[table].DefaultView;
                    deductionsTable.Columns[0].HeaderText = "Employee Number";
                    deductionsTable.Columns[1].HeaderText = "Deduct Date";
                    deductionsTable.Columns[2].HeaderText = "Deduct Amount";
                    deductionsTable.Columns[3].HeaderText = "Deduct Type";
                    showingDeductions.Text = "Showing last 100 deductions";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "holiday":
                    ds = new DataSet();
                    ds = c.getData(table);
                    holidayTable.DataSource = ds.Tables[table].DefaultView;
                    holidayTable.Columns[0].HeaderText = "Employee Number";
                    holidayTable.Columns[1].HeaderText = "Start Date";
                    holidayTable.Columns[2].HeaderText = "End Date";
                    showingHolidays.Text = "Showing last 100 holidays";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "sickleave":
                    ds = new DataSet();
                    ds = c.getData(table);
                    sickleaveTable.DataSource = ds.Tables[table].DefaultView;
                    sickleaveTable.Columns[0].HeaderText = "Employee Number";
                    sickleaveTable.Columns[1].HeaderText = "Start Date";
                    sickleaveTable.Columns[2].HeaderText = "End Date";
                    sickleaveTable.Columns[3].HeaderText = "Reason";
                    showingSickleaves.Text = "Showing last 100 sick leaves";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "paydetails":
                    ds = new DataSet();
                    ds = c.getData(table);
                    paydetailsTable.DataSource = ds.Tables[table].DefaultView;
                    paydetailsTable.Columns[0].HeaderText = "Employee Number";
                    paydetailsTable.Columns[1].HeaderText = "Start Date";
                    paydetailsTable.Columns[2].HeaderText = "Routing Number";
                    paydetailsTable.Columns[3].HeaderText = "Account Type";
                    paydetailsTable.Columns[4].HeaderText = "Bank Name";
                    paydetailsTable.Columns[5].HeaderText = "Bank Address";
                    paydetailsTable.Columns[6].HeaderText = "Pay Type";
                    showingPaydetails.Text = "Showing last 100 paid details";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;
                case "payhistory":
                    ds = new DataSet();
                    ds = c.getData(table);
                    payhistoryTable.DataSource = ds.Tables[table].DefaultView;
                    payhistoryTable.Columns[0].HeaderText = "Pay Number";
                    payhistoryTable.Columns[1].HeaderText = "Employee Number";
                    payhistoryTable.Columns[2].HeaderText = "Pay Date";
                    payhistoryTable.Columns[3].HeaderText = "Check Number";
                    payhistoryTable.Columns[4].HeaderText = "Pay Amount";
                    showingPayhistory.Text = "Showing last 100 payments";
                    if (searched == 0)
                    {
                        showingSalaries.Text = "No records found";
                    }
                    else
                    {
                        showingSalaries.Text = "Showing current salary of employee " + searched;
                    }
                    break;

            }
            Cursor.Current = Cursors.Arrow;
        }

        private void SearchBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search_Click(this, new EventArgs());
            }
           /* int index = tabControl1.SelectedIndex;
            if (index != 1)
            {
                search.Enabled = true;
            }*/
        }

        private void CancelSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string tableName = tabControl1.SelectedTab.Name;
            this.setData(tableName);
            this.hideFields_Click(this, new EventArgs());
            searchBar.Text = "Search by employee number";
            //cancelSearch.Enabled = false;
            //search.Enabled = false;
            /*if (tableName == "departments")
            {
                cancelSearch.Enabled = false;
                search.Enabled = false;
            }
            else
            {
                searchBar.Text = "Search by employee number";
                searchBar.Enabled = true;
                //cancelSearch.Enabled = true;
                //search.Enabled = true;
            }*/
            Cursor.Current = Cursors.Arrow;
        }

        private void BaBText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void OpenPayment_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("If you go to Payment any unsaved changes will be lost. Continue?", "Open Payment", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                /*new Payment().Show();
                this.Hide();*/
                var Payment = new Payment();
                Payment.Shown += (o, args) => { this.Hide(); };
                Payment.Show();
            }
                

        }
    }
        
}
