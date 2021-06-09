using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace rpg
{
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public interface IUnit: IObservable
    {
        int MaxHealth { get; }
        int Health();
        void SetHealth(int health);
        int Attack();
        void Defence(int attack);
        int Cost();
    }

    public interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }

    public interface IObserver
    {
        void Update(IUnit ob);
    }

    public interface IAbstractFactory
    {
        IUnit CreateUnit(ref int cash);
    }

    public interface ISpecialAction
    {
        int Range { get; }
        int Strength { get; }
        int DoSpecialAction();
    }

    public interface ICanBeHealed
    {
        void Heal(int value);
    }

    public interface IClonableUnit
    {
        IUnit Clone();
    }

    class FactoryGreedy: IAbstractFactory
    {
        public IUnit CreateUnit(ref int cash)
        {
            if (cash >= Wizard.cost)
            {
                cash -= Wizard.cost;
                return new Wizard();
            }

            if (cash >= Bowman.cost)
            {
                cash -= Bowman.cost;
                return new Bowman();
            }

            if (cash >= Doctor.cost)
            {
                cash -= Doctor.cost;
                return new Doctor();
            }

            if (cash >= HeavyInfantry.cost)
            {
                cash -= HeavyInfantry.cost;
                return new HeavyInfantry();
            }

            if (cash >= LightInfantry.cost)
            {
                cash -= LightInfantry.cost;
                return new LightInfantry();
            }

            return null;
        }
    }

    class FactoryBalanced : IAbstractFactory
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        public IUnit CreateUnit(ref int cash)
        {
            var costs = new List<int>() { LightInfantry.cost, HeavyInfantry.cost, Bowman.cost, Doctor.cost, Wizard.cost };
            int max = costs.Max() + 5;
            List<char> answer = new List<char>();

            if (cash >= Wizard.cost)
            {
                for (int i = 0; i < max - Wizard.cost; i++)
                {
                    answer.Add('w');
                }
            }

            if (cash >= Doctor.cost)
            {
                for (int i = 0; i < max - Doctor.cost; i++)
                {
                    answer.Add('d');
                }
            }

            if (cash >= Bowman.cost)
            {
                for (int i = 0; i < max - Bowman.cost; i++)
                {
                    answer.Add('b');
                }
            }

            if (cash >= HeavyInfantry.cost)
            {
                for (int i = 0; i < max - HeavyInfantry.cost; i++)
                {
                    answer.Add('h');
                }
            }

            if (cash >= LightInfantry.cost)
            {
                for (int i = 0; i < max - LightInfantry.cost; i++)
                {
                    answer.Add('l');
                }
            }


            if (answer.Count == 0) return null;
            var val = answer[rnd.Next(answer.Count)];

            switch (val)
            {
                case 'w':
                    cash -= Wizard.cost;
                    return new Wizard();
                case 'h':
                    cash -= HeavyInfantry.cost;
                    return new HeavyInfantry();
                case 'l':
                    cash -= LightInfantry.cost;
                    return new LightInfantry();
                case 'b':
                    cash -= Bowman.cost;
                    return new Bowman();
                case 'd':
                    cash -= Doctor.cost;
                    return new Doctor();
                default:
                    return null;
            }
        }
    }

    public class LightInfantry : IUnit, ICanBeHealed, IClonableUnit, ISpecialAction
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        public int MaxHealth { get; private set; } = 10;
        private int health = 10;
        public static int attack = 6;
        static public int cost { get; private set; } = 10;

        public int Cost()
        {
            return cost;
        }

        public void SetHealth(int health)
        {
            if (health <= MaxHealth)
                this.health = health;
        }

        public int Health()
        {
            return health;
        }

        public void Defence(int attack)
        {
            health -= attack;
            Notify();
        }

        public int Attack()
        {
            return attack;
        }

        public void Heal(int value)
        {
            if (value + health >= MaxHealth) health = MaxHealth;
            else health += value;
        }

        public IUnit Clone()
        {
            IUnit clone = (IUnit)MemberwiseClone();
            return clone;
        }

        public int Range { get; private set; } = 1;
        public int Strength { get; private set; }

        public int DoSpecialAction()
        {
            int chance = rnd.Next(1,17);
            return chance;
        }

        private List<IObserver> _observers = new List<IObserver>();   // паттерн наблюдателя

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Запуск обновления в каждом подписчике.
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

    }

    public class HeavyInfantry : IUnit
    {
        public int MaxHealth { get; private set; } = 15;
        private int health = 15;
        public static int attack = 4;
        static public int cost { get; private set; } = 15;

        virtual public bool spear { get; set; } = false;
        virtual public bool helmet { get; set; } = false;
        virtual public bool horse { get; set; } = false;
        virtual public bool shield { get; set; } = false;

        virtual public int Attack()
        {
            return attack;
        }

        virtual public void SetHealth(int health)
        {
            if (health <= MaxHealth)
                this.health = health;
        }

        virtual public int Health()
        {
            return health;
        }

        virtual public int Cost()
        {
            return cost;
        }

        virtual public void Defence(int attack)
        {
            health -= attack;
            Notify();
        }

        private List<IObserver> _observers = new List<IObserver>();   // паттерн наблюдателя

        virtual public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        virtual public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Запуск обновления в каждом подписчике.
        virtual public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

    }

    class ProxyHI : HeavyInfantry
    {
        private HeavyInfantry _unit;

        public override bool spear { get { return _unit.spear; } set { _unit.spear = value; } }
        public override bool horse { get { return _unit.horse; } set { _unit.horse = value; } }
        public override bool helmet { get { return _unit.helmet; } set { _unit.helmet = value; } }
        public override bool shield { get { return _unit.shield; } set { _unit.shield = value; } }

        string path = @"Logs_HeavyInfantry.txt";

        public ProxyHI(HeavyInfantry unit)
        {
            _unit = unit;
        }

        public override void SetHealth(int health)
        {
            _unit.SetHealth(health);
        }

        public override int Health()
        {
            return _unit.Health();
        }

        public override int Attack()
        {
            int _attack = _unit.Attack();
            
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Сопернику было нанесено " + Convert.ToString(_attack) + " DMG.\n");
            }
            return _attack;
        }

        public override int Cost()
        {
            return _unit.Cost();
        }

        public override void Defence(int attack)
        {
            _unit.Defence(attack);
            Notify();

            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Было принято на себя " + Convert.ToString(attack) + " DMG. Осталось " + _unit.Health() + " HP\n");
            }
        }

        public override void Attach(IObserver observer)
        {
            _unit.Attach(observer);
        }

        public override void Detach(IObserver observer)
        {
            _unit.Detach(observer);
        }

        public override void Notify()
        {
            _unit.Notify();
        }

    }

    public class Decorator: HeavyInfantry
    {
        protected readonly HeavyInfantry _unit;
        public override bool spear { get { return _unit.spear; } set { _unit.spear = value; } }
        public override bool horse { get { return _unit.horse; } set { _unit.horse = value; } }
        public override bool helmet { get { return _unit.helmet; } set { _unit.helmet = value; } }
        public override bool shield { get { return _unit.shield; } set { _unit.shield = value; } }

        public Decorator(HeavyInfantry unit)
        {
            _unit = unit;
        }

        public override void SetHealth(int health)
        {
            _unit.SetHealth(health);
        }

        public override int Health()
        {
            return _unit.Health();
        }

        public override int Attack()
        {
            return _unit.Attack(); 
        }

        public override void Defence(int attack)
        {
            _unit.Defence(attack);
        }

        public override int Cost()
        {
            return _unit.Cost();
        }

        public override void Attach(IObserver observer)
        {
            _unit.Attach(observer);
        }

        public override void Detach(IObserver observer)
        {
            _unit.Detach(observer);
        }

        public override void Notify()
        {
            _unit.Notify();
        }
    }

    public class SpearDecorator: Decorator
    {
        public SpearDecorator(HeavyInfantry unit) : base(unit) { }

        public override int Attack()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            if (rnd.Next(1, 8) == 1)
               spear = false;

            if (spear == true)
                return _unit.Attack() + 1;
            else return _unit.Attack();
        }

    }

    public class HorseDecorator : Decorator
    {
        public HorseDecorator(HeavyInfantry unit) : base(unit) { }

        public override int Attack()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            if (rnd.Next(1, 8) == 1)
                horse = false;

            if (horse == true)
                return _unit.Attack() + 1;
            else return _unit.Attack();
        }

    }

    public class HelmetDecorator : Decorator
    {
        public HelmetDecorator(HeavyInfantry unit) : base(unit) { }

        public override void Defence(int attack)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            if (rnd.Next(1, 8) == 1)
                helmet = false;

            if (helmet == true)
            {
                if ((attack - 1) >= 0) _unit.Defence(attack - 1);
                else _unit.Defence(0);
            }
            else
                _unit.Defence(attack);            
        }

    }

    public class ShieldDecorator : Decorator
    {
        public ShieldDecorator(HeavyInfantry unit) : base(unit) { }

        public override void Defence(int attack)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            if (rnd.Next(1, 8) == 1)
                shield = false;

            if (shield == true)
            {
                if ((attack - 1) >= 0) _unit.Defence(attack - 1);
                else _unit.Defence(0);
            }
            else
                _unit.Defence(attack);
        }
    }

    public class Bowman: IUnit, ISpecialAction, ICanBeHealed, IClonableUnit
    {

        Random rnd = new Random(DateTime.Now.Millisecond);

        public int MaxHealth { get; private set; } = 8;
        private int health = 8;
        public static int attack = 2;
        static public int cost { get; private set; } = 20;

        public int Cost()
        {
            return cost;
        }

        public int Health()
        {
            return health;
        }

        public void SetHealth(int health)
        {
            if (health <= MaxHealth)
                this.health = health;
        }

        public void Defence(int attack)
        {
            health -= attack;
            Notify();
        }

        public int Attack()
        {
            return attack;
        }

        public int Range { get; private set; } = 5;
        public int Strength { get; private set; } = 3;

        public int DoSpecialAction()
        {
            int chance = rnd.Next(3);
            if (chance == 1) return Strength;
            else return 0;
        }

        public void Heal(int value)
        {
            if (value + health >= MaxHealth) health = MaxHealth;
            else health += value;
        }

        public IUnit Clone()
        {
            IUnit clone = (IUnit)MemberwiseClone();
            return clone;
        }

        private List<IObserver> _observers = new List<IObserver>();   // паттерн наблюдателя

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Запуск обновления в каждом подписчике.
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

    }

    public class Doctor: IUnit, ISpecialAction//, IObservable
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        public int MaxHealth { get; private set; } = 8;
        private int health = 8;
        public static int attack = 2;
        static public int cost { get; private set; } = 20;

        public int Cost()
        {
            return cost;
        }

        public void SetHealth(int health)
        {
            if (health <= MaxHealth)
                this.health = health;
        }

        public int Health()
        {
            return health;
        }

        public int Attack()
        {
            return attack;
        }

        public void Defence(int attack)
        {
            health -= attack;
            Notify();
        }

        public int Range { get; private set; } = 1;
        public int Strength { get; private set; } = 0;

        public int DoSpecialAction()
        {
            int chance = rnd.Next(1, 101);
            if ((chance >=1) && (chance <= 10)) return Strength = 10;
            if (chance % 2 == 0) return Strength = 1;
            else return Strength = 0;
        }

        private List<IObserver> _observers = new List<IObserver>();   // паттерн наблюдателя

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Запуск обновления в каждом подписчике.
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }

    }

    public class Wizard: IUnit, ISpecialAction//, IObservable
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        public int MaxHealth { get; private set; } = 8;
        private int health = 8;
        public static int attack = 1;
        static public int cost { get; private set; } = 30;

        public int Cost()
        {
            return cost;
        }

        public void SetHealth(int health)
        {
            if (health <= MaxHealth)
                this.health = health;
        }

        public int Health()
        {
            return health;
        }

        public int Attack()
        {
            return attack;
        }

        public void Defence(int attack)
        {
            health -= attack;
            Notify();
        }

        public int Range { get; private set; } = 1;
        public int Strength { get; private set; }

        public int DoSpecialAction()
        {
            int chance = rnd.Next(1, 101);
            if ((chance >= 1) && (chance <= 10)) return 1;
            else return 0;
        }

        private List<IObserver> _observers = new List<IObserver>();   // паттерн наблюдателя

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Запуск обновления в каждом подписчике.
        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }
        }
    }

    class Army
    {
        public List<IUnit> warriors = new List<IUnit>();
        public int cash;

        public Army(int cash)
        {
            this.cash = cash;
        }
    }

    public interface IStrategy
    {
        void DoDuel(ref List<IUnit> army_1, ref List<IUnit> army_2);
    }

    class OneToOne : IStrategy
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        string path = @"Logs_RPG.txt";

        void Fight(ref List<IUnit> att, ref List<IUnit> def)
        {
            def[def.Count - 1].Defence(att[att.Count - 1].Attack());
        }

        private void DoSpecialActions(ref List<IUnit> att, ref List<IUnit> def)
        {
            for (int i = att.Count - 2; i >= 0; i--)
            {
                if (att[i] is ISpecialAction)
                {

                    if (att[i] is Bowman)               // Спец.действие лучника
                    {
                        ISpecialAction a = att[i] as ISpecialAction;
                        int numTarget = -1;
                        int numTargetMax = def.Count + att.Count - i - a.Range - 1;
                        if ((numTargetMax >= 0) && (numTargetMax < def.Count))
                            numTarget = rnd.Next(numTargetMax, def.Count);

                        if ((att.Count - i <= a.Range) && (numTarget >= 0))
                        {
                            def[numTarget].Defence(a.DoSpecialAction());
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                            {
                                sw.WriteLine("Лучник " + Convert.ToString(att.Count-1-i) + " выстрелил в " + Convert.ToString(def.Count - numTarget - 1) + "\n");
                            }
                        }
                    }

                    if (att[i] is Doctor)               // Спец.действие доктора
                    {
                        if ((i + (att[i] as Doctor).Range < att.Count) && (att[i + (att[i] as Doctor).Range] is ICanBeHealed))
                        {
                            (att[i + (att[i] as Doctor).Range] as ICanBeHealed).Heal((att[i] as ISpecialAction).DoSpecialAction());
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                            {
                                sw.WriteLine("Доктор " + Convert.ToString(att.Count - 1 - i) + " вылечил " + Convert.ToString(i + (att[i] as Doctor).Range) + "\n");
                            }
                        }
                        else
                        {
                            if ((i - (att[i] as Doctor).Range >= 0) && (att[i - (att[i] as Doctor).Range] is ICanBeHealed))
                            {
                                (att[i - (att[i] as Doctor).Range] as ICanBeHealed).Heal((att[i] as ISpecialAction).DoSpecialAction());
                            }
                            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                            {
                                sw.WriteLine("Доктор " + Convert.ToString(att.Count - 1 - i) + " вылечил " + Convert.ToString(i - (att[i] as Doctor).Range) + "\n");
                            }
                        }
                    }

                    if (att[i] is Wizard)                  // Спец.действие волшебника
                    {
                        if ((i + (att[i] as Wizard).Range < att.Count) && (att[i + (att[i] as Wizard).Range] is IClonableUnit))
                        {
                            if ((att[i] as ISpecialAction).DoSpecialAction() == 1)
                            {
                                att.Insert(i + (att[i] as Wizard).Range, (att[i + (att[i] as Wizard).Range] as IClonableUnit).Clone());
                                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                {
                                    sw.WriteLine("Волшебник " + Convert.ToString(att.Count - 1 - i) + " сделал клона\n");
                                }
                            }
                        }
                        else
                        {
                            if ((i - (att[i] as Wizard).Range >= 0) && (att[i - (att[i] as Wizard).Range] is IClonableUnit))
                            {
                                if ((att[i] as ISpecialAction).DoSpecialAction() == 1)
                                {
                                    att.Insert(i - (att[i] as Wizard).Range, (att[i - (att[i] as Wizard).Range] as IClonableUnit).Clone());
                                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                    {
                                        sw.WriteLine("Волшебник " + Convert.ToString(att.Count - 1 - i) + " сделал клона\n");
                                    }
                                }
                            }
                        }
                    }

                    if (att[i] is LightInfantry)  // Спец.действие легкого
                    {
                        int range = (att[i] as LightInfantry).Range;
                        if (i + range < att.Count)
                        {
                            if (att[i + range] is ProxyHI)
                            {
                                int choice = (att[i] as ISpecialAction).DoSpecialAction();

                                if (choice == 1)
                                    (att[i + range] as ProxyHI).horse = true;
                                if (choice == 2)
                                    (att[i + range] as ProxyHI).spear = true;
                                if (choice == 3)
                                    (att[i + range] as ProxyHI).shield = true;
                                if (choice == 4)
                                    (att[i + range] as ProxyHI).helmet = true;
                            }

                        }
                        else
                        {
                            if (i - range >= 0) 
                            {
                                if (att[i - range] is ProxyHI)
                                {
                                    int choice = (att[i] as ISpecialAction).DoSpecialAction();

                                    if (choice == 1)
                                        (att[i - range] as ProxyHI).horse = true;
                                    if (choice == 2)
                                        (att[i - range] as ProxyHI).spear = true;
                                    if (choice == 3)
                                        (att[i - range] as ProxyHI).shield = true;
                                    if (choice == 4)
                                        (att[i - range] as ProxyHI).helmet = true;
                                }
                            }
                        }

                    }

                }
            }
        }

        public void DoDuel(ref List<IUnit> army_1, ref List<IUnit> army_2)
        {
            int switcher = rnd.Next(1, 3);

            DoSpecialActions(ref army_1, ref army_2);
            DoSpecialActions(ref army_2, ref army_1);

            while (true)
            {
                if (switcher == 1) switcher = 2; else switcher = 1;

                if ((army_1[army_1.Count - 1].Health() > 0) && (army_2[army_2.Count - 1].Health() > 0))  // Здесь сам бой
                {
                    if (switcher == 1)
                    {
                        Fight(ref army_1, ref army_2);
                    }

                    if (switcher == 2)
                    {
                        Fight(ref army_2, ref army_1);
                    }
                } 
                else
                {
                    break;
                }
            }
        }
    }

    class ThreeToThree : IStrategy
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        string path = @"Logs_RPG.txt";

        void Fight(ref List<List<IUnit>> att, ref List<List<IUnit>> def, int i)
        {
            def[0][i].Defence(att[0][i].Attack());
        }

        private void DoSpecialActions(ref List<List<IUnit>> tempoAtt, ref List<List<IUnit>> tempoDef)
        {

            for (int i = 1; i < tempoAtt.Count - 1; i++)          // ПЕРЕДЕЛАТЬ АБИЛКИ ПОД ИНВЕРСИЮ
            {
                int j = 0;
                foreach (var item in tempoAtt[i])
                {

                    if (item is ISpecialAction)
                    {
                        if (item is Bowman)               // Спец.действие лучника
                        {
                            ISpecialAction a = item as ISpecialAction;
                            int numTarget = rnd.Next(1, a.Range) - i - 1;

                            if ((numTarget >= 0) && (numTarget < tempoDef.Count))
                            {
                                if (j < tempoDef[numTarget].Count)
                                {
                                    tempoDef[numTarget][j].Defence(a.DoSpecialAction());
                                    using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                    {
                                        sw.WriteLine("3x3 Лучник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " выстрелил в " + Convert.ToString(numTarget) + "\n");
                                    }
                                }
                            }
                        }
                                                                 //  0 3 5
                        if (item is Doctor)                      //  1   6
                        {                                        //  2 4 7
                            int target = rnd.Next(8);

                            if ((target == 0) && (i - 1 > 0))
                                if ((j - 1 < tempoAtt[i - 1].Count) && (j - 1 >= 0))
                                    if (tempoAtt[i - 1][j - 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i - 1][j - 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i-1) + "-" + Convert.ToString(j-1) + "\n");
                                        }
                                    }

                            if ((target == 1) && (i - 1 > 0))
                                if (j < tempoAtt[i - 1].Count)
                                    if (tempoAtt[i - 1][j] is ICanBeHealed)
                                    {
                                        (tempoAtt[i - 1][j] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i - 1) + "-" + Convert.ToString(j) + "\n");
                                        }
                                    }

                            if ((target == 2) && (i - 1 > 0))
                                if ((j + 1 < tempoAtt[i - 1].Count) && (j + 1 >= 0))
                                    if (tempoAtt[i - 1][j + 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i - 1][j + 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i - 1) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                            if (target == 3)
                                if ((j - 1 < tempoAtt[i].Count) && (j - 1 >= 0))
                                    if (tempoAtt[i][j - 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i][j - 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i) + "-" + Convert.ToString(j - 1) + "\n");
                                        }
                                    }

                            if (target == 4)
                                if ((j + 1 < tempoAtt[i].Count) && (j + 1 >= 0))
                                    if (tempoAtt[i][j + 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i][j + 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                            if ((target == 5) && (i + 1 < tempoAtt.Count))
                                if ((j - 1 < tempoAtt[i + 1].Count) && (j - 1 >= 0))
                                    if (tempoAtt[i + 1][j - 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i + 1][j - 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i + 1) + "-" + Convert.ToString(j - 1) + "\n");
                                        }
                                    }

                            if ((target == 6) && (i + 1 < tempoAtt.Count))
                                if (j < tempoAtt[i + 1].Count)
                                    if (tempoAtt[i + 1][j] is ICanBeHealed)
                                    {
                                        (tempoAtt[i + 1][j] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i + 1) + "-" + Convert.ToString(j) + "\n");
                                        }
                                    }

                            if ((target == 7) && (i + 1 < tempoAtt.Count))
                                if ((j + 1 < tempoAtt[i + 1].Count) && (j + 1 >= 0))
                                    if (tempoAtt[i + 1][j + 1] is ICanBeHealed)
                                    {
                                        (tempoAtt[i + 1][j + 1] as ICanBeHealed).Heal((item as Doctor).DoSpecialAction());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Доктор " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " вылечил " + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                        }

                        if (item is Wizard)
                        {
                            int target = rnd.Next(8);

                            if ((target == 0) && (i - 1 > 0))
                                if ((j - 1 < tempoAtt[i - 1].Count) && (j - 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i - 1][j - 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i - 1][j - 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i - 1) + "-" + Convert.ToString(j - 1) + "\n");
                                        }
                                    }

                            if ((target == 1) && (i - 1 > 0))
                                if (j < tempoAtt[i - 1].Count)
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i - 1][j] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i - 1][j] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i - 1) + "-" + Convert.ToString(j) + "\n");
                                        }
                                    }

                            if ((target == 2) && (i - 1 > 0))
                                if ((j + 1 < tempoAtt[i - 1].Count) && (j + 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i - 1][j + 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i - 1][j + 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i - 1) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                            if (target == 3)
                                if ((j - 1 < tempoAtt[i].Count) && (j - 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i][j - 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i][j - 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i) + "-" + Convert.ToString(j - 1) + "\n");
                                        }
                                    }

                            if (target == 4)
                                if ((j + 1 < tempoAtt[i].Count) && (j + 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i][j + 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i][j + 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                            if ((target == 5) && (i + 1 < tempoAtt.Count))
                                if ((j - 1 < tempoAtt[i + 1].Count) && (j - 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i + 1][j - 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i + 1][j - 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i + 1) + "-" + Convert.ToString(j - 1) + "\n");
                                        }
                                    }

                            if ((target == 6) && (i + 1 < tempoAtt.Count))
                                if (j < tempoAtt[i + 1].Count)
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i + 1][j] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i + 1][j] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i + 1) + "-" + Convert.ToString(j) + "\n");
                                        }
                                    }

                            if ((target == 7) && (i + 1 < tempoAtt.Count))
                                if ((j + 1 < tempoAtt[i + 1].Count) && (j + 1 >= 0))
                                    if (((item as Wizard).DoSpecialAction() == 1) && (tempoAtt[i + 1][j + 1] is IClonableUnit))
                                    {
                                        if (tempoAtt[tempoAtt.Count - 1].Count == 3)
                                        {
                                            tempoAtt.Add(new List<IUnit>());
                                        }
                                        tempoAtt[tempoAtt.Count - 1].Add((tempoAtt[i + 1][j + 1] as IClonableUnit).Clone());
                                        using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                                        {
                                            sw.WriteLine("3x3 Волшебник " + Convert.ToString(i) + " в ряду " + Convert.ToString(j) + " клонировал " + Convert.ToString(i + 1) + "-" + Convert.ToString(j + 1) + "\n");
                                        }
                                    }

                        }

                        if (item is LightInfantry)  // Спец.действие легкого
                        {
                            int range = (item as LightInfantry).Range;
                            if (j + range < 3)
                            {
                                if (tempoAtt[i][j + range] is ProxyHI)
                                {
                                    int choice = (item as ISpecialAction).DoSpecialAction();

                                    if (choice == 1)
                                        (tempoAtt[i][j + range] as ProxyHI).horse = true;
                                    if (choice == 2)
                                        (tempoAtt[i][j + range] as ProxyHI).spear = true;
                                    if (choice == 3)
                                        (tempoAtt[i][j + range] as ProxyHI).shield = true;
                                    if (choice == 4)
                                        (tempoAtt[i][j + range] as ProxyHI).helmet = true;
                                }

                            }
                            else
                            {
                                if (j - range >= 0)
                                {
                                    if (tempoAtt[i][j - range] is ProxyHI)
                                    {
                                        int choice = (item as ISpecialAction).DoSpecialAction();

                                        if (choice == 1)
                                            (tempoAtt[i][j - range] as ProxyHI).horse = true;
                                        if (choice == 2)
                                            (tempoAtt[i][j - range] as ProxyHI).spear = true;
                                        if (choice == 3)
                                            (tempoAtt[i][j - range] as ProxyHI).shield = true;
                                        if (choice == 4)
                                            (tempoAtt[i][j - range] as ProxyHI).helmet = true;
                                    }
                                }
                            }

                        }
                        j++;
                    }
                }
            }
        }

        public void DoDuel(ref List<IUnit> army_1, ref List<IUnit> army_2)
        {
            {
                List<List<IUnit>> tempoA1 = new List<List<IUnit>>();
                for (int i = 0; i < army_1.Count; i += 3)
                {
                    tempoA1.Add(new List<IUnit>());
                    if (i < army_1.Count) tempoA1[i / 3].Add(army_1[army_1.Count - i - 1]); else break;
                    if (army_1.Count - i - 2 >= 0) tempoA1[i / 3].Add(army_1[army_1.Count - i - 2]); else break;
                    if (army_1.Count - i - 3 >= 0) tempoA1[i / 3].Add(army_1[army_1.Count - i - 3]); else break;
                }
                List<List<IUnit>> tempoA2 = new List<List<IUnit>>();
                for (int i = 0; i < army_2.Count; i += 3)
                {
                    tempoA2.Add(new List<IUnit>());
                    if (i < army_2.Count) tempoA2[i / 3].Add(army_2[army_2.Count - i - 1]); else break;
                    if (army_2.Count - i - 2 >= 0) tempoA2[i / 3].Add(army_2[army_2.Count - i - 2]); else break;
                    if (army_2.Count - i - 3 >= 0) tempoA2[i / 3].Add(army_2[army_2.Count - i - 3]); else break;
                }



                DoSpecialActions(ref tempoA1, ref tempoA2);
                DoSpecialActions(ref tempoA2, ref tempoA1);

                int min = Math.Min(tempoA1[0].Count, tempoA2[0].Count);

                for (int i = 0; i < min; i++)
                {
                    int switcher = rnd.Next(1, 3);
                    while (true)
                    {
                        if (switcher == 1) switcher = 2; else switcher = 1;

                        if ((tempoA1[0][i].Health() > 0) && (tempoA2[0][i].Health() > 0))
                        {
                            if (switcher == 1)
                            {
                                Fight(ref tempoA1, ref tempoA2, i);
                            }

                            if (switcher == 2)
                            {
                                Fight(ref tempoA2, ref tempoA1, i);
                            }
                        }
                        else break;
                    }
                }

                for (int i = 0; i < tempoA1.Count; i++)  // Сборка мусора
                {
                    tempoA1[i].RemoveAll(item => item.Health() <= 0);
                }
                for (int i = 0; i < tempoA2.Count; i++)  // Сборка мусора
                {
                    tempoA2[i].RemoveAll(item => item.Health() <= 0);
                }
            }
        }

    } 

    class AllToAll : IStrategy
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        string path = @"Logs_RPG.txt";

        private void DoSpecialActions(ref List<IUnit> army_1, ref List<IUnit> army_2)
        {

            if (army_1.Count > army_2.Count)
                for (int i = army_1.Count - army_2.Count - 1; i >= 0; i--)
                {
                    if (army_1[i] is ISpecialAction)
                    {

                        if (army_1[i] is Bowman)               // Спец.действие лучника
                        {
                            ISpecialAction a = army_1[i] as ISpecialAction;
                            int numTarget = -1;
                            int numTargetMax = army_2.Count + army_1.Count - i - a.Range - 1;
                            if ((numTargetMax >= 0) && (numTargetMax < army_2.Count))
                                numTarget = rnd.Next(numTargetMax, army_2.Count);

                            if ((army_1.Count - i <= a.Range) && (numTarget >= 0))
                            {
                                army_2[numTarget].Defence(a.DoSpecialAction());
                            }
                        }

                        if (army_1[i] is Doctor)               // Спец.действие доктора
                        {
                            if ((i + (army_1[i] as Doctor).Range < army_1.Count) && (army_1[i + (army_1[i] as Doctor).Range] is ICanBeHealed))
                            {
                                (army_1[i + (army_1[i] as Doctor).Range] as ICanBeHealed).Heal((army_1[i] as ISpecialAction).DoSpecialAction());
                            }
                            else
                            {
                                if ((i - (army_1[i] as Doctor).Range >= 0) && (army_1[i - (army_1[i] as Doctor).Range] is ICanBeHealed))
                                {
                                    (army_1[i - (army_1[i] as Doctor).Range] as ICanBeHealed).Heal((army_1[i] as ISpecialAction).DoSpecialAction());
                                }
                            }
                        }

                        if (army_1[i] is Wizard)                  // Спец.действие волшебника
                        {
                            if ((i + (army_1[i] as Wizard).Range < army_1.Count) && (army_1[i + (army_1[i] as Wizard).Range] is IClonableUnit))
                            {
                                if ((army_1[i] as ISpecialAction).DoSpecialAction() == 1)
                                    army_1.Insert(i + (army_1[i] as Wizard).Range, (army_1[i + (army_1[i] as Wizard).Range] as IClonableUnit).Clone());
                            }
                            else
                            {
                                if ((i - (army_1[i] as Wizard).Range >= 0) && (army_1[i - (army_1[i] as Wizard).Range] is IClonableUnit))
                                {
                                    if ((army_1[i] as ISpecialAction).DoSpecialAction() == 1)
                                        army_1.Insert(i - (army_1[i] as Wizard).Range, (army_1[i - (army_1[i] as Wizard).Range] as IClonableUnit).Clone());
                                }
                            }
                        }

                        if (army_1[i] is LightInfantry)  // Спец.действие легкого
                        {
                            int range = (army_1[i] as LightInfantry).Range;
                            if (i + range < army_1.Count)
                            {
                                if ((army_1[i + range] is ProxyHI))
                                {
                                    int choice = (army_1[i] as ISpecialAction).DoSpecialAction();

                                    if (choice == 1)
                                        (army_1[i + range] as ProxyHI).horse = true;
                                    if (choice == 2)
                                        (army_1[i + range] as ProxyHI).spear = true;
                                    if (choice == 3)
                                        (army_1[i + range] as ProxyHI).shield = true;
                                    if (choice == 4)
                                        (army_1[i + range] as ProxyHI).helmet = true;
                                }
                            }
                            else
                            {
                                if (i - range >= 0)
                                {
                                    if (army_1[i - range] is ProxyHI)
                                    {
                                        int choice = (army_1[i] as ISpecialAction).DoSpecialAction();

                                        if (choice == 1)
                                            (army_1[i - range] as ProxyHI).horse = true;
                                        if (choice == 2)
                                            (army_1[i - range] as ProxyHI).spear = true;
                                        if (choice == 3)
                                            (army_1[i - range] as ProxyHI).shield = true;
                                        if (choice == 4)
                                            (army_1[i - range] as ProxyHI).helmet = true;
                                    }
                                }
                            }

                        }

                    }
                }

            if (army_2.Count > army_1.Count)
                for (int i = army_2.Count - army_1.Count - 1; i >= 0; i--)
                {
                    if (army_2[i] is ISpecialAction)
                    {

                        if (army_2[i] is Bowman)               // Спец.действие лучника
                        {
                            ISpecialAction a = army_2[i] as ISpecialAction;
                            int numTarget = -1;
                            int numTargetMax = army_1.Count + army_2.Count - i - a.Range - 1;
                            if ((numTargetMax >= 0) && (numTargetMax < army_1.Count))
                                numTarget = rnd.Next(numTargetMax, army_1.Count);

                            if ((army_2.Count - i <= a.Range) && (numTarget >= 0))
                            {
                                army_1[numTarget].Defence(a.DoSpecialAction());
                            }
                        }

                        if (army_2[i] is Doctor)               // Спец.действие доктора
                        {
                            if ((i + (army_2[i] as Doctor).Range < army_2.Count) && (army_2[i + (army_2[i] as Doctor).Range] is ICanBeHealed))
                            {
                                (army_2[i + (army_2[i] as Doctor).Range] as ICanBeHealed).Heal((army_2[i] as ISpecialAction).DoSpecialAction());
                            }
                            else
                            {
                                if ((i - (army_2[i] as Doctor).Range >= 0) && (army_2[i - (army_2[i] as Doctor).Range] is ICanBeHealed))
                                {
                                    (army_2[i - (army_2[i] as Doctor).Range] as ICanBeHealed).Heal((army_2[i] as ISpecialAction).DoSpecialAction());
                                }
                            }
                        }

                        if (army_2[i] is Wizard)                  // Спец.действие волшебника
                        {
                            if ((i + (army_2[i] as Wizard).Range < army_2.Count) && (army_2[i + (army_2[i] as Wizard).Range] is IClonableUnit))
                            {
                                if ((army_2[i] as ISpecialAction).DoSpecialAction() == 1)
                                    army_2.Insert(i + (army_2[i] as Wizard).Range, (army_2[i + (army_2[i] as Wizard).Range] as IClonableUnit).Clone());
                            }
                            else
                            {
                                if ((i - (army_2[i] as Wizard).Range >= 0) && (army_2[i - (army_2[i] as Wizard).Range] is IClonableUnit))
                                {
                                    if ((army_2[i] as ISpecialAction).DoSpecialAction() == 1)
                                        army_2.Insert(i - (army_2[i] as Wizard).Range, (army_2[i - (army_2[i] as Wizard).Range] as IClonableUnit).Clone());
                                }
                            }
                        }

                        if (army_2[i] is LightInfantry)  // Спец.действие легкого
                        {
                            int range = (army_2[i] as LightInfantry).Range;
                            if (i + range < army_2.Count)
                            {
                                if (army_2[i + range] is ProxyHI)
                                {
                                    int choice = (army_2[i] as ISpecialAction).DoSpecialAction();

                                    if (choice == 1)
                                        (army_2[i + range] as ProxyHI).horse = true;
                                    if (choice == 2)
                                        (army_2[i + range] as ProxyHI).spear = true;
                                    if (choice == 3)
                                        (army_2[i + range] as ProxyHI).shield = true;
                                    if (choice == 4)
                                        (army_2[i + range] as ProxyHI).helmet = true;
                                }

                            }
                            else
                            {
                                if (i - range >= 0)
                                {
                                    if (army_2[i - range] is ProxyHI)
                                    {
                                        int choice = (army_2[i] as ISpecialAction).DoSpecialAction();

                                        if (choice == 1)
                                            (army_2[i - range] as ProxyHI).horse = true;
                                        if (choice == 2)
                                            (army_2[i - range] as ProxyHI).spear = true;
                                        if (choice == 3)
                                            (army_2[i - range] as ProxyHI).shield = true;
                                        if (choice == 4)
                                            (army_2[i - range] as ProxyHI).helmet = true;
                                    }
                                }
                            }

                        }

                    }
                }

        }

        public void DoDuel(ref List<IUnit> army_1, ref List<IUnit> army_2)
        {
            List<int> switcher = new List<int>();
            DoSpecialActions(ref army_1, ref army_2);

            int count = Math.Abs(army_1.Count - army_2.Count);

            for (int i = 0; i < Math.Max(army_1.Count, army_2.Count); i++)
            {
                switcher.Add(rnd.Next(1, 3));
            }

            int j = Math.Min(army_1.Count, army_2.Count);
            if (j == army_2.Count)
            {
                j--;
                for (int i = army_1.Count - 1; i >= count; i--)
                {
                    while (true)
                    {
                        if (switcher[i] == 1) switcher[i] = 2; else switcher[i] = 1;
                        if ((army_1[i].Health() > 0) && (army_2[j].Health() > 0))  // Здесь сам бой
                        {
                            if (switcher[i] == 1)
                            {
                                army_2[j].Defence(army_1[i].Attack());
                            }

                            if (switcher[i] == 2)
                            {
                                army_1[i].Defence(army_2[j].Attack());
                            }
                        }
                        else
                        {
                            j--;
                            break;
                        }
                    }
                }
            }
            else
            {
                j--;
                for (int i = army_2.Count - 1; i >= count; i--)
                {
                    while (true)
                    {
                        if (switcher[i] == 1) switcher[i] = 2; else switcher[i] = 1;
                        if ((army_1[j].Health() > 0) && (army_2[i].Health() > 0))  // Здесь сам бой
                        {
                            if (switcher[i] == 1)
                            {
                                army_2[i].Defence(army_1[j].Attack());
                            }

                            if (switcher[i] == 2)
                            {
                                army_1[j].Defence(army_2[i].Attack());
                            }
                        }
                        else
                        {
                            j--;
                            break;
                        }
                    }
                }
            }
        }
    }

    class DoDuelCommand: ICommand
    {
        private Actions actions;

        private List<int> army_1_health_r = new List<int>();
        private List<int> army_2_health_r = new List<int>();

        private List<bool> horse_a1_r = new List<bool>();
        private List<bool> spear_a1_r = new List<bool>();
        private List<bool> helmet_a1_r = new List<bool>();
        private List<bool> shield_a1_r = new List<bool>();

        private List<bool> horse_a2_r = new List<bool>();
        private List<bool> spear_a2_r = new List<bool>();
        private List<bool> helmet_a2_r = new List<bool>();
        private List<bool> shield_a2_r = new List<bool>();

        private List<IUnit> army_1_redo;
        private List<IUnit> army_2_redo;

        /// //////////////////////////////////
        
        private List<int> army_1_health_u = new List<int>();
        private List<int> army_2_health_u = new List<int>();

        private List<bool> horse_a1_u = new List<bool>();
        private List<bool> spear_a1_u = new List<bool>();
        private List<bool> helmet_a1_u = new List<bool>();
        private List<bool> shield_a1_u = new List<bool>();

        private List<bool> horse_a2_u = new List<bool>();
        private List<bool> spear_a2_u = new List<bool>();
        private List<bool> helmet_a2_u = new List<bool>();
        private List<bool> shield_a2_u = new List<bool>();

        private List<IUnit> army_1_undo;
        private List<IUnit> army_2_undo;

        private void FillMemoryUndo()
        {
            army_1_undo = actions.GetArmy(1).GetRange(0, actions.GetArmy(1).Count);
            army_2_undo = actions.GetArmy(2).GetRange(0, actions.GetArmy(2).Count);

            army_1_health_u.Clear();
            army_2_health_u.Clear();
            horse_a1_u.Clear();
            spear_a1_u.Clear();
            helmet_a1_u.Clear();
            shield_a1_u.Clear();
            horse_a2_u.Clear();
            spear_a2_u.Clear();
            helmet_a2_u.Clear();
            shield_a2_u.Clear();

            foreach (var item in actions.GetArmy(1))
            {
                army_1_health_u.Add(item.Health());
                if (item is ProxyHI)
                {
                    if ((item as ProxyHI).horse == true) horse_a1_u.Add(true);
                    else horse_a1_u.Add(false);
                    if ((item as ProxyHI).spear == true) spear_a1_u.Add(true);
                    else spear_a1_u.Add(false);
                    if ((item as ProxyHI).helmet == true) helmet_a1_u.Add(true);
                    else helmet_a1_u.Add(false);
                    if ((item as ProxyHI).shield == true) shield_a1_u.Add(true);
                    else shield_a1_u.Add(false);
                }
                else
                {
                    horse_a1_u.Add(false);
                    spear_a1_u.Add(false);
                    helmet_a1_u.Add(false);
                    shield_a1_u.Add(false);
                }
            }
            foreach (var item in actions.GetArmy(2))
            {
                army_2_health_u.Add(item.Health());
                if (item is ProxyHI)
                {
                    if ((item as ProxyHI).horse == true) horse_a2_u.Add(true);
                    else horse_a2_u.Add(false);
                    if ((item as ProxyHI).spear == true) spear_a2_u.Add(true);
                    else spear_a2_u.Add(false);
                    if ((item as ProxyHI).helmet == true) helmet_a2_u.Add(true);
                    else helmet_a2_u.Add(false);
                    if ((item as ProxyHI).shield == true) shield_a2_u.Add(true);
                    else shield_a2_u.Add(false);
                }
                else
                {
                    horse_a2_u.Add(false);
                    spear_a2_u.Add(false);
                    helmet_a2_u.Add(false);
                    shield_a2_u.Add(false);
                }
            }
        }

        private void FillMemoryRedo()
        {
            army_1_redo = actions.GetArmy(1).GetRange(0, actions.GetArmy(1).Count);
            army_2_redo = actions.GetArmy(2).GetRange(0, actions.GetArmy(2).Count);

            army_1_health_r.Clear();
            army_2_health_r.Clear();
            horse_a1_r.Clear();
            spear_a1_r.Clear();
            helmet_a1_r.Clear();
            shield_a1_r.Clear();
            horse_a2_r.Clear();
            spear_a2_r.Clear();
            helmet_a2_r.Clear();
            shield_a2_r.Clear();

            foreach (var item in actions.GetArmy(1))
            {
                army_1_health_r.Add(item.Health());
                if (item is ProxyHI)
                {
                    if ((item as ProxyHI).horse == true) horse_a1_r.Add(true);
                    else horse_a1_r.Add(false);
                    if ((item as ProxyHI).spear == true) spear_a1_r.Add(true);
                    else spear_a1_r.Add(false);
                    if ((item as ProxyHI).helmet == true) helmet_a1_r.Add(true);
                    else helmet_a1_r.Add(false);
                    if ((item as ProxyHI).shield == true) shield_a1_r.Add(true);
                    else shield_a1_r.Add(false);
                }
                else
                {
                    horse_a1_r.Add(false);
                    spear_a1_r.Add(false);
                    helmet_a1_r.Add(false);
                    shield_a1_r.Add(false);
                }
            }
            foreach (var item in actions.GetArmy(2))
            {
                army_2_health_r.Add(item.Health());
                if (item is ProxyHI)
                {
                    if ((item as ProxyHI).horse == true) horse_a2_r.Add(true);
                    else horse_a2_r.Add(false);
                    if ((item as ProxyHI).spear == true) spear_a2_r.Add(true);
                    else spear_a2_r.Add(false);
                    if ((item as ProxyHI).helmet == true) helmet_a2_r.Add(true);
                    else helmet_a2_r.Add(false);
                    if ((item as ProxyHI).shield == true) shield_a2_r.Add(true);
                    else shield_a2_r.Add(false);
                }
                else
                {
                    horse_a2_r.Add(false);
                    spear_a2_r.Add(false);
                    helmet_a2_r.Add(false);
                    shield_a2_r.Add(false);
                }
            }
        }

        public DoDuelCommand(Actions actions)
        {
            this.actions = actions;
            FillMemoryUndo();
        }

        public void Execute()
        {
            actions.DoDuel();
        }

        public void Undo()
        {
            FillMemoryRedo();

            actions.SetArmy(1, army_1_undo, army_1_health_u, horse_a1_u,spear_a1_u,helmet_a1_u,shield_a1_u);
            actions.SetArmy(2, army_2_undo, army_2_health_u, horse_a2_u, spear_a2_u, helmet_a2_u, shield_a2_u);
        }

        public void Redo()
        {
            FillMemoryUndo();

            actions.SetArmy(1, army_1_redo, army_1_health_r, horse_a1_r, spear_a1_r, helmet_a1_r, shield_a1_r);
            actions.SetArmy(2, army_2_redo, army_2_health_r, horse_a2_r, spear_a2_r, helmet_a2_r, shield_a2_r);
        }
    }

    public class ActionsInvoker
    {
        private ICommand command;

        List<ICommand> undoStack = new List<ICommand>();
        List<ICommand> redoStack = new List<ICommand>();

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void DoDuel()
        {
            command.Execute();
            undoStack.Add(command);
            redoStack.Clear();
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                undoStack[undoStack.Count - 1].Undo();
                redoStack.Add(undoStack[undoStack.Count - 1]);
                undoStack.RemoveAt(undoStack.Count - 1);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                redoStack[redoStack.Count - 1].Redo();
                undoStack.Add(redoStack[redoStack.Count - 1]);
                redoStack.RemoveAt(redoStack.Count - 1);
            }
        }
    }

    public class Actions   // receiver
    {

        private IStrategy strategy;

        public Actions(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        static Random rnd = new Random(DateTime.Now.Millisecond);

        Army army_1 = new Army(0);
        Army army_2 = new Army(0);

        private void CreateArmy(ref Army army, uint cash)
        {
            army.cash = (int)cash;
            army.warriors.Clear();
            //var factory = new FactoryGreedy();           // Переключение фабрик
            var factory = new FactoryBalanced();


            while (true)
            {
                IUnit warrior = factory.CreateUnit(ref army.cash);
                if (warrior == null) break;
                army.warriors.Add(warrior);
                if (army.warriors[army.warriors.Count - 1] is HeavyInfantry) // сразу оборачиваем во все декораторы
                {
                    army.warriors[army.warriors.Count - 1] = new SpearDecorator(army.warriors[army.warriors.Count - 1] as HeavyInfantry);
                    army.warriors[army.warriors.Count - 1] = new HorseDecorator(army.warriors[army.warriors.Count - 1] as HeavyInfantry);
                    army.warriors[army.warriors.Count - 1] = new ShieldDecorator(army.warriors[army.warriors.Count - 1] as HeavyInfantry);
                    army.warriors[army.warriors.Count - 1] = new HelmetDecorator(army.warriors[army.warriors.Count - 1] as HeavyInfantry);
                    army.warriors[army.warriors.Count - 1] = new ProxyHI(army.warriors[army.warriors.Count - 1] as HeavyInfantry);
                }
            }
        }

        public void CreateArmies(int numArmy, uint cash)
        {
            if (numArmy == 1) CreateArmy(ref army_1, cash);
            if (numArmy == 2)
            {
                System.Threading.Thread.Sleep(17); // для более корректной работы рандома
                CreateArmy(ref army_2, cash);
            }
        }

        public int CheckWinArmy()
        {
            if (army_1.warriors.Count == 0) return 2; // победила 2 армия
            if (army_2.warriors.Count == 0) return 1;
            return 0;
        }

        public void SetArmy(int choice, List<IUnit> army, List<int> health, List<bool>horse, List<bool> spear, List<bool> helmet, List<bool> shield)
        {
            if (choice == 1)
            {
                army_1.warriors = army;
                for (int i = 0; i < army_1.warriors.Count; i++)
                {
                    army_1.warriors[i].SetHealth(health[i]);

                    if (army_1.warriors[i] is ProxyHI)
                    {
                        (army_1.warriors[i] as ProxyHI).horse = horse[i];
                        (army_1.warriors[i] as ProxyHI).spear = spear[i];
                        (army_1.warriors[i] as ProxyHI).helmet = helmet[i];
                        (army_1.warriors[i] as ProxyHI).shield = shield[i];
                    }
                }
            }
            if (choice == 2)
            {
                army_2.warriors = army;
                for (int i = 0; i < army_2.warriors.Count; i++)
                {
                    army_2.warriors[i].SetHealth(health[i]);

                    if (army_2.warriors[i] is ProxyHI)
                    {
                        (army_2.warriors[i] as ProxyHI).horse = horse[i];
                        (army_2.warriors[i] as ProxyHI).spear = spear[i];
                        (army_2.warriors[i] as ProxyHI).helmet = helmet[i];
                        (army_2.warriors[i] as ProxyHI).shield = shield[i];
                    }
                }
            }
        }

        public List<IUnit> GetArmy(int choice)
        {
            if (choice == 1) return army_1.warriors;
            if (choice == 2) return army_2.warriors;
            return null;
        }

        private void RemoveCorpses()
        {
            army_1.warriors.RemoveAll(i => i.Health() <= 0);
            army_2.warriors.RemoveAll(i => i.Health() <= 0);
        }

        public void DoDuel()
        {
            if ((army_1.warriors.Count != 0) && (army_2.warriors.Count != 0))
            {
                strategy.DoDuel(ref army_1.warriors, ref army_2.warriors);
                RemoveCorpses();
            }
        }

        public void AttachUnits(IObserver ob)
        {
            for (int i = 0; i < army_1.warriors.Count; i++)
            {
                army_1.warriors[i].Attach(ob);
            }
            for (int i = 0; i < army_2.warriors.Count; i++)
            {
                army_2.warriors[i].Attach(ob);
            }
        }

        public void DetachUnits(IObserver ob)
        {
            for (int i = 0; i < army_1.warriors.Count; i++)
            {
                army_1.warriors[i].Detach(ob);
            }
            for (int i = 0; i < army_2.warriors.Count; i++)
            {
                army_2.warriors[i].Detach(ob);
            }
        }

    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
