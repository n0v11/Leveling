using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Rand
    {
        private static Random Rnd = new Random();
        public static int damage(int a, int b) // Генерируем случайный урон в заданном диапазоне
        {
            return Rnd.Next(a, b);
        }
    }
}
