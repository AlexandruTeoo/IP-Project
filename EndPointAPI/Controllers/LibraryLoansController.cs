﻿using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EndPointAPI.Controllers
{
    public class LibraryLoansController : ControllerBase
    {
        [HttpGet("loans/{accountId}")]
        public IActionResult GetLoans(int accountId)
        {
            List <Loan> _loans = LoanDAO.GetLoans(accountId);
            //var loans = _loans.Where(l => l._accountId == accountId);
            if (_loans != null)
            {
                return Ok(_loans);
            }
            return NotFound();
        }

        [HttpGet("all_loans")]
        public IActionResult GetAllLoans(int accountId)
        {
            List<Loan> _loans = LoanDAO.GetAllLoans();
            var loans = _loans.Where(l => l._accountId == accountId);
            if (loans != null)
            {
                return Ok(loans);
            }
            return NotFound();
        }

        [HttpGet("loan/{loanId}")]
        public IActionResult GetLoan(int loanId)
        {
            Loan _loan = LoanDAO.GetLoan(loanId);
            
            if (_loan != null)
            {
                return Ok(_loan);
            }
            return NotFound();
        }

        [HttpPost("loans/add")]
        public IActionResult AddLoan([FromBody] Loan loan)
        {
            int status = LoanDAO.AddLoan(loan);

            switch(status)
            {
                case -1:
                    return Conflict("Book is not available for loan.");
                case -2: 
                    return NotFound("User not found."); // ca sa imprumuti cartea trebuie sa fi logat, deci user ul deja exista
                case 0:
                    return Ok(loan);
                default:
                    return NotFound("[AddLoan]Internal Err");
            }
        }
        //Asa ar trebui sa arate toate api-urile pana cand faceti unul sa mearga complet ca sa fie ca exemplu
        //Daca nu merge cv, nu trebuie facut totul sa mearga la fel de prost ca ala
        [HttpPut("loans/approve")]
        public IActionResult ApproveLoan([FromBody] string loanId)
        {
            int status = LoanDAO.ApproveLoan(loanId);

            switch (status)
            {
                case -1:
                    return Conflict("Book is not available for loan.");
                case -2:
                    return NotFound("User not found."); // ca sa imprumuti cartea trebuie sa fi logat, deci user ul deja exista
                case 0:
                    return Ok("Loan approved successfully");
                default:
                    return NotFound("[AddLoan]Internal Err");
            }
        }
    }
}
