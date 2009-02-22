// <copyright file="ValidatorBase.cs" company="BlogSharp">
// Apache Licence 2.0 
// </copyright>
// <author>Gonzalo Brusella</author>
// <email>gonzalo@brusella.com.ar</email>
// <date>2009-02-21</date>

namespace BlogSharp.Model.Validation
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using BlogSharp.Model.Validation.Interfaces;
    using FluentValidation;

    /// <summary>
    /// Base Validator.
    /// </summary>
    /// <typeparam name="T">Type of the Object to validate.</typeparam>
    public abstract class ValidatorBase<T> : AbstractValidator<T>, IValidatorBase<T>, IValidatorBase
    {
        /// <summary>
        /// Validates and throws an exception if the validation is not valid.
        /// </summary>
        /// <param name="instance">Object to Validate.</param>
        /// <exception cref="ValidationException">Exception thown when the instance is not valid.</exception>
        public virtual void ValidateAndThrowException(T instance)
        {
            var validationResult = this.Validate(instance);

            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors
                    .Select(x => new ValidationError
                    {
                        Message = x.ErrorMessage,
                        Object = x.AttemptedValue,
                        PropertyName = x.PropertyName
                    }).ToList();
                throw new ValidationException { Errors = errorList };
            }
        }

        /// <summary>
        /// Validates and throws an exception if the validation is not valid.
        /// </summary>
        /// <param name="instance">Object to Validate.</param>
        /// <param name="expression">Expression to user as validator.</param>
        /// <exception cref="ValidationException">Exception thown when the instance is not valid.</exception>
        public virtual void ValidateAndThrowException(T instance, params Expression<Func<T, object>>[] expression)
        {
            var validationResult = this.Validate(instance, expression);

            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors
                    .Select(x => new ValidationError
                    {
                        Message = x.ErrorMessage,
                        Object = x.AttemptedValue,
                        PropertyName = x.PropertyName
                    }).ToList();
                throw new ValidationException { Errors = errorList };
            }
        }

        #region IValidatorBase Members

        /// <summary>
        /// Validates and throws an exception if the validation is not valid.
        /// </summary>
        /// <param name="instance">Object to Validate.</param>
        public void ValidateAndThrowException(object instance)
        {
            this.ValidateAndThrowException((T)instance);
        }

        #endregion
    }
}