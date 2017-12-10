using System;

namespace CustomTestFramework.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomTestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeEachAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class BeforeAllAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class AfterEachAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class AfterAllAttribute : Attribute { }
}