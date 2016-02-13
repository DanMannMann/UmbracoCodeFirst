using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Felinesoft.UmbracoCodeFirst.Core
{
    internal class SyncLock<T>
    {
        private object _lock = new object();
        private Dictionary<T, ThreadAffinityLock> _lockMap = new Dictionary<T, ThreadAffinityLock>();

        private ThreadAffinityLock GetLock(T member)
        {
            lock (_lock)
            {
                if (!_lockMap.ContainsKey(member))
                {
                    _lockMap.Add(member, new ThreadAffinityLock());
                }
                return _lockMap[member];
            }
        }

        internal void Release(T member)
        {
            lock (_lock)
            {
                if (_lockMap.ContainsKey(member) && _lockMap[member].TryRelease())
                {
                    _lockMap.Remove(member);
                }
            }
        }

        internal void TakeOrWait(T member)
        {
            bool isNew;
            var currentLock = GetLock(member);
            if (currentLock != null)
            {
                if (currentLock.CurrentThreadIsOwner)
                {
                    currentLock.Take();
                }
                else
                {
                    currentLock.Wait();
                }
            }
        }
    }

    internal class ThreadAffinityLock
    {
        object _lock = new object();
        int _ownerThread;
        ManualResetEvent _waitHandle;
        int _takeCount;

        internal ThreadAffinityLock()
        {
            _ownerThread = Thread.CurrentThread.ManagedThreadId;
            _waitHandle = new ManualResetEvent(false);
            _takeCount = 0;
        }

        internal int OwnerThread
        {
            get { return _ownerThread; }
        }
        
        internal bool CurrentThreadIsOwner
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId == _ownerThread;
            }
        }

        internal void Take()
        {
            if (!CurrentThreadIsOwner)
            {
                throw new InvalidOperationException("The current thread does not own the lock");
            }
            lock (_lock)
            {
                if (_takeCount > 0)
                {
                    _takeCount++;
                }
            }
        }

        internal bool TryRelease()
        {
            if (CurrentThreadIsOwner)
            {
                lock (_lock)
                {
                    _takeCount--;
                    if (_takeCount < 1)
                    {
                        _waitHandle.Set();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        internal void Wait()
        {
            _waitHandle.WaitOne();
        }
    }
}