using System.Text.RegularExpressions;
using FluentValidation;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Domain.Common;

namespace LocationApp.API.Validators;

public class RemoveGeolocationRequestValidator : Validator<RemoveGeolocationRequest>
{
    public RemoveGeolocationRequestValidator()
    {
        RuleFor(x => x.IpAddressOrId)
            .NotEmpty()
            .Must(x =>
            {
                var isNumber = int.TryParse(x, out var id);

                if (isNumber) return true;

                var regexes = new[]
                {
                    new Regex(IpPatterns.PublicIPv4),
                    new Regex(IpPatterns.IPv6)
                };
                return regexes.Any(r => r.IsMatch(x));
            })
            .WithMessage("Provided data has to be either an Entity Id or an IP Address which is a public IPv4 or IPv6");
    }
}