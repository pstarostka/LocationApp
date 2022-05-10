using System.Text.RegularExpressions;
using FluentValidation;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Domain.Common;

namespace LocationApp.API.Validators;

public class GeolocationByIpAddressesRequestValidator : Validator<GeolocationByIpAddressesRequest>
{
    public GeolocationByIpAddressesRequestValidator()
    {
        RuleFor(x => x.IpAddresses)
            .NotEmpty()
            .Must(x =>
            {
                var ipAddresses = x.Split(",");
                var regexes = new[]
                {
                    new Regex(IpPatterns.PublicIPv4),
                    new Regex(IpPatterns.IPv6)
                };
                return regexes.Any(r => ipAddresses.All(r.IsMatch));
            })
            .WithMessage("Provided IP Addresses have to be same type of either public IPv4 or IPv6");
    }
}