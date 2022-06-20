using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.UpdateBook
{

    
    public class UpdateBookCommand{

        public UpdateBookModel Model { get; set; }

        private readonly BookStoreDbContext _context;
        public int BookId{get; set;}

        public UpdateBookCommand(BookStoreDbContext context){

              _context = context;

        }

         public void Handle(){

        var book = _context.Books.SingleOrDefault(x=> x.Id == BookId);
            
            if(book is null){
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı");

            }else{
                book.GenreId = Model.GenreId != default ? Model.GenreId :book.GenreId;
                book.Tittle = Model.Tittle != default ? Model.Tittle :book.Tittle;
                _context.SaveChanges();
               
            }
         }

            public class UpdateBookModel{
 
            public int GenreId { get; set; }

            public string Tittle { get; set; }



        }



         }

    }




