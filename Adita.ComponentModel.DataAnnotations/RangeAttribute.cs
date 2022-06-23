using Adita.ComponentModel.DataAnnotations.Resources;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Adita.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies the numeric range constraints for the value of a data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="RangeAttribute"/> using specified <paramref name="minPropertyName"/>
        /// as name of <see cref="double"/> property that contain minimum value <br/> and <paramref name="maxPropertyName"/>
        /// as name of <see cref="double"/> property that contain maximum value.
        /// </summary>
        /// <param name="minPropertyName">A <see cref="double"/> property name for specify the minimum value.</param>
        /// <param name="maxPropertyName">A <see cref="double"/> property name for specify the maximum value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="minPropertyName"/> 
        /// and or <paramref name="maxPropertyName"/> is <c>null</c>, <see cref="string.Empty"/> or only contains white space.
        /// </exception>
        public RangeAttribute(string minPropertyName, string maxPropertyName)
        {
            if (string.IsNullOrEmpty(minPropertyName))
            {
                throw new ArgumentException($"'{nameof(minPropertyName)}' cannot be null or empty.", nameof(minPropertyName));
            }

            if (string.IsNullOrEmpty(maxPropertyName))
            {
                throw new ArgumentException($"'{nameof(maxPropertyName)}' cannot be null or empty.", nameof(maxPropertyName));
            }

            _minPropertyName = minPropertyName;
            _maxPropertyName = maxPropertyName;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class by using the <paramref name="minimum"/> and
        /// <paramref name="maximum"/> values.
        /// </summary>
        /// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
        public RangeAttribute(double minimum, double maximum)
        {
            _minimum = minimum;
            _maximum = maximum;
        }
        /// <summary>
        /// Initialize new instance of <see cref="RangeAttribute"/> using specified <paramref name="minPropertyName"/>
        /// as name of <see cref="double"/> property that contain minimum value, <br/> <paramref name="maxPropertyName"/>
        /// as name of <see cref="double"/> property that contain maximum value
        /// and <paramref name="errorMessage"/> if validation failed.
        /// </summary>
        /// <param name="minPropertyName">A <see cref="double"/> property name for specify the minimum value.</param>
        /// <param name="maxPropertyName">A <see cref="double"/> property name for specify the maximum value.</param>
        /// <param name="errorMessage">An error message if validation failed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="minPropertyName"/>
        /// and or <paramref name="maxPropertyName"/> is <c>null</c>, <see cref="string.Empty"/> or only contains white space.
        /// </exception>
        public RangeAttribute(string minPropertyName, string maxPropertyName, string errorMessage)
            : this(minPropertyName, maxPropertyName)
        {
            ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class by using the <paramref name="minimum"/> value,
        /// <paramref name="maximum"/> value and <paramref name="errorMessage"/> if validation failed..
        /// </summary>
        /// <param name="minimum">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="maximum">Specifies the maximum value allowed for the data field value.</param>
        /// <param name="errorMessage">An error message if validation failed.</param>
        public RangeAttribute(double minimum, double maximum, string errorMessage)
            : this(minimum, maximum)
        {
            ErrorMessage = errorMessage;
        }
        #endregion Constructors

        #region Private fields
        private readonly string? _minPropertyName;
        private readonly string? _maxPropertyName;
        private readonly double? _minimum;
        private readonly double? _maximum;

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

            if (value is not double target)
                throw new ArgumentException(StringTable.TargetPropertyIsNotDouble);

            double minimum = GetMin(validationContext);
            double maximum = GetMax(validationContext);

            if (target >= minimum && target <= maximum)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? string.Format(StringTable.InvalidRange, minimum, maximum));
            }
        }
        #endregion Protected methods

        #region Private methods
        private double GetMin(ValidationContext validationContext)
        {
            if (_minPropertyName != null)
            {
                PropertyInfo? property = validationContext.ObjectType.GetProperty(_minPropertyName);

                if (property != null)
                {
                    object? propertyValue = property.GetValue(validationContext.ObjectInstance);

                    if (propertyValue is double minimum)
                    {
                        return minimum;
                    }
                    else
                    {
                        throw new ArgumentException(StringTable.RangeIsNotDouble);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(StringTable.PropertyNotFound, _minPropertyName));
                }
            }
            else if (_minimum is double minimum)
            {
                return minimum;
            }
            else
            {
                throw new InvalidOperationException(StringTable.RangeIsNotDouble);
            }
        }
        private double GetMax(ValidationContext validationContext)
        {
            if (_maxPropertyName != null)
            {
                PropertyInfo? property = validationContext.ObjectType.GetProperty(_maxPropertyName);

                if (property != null)
                {
                    object? propertyValue = property.GetValue(validationContext.ObjectInstance);

                    if (propertyValue is double maximum)
                    {
                        return maximum;
                    }
                    else
                    {
                        throw new ArgumentException(StringTable.RangeIsNotDouble);
                    }
                }
                else
                {
                    throw new ArgumentException(string.Format(StringTable.PropertyNotFound, _maxPropertyName));
                }
            }
            else if (_maximum is double maximum)
            {
                return maximum;
            }
            else
            {
                throw new InvalidOperationException(StringTable.RangeIsNotDouble);
            }
        }
        #endregion Private methods
    }
}
