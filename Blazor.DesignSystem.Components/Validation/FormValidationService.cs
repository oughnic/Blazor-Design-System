using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Blazor.DesignSystem.Components.Validation
{
    /// <summary>
    /// Provides validation utilities for GOV.UK Design System form components.
    /// These validators follow GOV.UK guidance for error messages and validation patterns.
    /// </summary>
    public static class FormValidationService
    {
        /// <summary>
        /// Result of a validation operation.
        /// </summary>
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string? ErrorMessage { get; set; }
            public List<string> AffectedFields { get; set; } = new();

            public static ValidationResult Success() => new() { IsValid = true };
            public static ValidationResult Failure(string errorMessage, params string[] affectedFields) => new()
            {
                IsValid = false,
                ErrorMessage = errorMessage,
                AffectedFields = new List<string>(affectedFields)
            };
        }

        #region Email Validation

        // RFC 5322 compliant email regex pattern
        private static readonly Regex EmailRegex = new(
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Validates an email address format.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <param name="fieldName">The name of the field for error messages.</param>
        /// <returns>Validation result with error message if invalid.</returns>
        public static ValidationResult ValidateEmail(string? email, string fieldName = "email address")
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ValidationResult.Failure($"Enter an {fieldName}");
            }

            // Check for common mistakes
            if (email.Contains("@@"))
            {
                return ValidationResult.Failure($"Enter an {fieldName} in the correct format, like name@example.com");
            }

            if (!email.Contains('@'))
            {
                return ValidationResult.Failure($"Enter an {fieldName} in the correct format, like name@example.com");
            }

            // Check for multiple @ symbols
            if (email.Count(c => c == '@') > 1)
            {
                return ValidationResult.Failure($"Enter an {fieldName} in the correct format, like name@example.com");
            }

            // Validate against regex
            if (!EmailRegex.IsMatch(email))
            {
                return ValidationResult.Failure($"Enter an {fieldName} in the correct format, like name@example.com");
            }

            // Check domain part has at least one dot (for TLD)
            var atIndex = email.IndexOf('@');
            var domain = email.Substring(atIndex + 1);
            if (!domain.Contains('.'))
            {
                return ValidationResult.Failure($"Enter an {fieldName} in the correct format, like name@example.com");
            }

            return ValidationResult.Success();
        }

        #endregion

        #region Date Validation

        /// <summary>
        /// Result of date validation with individual field error flags.
        /// </summary>
        public class DateValidationResult : ValidationResult
        {
            public bool HasDayError { get; set; }
            public bool HasMonthError { get; set; }
            public bool HasYearError { get; set; }
            public DateTime? ParsedDate { get; set; }

            public static DateValidationResult Success(DateTime date) => new()
            {
                IsValid = true,
                ParsedDate = date
            };

            public static DateValidationResult Failure(string errorMessage, bool dayError = false, bool monthError = false, bool yearError = false) => new()
            {
                IsValid = false,
                ErrorMessage = errorMessage,
                HasDayError = dayError,
                HasMonthError = monthError,
                HasYearError = yearError
            };
        }

        /// <summary>
        /// Validates a date entered as separate day, month, and year fields.
        /// Checks for valid calendar dates (e.g., 31/11 is invalid because November only has 30 days).
        /// </summary>
        /// <param name="day">Day value.</param>
        /// <param name="month">Month value.</param>
        /// <param name="year">Year value.</param>
        /// <param name="fieldName">The name of the date field for error messages.</param>
        /// <param name="options">Additional validation options.</param>
        /// <returns>Date validation result with individual field error flags.</returns>
        public static DateValidationResult ValidateDate(string? day, string? month, string? year, string fieldName = "date", DateValidationOptions? options = null)
        {
            options ??= new DateValidationOptions();

            var missingParts = new List<string>();
            bool dayMissing = string.IsNullOrWhiteSpace(day);
            bool monthMissing = string.IsNullOrWhiteSpace(month);
            bool yearMissing = string.IsNullOrWhiteSpace(year);

            // Check for missing fields
            if (dayMissing && monthMissing && yearMissing)
            {
                return DateValidationResult.Failure($"Enter {GetArticle(fieldName)} {fieldName}", true, true, true);
            }

            if (dayMissing) missingParts.Add("day");
            if (monthMissing) missingParts.Add("month");
            if (yearMissing) missingParts.Add("year");

            if (missingParts.Count > 0)
            {
                var missingText = string.Join(" and ", missingParts);
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must include a {missingText}",
                    dayMissing, monthMissing, yearMissing);
            }

            // Parse individual parts
            if (!int.TryParse(day, out int dayValue) || dayValue < 1 || dayValue > 31)
            {
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be a real date",
                    dayError: true);
            }

            if (!int.TryParse(month, out int monthValue) || monthValue < 1 || monthValue > 12)
            {
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be a real date",
                    monthError: true);
            }

            if (!int.TryParse(year, out int yearValue))
            {
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be a real date",
                    yearError: true);
            }

            // Validate year range
            if (yearValue < options.MinYear)
            {
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be after {options.MinYear - 1}",
                    yearError: true);
            }

            if (yearValue > options.MaxYear)
            {
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be before {options.MaxYear + 1}",
                    yearError: true);
            }

            // Validate actual calendar date (e.g., 31/11 is invalid)
            try
            {
                var parsedDate = new DateTime(yearValue, monthValue, dayValue);

                // Check if date is in the past
                if (options.MustBeInPast && parsedDate > DateTime.Today)
                {
                    return DateValidationResult.Failure(
                        $"{CapitalizeFirst(fieldName)} must be in the past",
                        true, true, true);
                }

                // Check if date is in the future
                if (options.MustBeInFuture && parsedDate < DateTime.Today)
                {
                    return DateValidationResult.Failure(
                        $"{CapitalizeFirst(fieldName)} must be in the future",
                        true, true, true);
                }

                // Check if date is today or in the past
                if (options.MustBeTodayOrPast && parsedDate > DateTime.Today)
                {
                    return DateValidationResult.Failure(
                        $"{CapitalizeFirst(fieldName)} must be today or in the past",
                        true, true, true);
                }

                // Check if date is today or in the future
                if (options.MustBeTodayOrFuture && parsedDate < DateTime.Today)
                {
                    return DateValidationResult.Failure(
                        $"{CapitalizeFirst(fieldName)} must be today or in the future",
                        true, true, true);
                }

                return DateValidationResult.Success(parsedDate);
            }
            catch (ArgumentOutOfRangeException)
            {
                // Invalid date like 31/11 or 29/02 in non-leap year
                return DateValidationResult.Failure(
                    $"{CapitalizeFirst(fieldName)} must be a real date",
                    DetermineInvalidDayForMonth(dayValue, monthValue, yearValue),
                    DetermineInvalidMonth(monthValue),
                    false);
            }
        }

        /// <summary>
        /// Determines if the day is invalid for the given month.
        /// </summary>
        private static bool DetermineInvalidDayForMonth(int day, int month, int year)
        {
            // February check
            if (month == 2)
            {
                bool isLeapYear = DateTime.IsLeapYear(year);
                int maxDay = isLeapYear ? 29 : 28;
                return day > maxDay;
            }

            // Months with 30 days
            if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                return day > 30;
            }

            return false;
        }

        /// <summary>
        /// Determines if the month value is invalid.
        /// </summary>
        private static bool DetermineInvalidMonth(int month)
        {
            return month < 1 || month > 12;
        }

        /// <summary>
        /// Options for date validation.
        /// </summary>
        public class DateValidationOptions
        {
            /// <summary>
            /// Minimum allowed year. Default is 1900.
            /// </summary>
            public int MinYear { get; set; } = 1900;

            /// <summary>
            /// Maximum allowed year. Default is 2100.
            /// </summary>
            public int MaxYear { get; set; } = 2100;

            /// <summary>
            /// If true, date must be in the past (before today).
            /// </summary>
            public bool MustBeInPast { get; set; }

            /// <summary>
            /// If true, date must be in the future (after today).
            /// </summary>
            public bool MustBeInFuture { get; set; }

            /// <summary>
            /// If true, date must be today or in the past.
            /// </summary>
            public bool MustBeTodayOrPast { get; set; }

            /// <summary>
            /// If true, date must be today or in the future.
            /// </summary>
            public bool MustBeTodayOrFuture { get; set; }
        }

        #endregion

        #region Password Validation

        /// <summary>
        /// Validates a password against common requirements.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <param name="options">Password requirements options.</param>
        /// <returns>Validation result.</returns>
        public static ValidationResult ValidatePassword(string? password, PasswordValidationOptions? options = null)
        {
            options ??= new PasswordValidationOptions();

            if (string.IsNullOrEmpty(password))
            {
                return ValidationResult.Failure("Enter a password");
            }

            if (password.Length < options.MinLength)
            {
                return ValidationResult.Failure($"Password must be at least {options.MinLength} characters");
            }

            if (options.MaxLength.HasValue && password.Length > options.MaxLength.Value)
            {
                return ValidationResult.Failure($"Password must be {options.MaxLength} characters or less");
            }

            if (options.RequireUppercase && !Regex.IsMatch(password, "[A-Z]"))
            {
                return ValidationResult.Failure("Password must include a capital letter");
            }

            if (options.RequireLowercase && !Regex.IsMatch(password, "[a-z]"))
            {
                return ValidationResult.Failure("Password must include a lowercase letter");
            }

            if (options.RequireDigit && !Regex.IsMatch(password, "[0-9]"))
            {
                return ValidationResult.Failure("Password must include a number");
            }

            // Comprehensive symbol set including common special characters
            if (options.RequireSymbol && !Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>\-+=_\[\]\\;`~]"))
            {
                return ValidationResult.Failure("Password must include a symbol");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Options for password validation.
        /// </summary>
        public class PasswordValidationOptions
        {
            public int MinLength { get; set; } = 8;
            public int? MaxLength { get; set; }
            public bool RequireUppercase { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireDigit { get; set; }
            public bool RequireSymbol { get; set; }
        }

        #endregion

        #region National Insurance Number Validation

        // UK National Insurance number pattern:
        // - First character: A-Z excluding D, F, I, Q, U, V (prefix cannot start with these)
        // - Second character: A-Z excluding D, F, I, O, Q, U, V (prefix cannot contain these in second position)
        // - Followed by 6 digits
        // - Optional suffix letter A-D
        // - Cannot start with BG, GB, NK, KN, TN, NT, or ZZ (administrative codes)
        private static readonly Regex NinoRegex = new(
            @"^(?!BG|GB|NK|KN|TN|NT|ZZ)[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D]?$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Validates a UK National Insurance number.
        /// </summary>
        /// <param name="nino">The National Insurance number to validate.</param>
        /// <returns>Validation result.</returns>
        public static ValidationResult ValidateNationalInsuranceNumber(string? nino)
        {
            if (string.IsNullOrWhiteSpace(nino))
            {
                return ValidationResult.Failure("Enter a National Insurance number");
            }

            // Remove spaces and make uppercase
            var cleaned = nino.Replace(" ", "").ToUpperInvariant();

            if (!NinoRegex.IsMatch(cleaned))
            {
                return ValidationResult.Failure("Enter a National Insurance number in the correct format");
            }

            return ValidationResult.Success();
        }

        #endregion

        #region Phone Number Validation

        /// <summary>
        /// Validates a UK phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>Validation result.</returns>
        public static ValidationResult ValidateUkPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return ValidationResult.Failure("Enter a telephone number");
            }

            // Remove spaces, hyphens, and parentheses
            var cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)]", "");

            // UK phone number formats:
            // - Landlines: 01234 567890 (10 digits after 0) or 0123 456 7890 (11 digits total including area code)
            // - Mobile: 07123 456789 (11 digits total)
            // - International: +44 1234 567890 or 0044 1234 567890
            // Pattern allows 10-11 digits after prefix to accommodate both formats
            if (!Regex.IsMatch(cleaned, @"^(?:0\d{10}|\+44\d{10}|0044\d{10})$"))
            {
                return ValidationResult.Failure("Enter a telephone number in the correct format, like 01234 567890 or +44 1234 567890");
            }

            return ValidationResult.Success();
        }

        #endregion

        #region Postcode Validation

        // UK postcode pattern
        private static readonly Regex PostcodeRegex = new(
            @"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) ?[0-9][A-Za-z]{2})$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Validates a UK postcode.
        /// </summary>
        /// <param name="postcode">The postcode to validate.</param>
        /// <returns>Validation result.</returns>
        public static ValidationResult ValidateUkPostcode(string? postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode))
            {
                return ValidationResult.Failure("Enter a postcode");
            }

            if (!PostcodeRegex.IsMatch(postcode.Trim()))
            {
                return ValidationResult.Failure("Enter a real postcode");
            }

            return ValidationResult.Success();
        }

        #endregion

        #region Helper Methods

        private static string GetArticle(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                return "a";

            var firstChar = char.ToLower(fieldName[0]);
            return "aeiou".Contains(firstChar) ? "an" : "a";
        }

        private static string CapitalizeFirst(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

        #endregion
    }
}
