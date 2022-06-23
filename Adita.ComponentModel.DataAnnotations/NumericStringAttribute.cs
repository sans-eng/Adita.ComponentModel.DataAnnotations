using Adita.ComponentModel.DataAnnotations.Resources;
using System.ComponentModel.DataAnnotations;

namespace Adita.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies a numeric datatype of a string data field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NumericStringAttribute : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="NumericStringAttribute"/> using specified <paramref name="type"/> as a type of a data field.
        /// </summary>
        /// <param name="type">A <see cref="System.Type"/> of a data field.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c></exception>
        public NumericStringAttribute(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        /// <summary>
        /// Initialize new instance of <see cref="NumericStringAttribute"/> using specified <paramref name="type"/> as a type of a data field and specified
        /// <paramref name="errorMessage"/> if validation failed.
        /// </summary>
        /// <param name="type">A <see cref="DataType"/> of a data field.</param>
        /// <param name="errorMessage">An error message if validation failed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c></exception>
        public NumericStringAttribute(Type type, string? errorMessage) : this(type)
        {
            ErrorMessage = errorMessage;
        }
        #endregion Constructors

        #region Public properties
        /// <summary>
        /// Gets A <see cref="Type"/> that associated with current <see cref="NumericStringAttribute"/>.
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
            if (value is not string target)
                throw new ArgumentException(StringTable.TargetPropertyIsNotString);

            if (Type == typeof(sbyte))
                return sbyte.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if(Type == typeof(byte))
                return byte.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if(Type == typeof(short))
                return short.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if(Type == typeof(ushort))
                return ushort.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if(Type == typeof(int))
                return int.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);      
            else if (Type == typeof(uint))
                return uint.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(long))
                return long.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(ulong))
                return ulong.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(nint))
                return nint.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(nuint))
                return nuint.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(float))
                return float.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else if (Type == typeof(double))
                return double.TryParse(target, out _) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
            else return new ValidationResult(ErrorMessage ?? StringTable.InvalidType);
        }
        #endregion Protected methods
    }
}