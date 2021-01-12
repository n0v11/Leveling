using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMiniGame
{
    class Entity
    {
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

        public virtual void Stats()
        { } // Вывод статы после раунда 

        public virtual void Heal() { }
    }

    class Hero : Entity
    {
        public string HeroClass;
        public int Flask;
        public int Lightnings;

        public override void Heal()
        {            
            if ((Name != "Warrior" || Name != "Rogue" || Name != "Mage") && Flask < 1)
            {
                    //Console.Clear();
                    Console.WriteLine("\nЗелий больше нет!");
                    Actions.Action();          
            }
            else
            {
                if (Flask > 0)
                {
                    Flask -= 1;
                    Health += 50;
                    //Console.Clear();
                    Console.WriteLine("\nЗдоровье {0} увеличилось на 50. Текущее количество здоровья {1}", Name, Health);
                    Actions.Action();
                }
                else { }
            }

        }

        public void LightningShot(Hero hero, Entity mob)
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

        public override void Stats()
        {
            Console.WriteLine("Герой {0}, Здоровье {1}, Зелий лечения {2}, Ударов Молнией {4}. Нанес урона {3}", Name, Health, Flask, Damage, Lightnings);
        } // Вывод статов после раунда 
    }

    class Warrior : Hero
    {
        public Warrior() { Name = "Warrior"; HeroClass = "Warrior"; Health = 140; Damage = Rand.damage(20, 40); Flask = 3; Lightnings = 2; }
    }

    class Rogue : Hero
    {
        public Rogue() { Name = "Rogue"; HeroClass = "Rogue"; Health = 115; Damage = Rand.damage(20, 35); Flask = 2; Lightnings = 1; }
    }

    class Mage : Hero
    {
        public Mage() { Name = "Mage"; HeroClass = "Mage"; Health = 100; Damage = Rand.damage(10, 20); Flask = 1; Lightnings = 5; }
    }

    class Mob : Entity
    {
         public override void Stats()
         {
             Console.WriteLine("Враг {0}, Здоровье {1}, Нанес урона {2}", Name, Health, Damage);
         } // Вывод статов после раунда  
    }

    class Enemy1 : Mob
    {
        public Enemy1() { Name = "Пуська"; Health = 30; Damage = Rand.damage(5, 15); }
    }

    class Enemy2 : Mob
    {
        public Enemy2() { Name = "Сруська"; Health = 60; Damage = Rand.damage(10, 20); }
    }

    class Enemy3 : Mob
    {
        public Enemy3() : base()
        {
            Name = "Финалька";
            Health = 80;
            Damage = Rand.damage(15, 20);
        }
    }
}
