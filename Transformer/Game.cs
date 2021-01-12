using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Actions
    {
        public int ChooseHero(Hero[] heroes)
        {
            Console.WriteLine("Выберите класс своего персонажа\n");
            for (int i = 0; i < heroes.Length; i++)
            {
                heroes[i].Damage = Rand.damage(heroes[i].DamageA, heroes[i].DamageB);
                Console.WriteLine(i + 1 + ": Класс {0} - Здоровье: {1}, Атака: {2}, Зелий лечения: {3}, Усилений {4}\n", heroes[i].HeroClass, heroes[i].Health, heroes[i].Damage, heroes[i].Flask, heroes[i].Lightnings);
            }
            int Choose = CheckHeroNumber(heroes);
            Console.Clear();
            Console.WriteLine("Вы выбрали {0}\n", heroes[Choose].HeroClass);
            return Choose;
        } // Выбор класс игрока

        private int CheckHeroNumber(Hero[] heroes)
        {
            string Choose;
            while (true)
            {
                Choose = Console.ReadLine();
                for (int i = 0; i < heroes.Length; i++)
                {
                    string m = Convert.ToString(i + 1);
                    if (Choose == m)
                    {
                        int Result = Convert.ToInt32(Choose) - 1;
                        return Result;
                    }
                }
                Console.WriteLine("Вы неверно выбрали класс персонажа. Попробуйте еще раз");
            } // Проверка ввода выбора героя
        }

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
            //Console.Clear();
            hero.Damage = Rand.damage(hero.DamageA, hero.DamageB);
            mob.Damage = Rand.damage(mob.DamageA, mob.DamageB);
            hero.Health -= mob.Damage;
            mob.Health -= hero.Damage;
        }

        }

        public void Choose(Hero hero, Entity mob)
        {
            Console.WriteLine("\nДля использования зелья лечения нажмите 'B'\nДля удара врага молнией нажмите 'F'\nДля продолжения нажмите любую другую клавишу");
            string key = Console.ReadLine(); // Ждем нажатия клавишы действия
            if (key == "B")
            {
                hero.Heal();
            }
            if (key == "F")
            {
                hero.LightningShot(hero, mob);
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
