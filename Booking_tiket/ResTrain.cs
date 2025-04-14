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
    public partial class ResTrain : Form
    {
        public event EventHandler<DataEventArgs> DataSent;
        public ResTrain()
        {
            InitializeComponent();
            loadDataGridView(guna2DataGridView);
        }

        private void guna2BtnRes_Click(object sender, EventArgs e)
        {
            if (DataSent != null) 
            {
                ValuePassed.idTrain = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrainID"].Value);
                Pagination.get_page.Add("ResPlace");
                DataSent(this, new DataEventArgs("ResPlace"));                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            }
        }
        private void loadDataGridView(DataGridView data) 
        {
            data.AutoGenerateColumns = true;
            data.ReadOnly = true;
            ControlerTrain train = new ControlerTrain();
            train.conn.open();
            data.DataSource = train.GetDataAboutTrain(ValuePassed.idTrajet);
            data.AllowUserToResizeRows = false;
            data.AllowUserToAddRows = false;
            data.Refresh();
            train.conn.close();
            train.conn.open();
            string depart = train.GetGareDepart(ValuePassed.idTrajet);
            train.conn.close();
            title.Text = "Train de " + depart;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {   
            if (DataSent != null)
            {
                ValuePassed.idTrain = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrainID"].Value);
                Pagination.get_page.Add("RegisterTrain");
                DataSent(this, new DataEventArgs("RegisterTrain"));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (DataSent != null)
            {
                ValuePassed.idTrain = 0;
                Pagination.get_page.Add("RegisterTrain");
                DataSent(this, new DataEventArgs("RegisterTrain"));
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView.CurrentRow != null && !guna2DataGridView.CurrentRow.IsNewRow)
            {
                int selectedId = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["TrainID"].Value);
                ControlerTrain train = new ControlerTrain();
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
    }
}
