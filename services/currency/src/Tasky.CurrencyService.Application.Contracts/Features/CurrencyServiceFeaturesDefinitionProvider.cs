using System;
using System.Collections.Generic;
using System.Text;
using Tasky.CurrencyService.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Tasky.CurrencyService.Features
{
    public class CurrencyServiceFeaturesDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var myGroup = context.AddGroup(CurrencyServiceFeatures.GroupName);
            myGroup.AddFeature(
                CurrencyServiceFeatures.Currency.Default,
                defaultValue: "false",
                displayName: L("Currency"),
                valueType: new ToggleStringValueType());
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CurrencyServiceResource>(name);
        }
    }
}