﻿namespace InsuraceClaimApp.Exceptions
{
    [Serializable]
    internal class CollectionEmptyException : Exception
    {
        public CollectionEmptyException()
        {
        }

        public CollectionEmptyException(string? message) : base(message)
        {
        }

        public CollectionEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}