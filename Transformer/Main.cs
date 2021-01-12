using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Hero[] Heroes = HeroArray();
            Actions game = new Actions();
            Entity entity = new Entity();
            Hero hero = Heroes[game.ChooseHero(Heroes)]; // Выбор класса игрока
            Entity[] enemy = EnemyArray(Others(hero, Heroes)); // Создание предателя и добавление его как финального босса к мобам

            game.ChooseName(hero); // Выбор имени игрока

            for (int i = 0; i < enemy.Length; i++)
            {
                Entity enemy = enemy[i]; // Запуск мобов по кругу
                game.Initial(hero, enemy); // Вывод статов персонажей
                while (hero.Health > 0 && enemy.Health > 0)
                {
                    game.Fight(hero, enemy); // Этап раунда
                    Console.Clear();
                    Console.WriteLine("Конец раунда!");
                    hero.Stats(); // Вывод статов героя после раунда
                    enemy.Stats(); // Вывод статов моба после раунда

                    Actions.Action();
                    game.Choose(hero, enemy); //Воможность выбора какого-либо действия
                }
                game.Final(hero, enemy, enemy.Length, i); //Вывод результата боя
            }
        }

        private static Hero[] HeroArray()
        {
            Hero[] heroes = new Hero[3];
            heroes[0] = new Warrior();
            heroes[1] = new Rogue();
            heroes[2] = new Mage();
            return heroes;
        } // Массив героев
        private static Entity[] EnemyArray(Hero traitor)
        {
            Entity[] enemies = new Entity[4];
            enemies[0] = new Enemy1();
            enemies[1] = new Enemy2();
            enemies[2] = new Enemy3();
            enemies[3] = traitor;
            return enemies;
        } // Массив мобов
        private static Hero Others(Hero hero, Hero[] all)
        {
            Hero[] others = new Hero[2];
            int count = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].HeroClass != hero.HeroClass)
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
// To Do: настроить баланс мобам
//Добавить босса +
//Генерировать мобов для интереса
//Сделать ng+ с переносом предметов
