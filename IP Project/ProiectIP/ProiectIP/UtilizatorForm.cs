﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using AccountLibrary;
using BookLibrary;
using LoanLibrary;
using WishlistLibrary;

namespace ProiectIP
{
    public partial class UtilizatorForm : Form
    {
        public Account account;
        public bool isOnLoan = false;
        public UtilizatorForm(Account account)
        {
            this.account = account;
            InitializeComponent();
            label1.Text = label1.Text + this.account.Username + "!";
        }
        #region Buttons
        private void AddtoWishlist_Click(object sender, EventArgs e) // adauga cartea in wishlist
        {
            if (isOnLoan)
                return;

            if (listBoxUtilizatorForm.SelectedItem != null)
            {
                Book selectedBook = (Book)listBoxUtilizatorForm.SelectedItem;
                Wishlist wishlist = new Wishlist();
                wishlist.AccountId = account.Id;
                wishlist.ISBN = selectedBook.ISBN;

                List<Book> wishlistedBooks = WishlistDAO.GetWishlist(account.Id);
                bool ok = false;
                foreach(Book book in wishlistedBooks)
                {
                    if (book.ISBN == selectedBook.ISBN)
                        ok = true;
                }

                 // Verificați dacă elementul selectat este deja în wishlist
                if (ok==true)
                {
                    MessageBox.Show("Cartea este deja în Wishlist!"); // carte deja introdusa in wishlist
                }
                else
                {
                    WishlistDAO.AddBookWishlist(wishlist);
                    MessageBox.Show("Carte adăugata în Wishlist!"); // carte introdusa in wishlist cu succes
                }
            }
            else
            {
                MessageBox.Show("Selectați o carte din lista pentru a o adăuga în Wishlist!");
            }
        }

        private void buttonImprumuta_Click(object sender, EventArgs e)  // trimite o cerere de imprumut
        {
            if (isOnLoan)
                return;

            Loan loan = new Loan();
            Book selectedBook = (Book)listBoxUtilizatorForm.SelectedItem;
            loan.AccountId = account.Id;
            loan.ISBN = selectedBook.ISBN;

            try
            {
                LoanDAO.AddLoan(loan);
                MessageBox.Show("Cerere de imprumut trimisa cu succes", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OracleException ex)
            {
                //SqlException ex = (SqlException)procedureError.InnerException;
                if (ex.Number == 20003)
                {
                    MessageBox.Show("Stoc insuficient!", "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message, "Avertizare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonShowWishlist_click(object sender, EventArgs e)// afiseaza cartile pe care utilizatorul le are in wishlist
        {
            isOnLoan = false;

            List<Book> wishlist = WishlistDAO.GetWishlist(account.Id);
            if (wishlist.Count > 0)
            {
                listBoxUtilizatorForm.Items.Clear();
                foreach (Book book in wishlist)
                {
                    listBoxUtilizatorForm.Items.Add(book);
                }
            }
            else
            {
                MessageBox.Show("Wishlist-ul este gol!");
            }
        }

        private void buttonShowBooks_click(object sender, EventArgs e) // afiseaza cartile
        {
            isOnLoan=false;

            listBoxUtilizatorForm.Items.Clear();
            List<Book> books = BookDAO.GetBooks();
            foreach(Book book in books)
            {
                listBoxUtilizatorForm.Items.Add(book);
            }
        }

        private void buttonDeconectare_Click(object sender, EventArgs e) // utilizatorul se deconecteaza de la cont
        {
            LogIn logIn = new LogIn();
            logIn.Show();
            this.Hide();
        }

        private void removeWishlist_Click(object sender, EventArgs e) // sterge din wishlist
        {
            if (isOnLoan)
                return;

            if (listBoxUtilizatorForm.SelectedItem != null)
            {
                Book selectedBook = (Book)listBoxUtilizatorForm.SelectedItem;
                Wishlist wishlist = new Wishlist();
                wishlist.AccountId = account.Id;
                wishlist.ISBN = selectedBook.ISBN;
                WishlistDAO.DeleteBookWishlist(wishlist);
                //wishlist.Remove(selectedBook);
                MessageBox.Show("Carte eliminată din Wishlist!");

                // Actualizare afișare Wishlist
                RefreshWishlist();
            }
            else
            {
                MessageBox.Show("Selectați o carte din Wishlist pentru a o elimina!");
            }
        }

        private void showLoans_Click(object sender, EventArgs e) // arata imprumuturile
        {
            isOnLoan = true;

            listBoxUtilizatorForm.Items.Clear();
            List<Loan> loans = LoanDAO.GetLoans(account.Id);
            foreach (Loan loan in loans)
            {
                listBoxUtilizatorForm.Items.Add(loan);
            }
        }
        #endregion
        #region RefreshList
        private void RefreshWishlist()
        {
            isOnLoan = false;

            // Ștergeți conținutul din ListBox-ul de Wishlist
            listBoxUtilizatorForm.Items.Clear();
            List<Book> wishlist = WishlistDAO.GetWishlist(account.Id);
            // Adăugați cărțile din wishlist în ListBox-ul de Wishlist
            foreach (Book book in wishlist)
            {
                listBoxUtilizatorForm.Items.Add(book);
            }
        }
        #endregion
    }
}
