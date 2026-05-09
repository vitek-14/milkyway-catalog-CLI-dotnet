using System.Globalization;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.ConsoleUI.Infrastructure
{
    public static class ConsoleInput
    {
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

        public static string ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

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

        public static SpectralClass ReadSpectralClass(string prompt)
        {
            return ReadEnum<SpectralClass>(prompt);
        }

        public static SpectralClass? ReadSpectralClassOrCancel(string prompt)
        {
            return ReadEnumOrCancel<SpectralClass>(prompt);
        }

        public static void WaitForEnter()
        {
            Console.WriteLine("Stisknete ENTER pro navrat do menu...");
            Console.ReadLine();
        }

        private static string GetAllowedValues<TEnum>() where TEnum : struct, Enum
        {
            return string.Join(", ", Enum.GetNames<TEnum>().Where(name => name != "Unknown"));
        }
    }
}
