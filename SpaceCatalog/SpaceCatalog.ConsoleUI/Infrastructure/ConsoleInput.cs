using System.Globalization;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.ConsoleUI.Infrastructure
{
    /// <summary>
    /// Reads and validates console input.
    /// </summary>
    public static class ConsoleInput
    {
        /// <summary>
        /// Reads a required string value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered string.</returns>
        public static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                Console.WriteLine("[CHYBA]: Hodnota je povinna.");
            }
        }

        /// <summary>
        /// Reads a required string value or cancels input.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered string, or null when canceled.</returns>
        public static string? ReadRequiredStringOrCancel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine()?.Trim() ?? string.Empty;

                if (value == "0")
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                Console.WriteLine("[CHYBA]: Hodnota je povinna. Pro navrat do menu zadejte 0.");
            }
        }

        /// <summary>
        /// Reads an optional string value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered string, or an empty string.</returns>
        public static string ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Reads an integer value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered integer.</returns>
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();

                if (int.TryParse(value, out var parsedValue))
                {
                    return parsedValue;
                }

                Console.WriteLine("[CHYBA]: Zadejte cele cislo.");
            }
        }

        /// <summary>
        /// Reads an integer value or cancels input.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered integer, or null when canceled.</returns>
        public static int? ReadIntOrCancel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine()?.Trim();

                if (value == "0")
                {
                    return null;
                }

                if (int.TryParse(value, out var parsedValue))
                {
                    return parsedValue;
                }

                Console.WriteLine("[CHYBA]: Zadejte cele cislo. Pro navrat do menu zadejte 0.");
            }
        }

        /// <summary>
        /// Reads an optional integer value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered integer, or null when empty or invalid.</returns>
        public static int? ReadNullableInt(string prompt)
        {
            Console.Write(prompt);
            var value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (int.TryParse(value, out var parsedValue))
            {
                return parsedValue;
            }

            Console.WriteLine("[CHYBA]: Neplatne cislo, hodnota zustane beze zmeny.");
            return null;
        }

        /// <summary>
        /// Reads a double value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered double.</returns>
        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();

                if (double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var currentCultureValue))
                {
                    return currentCultureValue;
                }

                if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var invariantCultureValue))
                {
                    return invariantCultureValue;
                }

                Console.WriteLine("[CHYBA]: Zadejte ciselnou hodnotu.");
            }
        }

        /// <summary>
        /// Reads a double value or cancels input.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered double, or null when canceled.</returns>
        public static double? ReadDoubleOrCancel(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine()?.Trim();

                if (value == "0")
                {
                    return null;
                }

                if (double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var currentCultureValue))
                {
                    return currentCultureValue;
                }

                if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var invariantCultureValue))
                {
                    return invariantCultureValue;
                }

                Console.WriteLine("[CHYBA]: Zadejte ciselnou hodnotu. Pro navrat do menu zadejte 0.");
            }
        }

        /// <summary>
        /// Reads an enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered enum value.</returns>
        public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, Enum
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine();

                if (Enum.TryParse<TEnum>(value, true, out var parsedValue) && Convert.ToInt32(parsedValue) != 0)
                {
                    return parsedValue;
                }

                Console.WriteLine($"[CHYBA]: Neplatna hodnota. Povolene hodnoty jsou: {GetAllowedValues<TEnum>()}.");
            }
        }

        /// <summary>
        /// Reads an enum value or cancels input.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered enum value, or null when canceled.</returns>
        public static TEnum? ReadEnumOrCancel<TEnum>(string prompt) where TEnum : struct, Enum
        {
            while (true)
            {
                Console.Write(prompt);
                var value = Console.ReadLine()?.Trim();

                if (value == "0")
                {
                    return null;
                }

                if (Enum.TryParse<TEnum>(value, true, out var parsedValue) && Convert.ToInt32(parsedValue) != 0)
                {
                    return parsedValue;
                }

                Console.WriteLine($"[CHYBA]: Neplatna hodnota. Povolene hodnoty jsou: {GetAllowedValues<TEnum>()}. Pro navrat do menu zadejte 0.");
            }
        }

        /// <summary>
        /// Reads an optional enum value.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="prompt">The prompt text.</param>
        /// <param name="currentValue">The current enum value.</param>
        /// <returns>The entered enum value, or the current value.</returns>
        public static TEnum ReadOptionalEnum<TEnum>(string prompt, TEnum currentValue) where TEnum : struct, Enum
        {
            Console.Write(prompt);
            var value = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(value))
            {
                return currentValue;
            }

            if (Enum.TryParse<TEnum>(value, true, out var parsedValue) && Convert.ToInt32(parsedValue) != 0)
            {
                return parsedValue;
            }

            Console.WriteLine($"[CHYBA]: Neplatna hodnota, zustava {currentValue}. Povolene hodnoty jsou: {GetAllowedValues<TEnum>()}.");
            return currentValue;
        }

        /// <summary>
        /// Reads a spectral class value.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered spectral class.</returns>
        public static SpectralClass ReadSpectralClass(string prompt)
        {
            return ReadEnum<SpectralClass>(prompt);
        }

        /// <summary>
        /// Reads a spectral class value or cancels input.
        /// </summary>
        /// <param name="prompt">The prompt text.</param>
        /// <returns>The entered spectral class, or null when canceled.</returns>
        public static SpectralClass? ReadSpectralClassOrCancel(string prompt)
        {
            return ReadEnumOrCancel<SpectralClass>(prompt);
        }

        /// <summary>
        /// Waits for the user to press Enter.
        /// </summary>
        public static void WaitForEnter()
        {
            Console.WriteLine("Stisknete ENTER pro navrat do menu...");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets allowed names for an enum type.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <returns>The allowed enum names.</returns>
        private static string GetAllowedValues<TEnum>() where TEnum : struct, Enum
        {
            return string.Join(", ", Enum.GetNames<TEnum>().Where(name => name != "Unknown"));
        }
    }
}
