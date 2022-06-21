using System.ComponentModel.DataAnnotations;

namespace Adita.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies a <see cref="System.Type"/> of a data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataTypeAttribute : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="DataTypeAttribute"/> using specified <paramref name="type"/> as a type of a data field.
        /// </summary>
        /// <param name="type">A <see cref="DataType"/> of a data field.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c></exception>
        public DataTypeAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        /// <summary>
        /// Initialize new instance of <see cref="DataTypeAttribute"/> using specified <paramref name="type"/> as a type of a data field and specified
        /// <paramref name="errorMessage"/> if validation failed.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="errorMessage"></param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c></exception>
        public DataTypeAttribute(Type type, string? errorMessage) : this(type)
        {
            ErrorMessage = errorMessage;
        }
        #endregion Constructors

        #region Public properties
        /// <summary>
        /// Gets A <see cref="Type"/> that associated with current <see cref="DataTypeAttribute"/>.
        /// </summary>
        public Type Type { get; }
        #endregion Public properties

        #region Protected methods
        /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <exception cref="InvalidOperationException">The current attribute is malformed.</exception>
        /// <returns>An instance of the <see cref="ValidationResult" /> class.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value?.GetType() == Type)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? string.Empty);
            }
        }
        #endregion Protected methods
    }
}