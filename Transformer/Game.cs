using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                Console.WriteLine(i + 1 + $": " +
                                  $"Класс {heroes[i].HeroClass} - " +
                                  $"Здоровье: {heroes[i].Health}, " +
                                  $"Атака: {heroes[i].Damage}, " +
                                  $"Зелий лечения: {heroes[i].Flask}, " +
                                  $"Отравлений: {heroes[i].decay}, " +
                                  $"Усилений {heroes[i].Lightnings}\n");
            }
            int Choose = CheckHeroNumber(heroes);
            Console.Clear();
            Console.WriteLine($"Вы выбрали {heroes[Choose].HeroClass}\n");
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
                    if (Choose == Convert.ToString(i + 1))
                    {
                        return Convert.ToInt32(Choose) - 1;
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
                if (hero.Name == "" && i < 2 || hero.Name == hero.HeroClass)
                {
                    Console.WriteLine("Вы не ввели имя или ввели неправильное имя\nВведите имя");
                    i++;
                }
                else if (hero.Name == "" && i >= 2 && hero.Name == hero.HeroClass)
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
            Console.WriteLine($"" +
                              $"Герой {hero.Name}, " +
                              $"Здоровье {hero.Health}, " +
                              $"Зелий лечения {hero.Flask}, " +
                              $"Ударов молнией {hero.Lightnings}, " +
                              $"Отравлений: {hero.decay}");
            Console.WriteLine($"Враг {mob.Name}, Здоровье {mob.Health}");
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

        public void Choose(Hero hero, Entity mob)
        {
            Console.WriteLine($"\nДля использования зелья лечения нажмите 'B'\n" +
                              $"Для удара врага молнией нажмите 'F'\n" +
                              $"Для наложения разложения нажмите 'D'\n" +
                              $"Для продолжения нажмите любую другую клавишу");

            KeyChoose();
            void KeyChoose()
            {
                string key = Console.ReadLine(); // Ждем нажатия клавиш действия
                switch (key)
                {
                    case "D":
                        hero.ApplyDecayOnTarget(hero, mob);
                        KeyChoose();
                        break;
                    case "B":
                        hero.Heal();
                        KeyChoose();
                        break;
                    case "F":
                        hero.LightningShot(hero, mob);
                        KeyChoose();
                        break;
                }
            }
        } // Воможность выбрать какое-либо действие

        public void Final(Hero hero, Entity mob, int lenght, int i)
        {
            if (mob.Health <= 0 && hero.Health > 0)
            {
                Console.WriteLine($"\nВ {i + 1} раунде победил {hero.Name}");
                Console.ReadKey();
                if (i == lenght - 1)
                {
                    Console.WriteLine("Ты победил!");
                    Console.ReadKey();
                }
            }
            else if (hero.Health <= 0 && mob.Health > 0)
            {
                Console.WriteLine($"В {i + 1} раунде {hero.Name} проиграл");
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

        public void EnemyAi(Entity enemy)
        {
            int chance = Rand.damage(0, 10);
            if (enemy.Health <= 70 && enemy.Health > 30 && chance < 3
                ||
                enemy.Health <= 30 && enemy.Health > 10 && chance < 7
                ||
                enemy.Health <= 10 && enemy.Health > 0)
            {
                enemy.Heal();
            }
            else { }
        }
    }
}