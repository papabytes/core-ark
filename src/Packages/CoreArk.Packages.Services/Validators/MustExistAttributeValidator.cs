using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CoreArk.Packages.Core;

namespace CoreArk.Packages.Services.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class MustExistAttribute : ValidationAttribute
    {
        private readonly Type _entityType;

        public MustExistAttribute(Type entityType)
        {
            _entityType = entityType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var valueAsGuid = (Guid) value;
            var dataService = validationContext.GetService(typeof(DataContext)) as DataContext;
            var entity = dataService.Set(_entityType).FirstOrDefault(e => e.Id == valueAsGuid);

            return entity == null
                ? new ValidationResult($"{_entityType.Name} with Id {value} does not exist")
                : ValidationResult.Success;
        }
    }
}