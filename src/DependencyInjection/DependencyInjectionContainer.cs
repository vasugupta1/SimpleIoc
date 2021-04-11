using System;
using System.Collections.Generic;
using System.Linq;
using src.DependencyInjection.Models;

namespace src.DependencyInjection
{
    public class DependencyInjectionContainer
    {
        private IList<Dependency> _dependencies;

        public DependencyInjectionContainer()
        {
            _dependencies = new List<Dependency>();
        }

        public void AddSingleton<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), LifeTime.Singleton));
        }

        public void AddTransient<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), LifeTime.Transient));
        }

        public void AddSingleton<T>(object implementation)
        {
            var dependency = new Dependency(typeof(T), LifeTime.Singleton);
            dependency.AddImplementation(implementation);
            _dependencies.Add(dependency);
        }

        public void AddTransient<T>(object implementation)
        {
            var dependency = new Dependency(typeof(T), LifeTime.Transient);
            dependency.AddImplementation(implementation);
            _dependencies.Add(dependency);
        }

        public Dependency GetDependency(Type type)
        {
            return _dependencies.First(x=> x.ObjectType.Name == type.Name);
        }
    }
}