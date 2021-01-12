﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Actions
    {
        private bool LightningChoosed = false; // Проверка выбор действия "Удар молнии"
        private bool HealChoosed = false; // Проверка выбор действия "Использование Зелья"

        public int ChooseHero(Hero[] heroes)
        {
            Console.WriteLine("Выберите класс своего персонажа\n");
            for (int i = 0; i < heroes.Length; i++)
            {
                //Console.Clear();
                Console.WriteLine(i + 1 + ": Класс {0} - Здоровье: {1}, Атака: {2}, Зелий лечения: {3}, Усилений {4}\n", heroes[i].HeroClass, heroes[i].Health, heroes[i].Damage, heroes[i].Flask, heroes[i].Lightnings);
            }
            int Choose = CheckHeroNumber();
            Console.Clear();
            Console.WriteLine("Вы выбрали {0}\n", heroes[Choose].HeroClass);
            return Choose;
        } // Выбор класс игрока

        private int CheckHeroNumber()
        {
            string Choose;
            int Res;
            while (true)
            {
                Choose = Console.ReadLine();
                if (Choose.Length > 0)
                {
                    if (Choose[0] == '1' || Choose[0] == '2' || Choose[0] == '3')
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Вы неверно выбрали класс персонажа. Попробуйте еще раз");
                    }
                }
                else
                {
                    Console.WriteLine("Вы неверно выбрали класс персонажа. Попробуйте еще раз");
                }
            }
            Res = Convert.ToInt32(Choose);
            Res--;
            return Res;
        } // Проверка ввода выбора героя

        public string ChooseName(Hero hero)
        {
            Console.WriteLine("Введите имя своего героя\n");
            int i = 0;
            while (true)
            {
                hero.Name = Console.ReadLine();
                if (hero.Name == "" && i < 2)
                {
                    Console.WriteLine("Вы не ввели имя\nВведите имя");
                    i++;
                }
                else if (hero.Name == "" && i >= 2)
                {
                    Console.WriteLine("Ты тупой? Введи имя!");
                }
                else
                {
                    break;
                }
            }
            return hero.Name;
        } // Проверка имени на условия

        public void Initial(Hero hero, Entity mob)
        {
            Console.Clear();
            Console.WriteLine("Герой {0}, Здоровье {1}, Зелий лечения {2}, Ударов молнией {3}", hero.Name, hero.Health, hero.Flask, hero.Lightnings);
            Console.WriteLine("Враг {0}, Здоровье {1}.", mob.Name, mob.Health);
            Actions.Action();
        } // Вывод статов персонажей

        public void Fight(Hero hero, Entity mob) // Бой, проверерка необходимости использования действий
        {
            hero.Health -= mob.Damage;
            mob.Health -= hero.Damage;
            if (HealChoosed == true)
            {
                hero.Heal();
                HealChoosed = false;
            }
            if (LightningChoosed == true)
            {
                hero.LightningShot(hero, mob);
                LightningChoosed = false;
            }

            mob.AiAction(hero);
        }

        public void Choose(Hero hero, Entity mob)
        {
            Console.WriteLine("\nДля использования зелья лечения нажмите 'B'\nДля усиления следующего удара молнией нажмите 'F'\nДля продолжения нажмите любую другую клавишу");
            string key = Console.ReadLine(); // Ждем нажатия клавишы действия
            if (key == "B")
            {
                HealChoosed = true;
            }
            if (key == "F")
            {
                LightningChoosed = true;
            }
        } // Воможность выбрать какое-либо действие

        public void Final(Hero hero, Entity mob, int lenght, int i)
        {
            if (mob.Health <= 0 && hero.Health > 0)
            {
                Console.WriteLine("\nВ {0} раунде победил {1}", i + 1, hero.Name);
                Console.ReadKey();
                if (i == lenght - 1)
                {
                    Console.WriteLine("Ты победил. Могу перевести тебе монетку");
                    Console.ReadKey();
                }
            }
            else if (hero.Health <= 0 && mob.Health > 0)
            {
                Console.WriteLine("В {0} раунде {1} проиграл", i + 1, hero.Name);
                Console.ReadKey();
            }
            else if (hero.Health <= 0 && mob.Health <= 0)
            {
                Console.WriteLine("Убил врага и умер сам!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Что-то пошло не так");
                Console.ReadKey();
            }
        } // Вывод результата боя

        public static void Action()
        {
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
        } // Разграничитель шагов
    }
}
