using Microsoft.Azure.WebJobs.Description;
using System;

namespace Indigo.Functions.Unity
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class InjectAttribute : Attribute
    {
    }
}
