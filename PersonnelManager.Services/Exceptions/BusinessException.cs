using System;

namespace PersonnelManager.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
