using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
   [TestMethod]
   public void ShouldReturnErrorWhenNameIsInvalid()
   {
      var command = new CreateBoletoSubscriptionCommand();
      command.FirstName = "a";
      command.Validate();
      Assert.AreEqual(false, command.IsValid);
   }
}