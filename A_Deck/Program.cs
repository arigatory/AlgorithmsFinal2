//65923285

/* 
 * -- ПРИНЦИП РАБОТЫ --
 * Я реализовал Дек, используя кольцевой буфер. В данном случае - это массив целых чисел и двумя индексами,
 * которые используются для определения границ добавленных элементов. При добавлении/удалении элементов, мы просто сдвигаем
 * соответствующие индексы, беря их по модулу максимально возможного размера массива.
 * 
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
 * Из описания алгоритма следует, что за добавлением и удалением элементов следят указатели на начало и конец массива.
 * Дополнительно при добавлении количество текущих элементов проверяется с максимально возможным, а при удалении смотрим, что 
 * массив не пустой.
 * 
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
 * О(1), так как количество элементов не влияет на скорость добавления и удаления. Всегда одно и тоже число операций:
 * сравнение с _maxSize, изменение индекса (перевод)
 * 
 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
 * O(n), мы храним все элементы массива, но не используем дополнительную память
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace A_Deck
{
    public class Program
    {
        public class Solution
        {
            private static TextReader _reader;
            private static TextWriter _writer;

            public static void Main(string[] args)
            {
                InitialiseStreams();

                var total_commands = ReadInt();
                var max_size = ReadInt();

                Deck myDeck = new Deck(max_size);
                for (int i = 0; i < total_commands; i++)
                {
                    var command = ReadList();
                    if (command[0] == "push_back")
                    {
                        int value = int.Parse(command[1]);
                        if (!myDeck.PushBack(value))
                        {
                            _writer.WriteLine("error");
                        }
                    }
                    else if (command[0] == "push_front")
                    {
                        int value = int.Parse(command[1]);
                        if (!myDeck.PushFront(value))
                        {
                            _writer.WriteLine("error");
                        }
                    }
                    else if (command[0] == "pop_front")
                    {
                        int? numberFromDeck = myDeck.PopFront();
                        if (numberFromDeck.HasValue)
                        {
                            _writer.WriteLine(numberFromDeck.Value);
                        }
                        else
                        {
                            _writer.WriteLine("error");
                        }
                    }
                    else if (command[0] == "pop_back")
                    {
                        int? numberFromDeck = myDeck.PopBack();
                        if (numberFromDeck.HasValue)
                        {
                            _writer.WriteLine(numberFromDeck.Value);
                        }
                        else
                        {
                            _writer.WriteLine("error");
                        }
                    }
                }

                CloseStreams();
            }

            private static void CloseStreams()
            {
                _reader.Close();
                _writer.Close();
            }

            private static void InitialiseStreams()
            {
                _reader = new StreamReader(Console.OpenStandardInput());
                _writer = new StreamWriter(Console.OpenStandardOutput());
            }

            private static int ReadInt()
            {
                return int.Parse(_reader.ReadLine());
            }

            private static List<string> ReadList()
            {
                return _reader.ReadLine()
                    .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
            }
        }
    }

    class Deck
    {
        private readonly int _maxSize;
        private int _currentSize;
        private int[] _items;
        private int _headIndex;
        private int _tailIndex;
        private bool _empty
        {
            get
            {
                return _currentSize == 0;
            }
        }

        public Deck(int maxSize)
        {
            _maxSize = maxSize;
            _tailIndex = 0;
            _headIndex = 0;
            _currentSize = 0;
            _items = new int[_maxSize];
        }

        public bool PushBack(int value)
        {
            if (_currentSize != _maxSize)
            {
                _items[_tailIndex] = value;
                _tailIndex = (_tailIndex + 1) % _maxSize;
                _currentSize++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PushFront(int value)
        {
            if (_currentSize != _maxSize)
            {
                _headIndex--;
                if (_headIndex < 0)
                {
                    _headIndex += _maxSize;
                }
                _items[_headIndex] = value;
                _currentSize++;
                return true;
            }
            return false;
        }

        public int? PopBack()
        {
            if (_empty)
            {
                return null;
            }
            _tailIndex--;
            if (_tailIndex < 0)
            {
                _tailIndex += _maxSize;
            }
            var result = _items[_tailIndex];
            _currentSize--;
            return result;
        }

        public int? PopFront()
        {
            if (_empty)
            {
                return null;
            }
            var result = _items[_headIndex];
            _headIndex = (_headIndex + 1) % _maxSize;
            _currentSize--;
            return result;
        }
    }
}


