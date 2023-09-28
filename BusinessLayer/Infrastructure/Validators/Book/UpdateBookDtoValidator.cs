using BusinessLayer.Models.Book;
using FluentValidation;

namespace BusinessLayer.Infrastructure.Validators.Book
{
	public class UpdateBookDtoValidator : AbstractValidator<UpdateBookDto>
	{
		public UpdateBookDtoValidator()
		{
			RuleFor(dto => dto.Isbn).NotEmpty().Length(13);
			RuleFor(dto => dto.Title).NotEmpty().MaximumLength(100);
			RuleFor(dto => dto.Genre).NotEmpty().MaximumLength(50);
			RuleFor(dto => dto.Description).MaximumLength(500);
			RuleFor(dto => dto.Author).NotEmpty().MaximumLength(100);
			RuleFor(dto => dto.IssuedDate).NotEmpty().LessThanOrEqualTo(dto => dto.ReturnDate);
			RuleFor(dto => dto.ReturnDate).NotEmpty().GreaterThanOrEqualTo(dto => dto.IssuedDate);
		}
	}
}
