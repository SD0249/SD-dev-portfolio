using System;
using System.Collections.Generic;

/* Amy Lee
 * 02/28/2025
 * Constructing a Custom Dictionary;
 * deepening the understanding of Data Structures */

namespace LeeA_HW3_CustomDictionary
{
    /// <summary>
    /// A Generic KeyValue Pair to hold a key-value pair.
    /// The Dictionary Class will hold multiple instances of this class.
    /// </summary>
    /// <typeparam name="T">Type of key to be determined in runtime</typeparam>
    /// <typeparam name="U">Type of value to be determined in runtime</typeparam>
    internal class CustomPair<T,U>
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------
        private T key;
        private U value;


        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------
        public T Key
        {
            get { return key; }
            set { key = value; }
        }

        public U Value
        { 
            get { return value; }
            set { this.value = value; }
        }


        // ---------------------------------------------------------------
        // Parameterized Constructor
        // ---------------------------------------------------------------
        /// <summary>
        /// Initializes the necessary fields for 
        /// the custom key-value pair to function
        /// </summary>
        /// <param name="key">Key to represent this data; 
        /// used to make hashcode</param>
        /// <param name="value">Value paired with the key; 
        /// the data to be retrieved using the key</param>
        public CustomPair(T key, U value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
