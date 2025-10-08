using System;
using System.Collections.Generic;

/* Amy Lee
 * 04/07/2025
 * Building a tree manually, and recursively traversing them! */

namespace PE_TreeTraversal
{
    /// <summary>
    /// TalentTreeNode Class
    /// Purpose: Holds a data value and 
    ///          references to the left child node and the right child node.
    /// </summary>
    internal class TalentTreeNode
    {
        //---------------------------------------------------------------
        // Fields
        //---------------------------------------------------------------

        // A string field to hold the ability name
        private string ability;

        // A boolean field that indicates whether the player has learned this talent
        private bool hasLearned;

        // A reference to the left child node to this node
        private TalentTreeNode left;

        // A reference to the right child node to this node
        private TalentTreeNode right;


        //---------------------------------------------------------------
        // Properties
        //---------------------------------------------------------------
        public TalentTreeNode Left
        {
            get { return left; }
            set { left = value; }
        }

        public TalentTreeNode Right
        {
            get { return right; }
            set { right = value; }
        }


        //---------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------
        /// <summary>
        /// Sets the initial values for the fields of TalentTreeNode class.
        /// </summary>
        /// <param name="ability">The name of the ability</param>
        /// <param name="hasLearned">Whether the player has learned this talent.</param>
        public TalentTreeNode(string ability, bool hasLearned)
        {
            this.ability = ability;
            this.hasLearned = hasLearned;

            // When initialized,
            // set these to null as they would be GET and SET by properties
            left = null;
            right = null;
        }


        //---------------------------------------------------------------
        // Class Methods (Helpers): Uses Recursion
        //---------------------------------------------------------------

        /// <summary>
        /// Prints All of the abilities in the tree using IN-ORDER traversal
        /// </summary>
        public void ListAllTalents()
        {
            // Call the method on the "L"eft node
            // ** WHEN THE LEFT NODE IS NOT NULL
            if(left != null)
            {
                left.ListAllTalents();
            }

            // Print the "C"urrent node (this)
            Console.WriteLine(this.ability);

            // Call the method on the "R"ight node
            // ** WHEN THE RIGHT NODE IS NOT NULL
            if(right != null)
            {
                right.ListAllTalents();
            }
        }


        /// <summary>
        /// Prints the player's LEARNED abilities
        /// </summary>
        public void ListKnownTalents()
        {
            // Determine whether this node has been learned or not
            if(this.hasLearned)
            {
                // Print the "C"urrent node's learned ability
                Console.WriteLine("Known ability: " + this.ability);

                // Call the method on the child nodes
                // ** WHEN THEY ARE NOT NULL
                if(left != null)
                {
                    left.ListKnownTalents();
                }
                
                if(right != null)
                {
                    right.ListKnownTalents();
                }
            }
            // The base case is handled by not having any code written;
            // if the current node hasn't been learned,
            // the child nodes couldn't have been learned.
        }


        /// <summary>
        /// Prints the abilities that the player COULD LEARN NEXT, 
        /// which are the abilities that have a PARENT that is known.
        /// </summary>
        public void ListPossibleTalents()
        {
            // If this data has been learned,
            // we need to access the child nodes.
            if(this.hasLearned)
            {
                // Call this method to the child nodes.
                // ** WHEN THEY ARE NOT NULL
                if(left != null)
                {
                    left.ListPossibleTalents();
                }
                
                if(right != null)
                {
                    right.ListPossibleTalents();
                }
            }
            // If the data hasn't been learned,
            // this is the talent that could be learnt next
            else
            {
                Console.WriteLine("Possible ability: " + this.ability);
            }
        }
    }
}
