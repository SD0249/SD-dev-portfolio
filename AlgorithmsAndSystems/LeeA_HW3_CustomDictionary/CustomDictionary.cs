using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace LeeA_HW3_CustomDictionary
{
    /// <summary>
    /// Generic Custom Dictionary Class
    /// Purpose: A custom dictionary class which implements a hash table
    ///          to store key-value pair objects using Bucket(Chaining)
    /// </summary>
    /// <typeparam name="TKey">Type of Key;the specific type
    /// will be determined in runtime</typeparam>
    /// <typeparam name="TValue">Type of Value;the specific type
    /// will be determined in runtime</typeparam>
    internal class CustomDictionary<TKey, TValue> 
    {
        // ---------------------------------------------------------------
        // Fields
        // ---------------------------------------------------------------

        // An array of Lists of CustomPair Objects to hold the
        // ACTUAL keys and values of the dictionary
        private List<CustomPair<TKey, TValue>>[] data;

        // The total count of items in the dictionary
        private double count;


        // ---------------------------------------------------------------
        // Properties
        // ---------------------------------------------------------------

        /// <summary>
        /// Gets the CURRENT count of ACTUAL data in the dictionary
        /// </summary>
        public double Count
        {
            get { return count; }
        }

        /// <summary>
        /// Calculate and return the load factor(count/arraysize).
        /// Ensure you're properly returning a fractional amount as necessary.
        /// </summary>
        public double LoadFactor
        {
            get { return count / (data.Length); }
        }

        // ** INDEXER Property
        //    (Write after the methods have been constructed)

        /// <summary>
        /// Get or set individual values in the dictionary.
        /// </summary>
        /// <param name="key">Unique Identifier to retrieve a specific value</param>
        /// <returns>The value associated to the key</returns>
        public TValue this[TKey key]
        {
            // Get: Run the key through the hash function
            //      to get an array index
            get
            {
                // Get the index of the key using the private method below
                int index = KeyToIndex(key);
                int targetIndex = -1;

                // 1) Determine if there is a list in that index yet
                if (data[index] != null)
                {
                    // 1-a) If so, seach all of the pairs in the list
                    //      of that index for the specified key.
                    for(int i = 0; i < data[index].Count; i++)
                    {
                        if (key.Equals(data[index][i].Key))
                        {
                            targetIndex = i;
                        }
                    }

                    // 2) If the key was found in the list,
                    //    return the corresponding value
                    if(targetIndex != -1)
                    {
                        return data[index][targetIndex].Value;
                    }
                    // 2-a) If the key was not found,
                    //      throw a KeyNotFoundException()
                    else
                    {
                        throw new KeyNotFoundException
                            ("That Key does not exist in the underlying list.");
                    }
                    
                }
                // 1-b) If there was no list, throw a KeyNotFoundException()
                else
                {
                    throw new KeyNotFoundException
                        ("That Key does not exist; there is no list of that index.");
                }
            }

            // Set: Run the key through the hash function
            //      to get an array index
            set  
            {
                // Get the index of this key for the underlying data array
                int index = KeyToIndex(key);

                // 1-a) If there is no list at the index
                if (data[index] == null)
                {
                    // Instantiate the list in that index
                    data[index] = new List<CustomPair<TKey, TValue>>();

                    // Create a new pair and store it to the list
                    // !! Don't forget to increment the count field
                    CustomPair<TKey, TValue> pairToAdd 
                        = new CustomPair<TKey, TValue>(key, value);

                    data[index].Add(pairToAdd);
                    count++;
                }
                // 1-b) If there is a list, search through for that key
                else
                {
                    for(int i = 0; i < data[index].Count; i++)
                    {
                        // If that key is found, update the pair's value
                        // with the new data
                        if (key.Equals(data[index][i].Key))
                        {
                            // After updating the value,
                            // do not continue running this code
                            data[index][i].Value = value;
                            return;
                        }
                    }

                    // When the for loop terminates = key not found
                    // Create a new pair and store it in the corresponding list
                    // !! Don't forget to increment the count field
                    CustomPair<TKey, TValue> pairToAdd 
                        = new CustomPair<TKey, TValue>(key, value);
                    data[index].Add(pairToAdd);
                    count++;
                }
            }
        } 


        // ---------------------------------------------------------------
        // Constructors
        // ---------------------------------------------------------------

        // Default Constructor

        /// <summary>
        /// Initialize the array with a size of 100.
        /// </summary>
        public CustomDictionary()
        {
            data = new List<CustomPair<TKey, TValue>>[100];
        }

        // Parameterized Constructor

        /// <summary>
        /// An integer array length specifies the initial size of the array
        /// </summary>
        /// <param name="arrayLength">
        /// Desired initial size of the array</param>
        public CustomDictionary(int arrayLength)
        {
            data = new List<CustomPair<TKey, TValue>>[arrayLength];
        }


        // ---------------------------------------------------------------
        // Methods
        // ---------------------------------------------------------------

        // ** ContainsKey()

        /// <summary>
        /// Determines whether if the specified key exists in the array
        /// </summary>
        /// <param name="key">Key to check whether
        /// it is in the dictionary</param>
        /// <returns>Returns a boolean value that represents whether
        /// or not the dictionary contains this key</returns>
        public bool ContainsKey(TKey key)
        {
            // 1) Hashing the key
            int hashCode = KeyToIndex(key);

            // 2) Check the proper element of the array
            //    If the generated index is in the range of the data array field,
            //    though it is guranteed from KeyToIndex method
            //    that it would be in the range
            if(hashCode >= 0 && hashCode < data.Length)
            {
                // Search through the appropriate bucket
                if (data[hashCode] != null)
                {
                    // While running through this for loop,
                    // if the key is found in the bucket, return true.
                    for(int i = 0; i < data[hashCode].Count; i++)
                    {
                        if (key.Equals(data[hashCode][i].Key))
                        {
                            return true;
                        }
                    }

                    // If the for loop has ended without returning anything,
                    // then it means this key is not in the dictionary!
                    return false;
                }
                else
                {
                    // This specific bucket is empty(not instantiated)
                    // --> No data for this key!
                    return false;
                }
            }
            // Index out of range! (which should not happen)
            else
            {
                return false;
            }
        }


        // ** Add()

        /// <summary>
        /// If the key does not exist in the dictionary, 
        /// add a CustomPair(Key-Value) to the appropriate bucket.
        /// If a key does exist, throw an Argument Exception.
        /// </summary>
        /// <param name="key">Unique Identifer to retrieve a specific value</param>
        /// <param name="value">A value associated with the key</param>
        public void Add(TKey key, TValue value)
        {
            // If the key does not exist in the dictionary
            if(!ContainsKey(key))
            {
                // 1) Get the Hashcode for the key passed into the Add method
                int index = KeyToIndex(key);

                // 2) Get the appropriate bucket from the data array field
                List<CustomPair<TKey, TValue>> bucket;

                // If the List of that index has not been made,
                // make it before giving the reference of it to
                // the local bucket variable
                if (data[index] == null)
                {
                    data[index] = new List<CustomPair<TKey, TValue>>();
                }

                // Give the reference of that bucket locally
                bucket = data[index];

                // 3) Add the Key and Value pair to the bucket
                //    we just accessed or initialized.
                bucket.Add(new CustomPair<TKey, TValue>(key, value));

                // 4) Increment the amount of items in the dictionary
                count++;
            }
            // If the key exists in the dictionary
            else
            {
                throw new ArgumentException
                    ("The input you have given as a key is already" +
                     " used as a key in the dictionary.");
            }
        }


        // ** Remove()

        /// <summary>
        /// Return the boolean value whether the key 
        /// was successfully removed from the dictionary
        /// </summary>
        /// <param name="key">Unique Identifier to retrieve a specific value</param>
        /// <returns>A boolean value whether the key was successfully
        /// removed from the dictionary</returns>
        public bool Remove(TKey key)
        {
            // Check whether the data array field contains this key
            if(ContainsKey(key))
            {
                // 1) Get the Hash Code of this specific key
                int index = KeyToIndex(key);

                // 2) Get reference to the appropriate bucket in the array field
                //    (It is ensured that this list is instantiated as the
                //    if conditional above will do that for us)
                List<CustomPair<TKey, TValue>> bucket = data[index];

                // 3) Search through this specific bucket
                //    for the Custom Pair with this Key value
                int targetIndex = -1;

                for(int i = 0; i < bucket.Count; i++)
                {
                    if (key.Equals(bucket[i].Key))
                    {
                        targetIndex = i;
                    }
                }

                // 4) Remove this Custom Pair from the bucket
                if(targetIndex != -1)
                {
                    bucket.RemoveAt(targetIndex);
                    count--;

                    // Success of removing and decrementing
                    // the total number of items in the dictionary
                    return true;
                }
                // If the target index is not updated, it means that the key wasn't
                // in the bucket(which is not a scenario that would happen here)
                else
                {
                    return false;
                }
            }
            // If the key does not exist in this dictionary
            else
            {
                return false;
            }
        }


        // ** Clear()

        /// <summary>
        /// Loop through the data array and replace all lists with null,
        /// then set count to zero
        /// </summary>
        public void Clear()
        {
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = null!;
            }

            // Reset the total items in the dictionary
            count = 0;
        }


        // ***************************************************************
        // Helper Method --> Hash Function
        // [ Keys with Integer ]
        // 1) Simple Mod Function
        // 2) Binning
        // 3) The Mid Square Method
        //    (Makes Sense! ex) 4567 * 4567 = 208'57'489)
        // [ Keys with String ]
        // 4) A simple hash functions for strings (Modulus)
        // 5) String Folding!
        //   (Interprets each of the four-byte chuncks
        //    as a single long integer value.)
        //   (The integer values for the four-byte chunks are added together.)
        //   (The resulting sum is converted to the range 0 to M-1
        //    using the Modulus Operator.)
        // ***************************************************************

        // [ Ideas ]
        // Taking advantage of GetHashCode() method
        // The integer falls inside the range (0, array length - 1)

        // ** KeyToIndex()

        /// <summary>
        /// A Custom method to get a hashcode of a key 
        /// using hash function implementations
        /// [ This Custom Dictionary's GetHashCode method currently works 
        ///   only for an integer key or a string key. Thinking of developing 
        ///   more of the hashfunction methods for different keys in the future. ]
        /// </summary>
        /// <returns>A index of the array of Custom Pair Object List
        /// retrieved using the hash function</returns>
        private int KeyToIndex(TKey key)
        {
            // I tried using if statements to handle
            // different types of keys in scenarios
            // (Definitely need to question this)
            /*
            // 1) If the key is an integer
            if(key is int)
            {
                // Totally does not work XD (Why?)
                return key / data.Length;
            }
            // 2) If the key is a string
            else if(key is string)
            {
                
            }
            */

            // A local index --> Get the hashcode of the key
            //                   using the built-in GetHashCode method
            // (Even though the Built-In GetHashCode is made to return
            //  the same HashCode whenever it is called on the same key,
            //  it might not, which is the risk and the purpose of
            //  developing a stabler hash function)
            // ** Get an absolute value because the
            //    Hashcode returned can also be negative
            int index = Math.Abs(key.GetHashCode());

            // Ensure that it is in the range (0, array.length - 1)
            // --> Mod Function!
            index = index % data.Length;

            // Finally, return the index :)
            return index;
        }

    }

}
