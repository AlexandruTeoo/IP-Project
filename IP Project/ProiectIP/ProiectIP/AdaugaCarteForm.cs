﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectIP
{
    public partial class AdaugaCarteForm : Form
    {
        public Account _account;
        public AdaugaCarteForm(Account account)
        {
            InitializeComponent();
            _account = account;
        }

        private void buttonAdaugaCartea_click(object sender, EventArgs e)
        {
            Book book = new Book();

            book._title = textBoxTitle.Text;
            book._author = textBoxAutor.Text;
            book._category = textBoxCategorie.Text;

            int cartiAdaugate = 0;
            if (int.TryParse(textBoxCartiAdaugate.Text, out cartiAdaugate))
            {
                
            }
            else
            {
                MessageBox.Show("Textul introdus nu este un număr întreg valid!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BookDAO.InsertBook(book, cartiAdaugate);
                MessageBox.Show("Carte inserata cu succes!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BibliotecarForms bibliotecarForms = new BibliotecarForms(_account);
                bibliotecarForms.Show();
                this.Hide();
            }
            catch (OracleException ex)
            {
                /*//SqlException ex = (SqlException)procedureError.InnerException;
                if (ex.Number == 20001)
                {
                    MessageBox.Show("Email deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (ex.Number == 20002)
                    {
                        MessageBox.Show("Username deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message, "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }*/
            }
        }

        private void buttonInapoi_click(object sender, EventArgs e)
        {
            BibliotecarForms bibliotecarForms = new BibliotecarForms(_account);
            bibliotecarForms.Show();
            this.Hide();
        }
    }
}
