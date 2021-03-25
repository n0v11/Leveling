using System;
using ConsoleMiniGame;

namespace Leveling
{
    class Entity
    {
        public string name { get; set; }
        private int _health;
        public int health
        {
            set
            {
                if (value < 0)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
            get
            {
                return _health;
            }
        }
        public int damageA { get; set; }
        public int damageB { get; set; }
        public int damage { get; set; }

        public delegate void MobStateChanger();

        public MobStateChanger mobStateChanger;

        public void EndOfMove()
        {
            mobStateChanger?.Invoke();
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
                    health += 50;
                    Console.WriteLine("\n{0} применяет зелье лечения и здоровье увеличилось на 50. Текущее количество здоровья {1}", name, health);
                }
                else { }
            }

        } // Лечение персонажа

        public void LightningShot(Hero hero, Entity mob)
        {
            if (hero.lightnings > 0)
            {
                mob.health -= Rand.damage(10, 20);
                hero.lightnings--;
                Console.WriteLine($"Здоровье {mob.name} уменьшилось на {damage}. Текущее количество здоровья {mob.name} {mob.health}");
            }
            else
            {
                Console.WriteLine("Молний больше нет!");
            }
        } // Удар молнией

        public void ApplyDecayOnTarget(Hero hero, Entity mob)
        {
            int turn = 2;
            MobStateChanger decayEffect = null;
            if (hero.decay > 0)
            {
                Console.WriteLine("На противника наложено гниение, он будет терять по несколько хп каждый ход");
                decayEffect = () =>
                {
                    if (turn-- > 0)
                    {
                        Decay(mob, turn);
                    }
                    else
                    {
                        Console.WriteLine("Гниение больше не действует");
                        mob.mobStateChanger -= decayEffect;
                        Actions.Action();
                    }
                };
                hero.decay--;
                mob.mobStateChanger += decayEffect;
            }
            else
            {
                Console.WriteLine("Гниений больше нет");
            }
        } // Наложение гниения

        private void Decay(Entity mob, int turn)
        {
            mob.health -= Rand.damage(5, 11); ;
            Console.WriteLine($"Противник теряет {damage} хп");
            Console.WriteLine($"Гниение продлится еще {turn} раз");
            Actions.Action();
        }  // Отравление

        public override void Stats()
        {
            Console.WriteLine($"Герой {name}, Здоровье {health}, Зелий лечения {flask}, Ударов Молнией {lightnings}, Отравлений {decay}, Нанес урона {damage}");
        } // Вывод статов после раунда 
    }

    class Warrior : Hero
    {
        public Warrior()
        {
            name = "Warrior";
            heroClass = "Warrior"; 
            health = 150; 
            damageA = 8; 
            damageB = 14; 
            flask = 3; 
            lightnings = 2;
            decay = 1;
        }
    }

    class Rogue : Hero
    {
        public Rogue()
        {
            name = "Rogue"; 
            heroClass = "Rogue"; 
            health = 115; 
            damageA = 6; 
            damageB = 15; 
            flask = 2; 
            lightnings = 1;
            decay = 3;
        }
    }

    class Mage : Hero
    {
        public Mage()
        {
            name = "Mage"; 
            heroClass = "Mage"; 
            health = 100; 
            damageA = 5;
            damageB = 10; 
            flask = 1; 
            lightnings = 5;
            decay = 0;
        }
    }

    class Mob : Entity
    {
        public override void Stats()
        {
            Console.WriteLine($@"Враг {name}, Здоровье {health}, Нанес урона {damage}");
        } // Вывод статов после раунда  
    }

    class Enemy1 : Mob
    {
        public Enemy1()
        {
            name = "Xiar"; 
            health = 60;
            damageA = 5;
            damageB = 8;
        }
    }

    class Enemy2 : Mob
    {
        public Enemy2()
        {
            name = "Munduru"; 
            health = 100; 
            damageA = 6; 
            damageB = 12;
        }
    }

    class Enemy3 : Mob
    {
        public Enemy3() : base()
        {
            name = "Lefmo";
            health = 100;
            damageA = 8;
            damageB = 14;
        }
    }
}
