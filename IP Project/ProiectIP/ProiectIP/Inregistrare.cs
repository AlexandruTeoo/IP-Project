﻿/**************************************************************************
 *                                                                        *
 *  File:        BookLibrary.dll                                               *
 *  Copyright:   (c) 2023, Florin Leon                                    *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ip.html             *
 *  Description: BookDAO ()                                          *
 *                                                                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using AccountLibrary;

namespace ProiectIP
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
        {
            InitializeComponent();
        }
        #region Buttons
        private void backLogIn_Click(object sender, EventArgs e) // butonul intoare utilizatorul pe pagina de autentificare
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Hide();
        }

        private void buttonInregistrare_Click(object sender, EventArgs e) // inregistreaza utilizatorul in baza de date
        { 
            Account account = new Account();

            account.Username = textBoxUsername.Text;
            account.Password = textBoxParola.Text;
            account.Nume = textBoxNume.Text;
            account.Prenume = textBoxPrenume.Text;
            account.CNP = textBoxCNP.Text;
            account.Email = textBoxEmail.Text;
            account.Address = textBoxAdresa.Text;
            account.City = textBoxOras.Text;
            account.PhoneNumber = textBoxTelefon.Text;

            if(account.CNP.Length != 13)
            {
                MessageBox.Show("CNP invalid", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // CNP introdus gresit
                return;
            }
            if (account.PhoneNumber.Length != 10)
            {
                MessageBox.Show("Numar de telefon invalid", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // numar de telefon gresit
                return;
            }

            try
            {
                AccountDAO.InsertAccount(account);
                MessageBox.Show("Utilizator inserat cu succes!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // utilizator introdus
                LogIn logare = new LogIn();
                logare.Show();
                this.Hide();
            }
            catch(OracleException ex) 
            {
                switch(ex.Number)
                {
                    case 20001:
                        MessageBox.Show("Email deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // email deja existent
                        break;
                    case 20002:
                        MessageBox.Show("Username deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // username deja existent
                        break;
                    case 20003:
                        MessageBox.Show("CNP deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // CNP deja existent
                        break;
                    case 20004:
                        MessageBox.Show("Numar de telefon deja existent!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning); // numar de telefon deja existent
                        break;
                    default:
                        MessageBox.Show(ex.Message, "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
        }
        #endregion
    }
}
