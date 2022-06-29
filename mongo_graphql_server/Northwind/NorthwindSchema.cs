using GraphQL.Instrumentation;
using GraphQL.Types;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind
{
    public class NorthwindSchema : Schema
    {
        public NorthwindSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<NorthwindQuery>();

            Mutation = provider.GetRequiredService<NorthwindMutation>();

            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
}