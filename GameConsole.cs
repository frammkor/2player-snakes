using System;

namespace Lab_4___Snake
{
    public class GameConsole
    {
        private Board _board;
        public GameConsole(Board theBoard)
        {
            this._board = theBoard;
        }
        public void PrintGame()
        {
            Console.ResetColor();
            this.PrintBoard();
            this.PrintPlayersControlls();
        }

        public void UpdateCell(Cell cell, ChoosableColors color = ChoosableColors.White)
        {
            Console.ForegroundColor = (ConsoleColor)color;
            Console.SetCursorPosition(cell.X, cell.Y);
            Console.Write(GetCharByCellStatus(cell));
        }

        private void PrintBoard()
        {
            foreach (var row in this._board.BoardMatrix)
            {
                foreach (var cell in row)
                {
                    Console.Write(GetCharByCellStatus(cell));
                }
                Console.Write("\n");
            }
        }

        private Char GetCharByCellStatus(Cell cell)
        {
            char printChar;
            switch (cell.Status)
            {
                case CellStatus.HasApple:
                    printChar = '*';
                    break;
                case CellStatus.HasSnake:
                    printChar = '8';
                    break;
                case CellStatus.IsOutOfBound:
                    printChar = '#';
                    break;
                default:
                    printChar = ' ';
                    break;
            }
            return printChar;
        }

        private void PrintPlayersControlls()
        {
            var (left, top) = Console.GetCursorPosition();
            Console.ForegroundColor = (ConsoleColor)this._board.Players[0].PlayerColor;
            Console.WriteLine($"Player 1: UP: {GlobalVariables.palyer1controls[0]}  RIGHT: {1}  UP: {2}  RIGHT: {3}\n", GlobalVariables.palyer1controls[0], GlobalVariables.palyer1controls[1], GlobalVariables.palyer1controls[2], GlobalVariables.palyer1controls[3]);
            Console.ForegroundColor = (ConsoleColor)this._board.Players[1].PlayerColor;
            Console.WriteLine($"Player 2: UP: {0}  RIGHT: {1}  UP: {2}  RIGHT: {3}\n", GlobalVariables.palyer2controls[0], GlobalVariables.palyer2controls[1], GlobalVariables.palyer2controls[2], GlobalVariables.palyer2controls[3]);
            Console.SetCursorPosition(left, top);
        }

        public void GameOver()
        {
            Console.Clear();
            
            Console.ForegroundColor = (ConsoleColor)this._board.Players[0].PlayerColor;
            Console.WriteLine($"\nWinner: {this._board.Players[0].PlayerName}!!!");
        }
    }
}