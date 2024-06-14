using Domain.Dtos;
using FluentValidation;

namespace Validation;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(255).WithMessage("Nome não pode ter mais que 255 caracteres.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Sobrenome é obrigatório.")
            .MaximumLength(255).WithMessage("Sobrenome não pode ter mais que 255 caracteres.");

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login é obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório.")
            .EmailAddress().WithMessage("E-mail inválido.");

        RuleFor(x => x.Age)
            .InclusiveBetween(10, 100).WithMessage("Idade deve estar entre 10 e 100 anos.");
    }
}
