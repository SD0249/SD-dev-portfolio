
/* Amy Lee
 * 04/07/2025
 * Building a tree manually, and recursively traversing them! */

namespace PE_TreeTraversal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //************************************************************
            // Creating the talent tree manually.
            //************************************************************

            // A) Making 10 TalentTreeNodes.
            TalentTreeNode magic = new TalentTreeNode("Magic", true);
            TalentTreeNode fireBall = new TalentTreeNode("Fireball", true);
            TalentTreeNode magicArrow = new TalentTreeNode("Magic Arrow", true);
            TalentTreeNode crazyBigFireBall = new TalentTreeNode("Crazy Big Fireball", false);
            TalentTreeNode thousandTinyFireBalls = new TalentTreeNode("1000 Tiny Fireballs", true);
            TalentTreeNode tinyBurstingFireBalls = new TalentTreeNode("Tiny Bursting Fireballs", true);
            TalentTreeNode tinyBouncingFireBalls = new TalentTreeNode("Tiny Bouncing Fireballs", false);
            TalentTreeNode iceArrow = new TalentTreeNode("Ice Arrow", false);
            TalentTreeNode explodingArrow = new TalentTreeNode("Exploding Arrow", false);
            TalentTreeNode freezingHeartArrow = new TalentTreeNode("Freezing Heart Arrow", false);

            // B) Connecting the 10 TalentTreeNodes together.
            magic.Left = fireBall;
            magic.Right = magicArrow;

            fireBall.Left = crazyBigFireBall;
            fireBall.Right = thousandTinyFireBalls;

            thousandTinyFireBalls.Left = tinyBurstingFireBalls;
            thousandTinyFireBalls.Right = tinyBouncingFireBalls;

            magicArrow.Left = iceArrow;
            magicArrow.Right = explodingArrow;

            iceArrow.Right = freezingHeartArrow;


            //************************************************************
            // TESTING the RECURSIVE methods in the TalentTreeNode class
            //************************************************************

            Console.WriteLine("--- Listing all abilities in the game ---\n");
            magic.ListAllTalents();
            Console.WriteLine();

            Console.WriteLine("--- Listing all my known abilities ---\n");
            magic.ListKnownTalents();
            Console.WriteLine();

            Console.WriteLine("--- Listing all abilities I could learn next ---\n");
            magic.ListPossibleTalents();
            Console.WriteLine();
        }
    }
}
