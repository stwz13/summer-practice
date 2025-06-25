using static System.Runtime.InteropServices.JavaScript.JSType;

namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();     
        void Rotate(int angle); 
        void Fire();             
        int Speed { get; }      
        int FirePower { get; }   
    }

    public class Cruiser : ISpaceship
    {
        public int Speed { get; }
        public int FirePower { get; }

        public int AngleArondAxis { get; private set; }
        public int Coordinate { get; private set; }
        public int NumbeOfMissiles {  get; private set; }

        public Cruiser()
        {
            Speed = 50;
            FirePower = 100;
            NumbeOfMissiles = 100;
            AngleArondAxis = 0;
            Coordinate = 0;
        }

        public void MoveForward() => Coordinate += Speed;

        public void Rotate(int angle) => AngleArondAxis = (AngleArondAxis + angle) % 360;

        public void Fire() => Console.WriteLine("Выпущена ракета");

    }

    public class Fighter : ISpaceship
    {
        public int Speed { get; }
        public int FirePower { get; }

        public int AngleArondAxis { get; private set; }
        public int Coordinate { get; private set; }
        public int NumbeOfMissiles { get; private set; }

        public Fighter()
        {
            Speed = 100;
            FirePower = 50;
            NumbeOfMissiles = 100;
            AngleArondAxis = 0;
            Coordinate = 0;
        }

        public void MoveForward() => Coordinate += Speed;

        public void Rotate(int angle) => AngleArondAxis = (AngleArondAxis + angle) % 360;

        public void Fire() => NumbeOfMissiles = NumbeOfMissiles > 0 ? NumbeOfMissiles - 1 : 0;

    }
}
