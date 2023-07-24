using System;
using System.Windows.Markup;

namespace KantorClient.Application.ControlExtenders
{
    public class ConstantExtension : MarkupExtension
    {
        private readonly Type _constantType;
        private readonly string _constantPropertyName;

        public ConstantExtension(Type constantType, string constantPropertyName)
        {
            _constantType = constantType;
            _constantPropertyName = constantPropertyName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var property = _constantType.GetProperty(_constantPropertyName);
            return property?.GetValue(null);
        }
    }
}
