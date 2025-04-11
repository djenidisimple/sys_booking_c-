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

namespace Booking_tiket
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
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
        }

        private void guna2BtnPas_Click(object sender, EventArgs e)
        {
            container(new Passagers());
        }

        private void guna2BtnRes_Click(object sender, EventArgs e)
        {
            container(new Reserved());
        }
    }
}
