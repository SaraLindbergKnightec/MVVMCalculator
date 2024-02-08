using MVVM_UnitTestedCalculator.Model;
using MVVM_UnitTestedCalculator.ViewModel;
namespace Test_MVVMCalculator
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleIntegers()
        {
            Assert.That(Calculate("1"), Is.EqualTo(1));
            Assert.That(Calculate("2"), Is.EqualTo(2));
            Assert.That(Calculate("3"), Is.EqualTo(3));
            Assert.That(Calculate("-4"), Is.EqualTo(-4));
        }

        [Test]
        public void Addition()
        {
            Assert.That(Calculate("1+1"), Is.EqualTo(2));
            Assert.That(Calculate("12+3"), Is.EqualTo(15));
            Assert.That(Calculate("7+-2"), Is.EqualTo(5));
        }

        [Test]
        public void Subtraction()
        {
            Assert.That(Calculate("3-1"), Is.EqualTo(2));
            Assert.That(Calculate("-1-3"), Is.EqualTo(-4));
            Assert.That(Calculate("1--3"), Is.EqualTo(4));
        }

        [Test]
        public void MultipleOperators()
        {
            Assert.That(Calculate("3+4+5+6"), Is.EqualTo(18));
            Assert.That(Calculate("3+2-4"), Is.EqualTo(1));
            Assert.That(Calculate("-3+4-1"), Is.EqualTo(0));
            Assert.That(Calculate("3+4+-5-1"), Is.EqualTo(1));
            Assert.That(Calculate("5--4"), Is.EqualTo(9));
        }

        [Test]
        public void Multiplication()
        {
            Assert.That(Calculate("2*3"), Is.EqualTo(6));
            Assert.That(Calculate("-2*3"), Is.EqualTo(-6));
            Assert.That(Calculate("2*-3"), Is.EqualTo(-6));
            Assert.That(Calculate("2*3+5"), Is.EqualTo(11));
            Assert.That(Calculate("2+3*5"), Is.EqualTo(17));
            Assert.That(Calculate("2+3*5-4"), Is.EqualTo(13));
            Assert.That(Calculate("2+3-4*5"), Is.EqualTo(-15));
            Assert.That(Calculate("2*3-4+5"), Is.EqualTo(7));
            Assert.That(Calculate("2*3-4*5"), Is.EqualTo(-14));

        }

        [Test]
        public void Division()
        {
            Assert.That(Calculate("8/4"), Is.EqualTo(2));
            Assert.That(Calculate("8/4 + 5"), Is.EqualTo(7));
            Assert.That(Calculate("5 + 8/4"), Is.EqualTo(7));
            Assert.That(Calculate("8/4 + 3*2"), Is.EqualTo(8));
            Assert.That(Calculate("3*2 + 8/4"), Is.EqualTo(8));
            Assert.That(Calculate("3*2 + 8/4 - 5"), Is.EqualTo(3));
            Assert.That(Calculate("5 - 3*2 + 8/4"), Is.EqualTo(1));
            Assert.That(Calculate("30/6*2"), Is.EqualTo(10));
            Assert.That(Calculate("30*6/2"), Is.EqualTo(90));
            Assert.That(Calculate("30/5*2"), Is.EqualTo(12));
            Assert.That(Calculate("30*6/2*15"), Is.EqualTo(1350));
            Assert.That(Calculate("6/2*8/4"), Is.EqualTo(6));
            Assert.That(Calculate("-6/3"), Is.EqualTo(-2));
            Assert.That(Calculate("6/-3"), Is.EqualTo(-2));
            Assert.That(Calculate("6*-2/-3"), Is.EqualTo(4));
            Assert.That(Calculate("-6*-2/-3"), Is.EqualTo(-4));
            Assert.That(Calculate("6/2*-3"), Is.EqualTo(-9));
        }

        [Test]
        public void DivisionWithZero()
        {
            Assert.Throws<DivideByZeroException>(() => Calculate("6/0"), "Tested divide by zero");
            Assert.Throws<DivideByZeroException>(() => Calculate("6/0 + 5"), "Tested divide by zero");
            Assert.Throws<DivideByZeroException>(() => Calculate("4 - 6/0 + 5"), "Tested divide by zero");
            // How to test if an error message is working
            try
            {
                Calculate("6/0 - 2");
                Assert.Fail(); // we want the error message to be thrown
            }
            catch (DivideByZeroException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Attempted to divide by zero."));
            }

        }

        [Test]
        public void WrongInputFormat()
        {
            Assert.Throws<FormatException>(() => Calculate("hejsan"), "Tested input format");
            // How to test if an error message is working
            try
            {
                Calculate("hejsan");
                Assert.Fail(); // we want the error message to be thrown
            }
            catch (FormatException ex)
            {
                Assert.That(ex.Message, Is.EqualTo("The input string 'hejsan' was not in a correct format."));
            }

        }

        public decimal Calculate(string expression)
        {
            return new Calculator().Calculate(expression);
        }
    }
}