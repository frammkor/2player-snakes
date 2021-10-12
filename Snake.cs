using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Lab_4___Snake
{
    public class Snake
    {
        public string PlayerName { get; }
        public ChoosableColors PlayerColor { get; }
        public Directions CurrentDirection { get; set; }
        public ConsoleKey[] Controlls { get; }
        private List<Cell> _occupiedCells;
        
        public Snake(string playerName, ChoosableColors color, Cell initialCell, Directions initialDirection, ConsoleKey[] controlls)
        {
            this.PlayerName = playerName;
            this.PlayerColor = color;
            // modify initial cell to be occupied
            initialCell.Status = CellStatus.HasSnake;
            this._occupiedCells = new List<Cell>() {initialCell};
            this.CurrentDirection = initialDirection;
            this.Controlls = controlls;
        }

        public static void Test()
        {
            Cell testCell = new Cell(0, 0, CellStatus.HasSnake);
            Snake testSnake = new Snake("ITest", ChoosableColors.Cyan, testCell, Directions.up, GlobalVariables.palyer1controls);

            // test player initial values
            Debug.Assert(testSnake.PlayerName == "ITest");
            Debug.Assert(testSnake._occupiedCells.Count == 1);

            // test ChangeCurrentDirectionByConsoleKey
            testSnake.ChangeCurrentDirectionByConsoleKey((ConsoleKey)68); // D
            Debug.Assert(testSnake.CurrentDirection == Directions.right);
            Debug.Assert(!(testSnake.CurrentDirection == Directions.up));
        }

        // change the snake direcition based on the user input (if valid control)
        public void ChangeCurrentDirectionByConsoleKey(ConsoleKey userInput)
        {
            int userInputIsValidControl = Array.IndexOf(this.Controlls, userInput);
            if (userInputIsValidControl > -1)
            {
                Directions newDirection = (Directions)userInputIsValidControl;
                this.CurrentDirection = newDirection;
            }
        }

        /*
        Based on the head postion, the current direction and the CellStatus
        it will update the head, the tail and CellStatus
        and return a MoveOutcome
        */
        public MoveOutcome MoveForward(Board currentBoard)
        {
            Cell nextCell;
            Cell headCell = this._occupiedCells[_occupiedCells.Count - 1];
            switch (this.CurrentDirection)
            {
                case Directions.up:
                    nextCell = currentBoard.BoardMatrix[headCell.Y - 1][headCell.X];
                    break;
                case Directions.right:
                    nextCell = currentBoard.BoardMatrix[headCell.Y][headCell.X + 1];
                    break;
                case Directions.down:
                    nextCell = currentBoard.BoardMatrix[headCell.Y + 1][headCell.X];
                    break;
                case Directions.left:
                    nextCell = currentBoard.BoardMatrix[headCell.Y][headCell.X - 1];
                    break;
                default:
                    // by default it moves to the right
                    nextCell = currentBoard.BoardMatrix[headCell.Y][headCell.X - 1];
                    break;
            }

            MoveOutcome movementWas;
            switch (nextCell.Status)
            {
                case CellStatus.HasApple:
                    // update head and cell status
                    nextCell.Status = CellStatus.HasSnake;
                    this._occupiedCells.Add(nextCell);
                    movementWas = MoveOutcome.HadApple;
                    break;
                case CellStatus.IsFree:
                    // update head, tail, and cells status
                    this._occupiedCells[0].Status = CellStatus.IsFree; // update tail
                    currentBoard.GameDisplay.UpdateCell(this._occupiedCells[0]); // update display
                    this._occupiedCells.RemoveAt(0); // remove tail
                    nextCell.Status = CellStatus.HasSnake;
                    this._occupiedCells.Add(nextCell);
                    movementWas = MoveOutcome.WasFree;
                    break;
                default:
                    movementWas = MoveOutcome.Failure;
                    break;
            }
            currentBoard.GameDisplay.UpdateCell(nextCell, this.PlayerColor);
            return movementWas;
        }
    }
}
