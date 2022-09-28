namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        private readonly IFrequentFlyerNumberValidator _frequentFlyerNumberValidator;
        private const int AutoReferralMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;
        public CreditCardApplicationEvaluator(IFrequentFlyerNumberValidator frequentFlyerNumberValidator)
        {
            _frequentFlyerNumberValidator = frequentFlyerNumberValidator ?? throw new System.ArgumentNullException(nameof(frequentFlyerNumberValidator));
        }

     

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }
            var isValidFrequenFlyerNumber = _frequentFlyerNumberValidator.IsValid(application.FrequentFlyerNumber);

            if(!isValidFrequenFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }
           
            if (application.Age <= AutoReferralMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }       
    }
}
