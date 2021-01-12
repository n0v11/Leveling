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
            var Heroes = HeroArray();
            var Enemy = EnemyArray();

            Actions game = new Actions();

            var hero = Heroes[game.ChooseHero(Heroes)]; // Выбираем класс игрока
            game.ChooseName(hero); // Выбираем имя игрока

            for (int i = 0; i < Enemy.Length; i++)
            {
                var mob = Enemy[i]; // Пускаем мобов по кругу
                game.Initial(hero, mob); //Выводим статы персонажей 
                while (hero.Health > 0 && mob.Health > 0)
                {
                    game.Fight(hero, mob); // Этап раунда
                    game.Stats(hero, mob); // Показываем статы после раунда
                    game.Choose(hero, mob); //Воможность выбрать какое-либо действие
                }
                game.Final(hero, mob, i); //Выводим результат боя
            }
        }

        private static Heroes[] HeroArray()
        {
            Heroes[] Hero = new Heroes[3];
            Hero[0] = new Warrior();
            Hero[1] = new Rogue();
            Hero[2] = new Mage();
            return Hero;
        }
        private static Mobs[] EnemyArray()
        {
            Mobs[] Enemy = new Mobs[3];
            Enemy[0] = new Enemy1();
            Enemy[1] = new Enemy2();
            Enemy[2] = new Enemy3();
            return Enemy;
        }
    }
}
// To Do: настроить баланс мобам
//Добавить босса
//Генерировать мобов для интереса
//Сделать ng+ с переносом предметов
