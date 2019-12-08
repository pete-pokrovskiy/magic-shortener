namespace MagicShortener.Logic.Commands.Links.CreateLink
{
    /// <summary>
    /// Данные для команды создания ссылки
    /// </summary>
    public class CreateLinkCommand : ICommand
    {
        public string Id { get; set; }
        public string FullLink { get; set; }
    }
}
