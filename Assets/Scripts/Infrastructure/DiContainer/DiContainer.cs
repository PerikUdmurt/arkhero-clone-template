using System;
using System.Collections.Generic;

namespace ArkheroClone.Services.DI
{
    public class DiContainer
    {
        private readonly DiContainer _parentContainer;
        private readonly Dictionary<(string, Type), DiRegistration> _registrations = new();
        private readonly HashSet<(string, Type)> _resolutions = new();

        public DiContainer(DiContainer parentContainer = null)
        { 
            _parentContainer = parentContainer;
        }

        public void RegisterInstance<T>(T instance, string tag = null)
        {
            Register(DiRegistrationType.AsSingle, null, tag, instance);
        }

        public DiRegistration Register<T>(DiRegistrationType registrationType, Func<DiContainer, T> factory, string tag = null, T instance = default)
        {
            (string, Type) key = (tag, typeof(T));

            if (_registrations.ContainsKey(key))
                throw new Exception($"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} has already registered");

            if (registrationType == DiRegistrationType.AsSingle)
            return _registrations[key] = new DiRegistration
            {
                RegistrationType = registrationType,
                Instance = instance
            };

            return _registrations[key] = new DiRegistration
            {
                RegistrationType = registrationType,
                Factory = c => factory(c)
            };
        }

        public T Resolve<T>(string tag = null) 
        { 
            (string, Type) key = (tag, typeof(T));

            if (_resolutions.Contains(key))
                throw new Exception($"Cyclic dependency for tag {key.Item1} and type {key.Item2.FullName}");

            _resolutions.Add(key);

            try
            {
                if (_registrations.TryGetValue(key, out DiRegistration registration))
                    return GetInstance<T>(registration);

                if (_parentContainer != null)
                    return _parentContainer.Resolve<T>(tag);
            }
            finally
            {
                _resolutions.Remove(key);
            }
            throw new Exception($"Couldn't find dependency for tag {tag} and type {key.Item2.FullName}.");
        }

        private T GetInstance<T>(DiRegistration registration)
        {
            return registration.RegistrationType switch
            {
                DiRegistrationType.AsSingle => GetSingleton<T>(registration),
                DiRegistrationType.AsTransient => (T)registration.Factory(this),
                _ => (T)registration.Factory(this),
            };
        }

        private T GetSingleton<T>(DiRegistration registration)
        {
            if (registration.Instance == null && registration.Factory != null)
                registration.Instance = registration.Factory(this);

            return (T)registration.Instance;
        }
    }
}