using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Booking_tiket.Models;
using Booking_tiket.Controler;

namespace Booking_tiket
{
    public partial class Home : Form
    {
        private string _storeData;
        public Home()
        {
            InitializeComponent();

            trainToolStripMenuItem.Click += trainToolStripMenuItem_Click;
            trajetToolStripMenuItem.Click += trajetToolStripMenuItem_Click;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            container(new Dashboard());
        }

        private void guna2BtnDashboard_Click(object sender, EventArgs e)
        {
           container(new Dashboard());
        }
        public void container(object form) 
        {
            if (guna2Panel_container.Controls.Count > 0) guna2Panel_container.Controls.Clear();
            Form fm = form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            guna2Panel_container.Controls.Add(fm);
            fm.Show();

            //guna2BtnDashboard.CustomBorderThickness.Left = 5;
            ControlerTrajet newTrajet = new ControlerTrajet();
            newTrajet.conn.open();
            newTrajet.insertNewTrajet();
            newTrajet.conn.close();

            ControlePlace place = new ControlePlace();
            place.conn.open();
            place.freePlace();
            place.conn.close();

        }

        private void guna2BtnPas_Click(object sender, EventArgs e)
        {
            _storeData = "";
            container(new Passagers());
        }

        private void guna2BtnRes_Click(object sender, EventArgs e)
        {
            _storeData = "";
            Reserved rs = new Reserved();
            container(rs);
            rs.DataSent += GetContentEvent;
        }
        private void GetContentEvent(object sender, DataEventArgs e) 
        {
            _storeData = e.Data;
            if (_storeData == "ResTrain") 
            {
                ResTrain train = new ResTrain();
                container(train);
                train.DataSent += GetContentEvent;
            }
            else if (_storeData == "ResPlace") 
            {
                ResPlace rsP = new ResPlace();
                container(rsP);
                rsP.DataSent += GetContentEvent;
            } else if (_storeData == "ResPassa")
            {
                container(new ResPassa());
            }
            else if (_storeData == "RegisterTrain") 
            {
                container(new RegisterTrain());
            }
            else if (_storeData == "RegisterTrajet")
            {
                container(new RegisterTrajet());
            }
        }

        private void guna2BtnBack_Click(object sender, EventArgs e)
        {
            if (Pagination.get_page.Count == 3) 
            {
                ValuePassed.place.Clear();
                Pagination.get_page.Remove("ResPassa");
                ResPlace rsP = new ResPlace();
                container(rsP);
                rsP.DataSent += GetContentEvent;
            } 
            else if (Pagination.get_page.Count == 2)
            {
                Pagination.get_page.Remove("ResPlace");
                Pagination.get_page.Remove("RegisterTrain");
                ResTrain rsT = new ResTrain();
                container(rsT);
                rsT.DataSent += GetContentEvent;
            }
            else if (Pagination.get_page.Count == 1)
            {
                _storeData = "";
                Pagination.get_page.Remove("ResTrain");
                Pagination.get_page.Remove("RegisterTrajet");
                Reserved rs = new Reserved();
                container(rs);
                rs.DataSent += GetContentEvent;
            }
        }

        private void trainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container(new RegisterTrain());
        }

        private void trajetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            container(new RegisterTrajet());
        }
    }
}
