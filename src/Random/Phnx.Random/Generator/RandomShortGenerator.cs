﻿using System;
using Phnx.Random.Generator.Interfaces;

namespace Phnx.Random.Generator
{
    /// <summary>
    /// Provides methods for generating a random <see cref="short"/>
    /// </summary>
    public class RandomShortGenerator : IRandomNumberGenerator<short>
    {
        /// <summary>
        /// Get a random <see cref="short"/>
        /// </summary>
        /// <returns>A random <see cref="short"/></returns>
        short IRandomGenerator<short>.Get()
        {
            return Get();
        }

        /// <summary>
        /// Get a new random instance of <see cref="short"/> within a limited range
        /// </summary>
        /// <param name="inclusiveMinValue">The inclusive minimum value to generate</param>
        /// <param name="inclusiveMaxValue">The inclusive maximum value to generate</param>
        /// <returns>A random instance of <see cref="short"/> with the specified range</returns>
        public short Get(short inclusiveMinValue = short.MinValue, short inclusiveMaxValue = short.MaxValue)
        {
            byte[] buffer = new byte[2];
            RandomHelper.Random.NextBytes(buffer);

            var randomGenerated = BitConverter.ToInt16(buffer, 0);
            var range = unchecked((inclusiveMaxValue - inclusiveMinValue) + 1);

            if (range == 0)
            {
                // Overflow, use full range of a short
                return randomGenerated;
            }

            return (short)((randomGenerated % range) + inclusiveMinValue);
        }
    }
}