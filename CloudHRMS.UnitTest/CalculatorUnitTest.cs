namespace CloudHRMS.UnitTest
{

    public class CalculatorUnitTest
    {
        [Fact]
        public void ShouldTrueAdd3Numbers()
        {
            //Arrange
            int n1 = 1, n2 = 2, n3 = 3;
            int expectedResult = 6;
            Calculator calculator = new Calculator();
            //Act
            int ActualResult = calculator.Add3Numbers(n1, n2, n3);
            //Assert
            Assert.Equal(expectedResult, ActualResult);// True
        }
        [Fact]
        public void ShouldFalseAdd3Numbers()
        {
            //Arrange
            int n1 = 1, n2 = 2, n3 = 3;
            int expectedResult = 1000;
            Calculator calculator = new Calculator();
            //Act
            int ActualResult = calculator.Add3Numbers(n1, n2, n3);
            //Assert
            Assert.NotEqual(expectedResult, ActualResult);//False
        }

        [Fact]
        public void ShouldTrueSayHello()
        {
            //Arrange
            string expectedResult = "Hello";
            Calculator calculator = new Calculator();
            //Act
            string ActualResult = calculator.SayHello();
            //Assert
            Assert.Equal(expectedResult, ActualResult);// True
        }

        [Fact]
        public void ShouldFalseSayHello()
        {
            //Arrange
            string expectedResult = "hello";
            Calculator calculator = new Calculator();
            //Act
            string ActualResult = calculator.SayHello();
            //Assert
            Assert.NotEqual(expectedResult, ActualResult);// True
        }
    }
}
