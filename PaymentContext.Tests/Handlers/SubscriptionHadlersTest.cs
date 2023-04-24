using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class SubscriptionHandlersTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailServices());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Claudio";
            command.LastName = "Filho";
            command.Document = "04164892307";
            command.Email = "brosson123@gmail.com2";
            command.BarCode = "123456789";
            command.BoletoNumber = "123456";
            command.PaymentNumber = "12345678";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddDays(5);
            command.Total = 10;
            command.TotalPaid = 10;
            command.Payer = "WAYNE INDUSTRIS";
            command.PayerDocument = "12345678901";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Street = "Av Jaime Assis Henrique";
            command.Number = "365";
            command.Neighborhood = "Centro";
            command.City = "Amontada";
            command.State = "Ceara";
            command.Country = "Brasil";
            command.ZipCode = "62540000";
            command.PayerEmail = "brosson123@gmail.com2";

            handler.Handler(command);
            command.Validate();
            Assert.AreEqual(false, handler.IsValid);

        }
    }
}