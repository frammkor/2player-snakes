using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Lab_4___Snake
{
  public class Board
    {
        public GameConsole GameDisplay;
        private int _width;
        private int _height;
        public List<List<Cell>> BoardMatrix { get; } // SAME AS public List<List<Cell>> BoardMatrix => _boardMatrix;
        public List<Snake> Players { get; set; }
        public Board(int width, int height)
        {
            this.GameDisplay = new GameConsole(this);
            this._width = width;
            this._height = height;
            this.BoardMatrix = GenerateBoardMatrix(width, height);
        }

        // return the board matrix filled with free cells and border cells ond the edges
        private List<List<Cell>> GenerateBoardMatrix(int width, int height)
        {
            List<List<Cell>> BoardMatrix = new List<List<Cell>>();
            for (int y = 0; y < height; y++)
            {
                List<Cell> matrixRow = new List<Cell> {};
                for (int x = 0; x < width; x++)
                {
                    if(y == 0 || y == this._height - 1 || x == this._width - 1 || x == 0)
                    {
                        matrixRow.Add(new Cell(x, y, CellStatus.IsOutOfBound));
                    } else {
                        matrixRow.Add(new Cell(x, y));
                    }
                }
                BoardMatrix.Add(matrixRow);
            }

            return BoardMatrix;
        }

        public static void Test()
        {
            Board TestBoard = new Board(5, 4);
            // text board matrix
            Debug.Assert(TestBoard.BoardMatrix.Count == 4);
            Debug.Assert(TestBoard.BoardMatrix.Count != 2);
            Debug.Assert(TestBoard.BoardMatrix[0].Count == 5);
            Debug.Assert(TestBoard.BoardMatrix[0].Count != 7);

            Snake Player1 = new Snake("Player 1", ChoosableColors.Magenta, TestBoard.GetRandomFeeCell(), Directions.up, GlobalVariables.palyer1controls);
            Snake Player2 = new Snake("Player 2", ChoosableColors.Cyan, TestBoard.GetRandomFeeCell(), Directions.up, GlobalVariables.palyer2controls);
            List<Snake> TestPlayers = new List<Snake>() {Player1, Player2};
            TestBoard.Players = TestPlayers;

            // test snakes move method
            Debug.Assert(Player1.MoveForward(TestBoard).GetType() == MoveOutcome.WasFree.GetType());

            TestBoard.Start();
        }

        // return a random free cell
        public Cell GetRandomFeeCell() // make private
        {
            while (true)
            {
                int randomYPostion = new Random().Next(1, this._height - 1);
                int randomXPostion = new Random().Next(1, this._width - 1);
                Cell randomCell = this.BoardMatrix[randomYPostion][randomXPostion];
                if (randomCell.Status == CellStatus.IsFree)
                {
                    return randomCell;
                }
            }
        }

        private ConsoleKey ListenToUserInput()
        {
            return Console.ReadKey(true).Key;
        }

        // runs a turn in which each snake will be move
        private void RunTurn()
        {
            System.Threading.Thread.Sleep(200);

            foreach (var snake in this.Players)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey userInput = this.ListenToUserInput();
                    snake.ChangeCurrentDirectionByConsoleKey(userInput); 
                }
                MoveOutcome moveOutcome = snake.MoveForward(this);
                switch (moveOutcome)
                {
                    case MoveOutcome.HadApple:
                        this.PlaceApple();
                        break;
                    case MoveOutcome.Failure:
                        Players.Remove(snake);
                        return; // need to return to avoid exemption: "Collection was modified; enumeration operation may not execute."
                    default:
                        continue;
                }

            }
        }

        // place an apple on the board!!!
        private void PlaceApple()
        {
            Cell newAppleCell = GetRandomFeeCell();
            newAppleCell.Status = CellStatus.HasApple;
            this.GameDisplay.UpdateCell(newAppleCell, ChoosableColors.Red);
        }

        // plays the game until only one player is left
        public void Start()
        {
            this.GameDisplay.PrintGame();
            this.PlaceApple();
            while (this.Players.Count > 1)
            {
                RunTurn();
            }

            this.GameDisplay.GameOver();
        }
    }
}
