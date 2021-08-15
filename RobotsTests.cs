namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void RobotManager_initialization()
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            Assert.That(robotM.Capacity, Is.EqualTo(cap));
        }

        [Test]
        public void Capacity_ThrowsExpt_WhenCapacityNegative()
        {
            int testCapacity = -50;
            Assert.Throws<ArgumentException>(() => new RobotManager(testCapacity));
        }

        //[Test]
        //public void Capacity_Successfull() 
        //{
        //    int testCapacity = 0;
        //    RobotManager robotM = new RobotManager(testCapacity);
        //    Assert.That(robotM.Capacity, Is.EqualTo(testCapacity)); 
        //}

        [Test]
        public void Count_IncreaseCount_WhenRobotAdded()
        {
            Robot robot = new Robot("Roby", 50);
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);
            robotM.Add(robot);

            int expectedCount = 1;
            Assert.That(robotM.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Add_ThrowsException_WhenRobotNameAlreadyExists()
        {
            Robot robot = new Robot("Roby", 50);
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);
            robotM.Add(robot);

            Assert.Throws<InvalidOperationException>(() => robotM.Add(robot));
        }

        [Test]
        public void Add_ThrowsException_WhenRobotManagerCapacityReached()
        {
            Robot robot = new Robot("Roby", 50);
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);
            robotM.Add(robot);

            Robot robotSec = new Robot("robot2", 20);
            robotM.Add(robotSec);

            Assert.Throws<InvalidOperationException>(() => robotM.Add(robotSec));
        }

        [Test]
        public void Remove_ThrowsExcept_WhenRobotNameExistNot()
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string nonExistingRobotName = null;

            Assert.Throws<InvalidOperationException>(() => robotM.Remove(nonExistingRobotName));

        }

        [Test]
        public void Remove_SuccessfullRemoved()
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            robotM.Add(robot);
            int expectedCount = 0;

            robotM.Remove(robotName);

            Assert.That(robotM.Count, Is.EqualTo(expectedCount));

        }

        [Test]
        public void Work_ThrowsExcept_WhenRobotNameExistNot() 
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            //robotM.Add(robot); //robot is not added, therefor exists not into the collection

            Assert.Throws <InvalidOperationException>(() => robotM.Work(robotName, "dig", 10));

        }

        [Test]
        public void Work_ThrowsException_WhenBaterryLessThanBatteryToUse() 
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            robotM.Add(robot);

            Assert.Throws<InvalidOperationException>(()=> robotM.Work(robotName, "dig", 51));
        }

        [Test]
        public void Work_SuccessfullyFinishedJob_WhenBaterryBiggerThanBatteryToUse() 
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            robotM.Add(robot);
            robotM.Work(robotName, "dig", 50);
            int expectedLeftBatterryPower = 0;

            Assert.That(robot.Battery, Is.EqualTo(expectedLeftBatterryPower));
        }

        [Test]
        public void Charge_ThrowsExcept_WhenRobotNameExistNot() 
        {
            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            //robotM.Add(robot); //robot is not added, therefor exists not into the collection

            Assert.Throws<InvalidOperationException>(() => robotM.Charge(robotName));

        }

        [Test]
        public void Charge_Successfull_WhenRobotNameExists() 
        {

            int cap = 2;
            RobotManager robotM = new RobotManager(cap);

            string robotName = "Roby";
            Robot robot = new Robot(robotName, 50);

            robotM.Add(robot);
            robotM.Work(robotName, "dig", 10);
            
            robotM.Charge(robotName);
            int expectedChargedBattery = 50;

            Assert.That(robot.Battery, Is.EqualTo(expectedChargedBattery));
        }
    }
}
