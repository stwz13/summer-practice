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
        public int Speed { get; } = 50;
        public int FirePower { get; } = 100;

        public int AngleAroundAxis { get; private set; } = 0;
        public int Coordinate { get; private set; } = 0;
        public int NumberOfMissiles { get; private set; } = 100;

        public void MoveForward() => Coordinate += Speed;

        public void Rotate(int angle) => AngleAroundAxis = (AngleAroundAxis + angle) % 360;

        public void Fire() => NumberOfMissiles = NumberOfMissiles > 0 ? NumberOfMissiles - 1 : 0;

    }

    public class Fighter : ISpaceship
    {
        public int Speed { get; } = 100;
        public int FirePower { get; } = 50;

        public int AngleArondAxis { get; private set; } = 0;
        public int Coordinate { get; private set; } = 0;
        public int NumbeOfMissiles { get; private set; } = 100;

        public void MoveForward() => Coordinate += Speed;

        public void Rotate(int angle) => AngleArondAxis = (AngleArondAxis + angle) % 360;

        public void Fire() => NumbeOfMissiles = NumbeOfMissiles > 0 ? NumbeOfMissiles - 1 : 0;

    }
}
