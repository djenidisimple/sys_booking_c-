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
    public partial class RegisterTrain : Form
    {
        public RegisterTrain()
        {
            InitializeComponent();
            if (Pagination.get_page[Pagination.get_page.Count - 1] == "RegisterTrain" && ValuePassed.idTrain > 0)
            {
                title.Text = "Modification du Train";
                ControlerTrain getValueTrain = new ControlerTrain();
                getValueTrain.conn.open();
                nameTrain.Text = getValueTrain.viewGetId(ValuePassed.idTrain)[0].nameTrain;
                capacity.Text = Convert.ToString(getValueTrain.viewGetId(ValuePassed.idTrain)[0].capacityTrain);
                getValueTrain.conn.close();
            }
        }

        private void nameTrain_Leave(object sender, EventArgs e)
        {
            if (nameTrain.Text.Length == 0) MessageBox.Show("Veuilliez remplir le champs!");
        }

        private void capacity_Leave(object sender, EventArgs e)
        {
            if (capacity.Text.Length == 0) MessageBox.Show("Veuilliez remplir le champs!");
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            
            ControlerForm form = new ControlerForm();
            if (Pagination.get_page[Pagination.get_page.Count - 1] == "RegisterTrain" && ValuePassed.idTrain > 0)
            {
                form.Train("edit", nameTrain.Text, capacity.Text);
            }
            else 
            {
                form.Train("add", nameTrain.Text, capacity.Text);
            }
        }
    }
}
