using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace Helm.Core.Infrastructure.Configuration
{

    public class AppSettingsValidator
    {
        public bool IsValid = true;
        public List<FluentValidation.Results.ValidationFailure> Errors = new ();
        private string message = "Проверьте appsettings.json - там должен быть раздел Settings";
        public AppSettingsValidator(AppSettings? settings) 
        {
            if(settings == null)
            {
                IsValid = false;
                Errors.Add(new("Settings",message));
                return;
            }
            var validator = new PropertiesValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(settings);
            IsValid = result.IsValid;
            Errors = result.Errors;
        }
        public class PropertiesValidator: AbstractValidator<AppSettings>
        {
            public PropertiesValidator()
            {
                RuleFor(s => s.ConnectionString).NotNull().NotEmpty();
                RuleFor(s => s.JWTSecretCode).NotNull().NotEmpty();
                RuleFor(s => s.MediatRLicense).NotNull().NotEmpty();
                RuleFor(s => s.ADFS).NotNull().DependentRules(() =>
                {
                    RuleFor(s => s.ADFS.ADFSDomain).NotNull().NotEmpty().When(s => s.ADFS != null);
                    RuleFor(s => s.ADFS.ADFSIssuer).NotNull().NotEmpty().When(s => s.ADFS != null);
                    RuleFor(s => s.ADFS.ADFSAudience).NotNull().NotEmpty().When(s => s.ADFS != null);
                });
            }
        }
    }
}
