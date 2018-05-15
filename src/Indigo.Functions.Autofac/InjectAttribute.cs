using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.Autofac
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
    }
}
