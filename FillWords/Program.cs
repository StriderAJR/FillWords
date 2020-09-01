using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillWords
{
    class Program
    {
        public class Field
        {
            char[,] field =
            {
                { 'ч', 'о', 'б', 'р', 'а', 'з' },
                { 'а', 'с', 'в', 'о', 'д', 'я' },
                { 'е', 'в', 'а', 'р', 'а', 'и' },
                { 'и', 'л', 'и', 'п', 'у', 'ц' },
                { 'н', 'е', 'н', 'р', 'м', 'а' },
                { 'с', 'у', 'ф', 'о', 'г', 'ю' }
            };

            private int hoverX;
            private int hoverY;
            private int margin = 5;
            private int fieldWidth = 6;
            private int fieldHeight = 6;
            private int cellSizeY = 3;
            private int cellSizeX = 5;

            private CellState[,] cellStates;

            enum CellState
            {
                None, Selected, Hover
            }

            private class WordWithCoords
            {
                public string Word { get; set; }
                public int[][] Coords { get; set; }
            }

            private List<WordWithCoords> words;

            public Field()
            {
                CellState[,] selected = new CellState[fieldWidth,fieldHeight];
                words = new List<WordWithCoords>();
                words.Add(new WordWithCoords
                {
                    Word = "час",
                    Coords = new[] { new[] { 0, 0 }, new[] { 1, 0 }, new[] { 1, 1 } }
                });
                words.Add(new WordWithCoords
                {
                    Word = "образ",
                    Coords = new[] { new[] { 0, 0 }, new[] { 0, 1 }, new[] { 1, 2 }, new[] { 1, 3 }, new[] { 1, 4 }, new[] { 1, 5 } }
                });
            }

            public void SelectCell(int x, int y)
            {
                cellStates[x, y] = CellState.Selected;
            }

            public void ResetSelection()
            {
                for(int i = 0; i < 6; i++)
                for(int j = 0; j < 6; j++)
                {
                    cellStates[i,j] = CellState.None;
                }
            }

            public void SetHover(int deltaX, int deltaY)
            {
                if(hoverX + deltaX < fieldWidth && hoverX + deltaX >= 0)  hoverX += deltaX;
                if(hoverY + deltaY < fieldHeight && hoverY + deltaY >= 0) hoverY += deltaY;
            }

            public void PrintBorder()
            {
                Console.SetCursorPosition(margin, margin);

                int maxX = fieldWidth * cellSizeX - fieldWidth;
                int maxY = fieldHeight * cellSizeY - fieldHeight;

                for (int y = 0; y <= maxY; y++)
                {
                    bool isFirstRow = y == 0;
                    bool isLastRow = y == maxY;
                    bool isBorderHorizontal = y % (cellSizeY - 1) == 0;

                    for (int x = 0; x <= maxX; x++)
                    {
                        Console.SetCursorPosition(margin + x, margin + y);
                        
                        int hoverXStart = hoverX * (cellSizeX - 1) + 1;
                        int hoverXEnd = hoverX * (cellSizeX - 1) + cellSizeX - 2;
                        int hoverYStart = hoverY * (cellSizeY - 1) + 1;
                        int hoverYEnd = hoverY * (cellSizeY - 1) + cellSizeY - 2;

                        if (x >= hoverXStart && x <= hoverXEnd && y >= hoverYStart && y <= hoverYEnd)
                            Console.BackgroundColor = ConsoleColor.Red;

                        bool isFirstColumn = x == 0;
                        bool isLastColumn = x == maxX;
                        bool isBorderVertical = x % (cellSizeX - 1) == 0;
                        bool isBorderCross = isBorderHorizontal && isBorderVertical;

                        if (isBorderCross)
                        {
                            if (isFirstColumn && isFirstRow)
                                Console.Write("┌");
                            else if(isFirstRow && !isFirstColumn && !isLastColumn)
                                Console.Write("┬");
                            else if (isFirstRow && isLastColumn)
                                Console.Write("┐");
                            else if(isFirstColumn && !isFirstRow && !isLastRow)
                                Console.Write("├");
                            else if(!isFirstRow && !isLastRow && !isFirstColumn && !isLastColumn)
                                Console.Write("┼");
                            else if(isLastColumn && !isFirstRow && !isLastRow)
                                Console.Write("┤");
                            else if (isLastRow && isFirstColumn)
                                Console.Write("└");
                            else if(isLastRow && !isFirstColumn && !isLastColumn)
                                Console.Write("┴");
                            else if (isLastColumn && isLastRow)
                                Console.Write("┘");
                        }
                        else
                        {
                            if(isBorderVertical)        Console.Write("│");
                            else if(isBorderHorizontal) Console.Write("─");
                            else                        Console.Write(" ");
                        }

                        Console.ResetColor();
                    }
                }
            }

            private void PrintLetters()
            {
                int xStep = cellSizeX - 1;
                int yStep = cellSizeY - 1;
                for (int currentY = 0; currentY < fieldHeight; currentY++)
                for (int currentX = 0; currentX < fieldWidth; currentX++)
                {
                    int x, y;
                    if(currentX == 0) x = margin + cellSizeX / 2;
                    else              x = margin + currentX * xStep + xStep - 2;

                    if(currentY == 0) y = margin + cellSizeY / 2;
                    else              y = margin + currentY * yStep + yStep - 1;

                    if(currentX == hoverX && currentY == hoverY)
                        Console.BackgroundColor = ConsoleColor.Red;

                    Console.SetCursorPosition(x, y);
                    Console.Write(field[currentY, currentX]);
                    Console.ResetColor();
                }

            }

            public void Print()
            {
                PrintBorder();
                PrintLetters();
                Console.SetCursorPosition(0, 0);

                SetHover(0, 0);
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Field field = new Field();

            ConsoleKeyInfo cki;
            do {
                field.Print();

                cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.DownArrow)  field.SetHover(0, 1);
                if (cki.Key == ConsoleKey.UpArrow)    field.SetHover(0, -1);
                if (cki.Key == ConsoleKey.RightArrow) field.SetHover(1, 0);
                if (cki.Key == ConsoleKey.LeftArrow)  field.SetHover(-1, 0);
            } while (true);
        }
    }
}
