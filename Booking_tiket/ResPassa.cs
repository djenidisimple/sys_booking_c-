using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Booking_tiket.Controler;
using Booking_tiket.Models;

namespace Booking_tiket
{
    public partial class ResPassa : Form
    {
        public ResPassa()
        {
            InitializeComponent();
            placeReserved.Text = ValuePassed.listToString();
            
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            ControlerForm form = new ControlerForm();
            form.Reservation(name.Text, mobile.Text, cin.Text);
        }
    }
}
