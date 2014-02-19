using System;
using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Funz
{
    internal class ParameterPropertyMatcher : IEqualityComparer<IReflectionElement>
    {
        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            return EqualsName(x, y) && EqualsType(x, y);
        }

        private static bool EqualsName(IReflectionElement x, IReflectionElement y)
        {
            var getNameVisitor = new GetNameVisitor();
            var xName = x.Accept(getNameVisitor).Value;
            var yName = y.Accept(getNameVisitor).Value;
            return xName.Equals(yName, StringComparison.OrdinalIgnoreCase);
        }

        private static bool EqualsType(IReflectionElement x, IReflectionElement y)
        {
            var getTypeVisitor = new GetTypeVisitor();
            var xType = x.Accept(getTypeVisitor).Value;
            var yType = y.Accept(getTypeVisitor).Value;
            return xType.IsAssignableFrom(yType) || yType.IsAssignableFrom(xType);
        }

        public int GetHashCode(IReflectionElement obj)
        {
            return obj.GetHashCode();
        }

        private class GetNameVisitor : ReflectionVisitor<string>
        {
            private readonly string _value;

            public GetNameVisitor()
            {
            }

            private GetNameVisitor(string value)
            {
                _value = value;
            }

            public override string Value
            {
                get { return _value; }
            }

            public override IReflectionVisitor<string> Visit(PropertyInfoElement propertyInfoElement)
            {
                return new GetNameVisitor(propertyInfoElement.PropertyInfo.Name);
            }

            public override IReflectionVisitor<string> Visit(ParameterInfoElement parameterInfoElement)
            {
                return new GetNameVisitor(parameterInfoElement.ParameterInfo.Name);
            }
        }

        private class GetTypeVisitor : ReflectionVisitor<Type>
        {
            private readonly Type _value;

            public GetTypeVisitor()
            {
            }

            private GetTypeVisitor(Type value)
            {
                _value = value;
            }

            public override Type Value
            {
                get { return _value; }
            }

            public override IReflectionVisitor<Type> Visit(PropertyInfoElement propertyInfoElement)
            {
                return new GetTypeVisitor(propertyInfoElement.PropertyInfo.PropertyType);
            }

            public override IReflectionVisitor<Type> Visit(ParameterInfoElement parameterInfoElement)
            {
                return new GetTypeVisitor(parameterInfoElement.ParameterInfo.ParameterType);
            }
        }
    }
}