using System;

namespace LiteNinja.DI
{
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Constructor)]
    public class InjectAttribute : Attribute
    {
    }
}