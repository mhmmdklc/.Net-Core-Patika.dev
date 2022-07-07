using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.DeleteBookCommand;
using static WebApi.BookOperations.DeleteBookCommand.DeleteBookCommand;
using static WebApi.BookOperations.CreateBooks.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using System;
using AutoMapper;
using WebApi.BookOperations.GetBookDetail;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {

         private readonly BookStoreDbContext _context;

         private readonly IMapper _mapper;

            public BookController (BookStoreDbContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }

        [HttpGet]
        public IActionResult GetBooks(){

                GetBooksQuery query = new GetBooksQuery(_context, _mapper);
                var result = query.Handle();
                return Ok(result);
            }

         [HttpGet()]
        public IActionResult GetById(int id){

            BookDetailViewModel result;
            try
            {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validation = new GetBookDetailQueryValidator();
            validation.ValidateAndThrow(query);
            result=query.Handle();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
   
            return Ok(result);
        }

        [HttpPost]

        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try{
                    
            command.Model = newBook;
            CreateBookCommandValidator validation = new CreateBookCommandValidator();
            validation.ValidateAndThrow(command);
            command.Handle();

           /* if(!result.IsValid)
            foreach(var item in result.Errors){
                Console.WriteLine("Özellik: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);

            }else{
                command.Handle();
            } */
            
            
                 
            }catch(Exception ex){
                return BadRequest(ex.Message);

            }

            return Ok();
        }

        [HttpPut()]

        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try{
                command.BookId = id;
                command.Model = updateBook;

                UpdateBookCommandValidator validation = new UpdateBookCommandValidator();
                validation.ValidateAndThrow(command);
                


                command.Handle();

            }catch(Exception ex){
                return BadRequest(ex.Message);

            }
             return Ok();


        }

        [HttpDelete()]

        public IActionResult DeleteBook(int id){

            
            try{
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                DeleteBookCommandValidator validation = new DeleteBookCommandValidator();
                validation.ValidateAndThrow(command);
                command.Handle();

            }catch(Exception ex){
                return BadRequest(ex.Message);

            }
             return Ok();

        }

        

      

    






    }






}