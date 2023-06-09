﻿/**************************************************************************
 *                                                                        *
 *  File:        LoanLibrary.dll                                               *
 *  Copyright:   (c) 2023, Florin Leon                                    *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ip.html             *
 *  Description: LoanDAO ()                                          *
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;

namespace LoanLibrary
{
    public class LoanDAO
    {
        #region GetLoans
        public static List<Loan> GetLoans(int accId)
        {
            using (OracleConnection connection = new OracleConnection(Database.GetConnectionString()))
            {
                String sql;
                sql = "Select * from LOAN WHERE account_id = '" + accId + "'"; // selecteaza din loan toate conturile care au id-ul dat ca si parametru

                OracleCommand command = new OracleCommand(sql, connection);

                command.Connection.Open();
                OracleDataReader dataReader = command.ExecuteReader();

                List<Loan> loans = new List<Loan>();
                while (dataReader.Read())
                {
                    Loan loan = new Loan(
                        dataReader.GetInt32(0),
                        dataReader.GetInt32(1),
                        dataReader.GetInt32(2),
                        dataReader.GetDateTime(3).Date,
                        dataReader.GetDateTime(4).Date,
                        dataReader.GetInt32(5)
                    );

                    loans.Add(loan);
                }
                return loans;
            }
        }
        #endregion
        #region GetAllLoans
        public static List<Loan> GetAllLoans()
        {
            using (OracleConnection connection = new OracleConnection(Database.GetConnectionString()))
            {
                String sql;
                sql = "Select * from LOAN where accepted = 0"; // selecteaza toate imprumuturile 

                OracleCommand command = new OracleCommand(sql, connection);

                command.Connection.Open();
                OracleDataReader dataReader = command.ExecuteReader();

                List<Loan> loans = new List<Loan>();

                while (dataReader.Read())
                {
                    Loan loan = new Loan(
                        dataReader.GetInt32(0),
                        dataReader.GetInt32(1),
                        dataReader.GetInt32(2),
                        dataReader.GetDateTime(3).Date,
                        dataReader.GetDateTime(4).Date,
                        dataReader.GetInt32(5)
                    );

                    loans.Add(loan);
                }

                return loans;
  
            }
        }
        #endregion
        #region ApproveLoan
        public static void ApproveLoan(int loanId)
        {
            using (OracleConnection connection = new OracleConnection(Database.GetConnectionString()))
            {
                String sql;
                sql = "BEGIN \n accept_loan(" + loanId + "); \n END;"; // accepta imprumutul cu id-ul (imprumutului) dat ca si parametru
                OracleCommand command = new OracleCommand(sql, connection);

                command.Connection.Open();
                command.ExecuteReader();
            }
        }
        #endregion
        #region AddLoan
        public static void AddLoan(Loan loan)
        {
            using (OracleConnection connection = new OracleConnection(Database.GetConnectionString()))
            {
                String sql;

                sql = "BEGIN \n insert_loan(" + loan.AccountId + "," +
                        loan.ISBN +"); \n END;"; // insereaza imprumutul in baza de date

                OracleCommand command = new OracleCommand(sql, connection);

                command.Connection.Open();
                command.ExecuteReader();
            }
        }
        #endregion
    }
}
