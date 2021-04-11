using System;
using System.Linq;
using System.Reflection;

namespace src.DependencyInjection
{
    public class DependencyResolver
    {
        private readonly DependencyInjectionContainer _container;
        public DependencyResolver(DependencyInjectionContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }
        public T GetService<T>()
        {
           return (T) GetService(typeof(T));
        }

        private object GetService(Type type)
        {
            var dependency = _container.GetDependency(type);

            Type currentObjectType;
            if(dependency.ObjectType.IsAbstract || dependency.ObjectType.IsInterface )
            {
                currentObjectType = dependency.Implementation.GetType();

            }
            else
            {
                currentObjectType = dependency.ObjectType;
            }

            var constructor = GetConstructorInfo(currentObjectType);
            var constructorParameters = GetConstructorPameterInfo(constructor);

            if(constructorParameters.Length > 0)
            {
                object[] parametersImplementations = new object[constructorParameters.Length];
                for(int i =0; i < constructorParameters.Length; i++ )
                {
                    parametersImplementations[i] = GetService(constructorParameters[i].ParameterType);
                }
                return CreateImplementation(dependency, type => Activator.CreateInstance(type, parametersImplementations));      
            }

            return CreateImplementation(dependency, type => Activator.CreateInstance(type));
        }


        private ConstructorInfo GetConstructorInfo(Type type)
        {
            return type.GetConstructors().First();
        }

        private ParameterInfo[] GetConstructorPameterInfo(ConstructorInfo constructor)
        {
            return constructor.GetParameters().ToArray();
        }

        private object CreateImplementation(Models.Dependency dependency, Func<Type, object> ActivatorFactory)
        {
            if(dependency.IsImplemented && dependency.Implementation != null)
            {
                return dependency.Implementation;
            }

            var implementation = ActivatorFactory(dependency.ObjectType); 

            if(dependency.LifeTime == Models.LifeTime.Singleton && dependency.Implementation is null)
            {      
                dependency.AddImplementation(implementation);
                return implementation;
            }

            return dependency.Implementation;
        }
    }
}