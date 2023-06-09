﻿/**************************************************************************
 *                                                                        *
 *  File:        BookLibrary.dll                                               *
 *  Copyright:   (c) 2023, Florin Leon                                    *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ip.html             *
 *  Description: Book (class)                                          *
 *                                                                        *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book
    {
        #region Getters and Setters
        public string Title { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public int Total { get; set; }
        public int ISBN { get; set; }
        #endregion
        #region Constructors
        public Book() { }
        public Book(Book b) // constructor
        {
            Title = b.Title;
            Author = b.Author;
            Category = b.Category;
            Stock = b.Stock;
            Total = b.Total;
            ISBN = b.ISBN;
        }

        public Book(int isbn, string title, string author, string category, int stock, int total) // constructor
        {
            Title = title;
            Author = author;
            Category = category;
            Stock = stock;
            Total = total;
            ISBN = isbn;
        }
        #endregion
        #region ToString
        public override string ToString() // suprascrierea metodei ToString()
        {
            return "ISBN: " + ISBN +
                " | Titlu: " + Title +
                " | Autor " + Author +
                " | Categorie: " + Category +
                " | Stoc: " + Stock +
                " | Total: " + Total;
        }
        #endregion
    }
}
