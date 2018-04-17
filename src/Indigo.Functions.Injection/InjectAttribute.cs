using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.Injection
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
    }
}
