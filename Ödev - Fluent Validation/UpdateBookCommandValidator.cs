using FluentValidation;
using System;

namespace WebApi.BookOperations.UpdateBook{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>{
        public UpdateBookCommandValidator(){
        RuleFor(command =>command.BookId).GreaterThan(0);
        RuleFor(command =>command.Model.GenreId).GreaterThan(0);
        RuleFor(command =>command.Model.Tittle).NotEmpty().MinimumLength(4);

            
        }
    }
}