using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Leveling;

namespace ConsoleMiniGame
{
    class Program
    {
        static void Main()
        {
            Hero[] Heroes = HeroArray();
            Actions game = new Actions();
            Hero hero = Heroes[game.ChooseHero(Heroes)]; // Выбор класса игрока
            Entity[] Enemies = EnemyArray(Others(hero, Heroes)); // Создание предателя и добавление его как финального босса к мобам

            game.ChooseName(hero); // Выбор имени игрока

            for (int i = 0; i < Enemies.Length; i++)
            {
                Entity enemy = Enemies[i]; // Запуск мобов по кругу
                game.Initial(hero, enemy); // Вывод статов персонажей 
                while (hero.Health > 0 && enemy.Health > 0)
                {
                    game.Fight(hero, enemy); // Этап раунда
                    Console.Clear();
                    Console.WriteLine("Конец раунда!");
                    hero.Stats(); // Вывод статов героя после раунда
                    enemy.Stats(); // Вывод статов моба после раунда
                    if (i == Enemies.Length - 1) // Если враг - босс
                    {
                        game.EnemyAi(enemy);
                    }
                    game.Choose(hero, enemy); //Воможность выбора какого-либо действия
                }
                game.Final(hero, enemy, Enemies.Length, i); //Вывод результата боя
            }
        }

        private static Hero[] HeroArray()
        {
            Hero[] Hero = new Hero[3];
            Hero[0] = new Warrior();
            Hero[1] = new Rogue();
            Hero[2] = new Mage();
            return Hero;
        } // Массив героев
        private static Entity[] EnemyArray(Hero traitor)
        {
            Entity[] Enemy = new Entity[4];
            Enemy[0] = new Enemy1();
            Enemy[1] = new Enemy2();
            Enemy[2] = new Enemy3();
            Enemy[3] = traitor;
            return Enemy;
        } // Массив мобов
        private static Hero Others(Hero hero, Hero[] all)
        {
            Hero[] others = new Hero[2];
            int count = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].heroClass != hero.heroClass)
                {
                    others[count] = all[i];
                    count++;
                }
            }
            Hero traitor = others[Rand.damage(0, count)];
            return traitor;
        } // Создание предателя и добавление его как финального босса к мобам
    }
}
// ToDo: Настроить баланс мобам
// ToDo: Добавить босса +
// ToDo: Генерация мобов
// ToDo: ng+ с переносом предметов
