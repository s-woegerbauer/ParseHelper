using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseHelper
{
    public static class ParseHelper
    {
        public static class Simple
        {
            /// <summary>
            /// Converts a file to an one dimensional Array
            /// Each line is one item
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="inputPath"></param>
            /// <returns></returns>
            public static T[] LinesToOneDimensionalArray<T>(string inputPath)
            {
                string[] lines = File.ReadAllLines(inputPath);
                T[] result = new T[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    result[i] = Help.ParseLine<T>(lines[i]);
                }

                return result;
            }

            /// <summary>
            /// Converts a file to an two dimensional Array
            /// Each line is one row
            /// Each split in a row is an item
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="inputPath"></param>
            /// <param name="splitter"></param>
            /// <returns></returns>
            public static T[,] LinesToTwoDimensionalArray<T>(string inputPath, char splitter)
            {
                string[] lines = File.ReadAllLines(inputPath);
                int maxLength = Help.GetMaxLengthOfSplit(lines, splitter);
                T[,] result = new T[lines.Length, maxLength];

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    for(int j = 0; j < maxLength; j++)
                    {
                        if(line.Split(splitter).Length > j)
                        {
                            result[i, j] = Help.ParseLine<T>(lines[i].Split(splitter)[j]);
                        }
                        else
                        {
                            result[i, j] = default;
                        }
                    }
                }

                return result;
            }


        }

        internal static class Help
        {
            /// <summary>
            /// Parses one line into a value
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="line"></param>
            /// <returns></returns>
            /// <exception cref="InvalidCastException"></exception>
            internal static T ParseLine<T>(string line)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)line;
                }
                else
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

                    if (converter.CanConvertFrom(typeof(string)))
                    {
                        return (T)converter.ConvertFrom(line);
                    }
                    else
                    {
                        throw new InvalidCastException($"Cannot parse '{line}' to type {typeof(T)}.");
                    }
                }
            }

            internal static int GetMaxLengthOfSplit(string[] input, char splitter)
            {
                int max = 0;

                foreach(string line in input)
                {
                    if(line.Split(splitter).Length >= max)
                    {
                        max = line.Split(splitter).Length;
                    }
                }

                return max;
            }
        }
    }
}