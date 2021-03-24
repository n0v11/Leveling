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
        public int DamageA { get; set; }
        public int DamageB { get; set; }
        public int Damage { get; set; }

        public delegate void MobStateChanger(Entity mob);

        public MobStateChanger mobStateChanger;

        public void EndOfMove()
        {
            mobStateChanger?.Invoke(this);
        }

        public virtual void Stats()
        { } // Вывод статы после раунда 

        public virtual void Heal() { }
    }

    class Hero : Entity
    {
        public string HeroClass { get; set; }
        public int Flask { get; set; }
        public int Lightnings { get; set; }
        public int decay { get; set; }

        public override void Heal()
        {
            
            if (HeroClass != Name && Flask < 1)
            {
                Console.WriteLine("\nЗелий больше нет!");
            }
            else
            {
                if (Flask > 0)
                {
                    Flask -= 1;
                    Health += 50;
                    Console.WriteLine("\n{0} применяет зелье лечения и здоровье увеличилось на 50. Текущее количество здоровья {1}", Name, Health);
                }
                else { }
            }

        } // Лечение персонажа

        public void LightningShot(Hero hero, Entity mob)
        {
            if (hero.Lightnings > 0)
            {
                int damage = Rand.damage(10, 20);
                mob.Health -= damage;
                hero.Lightnings--;
                Console.WriteLine($"Здоровье {mob.Name} уменьшилось на {damage}. Текущее количество здоровья {mob.Name} {mob.Health}");
            }
            else
            {
                Console.WriteLine("Молний больше нет!");
            }
        } // Удар молнией

        public void ApplyDecayOnTarget(Hero hero, Entity mob)
        {
            MobStateChanger a = new (Decay);
            mob.mobStateChanger += a;
            hero.decay--;
            Console.WriteLine("На противника наложено гниение, он будет терять по несколько хп каждый ход");
        }

        private void Decay(Entity mob)
        {
            if (turns > 0)
            {
                int damage = Rand.damage(5, 11);
                mob.Health -= damage;
                Console.WriteLine($"Противник теряет {damage} хп");
                Console.WriteLine($"Гниение продлится еще {turns} ходов");
                turns--;
                Actions.Action();
            }
            else
            {
                Console.WriteLine("Гниение больше не действует");
            }
        }

        public override void Stats()
        {
            Console.WriteLine("Герой {0}, Здоровье {1}, Зелий лечения {2}, Ударов Молнией {4}. Нанес урона {3}", Name, Health, Flask, Damage, Lightnings);
        } // Вывод статов после раунда 
    }

    class Warrior : Hero
    {
        public Warrior() { Name = "Warrior"; HeroClass = "Warrior"; Health = 140; DamageA = 20; DamageB = 40; Flask = 3; Lightnings = 2; }
    }

    class Rogue : Hero
    {
        public Rogue() { Name = "Rogue"; HeroClass = "Rogue"; Health = 115; DamageA = 20; DamageB = 35; Flask = 2; Lightnings = 1; }
    }

    class Mage : Hero
    {
        public Mage() { Name = "Mage"; HeroClass = "Mage"; Health = 100; DamageA = 10; DamageB = 20; Flask = 1; Lightnings = 5; }
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
        public Enemy1() { Name = "Xiar"; Health = 60; DamageA = 5; DamageB = 10; }
    }

    class Enemy2 : Mob
    {
        public Enemy2() { Name = "Munduru"; Health = 100; DamageA = 10; DamageB = 15; }
    }

    class Enemy3 : Mob
    {
        public Enemy3() : base()
        {
            Name = "Lefmo";
            Health = 100;
            DamageA = 10;
            DamageB = 20;
        }
    }
}
