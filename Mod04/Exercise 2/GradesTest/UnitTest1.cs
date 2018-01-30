using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradesPrototype;
using GradesPrototype.Data;
using System;

namespace GradesTest
{
    [TestClass]
    public class UnitTest1
    {
        public void Init()
        {
            DataSource.CreateData();
        }

        [TestMethod]
        public void TestValidGrade()
        {
            // arrange
            Init();

            // act
            Grade grade = new Grade(2, "2017-12-26", "English", "B+", "Great improvement. Keep it up!");
            Grade grade2 = new Grade();

            // assert
            String message = "grade not initialized correctly";
            Assert.AreEqual(2, grade.StudentID, message);
            Assert.AreEqual("2017-12-26", grade.AssessmentDate, message);
            Assert.AreEqual("English", grade.SubjectName, message);
            Assert.AreEqual("B+", grade.Assessment, message);
            Assert.AreEqual("Great improvement. Keep it up!", grade.Comments, message);

            message = "grade2 not initialized correctly";
            Assert.AreEqual(0, grade2.StudentID, message);
            Assert.AreEqual(DateTime.Now.Date.ToString("d"), grade2.AssessmentDate, message);
            Assert.AreEqual("Math", grade2.SubjectName, message);
            Assert.AreEqual("A", grade2.Assessment, message);
            Assert.AreEqual(String.Empty, grade2.Comments, message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBadDate()
        {
            // arrange
            Init();
            Grade grade = new Grade();

            // act
            grade.AssessmentDate = "2018-03-04";

            // assert
            Assert.Fail(); // If it gets to this line, no exception was thrown
        }

        // Can't use an ExpectedException here, because an ArgumentOutOfRangeException is an ArgumentException, and ArgumentOutOfRangeException needs to be excluded from the expected result.
        [TestMethod]
        public void TestDateNotRecognized()
        {
            // arrange
            Init();
            Grade grade = new Grade();

            // act
            try
            {
                grade.AssessmentDate = "bla";

            // assert
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(ArgumentOutOfRangeException)); // Wrong exception type
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }
        }

        // Can't use an ExpectedException here either, because multiple exceptions are asserted to be thrown.
        [TestMethod]
        public void TestBadAssessment()
        {
            // arrange
            Init();
            Grade grade = new Grade();

            // act
            try
            {
                grade.Assessment = "F";

            // assert
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }

            // act
            try
            {
                grade.Assessment = "A++";

            // assert
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBadSubject()
        {
            // arrange
            Init();
            Grade grade = new Grade();

            // act
            grade.SubjectName = "Made up subject";

            // assert
            Assert.Fail(); // If it gets to this line, no exception was thrown
        }

    }
}
