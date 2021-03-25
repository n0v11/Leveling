using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leveling;

namespace ConsoleMiniGame
{
    class Actions
    {
        public int ChooseHero(Hero[] heroes)
        {
            Console.WriteLine("Выберите класс своего персонажа\n");
            for (int i = 0; i < heroes.Length; i++)
            {
                heroes[i].damage = Rand.damage(heroes[i].damageA, heroes[i].damageB);
                Console.WriteLine(i + 1 + $": " +
                                  $"Класс {heroes[i].heroClass} - " +
                                  $"Здоровье: {heroes[i].health}, " +
                                  $"Атака: {heroes[i].damage}, " +
                                  $"Зелий лечения: {heroes[i].flask}, " +
                                  $"Отравлений: {heroes[i].decay}, " +
                                  $"Усилений {heroes[i].lightnings}\n");
            }
            int Choose = CheckHeroNumber(heroes);
            Console.Clear();
            Console.WriteLine($"Вы выбрали {heroes[Choose].heroClass}\n");
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
                hero.name = Console.ReadLine();
                if (hero.name == "" && i < 2 || hero.name == hero.heroClass)
                {
                    Console.WriteLine("Вы не ввели имя или ввели неправильное имя\nВведите имя");
                    i++;
                }
                else if (hero.name == "" && i >= 2 && hero.name == hero.heroClass)
                {
                    Console.WriteLine("Ты тупой? Введи имя!");
                }
                else
                {
                    break;
                }
            }
            return hero.name;
        } // Проверка имени на условия

        public void Initial(Hero hero, Entity mob)
        {
            Console.Clear();
            Console.WriteLine($"" +
                              $"Герой {hero.name}, " +
                              $"Здоровье {hero.health}, " +
                              $"Зелий лечения {hero.flask}, " +
                              $"Ударов молнией {hero.lightnings}, " +
                              $"Отравлений: {hero.decay}");
            Console.WriteLine($"Враг {mob.name}, Здоровье {mob.health}");
            Actions.Action();
        } // Вывод статов персонажей 

        public void Fight(Hero hero, Entity mob) // Бой, проверерка необходимости использования действий
        {
            //Console.Clear();
            hero.damage = Rand.damage(hero.damageA, hero.damageB);
            mob.damage = Rand.damage(mob.damageA, mob.damageB);
            hero.health -= mob.damage;
            mob.health -= hero.damage;
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

            mob.EndOfMove();
        } // Воможность выбрать какое-либо действие

        public void Final(Hero hero, Entity mob, int lenght, int i)
        {
            if (mob.health <= 0 && hero.health > 0)
            {
                Console.WriteLine($"\nВ {i + 1} раунде победил {hero.name}");
                Console.ReadKey();
                if (i == lenght - 1)
                {
                    Console.WriteLine("Ты победил!");
                    Console.ReadKey();
                }
            }
            else if (hero.health <= 0 && mob.health > 0)
            {
                Console.WriteLine($"В {i + 1} раунде {hero.name} проиграл");
                Console.ReadKey();
            }
            else if (hero.health <= 0 && mob.health <= 0)
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
            if (enemy.health <= 70 && enemy.health > 30 && chance < 3
                ||
                enemy.health <= 30 && enemy.health > 10 && chance < 7
                ||
                enemy.health <= 10 && enemy.health > 0)
            {
                enemy.Heal();
            }
            else { }
        }
    }
}