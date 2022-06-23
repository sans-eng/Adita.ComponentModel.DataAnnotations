using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Adita.ComponentModel.DataAnnotations.Resources;

namespace Adita.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies the minimum and maximum length of characters that are allowed in a data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringLengthAttribute : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="StringLengthAttribute"/> using specified <paramref name="minPropertyName"/>
        /// as name of <see cref="int"/> property that contain minimum value <br/> and <paramref name="maxPropertyName"/>
        /// as name of <see cref="int"/> property that contain maximum value.
        /// </summary>
        /// <param name="minPropertyName">An <see cref="int"/> property name for specify the minimum length.</param>
        /// <param name="maxPropertyName">An <see cref="int"/> property name for specify the maximum length.</param>
        /// <exception cref="ArgumentNullException"><paramref name="minPropertyName"/>
        /// and or <paramref name="maxPropertyName"/> is <c>null</c>, <see cref="string.Empty"/> or only contains white space.
        /// </exception>
        public StringLengthAttribute(string minPropertyName, string maxPropertyName)
        {
            if (string.IsNullOrEmpty(minPropertyName))
            {
                throw new ArgumentException($"'{nameof(minPropertyName)}' cannot be null or empty.", nameof(minPropertyName));
            }

            if (string.IsNullOrEmpty(maxPropertyName))
            {
                throw new ArgumentException($"'{nameof(maxPropertyName)}' cannot be null or empty.", nameof(maxPropertyName));
            }

            _minLengthPropertyName = minPropertyName;
            _maxLengthPropertyName = maxPropertyName;
        }
        /// <summary>
        /// Initialize new instance of <see cref="StringLengthAttribute"/> using specified <paramref name="minLength"/>
        /// and specified <paramref name="minLength"/>.
        /// </summary>
        /// <param name="minLength">An allowed minimum length.</param>
        /// <param name="maxLength">an allowed maximum length.</param>
        public StringLengthAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }
        /// <summary>
        /// Initialize new instance of <see cref="StringLengthAttribute"/> using specified <paramref name="minPropertyName"/>
        /// as name of <see cref="int"/> property that contain minimum value, <br/>
        /// <paramref name="maxPropertyName"/> as name of <see cref="int"/> property that contain maximum value and
        /// <paramref name="errorMessage"/> if validation failed.
        /// </summary>
        /// <param name="minPropertyName">An <see cref="int"/> property name for specify the minimum length.</param>
        /// <param name="maxPropertyName">An <see cref="int"/> property name for specify the maximum length.</param>
        /// <param name="errorMessage">An error message if validation failed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="minPropertyName"/>
        /// and or <paramref name="maxPropertyName"/> is <c>null</c>, <see cref="string.Empty"/> or only contains white space.
        /// </exception>
        public StringLengthAttribute(string minPropertyName, string maxPropertyName, string errorMessage)
            : this(minPropertyName, maxPropertyName)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Initialize new instance of <see cref="StringLengthAttribute"/> using specified <paramref name="minLength"/>,
        /// <paramref name="minLength"/> and <paramref name="errorMessage"/> if validation failed.
        /// </summary>
        /// <param name="minLength">An allowed minimum length.</param>
        /// <param name="maxLength">an allowed maximum length.</param>
        /// <param name="errorMessage">An error message if validation failed.</param>
        public StringLengthAttribute(int minLength, int maxLength, string errorMessage)
            : this(minLength, maxLength)
        {
            ErrorMessage = errorMessage;
        }
        #endregion Constructors

        #region Private fields
        private readonly string? _minLengthPropertyName;
        private readonly int? _minLength;
        private readonly string? _maxLengthPropertyName;
        private readonly int? _maxLength;
        #endregion Private fields

        #region Protected methods
        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult" /> class.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (validationContext == null)
                throw new ArgumentNullException(nameof(validationContext));

            if (value is not string target)
                throw new ArgumentException(StringTable.TargetPropertyIsNotString);

            int minLength = GetMinLength(validationContext);
            int maxLength = GetMaxLength(validationContext);

            if (target.Length >= minLength && target.Length <= maxLength)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? string.Format(StringTable.InvalidLength, minLength, maxLength));
            }
        }
        #endregion Protected methods

        #region Private methods
        private int GetMinLength(ValidationContext validationContext)
        {
            if (_minLengthPropertyName != null)
            {
                PropertyInfo? property = validationContext.ObjectType.GetProperty(_minLengthPropertyName);

                if (property != null)
                {
                    object? propertyValue = property.GetValue(validationContext.ObjectInstance);

                    if (propertyValue is int minLength)
                    {
                        return minLength;
                    }
                    else
                    {
                        throw new ArgumentException(StringTable.LengthIsNotInteger);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(StringTable.PropertyNotFound, _minLengthPropertyName));
                }
            }
            else if (_minLength is int minLength)
            {
                return minLength;
            }
            else
            {
                throw new InvalidOperationException(StringTable.LengthIsNotInteger);
            }
        }
        private int GetMaxLength(ValidationContext validationContext)
        {
            if (_maxLengthPropertyName != null)
            {
                PropertyInfo? property = validationContext.ObjectType.GetProperty(_maxLengthPropertyName);

                if (property != null)
                {
                    object? propertyValue = property.GetValue(validationContext.ObjectInstance);

                    if (propertyValue is int maxLength)
                    {
                        return maxLength;
                    }
                    else
                    {
                        throw new ArgumentException(StringTable.LengthIsNotInteger);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(StringTable.PropertyNotFound, _maxLengthPropertyName));
                }
            }
            else if (_maxLength is int maxLength)
            {
                return maxLength;
            }
            else
            {
                throw new InvalidOperationException(StringTable.LengthIsNotInteger);
            }
        }
        #endregion Private methods
    }
}
