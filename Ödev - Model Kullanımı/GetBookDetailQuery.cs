using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.GetBooks{

    public class GetBookDetailQuery{

    private readonly BookStoreDbContext _dbContext;
    public int BookId{get; set;}

    public GetBookDetailQuery(BookStoreDbContext dbContext){

        _dbContext = dbContext;
       
       }

    public BookDetailViewModel Handle(){
        var book = _dbContext.Books.Where(book => book.Id == BookId).SingleOrDefault();
        if(book is null)
        throw new InvalidOperationException("Kitap bulunamadı");
        BookDetailViewModel vm = new BookDetailViewModel();
        vm.Author =book.Author;
        vm.Tittle = book.Tittle;
        vm.PageCount = book.PageCount;
        vm.PublishDate = book.PublishDate;
        vm.Genre = ((genreEnum)book.GenreId).ToString();
        

        return vm;
    }

    }

    public class BookDetailViewModel{
        public string Tittle { get; set; }
        public string Genre { get; set; }

        public int PageCount { get; set; }

        public string Author { get; set; }

        public string PublishDate { get; set; }

         
    }

  



}