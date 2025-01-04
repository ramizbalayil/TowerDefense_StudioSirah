using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace frameworks.services.events
{
    public class EventHandlerService : BaseService
    {
        #region Delegates and Lookups
        public delegate void EventDelegate<T>(T e) where T : AppEvent;
        private delegate void EventDelegate(AppEvent e);

        private Dictionary<Type, EventDelegate> delegates = new Dictionary<Type, EventDelegate>();
        private Dictionary<Delegate, EventDelegate> delegateLookup = new Dictionary<Delegate, EventDelegate>();
        private Dictionary<Delegate, Delegate> triggerOnceLookup = new Dictionary<Delegate, Delegate>();

        #endregion

        #region Overridable Functions
        public async override Task Init()
        {
            await base.Init();
            delegates = new Dictionary<Type, EventDelegate>();
            delegateLookup = new Dictionary<Delegate, EventDelegate>();
            triggerOnceLookup = new Dictionary<Delegate, Delegate>();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ClearAllDelegates();
        }
        #endregion

        #region Private Methods
        private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : AppEvent
        {
            if (HasListener(del)) return null;

            EventDelegate internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            if (delegates.TryGetValue(typeof(T), out EventDelegate previouslyAvailableDelgates))
            {
                previouslyAvailableDelgates += internalDelegate;
                delegates[typeof(T)] = previouslyAvailableDelgates;
            }
            else
            {
                delegates[typeof(T)] = internalDelegate;
            }
            return internalDelegate;
        }

        private void RemoveDelegate<T>(EventDelegate<T> del) where T : AppEvent
        {
            if (delegateLookup != null && delegateLookup.TryGetValue(del, out EventDelegate internalDelegate))
            {
                if (delegates.TryGetValue(typeof(T), out EventDelegate previouslyAvailableDelgates))
                {
                    previouslyAvailableDelgates -= internalDelegate;
                    if (previouslyAvailableDelgates == null)
                    {
                        delegates.Remove(typeof(T));
                        delegateLookup.Remove(del);
                    }
                    else
                    {
                        delegates[typeof(T)] = previouslyAvailableDelgates;
                    }
                }
                else
                {
                    delegates.Remove(typeof(T));
                    delegateLookup.Remove(del);
                }
            }
        }

        private void ClearAllDelegates()
        {
            delegates.Clear();
            delegateLookup.Clear();
            triggerOnceLookup.Clear();

            delegates = null;
            delegateLookup = null;
            triggerOnceLookup = null;
        }
        #endregion

        #region Public Methods
        public void AddListener<T>(EventDelegate<T> del, bool isOneShot = false) where T : AppEvent
        {
            EventDelegate delegateAdded = AddDelegate<T>(del);

            if (isOneShot)
            {
                triggerOnceLookup[delegateAdded] = del;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : AppEvent
        {
            RemoveDelegate<T>(del);
        }

        public bool HasListener<T>(EventDelegate<T> del) where T : AppEvent
        {
            return delegateLookup.ContainsKey(del);
        }

        public void TriggerEvent(AppEvent e)
        {
            if (delegates.TryGetValue(e.GetType(), out EventDelegate del))
            {
                del.Invoke(e);

                foreach (EventDelegate k in del.GetInvocationList())
                {
                    if (triggerOnceLookup.ContainsKey(k) && triggerOnceLookup[k] != null)
                    {
                        delegateLookup.Remove(triggerOnceLookup[k]);

                        triggerOnceLookup.Remove(k);

                        del -= k;
                        if (del == null)
                        {
                            delegates.Remove(e.GetType());
                        }
                        else
                        {
                            delegates[e.GetType()] = del;
                        }

                    }
                }
            }
        }

        public void Release()
        {
            ClearAllDelegates();
        }
        #endregion
    }
}

