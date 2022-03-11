//65951608

/* 
 * -- ПРИНЦИП РАБОТЫ --
 * Я реализовал Калькулятор, используя кольцевой стек. В данном случае мой самописный, а не из готовой библиотеки,
 * так как там он более навороченный и может быть не такой эффективный, как я хочу. Да и я не могу никак контролировать его 
 * эффективность.
 * Проходим по числам, если встречаем число, то кладем его в стек, иначе извлекаем и стека 2 числа и проводим ту операцию, которая 
 * нам попалась. Если все хорошо, то в стеке останется только одно число - ответ.
 * 
 * 
 * 
 * -- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
 * Из описания алгоритма следует, что мы проходим все числа и все операции, поэтому ничего не пропускаем. Корректность вычисления следует
 * из того, что действия выполняются по порядку и ровно те, которые нам заданы во входящей строке.
 * 
 * -- ВРЕМЕННАЯ СЛОЖНОСТЬ --
 * О(n), так как количество элементов n и нам нужно сделать C*n действий при обработке каждого элемента.
 * 
 * -- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
 * O(n), так как в худшем случае мы храним все числа в стеке, это (n-1)/2 + 1. 
 * В исходной строке всего n=2k+1 элементов, где k операций и k+1 операндов.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace B_Calculator
{
    public class Solution
    {
        private static TextReader _reader;
        private static TextWriter _writer;

        public static void Main(string[] args)
        {
            InitialiseStreams();

            var inputItems = ReadList();

            int result = Solve(inputItems);

            PrintResult(result);

            CloseStreams();
        }

        private static void PrintResult(int result)
        {
            _writer.WriteLine(result);
        }

        private static int Solve(List<string> inputItems)
        {
            var myStack = new MyStack();
            int n = inputItems.Count;
            int number;
            for (int i = 0; i < n; i++)
            {
                if (int.TryParse(inputItems[i], out number))
                {
                    myStack.Push(number);
                }
                else
                {
                    var op2 = myStack.Pop();
                    var op1 = myStack.Pop();

                    if (inputItems[i] == "+")
                    {
                        myStack.Push(op1 + op2);
                    }
                    else if (inputItems[i] == "-")
                    {
                        myStack.Push(op1 - op2);
                    }
                    else if (inputItems[i] == "*")
                    {
                        myStack.Push(op1 * op2);
                    }
                    else if (inputItems[i] == "/")
                    {
                        if (op1 >= 0)
                        {
                            myStack.Push(op1 / op2);
                        }
                        else
                        {
                            myStack.Push((op1 - op2 + 1) / op2);
                        }
                    }
                }
            }

            return myStack.Pop();
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

    public class MyStack
    {
        Node _start = null;

        public int Pop()
        {
            if (_start == null)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                var result = _start.Value;
                _start = _start.Previous;
                return result;
            }
        }

        public void Push(int value)
        {
            if (_start == null)
            {
                _start = new Node(value, null);
            }
            else
            {
                var _newNode = new Node(value, _start);
                _start = _newNode;
            }
        }

        private class Node
        {
            public int Value { get; set; }
            public Node Previous { get; set; }

            public Node(int value, Node previous)
            {
                Value = value;
                Previous = previous;
            }

        }

    }
}
