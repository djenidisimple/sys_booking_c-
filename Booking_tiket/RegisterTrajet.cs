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
using Booking_tiket;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace Booking_tiket
{
    public partial class RegisterTrajet : Form
    {
        public RegisterTrajet()
        {
            InitializeComponent();
            loadListTrain();
        }

        public void loadListTrain()
        {
            if (ValuePassed.place[ValuePassed.place.Count - 1] == "RegisterTrajet" && ValuePassed.idTrajet > 0)
            {
                title.Text = "Modification sur le trajet";
                btnAdd.Text = "Modifier";
                ControlerTrajet getValueTrajet = new ControlerTrajet();
                getValueTrajet.conn.open();
                depart.Text = getValueTrajet.GetsDataById(ValuePassed.idTrajet)[0].gareDepart;
                arriver.Text = getValueTrajet.GetsDataById(ValuePassed.idTrajet)[0].gareArriver;
                dateDepart.Value = getValueTrajet.GetsDataById(ValuePassed.idTrajet)[0].dateDepart;
                dateDArriver.Value = getValueTrajet.GetsDataById(ValuePassed.idTrajet)[0].dateArriver;
                getValueTrajet.conn.close();
            }
            try
            {
                ControlerTrain listT = new ControlerTrain();
                listT.conn.open();
                idTrain.DataSource = listT.GetData();
                idTrain.DisplayMember = "NomTrain";
                idTrain.ValueMember = "TrainID";
                idTrain.SelectedIndex = 0;
                listT.conn.close();

            }
            catch (SqlException sqlerr)
            {
                MessageBox.Show("Erreur : " + sqlerr);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (depart.Text.Length > 0 && arriver.Text.Length > 0)
            {
                ControlerForm form = new ControlerForm();

                if (ValuePassed.place[ValuePassed.place.Count - 1] == "RegisterTrajet" && ValuePassed.idTrajet > 0)
                {
                    Trajet trajet = new Trajet(ValuePassed.idTrain, depart.Text, arriver.Text, dateDepart.Value, dateDArriver.Value);
                    form.Trajet(trajet, "edit");
                }
                else 
                {
                    int selectedId = (int)idTrain.SelectedValue;
                    Trajet trajet = new Trajet(selectedId, depart.Text, arriver.Text, dateDepart.Value, dateDArriver.Value);
                    form.Trajet(trajet);
                    ControlePlace place = new ControlePlace();
                    place.conn.open();
                    place.addPlace();
                    place.conn.close();
                }
                
            }
            else 
            {
                MessageBox.Show("Veuilliez remplir tous les champs existants!");
            }
        }
    }
}
