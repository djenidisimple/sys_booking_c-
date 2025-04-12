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
    public partial class Reserved : Form
    {
        public event EventHandler<DataEventArgs> DataSent;
        public Reserved()
        {
            InitializeComponent();
        }

        private void guna2Btn_voir_Click(object sender, EventArgs e)
        {
            if (DataSent != null) 
            {
                Pagination.get_page.Add("ResTrain");    
                DataSent(this, new DataEventArgs("ResTrain"));
            }
        }
    }
}
