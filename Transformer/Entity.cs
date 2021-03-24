using System;
using ConsoleMiniGame;

namespace Leveling
{
    class Entity
    {
        public string name { get; set; }
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
        public int damageA { get; set; }
        public int damageB { get; set; }
        public int damage { get; set; }

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
        public string heroClass { get; set; }
        public int flask { get; set; }
        public int lightnings { get; set; }
        public int decay { get; set; }

        public override void Heal()
        {
            
            if (heroClass != name && flask < 1)
            {
                Console.WriteLine("\nЗелий больше нет!");
            }
            else
            {
                if (flask > 0)
                {
                    flask -= 1;
                    Health += 50;
                    Console.WriteLine("\n{0} применяет зелье лечения и здоровье увеличилось на 50. Текущее количество здоровья {1}", name, Health);
                }
                else { }
            }

        } // Лечение персонажа

        public void LightningShot(Hero hero, Entity mob)
        {
            if (hero.lightnings > 0)
            {
                mob.Health -= Rand.damage(10, 20);
                hero.lightnings--;
                Console.WriteLine($"Здоровье {mob.name} уменьшилось на {damage}. Текущее количество здоровья {mob.name} {mob.Health}");
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
            Console.WriteLine($"Герой {name}, Здоровье {Health}, Зелий лечения {flask}, Ударов Молнией {lightnings}. Нанес урона {damage}");
        } // Вывод статов после раунда 
    }

    class Warrior : Hero
    {
        public Warrior()
        {
            name = "Warrior";
            heroClass = "Warrior"; 
            Health = 140; 
            damageA = 20; 
            damageB = 40; 
            flask = 3; 
            lightnings = 2;
        }
    }

    class Rogue : Hero
    {
        public Rogue()
        {
            name = "Rogue"; 
            heroClass = "Rogue"; 
            Health = 115; 
            damageA = 20; 
            damageB = 35; 
            flask = 2; 
            lightnings = 1;
        }
    }

    class Mage : Hero
    {
        public Mage()
        {
            name = "Mage"; 
            heroClass = "Mage"; 
            Health = 100; 
            damageA = 10;
            damageB = 20; 
            flask = 1; 
            lightnings = 5;
        }
    }

    class Mob : Entity
    {
        public override void Stats()
        {
            Console.WriteLine($@"Враг {name}, Здоровье {Health}, Нанес урона {damage}");
        } // Вывод статов после раунда  
    }

    class Enemy1 : Mob
    {
        public Enemy1()
        {
            name = "Xiar"; 
            Health = 60;
            damageA = 5;
            damageB = 10;
        }
    }

    class Enemy2 : Mob
    {
        public Enemy2()
        {
            name = "Munduru"; 
            Health = 100; 
            damageA = 10; 
            damageB = 15;
        }
    }

    class Enemy3 : Mob
    {
        public Enemy3() : base()
        {
            name = "Lefmo";
            Health = 100;
            damageA = 10;
            damageB = 20;
        }
    }
}
