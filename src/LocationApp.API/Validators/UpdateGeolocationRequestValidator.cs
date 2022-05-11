using System.Text.RegularExpressions;
using FluentValidation;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Domain.Common;

namespace LocationApp.API.Validators;

public class UpdateGeolocationRequestValidator : Validator<UpdateGeolocationRequest>
{
    public UpdateGeolocationRequestValidator()
    {
        RuleFor(x => x.Ip)
            .NotEmpty()
            .Must(x =>
            {
                var regexes = new[]
                {
                    new Regex(IpPatterns.PublicIPv4),
                    new Regex(IpPatterns.IPv6)
                };
                return regexes.Any(r => r.IsMatch(x));
            })
            .WithMessage("Provided IP Address have to be public IPv4 or IPv6");

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must be provided");
    }
}