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

            Hero hero = Heroes[game.ChooseHero(Heroes)]; // Выбираем класс игрока
            Entity[] enemy = EnemyArray(Others(hero, Heroes)); // Создаем предателя и добавляем его как финального босса к мобам
            game.ChooseName(hero); // Выбираем имя игрока

            for (int i = 0; i < enemy.Length; i++)
            {
                Entity mob = enemy[i]; // Пускаем мобов по кругу
                game.Initial(hero, mob); //Выводим статы персонажей 
                while (hero.Health > 0 && mob.Health > 0)
                {
                    game.Fight(hero, mob); // Этап раунда
                    hero.Stats(); // Показываем статы после раунда
                    entity.Stats();
                    game.Choose(hero, mob); //Воможность выбрать какое-либо действие
                }
                game.Final(hero, mob, enemy.Length, i); //Выводим результат боя
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
                if (all[i].HeroClass != hero.HeroClass)
                {
                    others[count] = all[i];
                    count++;
                }
            }
            Hero traitor = others[Rand.damage(0, count)];
            return traitor;
        } // Создаем предателя и добавляем его как финального босса к мобам
    }
}
// To Do: настроить баланс мобам
//Добавить босса +
//Генерировать мобов для интереса
//Сделать ng+ с переносом предметов
