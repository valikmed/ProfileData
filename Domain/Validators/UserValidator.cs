using System;
using FluentValidation;
using Domain.DTO;
using Domain.Entities;

namespace Domain.Validators
{
    public class UserValidator : AbstractValidator<UserFullInfoDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotNull();
            RuleFor(user => user.FirstName).Matches(@"[A-Z]\w+");

            RuleFor(user => user.LastName).NotNull();
            RuleFor(user => user.LastName).Matches(@"[A-Z]\w+");

            RuleFor(user => user.RoleID).NotNull();

            RuleFor(user => user.BirthDate).NotNull();
            RuleFor(user => User.GetAge(user.BirthDate)).GreaterThan(17);

        }
    }
}

