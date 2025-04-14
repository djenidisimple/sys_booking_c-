using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking_tiket
{
    public partial class Passagers : Form
    {
        public Passagers()
        {
            InitializeComponent();
            loadDataGridView(guna2DataGridView);
        }
        private void loadDataGridView(DataGridView data)
        {
            data.AutoGenerateColumns = true;
            data.ReadOnly = true;
            ControlerClient client = new ControlerClient();
            client.conn.open();
            data.DataSource = client.GetData();
            data.AllowUserToResizeRows = false;
            data.AllowUserToAddRows = false;
            data.Refresh();
            client.conn.close();
            client.conn.open();
            nbPass.Text = "" + client.GetNumberPassager();
            client.conn.close();
        }
    }
}
