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
    public partial class Reserved : Form
    {
        public event EventHandler<DataEventArgs> DataSent;
        public Reserved()
        {
            InitializeComponent();
            loadDataGridView(guna2DataGridView);
        }

        private void guna2Btn_voir_Click(object sender, EventArgs e)
        {
            if (DataSent != null) 
            {
                Pagination.get_page.Add("ResTrain");    
                DataSent(this, new DataEventArgs("ResTrain"));
            }
        }
        private void loadDataGridView(DataGridView data) 
        {
            data.AutoGenerateColumns = true;
            data.ReadOnly = true;
            ControlerTrajet trajet = new ControlerTrajet();
            trajet.conn.open();
            data.DataSource = trajet.GetData();
            data.AllowUserToResizeRows = false;
            data.AllowUserToAddRows = false;
            data.Refresh();
            trajet.conn.close();
        }

        private void guna2DataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataSent != null)
            {
                Pagination.get_page.Add("ResTrain");
                ValuePassed.idTrajet = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrajetID"].Value);
                DataSent(this, new DataEventArgs("ResTrain"));
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            int selectedId = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrajetID"].Value);
            int selectedIdTrain = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrainID"].Value);
            MessageBox.Show("Selectionner : " + selectedId);
            if (DataSent != null)
            {
                ValuePassed.idTrajet = selectedId;
                ValuePassed.idTrain = selectedIdTrain;
                ValuePassed.place.Add("RegisterTrajet");
                Pagination.get_page.Add("RegisterTrajet");
                DataSent(this, new DataEventArgs("RegisterTrajet"));
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView.CurrentRow != null && !guna2DataGridView.CurrentRow.IsNewRow)
            {
                int selectedId = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrajetID"].Value);
                ControlerTrajet train = new ControlerTrajet();
                train.conn.open();
                train.delete(selectedId);
                train.conn.close();
                guna2DataGridView.Rows.Remove(guna2DataGridView.CurrentRow);
                MessageBox.Show("Suppression reussite!");
            }
            else
            {
                MessageBox.Show("Echec du Suppression!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (DataSent != null)
            {
                ValuePassed.idTrajet = 0;
                ValuePassed.place.Add("RegisterTrajet");
                Pagination.get_page.Add("RegisterTrajet");
                DataSent(this, new DataEventArgs("RegisterTrajet"));
            }
        }
    }
}
