using Flunt.Notifications;

namespace PaymentContext.Shared.ValueObjects
{
    public abstract class ValueObject : Notifiable<Notification>
    {
        public bool MinLen(string name, int len)
        {
            if(name.Length >= len)
                return true;

            return false;
        }

        public bool MaxLen(string name, int len)
        {
            if(name.Length <= len)
                return true;

            return false;
        }
    }
}