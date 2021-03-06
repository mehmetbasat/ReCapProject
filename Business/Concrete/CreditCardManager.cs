using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.Validators;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        //[SecuredOperation("admin,creditCard.all,creditCard.get,customer")]
        public IDataResult<CreditCard> Get(string cardNumber, string expireYear, string expireMonth, string cvc, string cardHolderFullName)
        {
            var creditCard = GetCreditCardByCardInfo(cardNumber, expireYear, expireMonth, cvc, cardHolderFullName);
            if (creditCard != null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>(null, Messages.CreditCardNotValid);
        }

        //[SecuredOperation("admin,creditCard.all,creditCard.validate,customer")]
        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Validate(CreditCard creditCard)
        {
            var validateResult = GetCreditCardByCardInfo(creditCard.CardNumber, creditCard.ExpireYear, creditCard.ExpireMonth, creditCard.Cvc, creditCard.CardHolderFullName);
            if (validateResult != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CreditCardNotValid);
        }

        //[SecuredOperation("admin,creditCard.all,creditCard.update,customer")]
        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult();
        }

        private CreditCard GetCreditCardByCardInfo(string cardNumber, string expireYear, string expireMonth, string cvc, string cardHolderFullName)
        {
            return _creditCardDal.GetById(c => c.CardNumber == cardNumber &&
                                           c.ExpireYear == expireYear &&
                                           c.ExpireMonth == expireMonth &&
                                           c.Cvc == cvc &&
                                           c.CardHolderFullName == cardHolderFullName.ToLower().ToUpper()); // Convert Turkish characters into standard characters.
        }
    }
}
