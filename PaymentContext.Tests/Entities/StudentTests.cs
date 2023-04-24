using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Document _document;
    private readonly Student _student;

    public StudentTests()
    {
        _name =  new Name("Claudio", "Filho");
        _email = new Email("claudiojbf11@gmail.com");
        _address = new Address("Rua 1","365","Centro","Amontada","Ceara","Brasil","62540000");
        _document =  new Document("04164892307", EDocumentType.CPF);
        _student = new Student(_name, _document, _email);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {    
        var subscription = new Subscription(null);
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Industrias Wayne", _document, _address, _email, "12345678");
        
        subscription.AddPayment(payment);    
        _student.AddSubscription(subscription);
        _student.AddSubscription(subscription);

        Assert.IsFalse(_student.IsValid);

    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {  
        var subscription = new Subscription(null);
        _student.AddSubscription(subscription);
        Assert.IsFalse(_student.IsValid);

    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    {
        var subscription = new Subscription(null);
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Industrias Wayne", _document, _address, _email, "12345678");
        
        subscription.AddPayment(payment);    
        _student.AddSubscription(subscription);

        Assert.IsTrue(_student.IsValid);
    }
}