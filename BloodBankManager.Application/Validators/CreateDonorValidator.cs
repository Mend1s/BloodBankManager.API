using BloodBankManager.Application.InputModels;
using FluentValidation;

namespace BloodBankManager.Application.Validators;

public class CreateDonorValidator : AbstractValidator<CreateDonorInputModel>
{
    public CreateDonorValidator()
    {
        RuleFor(d => d.FullName)
            .NotEmpty()
                .WithMessage("O nome completo é obrigatório.")
            .MaximumLength(100)
                .WithMessage("O nome completo deve ter no máximo 100 caracteres.");

        RuleFor(d => d.Email)
            .NotEmpty()
                .WithMessage("O e-mail é obrigatório.")
            .MaximumLength(80)
                .WithMessage("O e-mail deve ter no máximo 80 caracteres.")
            .EmailAddress()
                .WithMessage("O e-mail informado não é válido.");

        RuleFor(d => d.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser maior que a data atual.");
    }
}
