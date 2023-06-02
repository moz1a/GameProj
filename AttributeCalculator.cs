using System.Collections.Generic;

namespace GameProj
{
    public static class AttributeCalculator
    {
        public static Attributes Sum(this IEnumerable<Attributes> attributes)
        {
            var resultAttributes = new Attributes();

            foreach (var attribute in attributes)
                resultAttributes += attribute;

            return resultAttributes;
        }
    }
}
