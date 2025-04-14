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
using Guna.UI2.WinForms;

namespace Booking_tiket
{
    public partial class ResPlace : Form
    {
        private List<Guna2Panel> _selectedPanel = new List<Guna2Panel>();
        public event EventHandler<DataEventArgs> DataSent;
        private int _nbPassager = 0;
        public ResPlace()
        {
            InitializeComponent();
            loadPanelPlace();
            loadView();
        }

        private void loadPanelPlace()
        {
            background.Controls.Clear();
            ControlePlace place = new ControlePlace();

            place.conn.open();
            DataTable t = place.GetPlaceById(ValuePassed.idTrajet);
            place.conn.close();

            place.conn.open();
            int line = 0;
            int nb = place.GetNumberPlaceById(ValuePassed.idTrajet);
            place.conn.close();

            int i = 45, counter = 0, j = 0;
            foreach (DataRow row in t.Rows)
            {
                Guna2Panel itemPanel = new Guna2Panel();
                itemPanel.Width = 56;
                itemPanel.Height = 49;
                itemPanel.Margin = new Padding(4);
                itemPanel.BackColor = Color.Transparent;
                itemPanel.BorderThickness = 1;
                itemPanel.BorderRadius = 12;
                

                if ((bool)row["EstDisponible"])
                {
                    itemPanel.FillColor = Color.DarkBlue;
                }
                else
                {
                    itemPanel.FillColor = Color.MediumSeaGreen;
                    _nbPassager++;
                }


                if (line == 0)
                {
                    i = 45;
                }
                else if (line == 1)
                {
                    i = 154;
                }
                else if (line == 2)
                {
                    i = 558;
                }
                else if (line == 3)
                {
                    i = 667;
                }
                else
                {
                    line = 0;
                    j += 87;
                    i = 45;
                }


                itemPanel.Margin = new Padding(5);
                
                itemPanel.Location = new Point(i, j);
                itemPanel.Tag = row;

                itemPanel.MouseClick += Panel_Mouse_Click;

                background.Controls.Add(itemPanel);

                background.AutoScroll = true;

                
                line++;
                counter++;
            }
        }

        private void guna2BtnRes_Click(object sender, EventArgs e)
        {
            if (ValuePassed.place.Count > 0)
            {
                if (DataSent != null)
                {
                    Pagination.get_page.Add("ResPassa");
                    DataSent(this, new DataEventArgs("ResPassa"));
                }
            }
            else 
            {
                MessageBox.Show("Veuilliez sélectionner une place!");
            }
        }
        private void loadView() 
        {
            ControlerTrain train = new ControlerTrain();
            train.conn.open();
            labelNameTrain.Text = train.GetNameTrain(ValuePassed.idTrain);
            train.conn.close();
            train.conn.open();
            labelNb.Text = Convert.ToString(_nbPassager);
            passager.Text = (_nbPassager > 1) ? "Passagers" : "Passager";
            train.conn.close();
        }

        private void Panel_Mouse_Click(Object sender, EventArgs e)
        {
            int i = 0;
            Guna2Panel clickPanel = (Guna2Panel)sender;
            DataRow row = (DataRow)clickPanel.Tag;
            if (clickPanel.FillColor == Color.DarkBlue || clickPanel.FillColor == Color.MediumSeaGreen && _selectedPanel.Contains(clickPanel))
            {
                if (_selectedPanel.Contains(clickPanel))
                {
                    i = _selectedPanel.IndexOf(clickPanel);
                    ValuePassed.idPlace.Remove((int)row["PlaceID"]);
                    ValuePassed.place.Remove((string)row["NumeroPlace"]);
                    _selectedPanel[i].FillColor = Color.DarkBlue;
                    _selectedPanel.Remove(clickPanel);
                }
                else
                {

                    clickPanel.FillColor = Color.MediumSeaGreen;
                    _selectedPanel.Add(clickPanel);
                    _selectedPanel[i].FillColor = Color.MediumSeaGreen;
                    ValuePassed.idPlace.Add((int)row["PlaceID"]);
                    ValuePassed.place.Add((string)row["NumeroPlace"]);
                }
                labelPlace.Text = "Place choici : " + ValuePassed.listToString();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Form view = new ViewPdf();
            view.ShowDialog();
        }

    }
}
