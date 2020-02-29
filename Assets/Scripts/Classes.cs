using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour
{
    void Start()
    {
        Cat cat = new Cat("Vasia", 4f, 12.2f, 23.4f, 13f);
        if (cat != null)
        {
            cat.Myu();
            Debug.Log(cat.Waight);
        }

        SCat sCat = new SCat();
        sCat.name = "Vasia";
        sCat.age = 4f;
        sCat.weight = 14.5f;
        sCat.height = 30.4f;
        sCat.tailLength = 13.4f;
        sCat.Myu();

        Vehicle bus = new Bus();
        Vehicle car = new Car();
        Vehicle tractor = new Tractor();
        bus.Beep();
        car.Beep();
        tractor.Beep();
        

    }

    public class Cat
    {
        public string Name { get; set; }
        private float _age;
        private float _waight;
        public float Waight => _waight;
        // private float _waight;
        public float Height { get; set; }
        public float TailLength { get; set; }

        public Cat ()
        {
            Cat cat = new Cat();
            cat.Name = "Noname";
            cat._age = 0f;
            cat._waight = 0f;
            cat.Height = 0f;
            cat.TailLength = 0f;
        }

        public Cat (string _name, float _age, float _weight, float _height, float _tailLength)
        {
            this.Name = _name;
            this._age = _age;
            this._waight = _weight;
            this.Height = _height;
            this.TailLength = _tailLength;
        }

        public float Age
        {
            get { return _age; }
        }



        public void Myu()
        {
            Debug.Log($"Name - {Name}, Age - {_age}y., Weight - {_waight}kg., Height - {Height}cm., Tail - {TailLength}cm.");
        }
    }

    public struct SCat
    {
        public string name;
        public float age;
        public float weight;
        public float height;
        public float tailLength;
        public void Myu()
        {
            Debug.Log($"Name - {name}, Age - {age}y., Weight - {weight}kg., Height - {height}cm., Tail - {tailLength}cm.");
        }
    }

    abstract public class Vehicle
    {
        public string name;
        public virtual void Beep()
        {
            Debug.Log("");
        }
        public abstract void AbsBeep();
    }
    public class Bus : Vehicle
    {
        public override void Beep()
        {
            Debug.Log("Bes");
        }
        public override void AbsBeep()
        {
            Debug.Log("Bes");
        }
    }
    public class Tractor : Vehicle
    {
        public override void Beep()
        {
            base.Beep();
        }
        public override void AbsBeep()
        {
            Debug.Log("Tres");
        }
    }
    public class Car : Vehicle, IVehicle
    {
        public override void Beep()
        {
            Debug.Log("Bleep");
        }
        public override void AbsBeep()
        {
            Debug.Log("Bleep");
        }

        public void Sound()
        {
            Debug.Log("Rrr");
        }
    }
    interface IVehicle
    {
        void Sound();
    }
}


