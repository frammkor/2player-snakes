/* 
Franco Cespi
CS-1415 Lab 4: Snake
Started: 9/22/2021
The Traditional Snake Game but for two players!
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab_4___Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test();
            Play();
        }

        static void Test()
        {
            Console.WriteLine("Starting Test");
            Snake.Test();
            Board.Test();
            Console.WriteLine("Ending Test");
        }

        static void Play()
        {
            Console.WriteLine("\n=== Welcome to 'The Snakes' ===");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Swing around, eat the food, avoid the rest!\n"); 
            Console.WriteLine("\n\n=== Press ANY key to start palying ===\n"); 

            while (Console.KeyAvailable == false)
               Thread.Sleep(400); // Loop until input is entered.

            Console.Clear();

            Board GameBoard = new Board(70, 30);
            Snake Player1 = new Snake("Player 1", ChoosableColors.Magenta, GameBoard.GetRandomFeeCell(), Directions.up, GlobalVariables.palyer1controls);
            Snake Player2 = new Snake("Player 2", ChoosableColors.Cyan, GameBoard.GetRandomFeeCell(), Directions.up, GlobalVariables.palyer2controls);
            List<Snake> TestPlayers = new List<Snake>() {Player1, Player2};
            GameBoard.Players = TestPlayers;
            GameBoard.Start();
        }
    }
}
