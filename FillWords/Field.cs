using System.Collections.Generic;

namespace FillWords
{
    public class Field
    {
        private char[,] _field;
        private readonly int _fieldWidth;
        private readonly int _fieldHeight;

        private class WordWithCoords
        {
            public string Word { get; set; }
            public int[][] Coords { get; set; }
        }

        private List<WordWithCoords> _words;

        public Field(int fieldWidth, int fieldHeight)
        {
            _fieldWidth = fieldWidth;
            _fieldHeight = fieldHeight;
            GenerateField();
        }

        private void GenerateField()
        {
            _field = new[,]
            {
                {'ч', 'о', 'б', 'р', 'а', 'з'},
                {'а', 'с', 'в', 'о', 'д', 'я'},
                {'е', 'в', 'а', 'р', 'а', 'и'},
                {'и', 'л', 'и', 'п', 'у', 'ц'},
                {'н', 'е', 'н', 'р', 'м', 'а'},
                {'с', 'у', 'ф', 'о', 'г', 'ю'}
            };

            _words = new List<WordWithCoords>();
            _words.Add(new WordWithCoords
            {
                Word = "час",
                Coords = new[] {new[] {0, 0}, new[] {1, 0}, new[] {1, 1}}
            });
            _words.Add(new WordWithCoords
            {
                Word = "образ",
                Coords = new[] {new[] {0, 0}, new[] {0, 1}, new[] {1, 2}, new[] {1, 3}, new[] {1, 4}, new[] {1, 5}}
            });
        }

        public char GetLetter(int x, int y)
        {
            return _field[y, x];
        }

        public int GetFieldWidth()
        {
            return _fieldWidth;
        }

        public int GetFieldHeight()
        {
            return _fieldHeight;
        }

        public bool CheckWord(string word, List<int[]> coords, out string message)
        {
            message = "All is good";
            return true;
        }
    }
}