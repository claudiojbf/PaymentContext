using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;

        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar seu cadastro");
            }
            
            if(_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este documento já está em uso!");
            
            if(_repository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso!");

            var name =  new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var address = new Address(command.Street,command.Number,command.Neighborhood,command.City,command.State,command.Country,command.ZipCode);
            var document =  new Document(command.PayerDocument, command.PayerDocumentType);

            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.PaidDate,
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                command.Payer, 
                document, 
                address, 
                email, 
                command.BarCode, 
                command.BoletoNumber
                );
            
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if(!IsValid)
                return new CommandResult(false, "Não foi possivel efetuar seu cadastro"); 

            _repository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Claudio.Io", "Sua assinatura foi criada");


            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}