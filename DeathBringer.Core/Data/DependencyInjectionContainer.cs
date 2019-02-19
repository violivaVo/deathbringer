using System;
using System.Collections.Generic;

namespace DeathBringer.Core.Data
{
    /// <summary>
    /// Container per DependencyInjection
    /// </summary>
    public static class DependencyInjectionContainer
    {
        /// <summary>
        /// Dizionario statico per conservare le mappature
        /// </summary>
        private static IDictionary<Type, Type> _Cache = new Dictionary<Type, Type>();

        /// <summary>
        /// Esegue la risoluzione dell'interfaccia definita con la sua instanza
        /// </summary>
        /// <typeparam name="TInterface">Tipo dell'interfaccia</typeparam>
        /// <returns>Ritorna un'istanza della risoluzione</returns>
        public static TInterface Resolve<TInterface>()
        {
            //Recupero il type dell'interfaccia
            var interfaceType = typeof(TInterface);

            //Se non ho la entry, eccezione
            if (!_Cache.ContainsKey(interfaceType))
                throw new InvalidOperationException($"Interface of type {interfaceType.Name} is not registered");

            //Recupero del type dal dizionario
            var instanceType = _Cache[interfaceType];

            //Creazione dell'istanza del tipo
            var instance = Activator.CreateInstance(instanceType);

            //Casting dell'elemento all'interfaccia
            return (TInterface)instance;
        }

        /// <summary>
        /// Esegue la registrazione dell'interfaccia e del tipo di risoluzione
        /// </summary>
        /// <typeparam name="TInterface">Tipo dell'interfaccia</typeparam>
        /// <typeparam name="TInstance">Tipo dell'istanza</typeparam>
        public static void Register<TInterface, TInstance>()
            where TInstance: class, TInterface, new()
        {
            //Ricavo il tipo dell'elemento
            var type = typeof(TInterface);

            //Se non è già in cache
            if (_Cache.ContainsKey(type))
                return;

            //Tipo dell'istanza da risolvere
            var instanceType = typeof(TInstance);

            //Metto in cache
            _Cache.Add(type, instanceType);
        }
    }
}
