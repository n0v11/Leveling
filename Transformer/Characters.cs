using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Entity
    {
        private string name;
        public string Name { get; set; }
        private int health;
        public int Health
        {
            set
            {
                if (value < 0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
            get
            {
                return health;
            }
        }
        public int Damage;
    }

    class Heroes : Entity
    {
        public int Flask;
        public int Lightnings;
        public string HeroClass;

        public void Heal(Heroes hero)
        {
            if (Flask > 0)
            {
                Flask -= 1;
                Health += 50;
                Console.Clear();
                Console.WriteLine("Здоровье увеличилось на 50. Текущее количество здоровья {0}", Health);
                Actions.Action();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Зелий больше нет!");
                Actions.Action();
            }
        }

        public void LightningShot(Heroes hero, Entity mob)
        {
            if (hero.Lightnings > 0)
            {
                int damage = Rand.damage(10, 20);
                mob.Health -= damage;
                hero.Lightnings--;
                Console.Clear();
                Console.WriteLine("Здоровье {0} уменьшилось на {1}. Текущее количество здоровья {0} {2}", mob.Name, damage, mob.Health);
                Actions.Action();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Молний больше нет!");
                Actions.Action();
            }
        }
    }

    class Warrior : Heroes
    {
        public Warrior() { HeroClass = "Warrior"; Health = 140; Damage = Rand.damage(15, 30); Flask = 1; Lightnings = 2; }
    }

    class Rogue : Heroes
    {
        public Rogue() { HeroClass = "Rogue"; Health = 115; Damage = Rand.damage(20, 35); Flask = 2; Lightnings = 1; }
    }

    class Mage : Heroes
    {
        public Mage() { HeroClass = "Mage"; Health = 100; Damage = Rand.damage(10, 20); Flask = 1; Lightnings = 5; }
    }

    class Mobs : Entity { }

    class Enemy1 : Mobs
    {
        public Enemy1() { Name = "Пуська"; Health = 30; Damage = Rand.damage(5, 15); }
    }

    class Enemy2 : Mobs
    {
        public Enemy2() { Name = "Сруська"; Health = 60; Damage = Rand.damage(10, 20); }
    }

    class Enemy3 : Mobs
    {
        public Enemy3() : base()
        {
            Name = "Финалька";
            Health = 80;
            Damage = Rand.damage(15, 20);
        }
    }
}
