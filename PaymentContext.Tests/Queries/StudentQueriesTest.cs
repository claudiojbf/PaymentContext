using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class StudentQueriesTest
    {
        private IList<Student> _student = new List<Student>();

        public StudentQueriesTest()
        {
            for(var i = 0; i <= 10; i++)
            {
                _student.Add(new Student(
                    new Name("Aluno", "Sobre"+i.ToString()), 
                    new Document("1234567890"+i.ToString(), EDocumentType.CPF),
                    new Email(i.ToString()+"@balta.io")
                ));
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("04164892307");
            var student = _student.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678901");
            var student = _student.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, student);
        }
    }
}