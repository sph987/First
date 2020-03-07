using FluentValidation;

namespace application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T,string> ruleBuilder)
        {
            var options = ruleBuilder
                .MinimumLength(6).WithMessage("password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("password must contain 1 uppercase password")
                .Matches("[a-z]").WithMessage("password must contain 1 lowercase password")
                .Matches("[0-9]").WithMessage("password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("password musrt contain a special character");

                return options;
        }
        
    }
}