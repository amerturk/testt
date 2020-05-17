using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {
        }
        public BaseException(string message, Exception exception) : base(message, GetDeeperException(exception))
        {
            
        }
        public static Exception GetDeeperException(Exception ex)
        {
            while (true)
            {
                if (ex.InnerException == null)
                    return ex;
                ex = ex.InnerException;
            }
        }
        
    }
}
