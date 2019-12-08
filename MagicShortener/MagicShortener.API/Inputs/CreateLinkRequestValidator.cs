using FluentValidation;

namespace MagicShortener.API.Inputs
{
    /// <summary>
    /// Валидатор запроса на создание короткой ссылки
    /// </summary>
    public class CreateLinkRequestValidator : AbstractValidator<CreateLinkRequest>
    {
        //регулярное выражение для проверки url-a на валидность -  отсюда https://urlregex.com/
        private const string UrlMatchRegExp = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";

        public CreateLinkRequestValidator()
        {
            // TODO: если обращение к основной бизнес-операции - в данном случае к созданию экземпляра ссылки - будет происходить из нескольких точек,
            // то имеет смысл вынести слой валидации в Logic, добавив к операции соответствующий декоратор
            RuleFor(r => r.Url)
                .NotNull()
                .NotEmpty()
                .Matches(UrlMatchRegExp);
        }
    }
}
