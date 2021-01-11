using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Rand
    {
        private static Random Rng = new Random();
        public static int damage(int a, int b) // Генерируем случайный урон в заданном диапазоне
        {
            return Rng.Next(a, b);
        }
    }
}
