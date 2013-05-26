using System;
using System.Collections.Generic;
using WorldMessages.CommonFramework.InversionOfControl;

namespace WorldMessages.DomainModelFramework.Command
{
    public class DomainObject
    {
        /// <summary>
        /// Default constructor that register all events.
        /// </summary>
        public DomainObject()
        {
            RegisterAllEvents();
        }

        private Dictionary<Type, object> registeredEvents = new Dictionary<Type, object>();

        /// <summary>
        /// To override to specify events types known by this type
        /// </summary>
        protected virtual void RegisterAllEvents()
        {
        }

        /// <summary>
        /// Helper to register an event type with its method called when applied to change state of the object.
        /// Object state must be changed only in methods registered on event.
        /// </summary>
        /// <typeparam name="TDomainEvent">Type of event</typeparam>
        /// <param name="changeState">Change state method when <see cref="TDomainEvent"/> is applied</param>
        protected void OnEvent<TDomainEvent>(Action<TDomainEvent> changeState)
        {
            registeredEvents.Add(typeof(TDomainEvent), changeState);
        }

        /// <summary>
        /// Allows to apply an event.
        /// 1) It calls the "change state" callback function declared
        /// 2) It raises the event (through IoC/DI on <see cref="IHandles{T}"/> interface implementations).
        /// </summary>
        /// <typeparam name="TDomainEvent">Type of event to apply</typeparam>
        /// <param name="eventToApply">Event to apply</param>
        protected void Apply<TDomainEvent>(TDomainEvent eventToApply)
            where TDomainEvent : IDomainEvent
        {
            ChangeState(eventToApply);
            RaiseDomainEvent(eventToApply);
        }

        private void RaiseDomainEvent<TDomainEvent>(TDomainEvent eventToApply)
            where TDomainEvent : IDomainEvent
        {
            foreach (var handler in ServiceLocator.GetServices<IHandles<TDomainEvent>>())
            {
                handler.Handle(eventToApply);
            }
        }

        private void ChangeState<TDomainEvent>(TDomainEvent eventToApply)
            where TDomainEvent : IDomainEvent
        {
            object changeState;
            registeredEvents.TryGetValue(typeof(TDomainEvent), out changeState);
            (changeState as Action<TDomainEvent>)(eventToApply);
        }
    }
}
