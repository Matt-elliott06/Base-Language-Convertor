using System;
using System.Collections.Generic;

namespace MultiLanguageConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Morse Code Dictionary
            Dictionary<char, string> morseDict = new Dictionary<char, string>
            {
                {'A', ".-"}, {'B', "-..."}, {'C', "-.-."}, {'D', "-.."}, {'E', "."},
                {'F', "..-."}, {'G', "--."}, {'H', "...."}, {'I', ".."}, {'J', ".---"},
                {'K', "-.-"}, {'L', ".-.."}, {'M', "--"}, {'N', "-."}, {'O', "---"},
                {'P', ".--."}, {'Q', "--.-"}, {'R', ".-."}, {'S', "..."}, {'T', "-"},
                {'U', "..-"}, {'V', "...-"}, {'W', ".--"}, {'X', "-..-"}, {'Y', "-.--"},
                {'Z', "--.."}, {'1', ".----"}, {'2', "..---"}, {'3', "...--"}, {'4', "....-"},
                {'5', "....."}, {'6', "-...."}, {'7', "--..."}, {'8', "---.."}, {'9', "----."},
                {'0', "-----"}, {' ', "/"}
            };
            Dictionary<string, char> reverseMorseDict = new Dictionary<string, char>();
            foreach (var pair in morseDict)
            {
                reverseMorseDict[pair.Value] = pair.Key;
            }

            void Converter()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Choose your input type:");
                Console.WriteLine("1. Binary");
                Console.WriteLine("2. ASCII (Text)");
                Console.WriteLine("3. Hexadecimal");
                Console.WriteLine("4. Morse Code");
                Console.ResetColor();

                string inputType = Console.ReadLine()?.Trim();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Choose your output type:");
                Console.WriteLine("1. Binary");
                Console.WriteLine("2. ASCII (Text)");
                Console.WriteLine("3. Hexadecimal");
                Console.WriteLine("4. Morse Code");
                Console.ResetColor();

                string outputType = Console.ReadLine()?.Trim();

                if (inputType == outputType)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input and output types cannot be the same. Please try again.");
                    Console.ResetColor();
                    Converter();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enter your input:");
                    Console.ResetColor();
                    string input = Console.ReadLine();

                    try
                    {
                        string result = Convert(inputType, outputType, input);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Conversion Result:");
                        Console.ResetColor();
                        Console.WriteLine(result);
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.ResetColor();
                    }
                }

                Again();
            }

            string Convert(string inputType, string outputType, string input)
            {
                switch (inputType)
                {
                    case "1": // Binary
                        if (outputType == "2") return BinaryToAscii(input);
                        if (outputType == "3") return BinaryToHex(input);
                        if (outputType == "4") return BinaryToMorse(input);
                        break;
                    case "2": // ASCII
                        if (outputType == "1") return AsciiToBinary(input);
                        if (outputType == "3") return AsciiToHex(input);
                        if (outputType == "4") return AsciiToMorse(input);
                        break;
                    case "3": // Hexadecimal
                        if (outputType == "1") return HexToBinary(input);
                        if (outputType == "2") return HexToAscii(input);
                        if (outputType == "4") return HexToMorse(input);
                        break;
                    case "4": // Morse Code
                        if (outputType == "1") return MorseToBinary(input);
                        if (outputType == "2") return MorseToAscii(input);
                        if (outputType == "3") return MorseToHex(input);
                        break;
                }

                throw new ArgumentException("Invalid conversion types.");
            }

            // Conversion Methods
            string BinaryToAscii(string binaryInput)
            {
                string[] binaryArray = binaryInput.Split(' ');
                List<char> asciiList = new List<char>();
                foreach (string binary in binaryArray)
                {
                    int asciiValue = System.Convert.ToInt32(binary, 2);
                    asciiList.Add((char)asciiValue);
                }
                return new string(asciiList.ToArray());
            }

            string BinaryToHex(string binaryInput) => AsciiToHex(BinaryToAscii(binaryInput));
            string BinaryToMorse(string binaryInput) => AsciiToMorse(BinaryToAscii(binaryInput));

            string AsciiToBinary(string asciiInput)
            {
                List<string> binaryList = new List<string>();
                foreach (char character in asciiInput)
                {
                    binaryList.Add(System.Convert.ToString(character, 2).PadLeft(8, '0'));
                }
                return string.Join(" ", binaryList);
            }

            string AsciiToHex(string asciiInput)
            {
                List<string> hexList = new List<string>();
                foreach (char character in asciiInput)
                {
                    hexList.Add(((int)character).ToString("X"));
                }
                return string.Join(" ", hexList);
            }

            string AsciiToMorse(string asciiInput)
            {
                List<string> morseList = new List<string>();
                foreach (char character in asciiInput.ToUpper())
                {
                    if (morseDict.ContainsKey(character))
                        morseList.Add(morseDict[character]);
                    else
                        throw new ArgumentException($"Character '{character}' cannot be converted to Morse Code.");
                }
                return string.Join(" ", morseList);
            }

            string HexToBinary(string hexInput) => AsciiToBinary(HexToAscii(hexInput));
            string HexToAscii(string hexInput)
            {
                List<char> asciiList = new List<char>();
                string[] hexArray = hexInput.Split(' ');
                foreach (string hex in hexArray)
                {
                    int asciiValue = System.Convert.ToInt32(hex, 16);
                    asciiList.Add((char)asciiValue);
                }
                return new string(asciiList.ToArray());
            }

            string HexToMorse(string hexInput) => AsciiToMorse(HexToAscii(hexInput));

            string MorseToBinary(string morseInput) => AsciiToBinary(MorseToAscii(morseInput));
            string MorseToAscii(string morseInput)
            {
                List<char> asciiList = new List<char>();
                string[] morseArray = morseInput.Split(' ');
                foreach (string morse in morseArray)
                {
                    if (reverseMorseDict.ContainsKey(morse))
                        asciiList.Add(reverseMorseDict[morse]);
                    else
                        throw new ArgumentException($"Morse code '{morse}' cannot be converted to ASCII.");
                }
                return new string(asciiList.ToArray());
            }

            string MorseToHex(string morseInput) => AsciiToHex(MorseToAscii(morseInput));

            void Again()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Another conversion? Y/N");
                Console.ResetColor();
                string choice = Console.ReadLine();
                if (choice?.ToUpper() == "Y")
                {
                    Console.Clear();
                    Converter();
                }
                else if (choice?.ToUpper() == "N")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Goodbye!");
                    Console.ResetColor();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input! Please enter 'Y' or 'N'.");
                    Console.ResetColor();
                    Again();
                }
            }

            // Start Program
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to the Multi-Language Converter!");
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();
            Converter();
        }
    }
}
