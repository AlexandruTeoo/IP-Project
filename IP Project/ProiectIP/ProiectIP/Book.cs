﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIP
{
    public class Book
    {
        public string _title { get; set; }
        public string _author { get; set; }
        public string _category { get; set; }
        public int _stock { get; set; }
        public int _total { get; set; }
        public int _isbn { get; set; }
        public Book() { }
        public Book(Book b)
        {
            _title = b._title;
            _author = b._author;
            _category = b._category;
            _stock = b._stock;
            _total = b._total;
            _isbn = b._isbn;
        }

        public Book(int isbn, string title, string author, string category, int stock, int total)
        {
            _title = title;
            _author = author;
            _category = category;
            _stock = stock;
            _total = total;
            _isbn = isbn;
        }

        public override string ToString()
        {
            return "ISBN: " + _isbn +
                " | Titlu: " + _title +
                " | Autor " + _author +
                " | Categorie: " + _category +
                " | Stoc: " + _stock +
                " | Total: " + _total;
        }

    }
}
