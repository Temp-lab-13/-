﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_3_Collection
{
    // тут много коментариев, в первую очередь для меня самого. Извиняюсь.
    internal class HomeWorkSeminar3
    { 
        public static void Labirint(int y, int x) // При вызове отправляем стартовые координаты.
        {
            int[,] lab13 = new int[,] //Задём игровое поле двумерным массивом.
            {
                { 1, 1, 1, 1, 2, 1, 1 },
                { 1, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 1, 1, 1, 0, 1 },
                { 2, 0, 0, 0, 1, 0, 2 },
                { 1, 1, 0, 0, 1, 1, 1 },
                { 1, 1, 1, 2, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1 }
            };

            //FindPath(y, x, lab13); // Метод поиска выхода из лаберинта. Передаём полученные стартовые координаты и сам лабиринт.
            List<string> list = FindPath(y, x, lab13); // Используем лист стрингов для получения результатов поиска. Возможно, не лучший вариант, но мне просто было так удобно.
            
            foreach (var item in list) // И простым форычем всё выводим на экран. Что бы всё было красиво.
            {
                Console.WriteLine(item);
            }
        }

        static List<string> FindPath(int i, int j, int[,] lab13)
        {
            Stack<Tuple<int, int>> path = new Stack<Tuple<int, int>>(); // стэк хранения путей.
            List<string> resalt = new List<string>(); // Сюда будем добавлять результаты. Как альтернатива. Можно было бы запариться со стрингБилдером.
            int exit = 0; // Счётчик выходов.

            if (lab13[i, j] == 1) // Проверка сnартовой локации.
            {
                resalt.Add("Вы заспавнились в стене и умерли.");
                return resalt;
            }

            path.Push(new(i, j)); // Закидываем стартовую локации в стэк пути.

            while (path.Count > 0) // Обходим лабиринт.
            {
                var current = path.Pop(); // Выкидываем из стэка "текущее положение" в промежуточную переменную, которую используем для проверки имеющихся ходов вокруг.
                
                if (lab13[current.Item1, current.Item2] == 2) // Проверка текущего местоположения на соответсвие критерию выхода.
                {
                    exit++; // Если выход найден, то увеличиваем счётчик.
                    resalt.Add($"Выход найден по координатам: x = {current.Item2 + 1} y = {current.Item1 + 1};\n   Колличество досупных выходов: {exit} "); //Добавляем найденный результат. +1 нужен, что бы счёт для пользователя был с 1 а не с 0. Чисчто декоративный момент, в котором есть смыл, только если потом эти значения не нужны для работы с массивом лабиринта. 
                } // Сохраняем в листе отдельным элементом вот такую сложную строку с вкраплением номеров позиции выхода, и его нумерования.
                  // Таким образом каждый выход получает свой номер, а последний ещё и указывает их общее колличество.
                  // Ретёрн мы сдесь убираем, что бы цикл прошёлся по всему лабиринту.

                lab13[current.Item1, current.Item2] = 1; // Меняем текущие положение в лабиринте на 1, тем самы отечая пройденный путь, что бы не зациклиться.

                if (current.Item1 + 1 < lab13.GetLength(0) && lab13[current.Item1 + 1, current.Item2] != 1) // Провереям, что при апросе следующего элемента не выйдем за приделы массива, 
                {                                                                                           // а потом проверяем, это элемент на наличие стены.
                    path.Push(new(current.Item1 + 1, current.Item2));                                       // если стены нет и элемент существует, добавляем в стэк возможных путей.
                }                                                                                           // при следующих итерациях записанный путь-элемент проверится на соответсвие критерия выхода.

                if (current.Item2 + 1 < lab13.GetLength(1) && lab13[current.Item1, current.Item2 + 1] != 1)
                {
                    path.Push(new(current.Item1, current.Item2 + 1));
                }

                if (current.Item1 > 0 && lab13[current.Item1 - 1, current.Item2] != 1)
                {
                    path.Push(new(current.Item1 - 1, current.Item2));
                }

                if (current.Item2 > 0 && lab13[current.Item1, current.Item2 - 1] != 1)
                {
                    path.Push(new(current.Item1, current.Item2 - 1));
                }
            }

            if (resalt.Count > 0) // Если найденных выходов больше нуля, то возращаем все найденные выходы.
            {
                return resalt;
            }
            else // В противном случаи, мы ничего не нашли.
            {
                resalt.Add("Выход не найден, вы обречены на медлунню и мучительную смерть в полном одиночестве.");
                return resalt;
            }
        }
    }
}
