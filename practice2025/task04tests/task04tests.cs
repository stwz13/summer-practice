using Xunit;
using task04;

namespace task04tests
{
   
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            var cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
            Assert.Equal(100, cruiser.NumbeOfMissiles);
            Assert.Equal(0, cruiser.AngleArondAxis);
            Assert.Equal(0, cruiser.Coordinate);
        }

        [Fact]
        public void Fighter_ShouldHaveCorrectStats()
        {
            var fighter = new Fighter();
            Assert.Equal(100, fighter.Speed);
            Assert.Equal(50, fighter.FirePower);
            Assert.Equal(100, fighter.NumbeOfMissiles);
            Assert.Equal(0, fighter.AngleArondAxis);
            Assert.Equal(0, fighter.Coordinate);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void Cruiser_ShouldBeStrongerThanFighter()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(cruiser.FirePower > fighter.FirePower);
        }

        [Fact]
        public void Cruiser_MovesForvardCorrect()
        {
            var cruiser = new Cruiser();
            int startCoordinate = cruiser.Coordinate;
            cruiser.MoveForward();
            Assert.Equal(startCoordinate + cruiser.Speed, cruiser.Coordinate);
        }

        [Fact]
        public void Fighter_MovesForvardCorrect()
        {
            var cruiser = new Cruiser();
            int startCoordinate = cruiser.Coordinate;
            cruiser.MoveForward();
            Assert.Equal(startCoordinate + cruiser.Speed, cruiser.Coordinate);
        }

        [Fact]
        public void Cruiser_RotatesCorrect()
        {
            var cruiser = new Cruiser();
            int startRotate = cruiser.AngleArondAxis;

            cruiser.Rotate(10);
            Assert.Equal(10, cruiser.AngleArondAxis);
            cruiser.Rotate(360);
            Assert.Equal(10, cruiser.AngleArondAxis);
        }

        [Fact]
        public void Fighter_RotatesCorrect()
        {
            var fighter = new Fighter();
            int startRotate = fighter.AngleArondAxis;

            fighter.Rotate(70);
            Assert.Equal(70, fighter.AngleArondAxis);
            fighter.Rotate(180);
            Assert.Equal(250, fighter.AngleArondAxis);
        }

        [Fact]
        public void Cruiser_FiresCorrect()
        {
            var cruiser = new Cruiser();
            int startFires = cruiser.NumbeOfMissiles;
            cruiser.Fire();
            Assert.Equal(startFires - 1, cruiser.NumbeOfMissiles);
        }

        [Fact]
        public void Fighter_FiresCorrect()
        {
            var fighter = new Fighter();
            int startFires = fighter.NumbeOfMissiles;
            fighter.Fire();
            Assert.Equal(startFires - 1, fighter.NumbeOfMissiles);
        }

    }
}
