﻿using AutoMapper;
using System;
using System.Linq;

namespace LearningSystem.Helpers.Mappings
{
    /// <summary>
    /// In the startup file we need to add services.AddAutoMapper(typeof(AutoMapperProfile));
    /// The (typeof(AutoMapperProfile)) will set the assembly that AutoMapperProfile is to be the
    /// one that automapper will search for profiles to register. Therefore AutoMapperProfile will 
    /// be registered and from him - all other prifiles.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// We need to know from wich assemblies to take the types.
        /// </summary>
        private const string AssembliesCommonName = "LearningSystem";

        /// <summary>
        /// First we get all types that we wrote in the asemblies. Not all in our solution file, because there are 
        /// types from the packages etc.
        /// 
        /// Second we get all models - they have to be classes, not abstract classes, have to implement IMapFrom<>.
        /// Then we select the destination, wich is the model class and we take the source, wich is the type insinde 
        /// the diamond brackets of the IMapFrom<> interface. After that we iterate the key value pairs and create a 
        /// mapping for each one of them.
        /// 
        /// Third we give the options in each model class that will be destination in the mappig to have the options 
        /// to configure some mappings that are not default. Because the default mapping will map only the properties 
        /// tha are with the same name and type in the source and destination type. For this, each destination type 
        /// must implement IHaveCustomMapping and make the custom configurations there.
        /// </summary>
        public AutoMapperProfile()
        {
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName().Name.Contains(AssembliesCommonName))
                .SelectMany(a => a.GetTypes())
                .ToList();

            allTypes
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                        .GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => new
                        {
                            Definition = i.GetGenericTypeDefinition(),
                            Arguments = i.GetGenericArguments()
                        })
                        .Where(i => i.Definition == typeof(IMapFrom<>))
                        .SelectMany(i => i.Arguments)
                        .First()
                })
                .ToList()
                .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            allTypes
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    typeof(IHaveCustomMapping).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHaveCustomMapping>()
                .ToList()
                .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}
