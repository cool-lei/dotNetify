﻿using System;
using System.Reflection;

namespace DotNetify
{
   public interface IDotNetifyConfiguration
   {
      /// <summary>
      /// How long to keep a view model controller in memory after it hasn't been accessed for a while. Default to never expire.
      /// </summary>
      TimeSpan? VMControllerCacheExpiration { get; set; }

      /// <summary>
      /// Provides a factory method to create view model instances.
      /// The method accepts a class type and constructor arguments, and returns an instance of that type.
      /// </summary>
      void SetFactoryMethod(Func<Type, object[], object> factoryMethod);

      /// <summary>
      /// Register an assembly that has the view model classes.
      /// </summary>
      void RegisterAssembly(Assembly assembly);
      void RegisterAssembly(string assemblyName);
   }

   public class DotNetifyConfiguration : IDotNetifyConfiguration
   {
      public TimeSpan? VMControllerCacheExpiration { get; set; }

      public void SetFactoryMethod(Func<Type, object[], object> factoryMethod) => VMController.CreateInstance = (type, args) => factoryMethod(type, args);

      public void RegisterAssembly(Assembly assembly) => VMController.RegisterAssembly(assembly);

      public void RegisterAssembly(string assemblyName) => VMController.RegisterAssembly(Assembly.Load(new AssemblyName(assemblyName)));

      public void RegisterEntryAssembly() => VMController.RegisterAssembly(Assembly.GetEntryAssembly());

      public bool HasAssembly => VMController._registeredAssemblies.Count > 0;
   }
}
