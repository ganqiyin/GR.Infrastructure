﻿namespace GR.Dapper
{
    /// <summary>
    /// Represents that the derived classes are disposable objects.
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        ///  Finalizes the object.
        /// </summary>
        ~DisposableObject()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">A <see cref="bool"/> value which indicates whether
        /// the object should be disposed explicitly.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Provides the facility that disposes the object in an explicit manner,
        /// preventing the Finalizer from being called after the object has been
        /// disposed explicitly.
        /// </summary>
        protected void ExplicitDispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// </summary>
        public void Dispose()
        {
            ExplicitDispose();
        }
    }
}
