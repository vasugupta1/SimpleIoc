using System;

namespace src.DependencyInjection.Models
{
    public class Dependency
    {
        public Type ObjectType {get;}
        public LifeTime LifeTime {get;}
        public object Implementation {get; set;}
        public bool IsImplemented {get; set;}
        public Dependency(Type objectType, LifeTime lifeTime)
        { 
            ObjectType = objectType;
            LifeTime = lifeTime;    
        }

        public void AddImplementation(object implementation)
        {
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
            IsImplemented = true;
        }
    }
}