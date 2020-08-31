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

            private CellState[,] selected;

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
                selected[x, y] = CellState.Selected;
            }

            public void ResetSelection()
            {
                for(int i = 0; i < 6; i++)
                for(int j = 0; j < 6; j++)
                {
                    selected[i,j] = CellState.None;
                }
            }

            public void SetHover(int newX, int newY)
            {
                hoverX = newX;
                hoverY = newY;
            }

            public void PrintBorder()
            {
                int x = margin;
                int y = margin;
                Console.SetCursorPosition(x, y);

                int maxI = fieldHeight * cellSizeX - fieldHeight;
                int maxJ = fieldWidth * cellSizeY - fieldWidth;

                for (int i = 0; i <= maxI; i++)
                {
                    bool isFirstRowVertical = i == 0;
                    bool isLastRowVertical = i == maxI;
                    bool isBorderVertical = i % (cellSizeX - 1) == 0;

                    for (int j = 0; j <= maxJ; j++)
                    {
                        Console.SetCursorPosition(margin + i, margin + j);

                        bool isFirstRowHorizontal = j == 0;
                        bool isLastRowHorizontal = j == maxJ;
                        bool isBorderHorizontal = j % (cellSizeY - 1) == 0;
                        bool isBorderCross = isBorderHorizontal && isBorderVertical;

                        if (isBorderCross)
                        {
                            if (isFirstRowHorizontal && isFirstRowVertical)
                                Console.Write("┌");
                            else if(isFirstRowHorizontal && !isFirstRowVertical && !isLastRowVertical)
                                Console.Write("┬");
                            else if (isFirstRowHorizontal && isLastRowVertical)
                                Console.Write("┐");
                            else if(isFirstRowVertical && !isFirstRowHorizontal && !isLastRowHorizontal)
                                Console.Write("├");
                            else if(!isFirstRowVertical && !isLastRowVertical && !isFirstRowHorizontal && !isLastRowHorizontal)
                                Console.Write("┼");
                            else if(!isFirstRowHorizontal && !isLastRowHorizontal && isLastRowVertical)
                                Console.Write("┤");
                            else if (isLastRowHorizontal && isFirstRowVertical)
                                Console.Write("└");
                            else if(isLastRowHorizontal && !isFirstRowVertical && !isLastRowVertical)
                                Console.Write("┴");
                            else if (isLastRowHorizontal && isLastRowVertical)
                                Console.Write("┘");
                        }
                        else
                        {
                            if(isBorderVertical)
                                Console.Write("│");
                            else if(isBorderHorizontal)
                                Console.Write("─");
                        }
                    }
                }
            }

            private void PrintLetters()
            {
                int xStep = (cellSizeX - 1);
                int yStep = (cellSizeY - 1);
                for (int i = 0; i < fieldHeight; i++)
                for (int j = 0; j < fieldWidth; j++)
                {
                    int x, y;
                    if(i == 0)
                        x = margin + cellSizeX / 2;
                    else 
                        x = margin + i * xStep + xStep - 2;
                    if(j == 0)
                        y = margin + cellSizeY / 2;
                    else
                        y = margin + j * yStep + yStep - 1;

                    Console.SetCursorPosition(x, y);
                    if (y == 6) Console.BackgroundColor = ConsoleColor.Green;
                    else if (y == 8) Console.BackgroundColor = ConsoleColor.Blue;
                    else Console.ResetColor();
                    Console.Write(field[j, i]);
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
            Field field = new Field();
            field.Print();
        }
    }
}
