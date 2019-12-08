using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicShortener.Logic.Commands.Links.IncrementLinkRedirectsCount
{
    public class IncrementLinkRedirectsCountCommand : ICommand
    {
        public string LinkId { get; set; }
    }
}
